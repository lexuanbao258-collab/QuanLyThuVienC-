using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class PhieuTraDAL
{
    public List<PhieuTraDTO> GetAll()
    {
        const string sql = """
            SELECT pt.id, pt.ma_phieu_tra, pm.ma_phieu_muon, nm.ho_ten, pt.ngay_tra, pt.so_ngay_tre, COALESCE(pt.ghi_chu, '')
            FROM phieu_tra pt
            JOIN phieu_muon pm ON pt.phieu_muon_id = pm.id
            JOIN nguoi_muon nm ON pm.nguoi_muon_id = nm.id
            ORDER BY pt.id DESC
            """;
        return Query(sql);
    }

    public List<PhieuTraDTO> Search(string keyword)
    {
        const string sql = """
            SELECT pt.id, pt.ma_phieu_tra, pm.ma_phieu_muon, nm.ho_ten, pt.ngay_tra, pt.so_ngay_tre, COALESCE(pt.ghi_chu, '')
            FROM phieu_tra pt
            JOIN phieu_muon pm ON pt.phieu_muon_id = pm.id
            JOIN nguoi_muon nm ON pm.nguoi_muon_id = nm.id
            WHERE LOWER(pt.ma_phieu_tra) LIKE LOWER(@keyword)
               OR LOWER(pm.ma_phieu_muon) LIKE LOWER(@keyword)
               OR LOWER(nm.ho_ten) LIKE LOWER(@keyword)
            ORDER BY pt.id DESC
            """;
        return Query(sql, new NpgsqlParameter("@keyword", $"%{keyword.Trim()}%"));
    }

    public string GenerateNewCode()
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                SELECT COALESCE(MAX(CAST(SUBSTRING(ma_phieu_tra FROM 3) AS INTEGER)), 0)
                FROM phieu_tra
                WHERE ma_phieu_tra ~ '^PT[0-9]+$'
                """, conn);
            var last = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            return $"PT{last + 1:000}";
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    public List<LookupItemDTO> GetPendingBorrowSlips()
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                SELECT pm.id, pm.ma_phieu_muon, nm.ho_ten
                FROM phieu_muon pm
                JOIN nguoi_muon nm ON pm.nguoi_muon_id = nm.id
                LEFT JOIN phieu_tra pt ON pt.phieu_muon_id = pm.id
                WHERE pm.trang_thai = 'dang_muon'
                  AND pt.id IS NULL
                ORDER BY pm.id DESC
                """, conn);
            using var reader = cmd.ExecuteReader();
            var items = new List<LookupItemDTO>();
            while (reader.Read())
            {
                items.Add(new LookupItemDTO
                {
                    Id = reader.GetInt32(0),
                    Code = reader.GetString(1),
                    Name = reader.GetString(2)
                });
            }
            return items;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    public List<ReturnDetailDTO> GetBorrowDetailsForReturn(int phieuMuonId)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                SELECT ctpm.id, ctpm.sach_id, s.ten_sach, ctpm.so_luong
                FROM chi_tiet_phieu_muon ctpm
                JOIN sach s ON ctpm.sach_id = s.id
                WHERE ctpm.phieu_muon_id = @id
                ORDER BY ctpm.id
                """, conn);
            cmd.Parameters.AddWithValue("@id", phieuMuonId);
            using var reader = cmd.ExecuteReader();
            var items = new List<ReturnDetailDTO>();
            while (reader.Read())
            {
                var soLuongMuon = reader.GetInt32(3);
                items.Add(new ReturnDetailDTO
                {
                    ChiTietPhieuMuonId = reader.GetInt32(0),
                    SachId = reader.GetInt32(1),
                    TenSach = reader.GetString(2),
                    SoLuongMuon = soLuongMuon,
                    SoLuongTra = soLuongMuon,
                    SoLuongHong = 0,
                    SoLuongMat = 0,
                    GhiChu = string.Empty
                });
            }
            return items;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    public void Create(ReturnSlipInputDTO input, int nguoiXuLyId)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var tran = conn.BeginTransaction();

            using var dupCode = new NpgsqlCommand("SELECT 1 FROM phieu_tra WHERE ma_phieu_tra = @ma", conn, tran);
            dupCode.Parameters.AddWithValue("@ma", input.MaPhieuTra);
            if (dupCode.ExecuteScalar() is not null)
                throw new InvalidOperationException($"Mã phiếu trả {input.MaPhieuTra} đã tồn tại.");

            using var dupBorrow = new NpgsqlCommand("SELECT 1 FROM phieu_tra WHERE phieu_muon_id = @id", conn, tran);
            dupBorrow.Parameters.AddWithValue("@id", input.PhieuMuonId);
            if (dupBorrow.ExecuteScalar() is not null)
                throw new InvalidOperationException("Phiếu mượn này đã được lập phiếu trả.");

            using var getBorrow = new NpgsqlCommand("""
                SELECT pm.ngay_hen_tra, pm.nguoi_muon_id
                FROM phieu_muon pm
                WHERE pm.id = @id
                """, conn, tran);
            getBorrow.Parameters.AddWithValue("@id", input.PhieuMuonId);
            using var borrowReader = getBorrow.ExecuteReader();
            if (!borrowReader.Read()) throw new InvalidOperationException("Không tìm thấy phiếu mượn.");
            var ngayHenTra = borrowReader.GetDateTime(0).Date;
            var nguoiMuonId = borrowReader.GetInt32(1);
            borrowReader.Close();

            var soNgayTre = Math.Max((input.NgayTra.Date - ngayHenTra).Days, 0);

            using var insertTra = new NpgsqlCommand("""
                INSERT INTO phieu_tra (ma_phieu_tra, phieu_muon_id, nguoi_xu_ly_id, ngay_tra, so_ngay_tre, ghi_chu)
                VALUES (@ma, @phieuMuonId, @nguoiXuLyId, @ngayTra, @soNgayTre, @ghiChu)
                RETURNING id
                """, conn, tran);
            insertTra.Parameters.AddWithValue("@ma", input.MaPhieuTra);
            insertTra.Parameters.AddWithValue("@phieuMuonId", input.PhieuMuonId);
            insertTra.Parameters.AddWithValue("@nguoiXuLyId", nguoiXuLyId);
            insertTra.Parameters.AddWithValue("@ngayTra", input.NgayTra.Date);
            insertTra.Parameters.AddWithValue("@soNgayTre", soNgayTre);
            insertTra.Parameters.AddWithValue("@ghiChu", soNgayTre > 0 ? $"Trả sách - trễ {soNgayTre} ngày" : "Trả sách");
            var phieuTraId = Convert.ToInt32(insertTra.ExecuteScalar());

            decimal tongTienPhat = 0m;
            var dsPhat = new List<(int SachId, string LyDo, int SoLuong, decimal DonGia, decimal ThanhTien, string GhiChu)>();
            int? sachIdDauTien = null;

            foreach (var item in input.Items)
            {
                var tong = item.SoLuongTra + item.SoLuongHong + item.SoLuongMat;
                if (tong != item.SoLuongMuon)
                    throw new InvalidOperationException($"Tổng trả/hỏng/mất của sách {item.TenSach} phải bằng số lượng mượn.");

                using var insertDetail = new NpgsqlCommand("""
                    INSERT INTO chi_tiet_phieu_tra (
                        phieu_tra_id, chi_tiet_phieu_muon_id, so_luong_tra,
                        so_luong_hong, so_luong_mat, ghi_chu_tinh_trang
                    )
                    VALUES (@phieuTraId, @ctpmId, @tra, @hong, @mat, @ghiChu)
                    """, conn, tran);
                insertDetail.Parameters.AddWithValue("@phieuTraId", phieuTraId);
                insertDetail.Parameters.AddWithValue("@ctpmId", item.ChiTietPhieuMuonId);
                insertDetail.Parameters.AddWithValue("@tra", item.SoLuongTra);
                insertDetail.Parameters.AddWithValue("@hong", item.SoLuongHong);
                insertDetail.Parameters.AddWithValue("@mat", item.SoLuongMat);
                insertDetail.Parameters.AddWithValue("@ghiChu", string.IsNullOrWhiteSpace(item.GhiChu) ? DBNull.Value : item.GhiChu);
                insertDetail.ExecuteNonQuery();

                if (!sachIdDauTien.HasValue) sachIdDauTien = item.SachId;

                if (item.SoLuongTra > 0)
                {
                    using var updateBook = new NpgsqlCommand("""
                        UPDATE sach
                        SET so_luong_con = so_luong_con + @soLuongTra
                        WHERE id = @sachId
                        """, conn, tran);
                    updateBook.Parameters.AddWithValue("@soLuongTra", item.SoLuongTra);
                    updateBook.Parameters.AddWithValue("@sachId", item.SachId);
                    updateBook.ExecuteNonQuery();
                }

                if (item.SoLuongHong > 0)
                {
                    var thanhTien = item.SoLuongHong * 45000m;
                    tongTienPhat += thanhTien;
                    dsPhat.Add((item.SachId, "lam_hong", item.SoLuongHong, 45000m, thanhTien, string.IsNullOrWhiteSpace(item.GhiChu) ? "Sách bị hỏng" : item.GhiChu));
                }

                if (item.SoLuongMat > 0)
                {
                    var thanhTien = item.SoLuongMat * 80000m;
                    tongTienPhat += thanhTien;
                    dsPhat.Add((item.SachId, "lam_mat", item.SoLuongMat, 80000m, thanhTien, string.IsNullOrWhiteSpace(item.GhiChu) ? "Sách bị mất" : item.GhiChu));
                }
            }

            if (soNgayTre > 0 && sachIdDauTien.HasValue)
            {
                var thanhTien = soNgayTre * 5000m;
                tongTienPhat += thanhTien;
                dsPhat.Add((sachIdDauTien.Value, "tra_tre", soNgayTre, 5000m, thanhTien, $"Trễ {soNgayTre} ngày"));
            }

            using var updateBorrow = new NpgsqlCommand("UPDATE phieu_muon SET trang_thai = 'da_tra' WHERE id = @id", conn, tran);
            updateBorrow.Parameters.AddWithValue("@id", input.PhieuMuonId);
            updateBorrow.ExecuteNonQuery();

            if (tongTienPhat > 0)
            {
                using var getMaxFine = new NpgsqlCommand("""
                    SELECT COALESCE(MAX(CAST(SUBSTRING(ma_phieu_phat FROM 3) AS INTEGER)), 0)
                    FROM phieu_phat
                    WHERE ma_phieu_phat ~ '^PP[0-9]+$'
                    """, conn, tran);
                var max = Convert.ToInt32(getMaxFine.ExecuteScalar() ?? 0);
                var maPhieuPhat = $"PP{max + 1:000}";

                using var insertFine = new NpgsqlCommand("""
                    INSERT INTO phieu_phat (
                        ma_phieu_phat, nguoi_muon_id, phieu_muon_id, phieu_tra_id,
                        tong_tien, trang_thai_thanh_toan, nguoi_lap_id
                    )
                    VALUES (@ma, @nguoiMuonId, @phieuMuonId, @phieuTraId, @tongTien, 'chua_thanh_toan', @nguoiLapId)
                    RETURNING id
                    """, conn, tran);
                insertFine.Parameters.AddWithValue("@ma", maPhieuPhat);
                insertFine.Parameters.AddWithValue("@nguoiMuonId", nguoiMuonId);
                insertFine.Parameters.AddWithValue("@phieuMuonId", input.PhieuMuonId);
                insertFine.Parameters.AddWithValue("@phieuTraId", phieuTraId);
                insertFine.Parameters.AddWithValue("@tongTien", tongTienPhat);
                insertFine.Parameters.AddWithValue("@nguoiLapId", nguoiXuLyId);
                var phieuPhatId = Convert.ToInt32(insertFine.ExecuteScalar());

                foreach (var fine in dsPhat)
                {
                    using var insertFineDetail = new NpgsqlCommand("""
                        INSERT INTO chi_tiet_phieu_phat (
                            phieu_phat_id, sach_id, ly_do, so_luong, don_gia_phat, thanh_tien, ghi_chu
                        )
                        VALUES (@phieuPhatId, @sachId, @lyDo, @soLuong, @donGia, @thanhTien, @ghiChu)
                        """, conn, tran);
                    insertFineDetail.Parameters.AddWithValue("@phieuPhatId", phieuPhatId);
                    insertFineDetail.Parameters.AddWithValue("@sachId", fine.SachId);
                    insertFineDetail.Parameters.AddWithValue("@lyDo", fine.LyDo);
                    insertFineDetail.Parameters.AddWithValue("@soLuong", fine.SoLuong);
                    insertFineDetail.Parameters.AddWithValue("@donGia", fine.DonGia);
                    insertFineDetail.Parameters.AddWithValue("@thanhTien", fine.ThanhTien);
                    insertFineDetail.Parameters.AddWithValue("@ghiChu", fine.GhiChu);
                    insertFineDetail.ExecuteNonQuery();
                }
            }

            tran.Commit();
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }


    public ReturnSlipPdfDTO GetSlipPdfData(int phieuTraId)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();

            using var headerCmd = new NpgsqlCommand("""
                SELECT pt.ma_phieu_tra,
                       pm.ma_phieu_muon,
                       nm.ma_nguoi_muon,
                       nm.ho_ten,
                       pt.ngay_tra::timestamp,
                       pt.so_ngay_tre,
                       COALESCE(pt.ghi_chu, ''),
                       COALESCE(nd.ho_ten, '')
                FROM phieu_tra pt
                JOIN phieu_muon pm ON pt.phieu_muon_id = pm.id
                JOIN nguoi_muon nm ON pm.nguoi_muon_id = nm.id
                LEFT JOIN nguoi_dung nd ON pt.nguoi_xu_ly_id = nd.id
                WHERE pt.id = @id
                """, conn);
            headerCmd.Parameters.AddWithValue("@id", phieuTraId);

            ReturnSlipPdfDTO? data = null;
            using (var reader = headerCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    data = new ReturnSlipPdfDTO
                    {
                        MaPhieuTra = reader.GetString(0),
                        MaPhieuMuon = reader.GetString(1),
                        MaNguoiMuon = reader.GetString(2),
                        HoTenNguoiMuon = reader.GetString(3),
                        NgayTra = reader.GetDateTime(4),
                        SoNgayTre = reader.GetInt32(5),
                        GhiChu = reader.GetString(6),
                        NguoiXuLy = reader.GetString(7)
                    };
                }
            }

            if (data is null)
                throw new InvalidOperationException("Không tìm thấy phiếu trả để xuất PDF.");

            using var detailCmd = new NpgsqlCommand("""
                SELECT s.ma_sach, s.ten_sach, ctpm.so_luong,
                       ctpt.so_luong_tra, ctpt.so_luong_hong, ctpt.so_luong_mat,
                       COALESCE(ctpt.ghi_chu_tinh_trang, '')
                FROM chi_tiet_phieu_tra ctpt
                JOIN chi_tiet_phieu_muon ctpm ON ctpt.chi_tiet_phieu_muon_id = ctpm.id
                JOIN sach s ON ctpm.sach_id = s.id
                WHERE ctpt.phieu_tra_id = @id
                ORDER BY ctpt.id
                """, conn);
            detailCmd.Parameters.AddWithValue("@id", phieuTraId);
            using (var reader = detailCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    data.Items.Add(new ReturnSlipPdfItemDTO
                    {
                        MaSach = reader.GetString(0),
                        TenSach = reader.GetString(1),
                        SoLuongMuon = reader.GetInt32(2),
                        SoLuongTra = reader.GetInt32(3),
                        SoLuongHong = reader.GetInt32(4),
                        SoLuongMat = reader.GetInt32(5),
                        GhiChuTinhTrang = reader.GetString(6)
                    });
                }
            }

            return data;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    private static List<PhieuTraDTO> Query(string sql, params NpgsqlParameter[] parameters)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            using var reader = cmd.ExecuteReader();
            var items = new List<PhieuTraDTO>();
            while (reader.Read())
            {
                items.Add(new PhieuTraDTO
                {
                    Id = reader.GetInt32(0),
                    MaPhieuTra = reader.GetString(1),
                    MaPhieuMuon = reader.GetString(2),
                    HoTenNguoiMuon = reader.GetString(3),
                    NgayTra = reader.GetDateTime(4),
                    SoNgayTre = reader.GetInt32(5),
                    GhiChu = reader.GetString(6)
                });
            }
            return items;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }
}
