using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class PhieuMuonDAL
{
    public List<PhieuMuonDTO> GetAll()
    {
        const string sql = BaseSelect + " ORDER BY pm.id DESC, ctpm.id";
        return Query(sql);
    }

    public List<PhieuMuonDTO> Search(string keyword)
    {
        const string sql = BaseSelect + """
            WHERE LOWER(pm.ma_phieu_muon) LIKE LOWER(@keyword)
               OR LOWER(nm.ma_nguoi_muon) LIKE LOWER(@keyword)
               OR LOWER(nm.ho_ten) LIKE LOWER(@keyword)
               OR LOWER(s.ten_sach) LIKE LOWER(@keyword)
               OR LOWER(s.ma_sach) LIKE LOWER(@keyword)
            ORDER BY pm.id DESC, ctpm.id
            """;
        return Query(sql, new NpgsqlParameter("@keyword", $"%{keyword.Trim()}%"));
    }

    public string GenerateNewCode()
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                SELECT COALESCE(MAX(CAST(SUBSTRING(ma_phieu_muon FROM 3) AS INTEGER)), 0)
                FROM phieu_muon
                WHERE ma_phieu_muon ~ '^PM[0-9]+$'
                """, conn);
            var last = Convert.ToInt32(cmd.ExecuteScalar() ?? 0);
            return $"PM{last + 1:000}";
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    public void Create(BorrowSlipInputDTO input, int nguoiLapId)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var tran = conn.BeginTransaction();

            using var dupCmd = new NpgsqlCommand("SELECT 1 FROM phieu_muon WHERE ma_phieu_muon = @ma", conn, tran);
            dupCmd.Parameters.AddWithValue("@ma", input.MaPhieuMuon);
            if (dupCmd.ExecuteScalar() is not null)
                throw new InvalidOperationException($"Mã phiếu mượn {input.MaPhieuMuon} đã tồn tại.");

            using var insertSlip = new NpgsqlCommand("""
                INSERT INTO phieu_muon (ma_phieu_muon, nguoi_muon_id, nguoi_lap_id, ngay_muon, ngay_hen_tra, trang_thai)
                VALUES (@ma, @nguoiMuonId, @nguoiLapId, @ngayMuon, @ngayHenTra, 'dang_muon')
                RETURNING id
                """, conn, tran);
            insertSlip.Parameters.AddWithValue("@ma", input.MaPhieuMuon);
            insertSlip.Parameters.AddWithValue("@nguoiMuonId", input.NguoiMuonId);
            insertSlip.Parameters.AddWithValue("@nguoiLapId", nguoiLapId);
            insertSlip.Parameters.AddWithValue("@ngayMuon", input.NgayMuon.Date);
            insertSlip.Parameters.AddWithValue("@ngayHenTra", input.NgayHenTra.Date);
            var phieuMuonId = Convert.ToInt32(insertSlip.ExecuteScalar());

            foreach (var item in input.Items)
            {
                using var getQty = new NpgsqlCommand("SELECT so_luong_con FROM sach WHERE id = @id FOR UPDATE", conn, tran);
                getQty.Parameters.AddWithValue("@id", item.SachId);
                var remain = Convert.ToInt32(getQty.ExecuteScalar() ?? 0);
                if (item.SoLuong > remain)
                    throw new InvalidOperationException($"Sách {item.MaSach} không đủ số lượng còn.");

                using var insertDetail = new NpgsqlCommand("""
                    INSERT INTO chi_tiet_phieu_muon (phieu_muon_id, sach_id, so_luong)
                    VALUES (@phieuMuonId, @sachId, @soLuong)
                    """, conn, tran);
                insertDetail.Parameters.AddWithValue("@phieuMuonId", phieuMuonId);
                insertDetail.Parameters.AddWithValue("@sachId", item.SachId);
                insertDetail.Parameters.AddWithValue("@soLuong", item.SoLuong);
                insertDetail.ExecuteNonQuery();

                using var updateBook = new NpgsqlCommand("""
                    UPDATE sach
                    SET so_luong_con = so_luong_con - @soLuong
                    WHERE id = @id
                    """, conn, tran);
                updateBook.Parameters.AddWithValue("@soLuong", item.SoLuong);
                updateBook.Parameters.AddWithValue("@id", item.SachId);
                updateBook.ExecuteNonQuery();
            }

            tran.Commit();
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    public void DeleteBorrowSlip(int phieuMuonId)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var tran = conn.BeginTransaction();

            using var checkReturn = new NpgsqlCommand("SELECT 1 FROM phieu_tra WHERE phieu_muon_id = @id", conn, tran);
            checkReturn.Parameters.AddWithValue("@id", phieuMuonId);
            if (checkReturn.ExecuteScalar() is not null)
                throw new InvalidOperationException("Phiếu mượn đã có phiếu trả nên không thể xóa.");

            using var getDetails = new NpgsqlCommand("""
                SELECT sach_id, so_luong
                FROM chi_tiet_phieu_muon
                WHERE phieu_muon_id = @id
                """, conn, tran);
            getDetails.Parameters.AddWithValue("@id", phieuMuonId);

            var list = new List<(int SachId, int SoLuong)>();
            using (var reader = getDetails.ExecuteReader())
            {
                while (reader.Read()) list.Add((reader.GetInt32(0), reader.GetInt32(1)));
            }

            foreach (var detail in list)
            {
                using var updateBook = new NpgsqlCommand("""
                    UPDATE sach
                    SET so_luong_con = so_luong_con + @soLuong
                    WHERE id = @id
                    """, conn, tran);
                updateBook.Parameters.AddWithValue("@soLuong", detail.SoLuong);
                updateBook.Parameters.AddWithValue("@id", detail.SachId);
                updateBook.ExecuteNonQuery();
            }

            using var deleteDetails = new NpgsqlCommand("DELETE FROM chi_tiet_phieu_muon WHERE phieu_muon_id = @id", conn, tran);
            deleteDetails.Parameters.AddWithValue("@id", phieuMuonId);
            deleteDetails.ExecuteNonQuery();

            using var deleteHeader = new NpgsqlCommand("DELETE FROM phieu_muon WHERE id = @id", conn, tran);
            deleteHeader.Parameters.AddWithValue("@id", phieuMuonId);
            deleteHeader.ExecuteNonQuery();

            tran.Commit();
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }


    public BorrowSlipPdfDTO GetSlipPdfData(int phieuMuonId)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();

            using var headerCmd = new NpgsqlCommand("""
                SELECT pm.ma_phieu_muon,
                       nm.ma_nguoi_muon,
                       nm.ho_ten,
                       pm.ngay_muon::timestamp,
                       pm.ngay_hen_tra::timestamp,
                       CASE
                           WHEN pm.trang_thai = 'dang_muon' AND pm.ngay_hen_tra < CURRENT_DATE THEN 'qua_han'
                           ELSE pm.trang_thai
                       END AS trang_thai_hien_thi,
                       COALESCE(nd.ho_ten, '')
                FROM phieu_muon pm
                JOIN nguoi_muon nm ON pm.nguoi_muon_id = nm.id
                LEFT JOIN nguoi_dung nd ON pm.nguoi_lap_id = nd.id
                WHERE pm.id = @id
                """, conn);
            headerCmd.Parameters.AddWithValue("@id", phieuMuonId);

            BorrowSlipPdfDTO? data = null;
            using (var reader = headerCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    data = new BorrowSlipPdfDTO
                    {
                        MaPhieuMuon = reader.GetString(0),
                        MaNguoiMuon = reader.GetString(1),
                        HoTenNguoiMuon = reader.GetString(2),
                        NgayMuon = reader.GetDateTime(3),
                        NgayHenTra = reader.GetDateTime(4),
                        TrangThai = reader.GetString(5),
                        NguoiLap = reader.GetString(6)
                    };
                }
            }

            if (data is null)
                throw new InvalidOperationException("Không tìm thấy phiếu mượn để xuất PDF.");

            using var detailCmd = new NpgsqlCommand("""
                SELECT s.ma_sach, s.ten_sach, ctpm.so_luong
                FROM chi_tiet_phieu_muon ctpm
                JOIN sach s ON ctpm.sach_id = s.id
                WHERE ctpm.phieu_muon_id = @id
                ORDER BY ctpm.id
                """, conn);
            detailCmd.Parameters.AddWithValue("@id", phieuMuonId);
            using (var reader = detailCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    data.Items.Add(new BorrowSlipPdfItemDTO
                    {
                        MaSach = reader.GetString(0),
                        TenSach = reader.GetString(1),
                        SoLuong = reader.GetInt32(2)
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

    private const string BaseSelect = """
        SELECT pm.id, ctpm.id, pm.ma_phieu_muon,
               nm.id, nm.ma_nguoi_muon, nm.ho_ten,
               s.id, s.ma_sach, s.ten_sach,
               pm.ngay_muon::timestamp, pm.ngay_hen_tra::timestamp,
               ctpm.so_luong,
               CASE
                   WHEN pm.trang_thai = 'dang_muon' AND pm.ngay_hen_tra < CURRENT_DATE THEN 'qua_han'
                   ELSE pm.trang_thai
               END AS trang_thai_hien_thi
        FROM phieu_muon pm
        JOIN nguoi_muon nm ON pm.nguoi_muon_id = nm.id
        JOIN chi_tiet_phieu_muon ctpm ON pm.id = ctpm.phieu_muon_id
        JOIN sach s ON ctpm.sach_id = s.id
        """;

    private static List<PhieuMuonDTO> Query(string sql, params NpgsqlParameter[] parameters)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            using var reader = cmd.ExecuteReader();
            var items = new List<PhieuMuonDTO>();
            while (reader.Read())
            {
                items.Add(new PhieuMuonDTO
                {
                    PhieuMuonId = reader.GetInt32(0),
                    ChiTietPhieuMuonId = reader.GetInt32(1),
                    MaPhieuMuon = reader.GetString(2),
                    NguoiMuonId = reader.GetInt32(3),
                    MaNguoiMuon = reader.GetString(4),
                    HoTenNguoiMuon = reader.GetString(5),
                    SachId = reader.GetInt32(6),
                    MaSach = reader.GetString(7),
                    TenSach = reader.GetString(8),
                    NgayMuon = reader.GetDateTime(9),
                    HanTra = reader.GetDateTime(10),
                    SoLuong = reader.GetInt32(11),
                    TrangThai = reader.GetString(12)
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
