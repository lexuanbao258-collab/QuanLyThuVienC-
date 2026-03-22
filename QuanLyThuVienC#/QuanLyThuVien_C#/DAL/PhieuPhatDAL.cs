using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class PhieuPhatDAL
{
    public List<PhieuPhatDTO> GetAll()
    {
        const string sql = """
            SELECT pp.id, pp.ma_phieu_phat, nm.ma_nguoi_muon, nm.ho_ten, pm.ma_phieu_muon,
                   pp.tong_tien, pp.trang_thai_thanh_toan, pp.ngay_tao
            FROM phieu_phat pp
            JOIN nguoi_muon nm ON pp.nguoi_muon_id = nm.id
            JOIN phieu_muon pm ON pp.phieu_muon_id = pm.id
            ORDER BY pp.id DESC
            """;
        return Query(sql);
    }

    public List<PhieuPhatDTO> Search(string keyword)
    {
        const string sql = """
            SELECT pp.id, pp.ma_phieu_phat, nm.ma_nguoi_muon, nm.ho_ten, pm.ma_phieu_muon,
                   pp.tong_tien, pp.trang_thai_thanh_toan, pp.ngay_tao
            FROM phieu_phat pp
            JOIN nguoi_muon nm ON pp.nguoi_muon_id = nm.id
            JOIN phieu_muon pm ON pp.phieu_muon_id = pm.id
            WHERE LOWER(pp.ma_phieu_phat) LIKE LOWER(@keyword)
               OR LOWER(nm.ma_nguoi_muon) LIKE LOWER(@keyword)
               OR LOWER(nm.ho_ten) LIKE LOWER(@keyword)
               OR LOWER(pm.ma_phieu_muon) LIKE LOWER(@keyword)
            ORDER BY pp.id DESC
            """;
        return Query(sql, new NpgsqlParameter("@keyword", $"%{keyword.Trim()}%"));
    }


    public FineSlipPdfDTO GetSlipPdfData(int phieuPhatId)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();

            using var headerCmd = new NpgsqlCommand("""
                SELECT pp.ma_phieu_phat,
                       nm.ma_nguoi_muon,
                       nm.ho_ten,
                       pm.ma_phieu_muon,
                       COALESCE(pt.ma_phieu_tra, ''),
                       pp.tong_tien,
                       pp.trang_thai_thanh_toan,
                       pp.ngay_tao,
                       COALESCE(nd.ho_ten, '')
                FROM phieu_phat pp
                JOIN nguoi_muon nm ON pp.nguoi_muon_id = nm.id
                JOIN phieu_muon pm ON pp.phieu_muon_id = pm.id
                LEFT JOIN phieu_tra pt ON pp.phieu_tra_id = pt.id
                LEFT JOIN nguoi_dung nd ON pp.nguoi_lap_id = nd.id
                WHERE pp.id = @id
                """, conn);
            headerCmd.Parameters.AddWithValue("@id", phieuPhatId);

            FineSlipPdfDTO? data = null;
            using (var reader = headerCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    data = new FineSlipPdfDTO
                    {
                        MaPhieuPhat = reader.GetString(0),
                        MaNguoiMuon = reader.GetString(1),
                        HoTenNguoiMuon = reader.GetString(2),
                        MaPhieuMuon = reader.GetString(3),
                        MaPhieuTra = reader.GetString(4),
                        TongTien = reader.GetDecimal(5),
                        TrangThaiThanhToan = reader.GetString(6),
                        NgayTao = reader.GetDateTime(7),
                        NguoiLap = reader.GetString(8)
                    };
                }
            }

            if (data is null)
                throw new InvalidOperationException("Không tìm thấy phiếu phạt để xuất PDF.");

            using var detailCmd = new NpgsqlCommand("""
                SELECT COALESCE(s.ma_sach, ''), COALESCE(s.ten_sach, '(Không gắn sách)'),
                       ctp.ly_do, ctp.so_luong, ctp.don_gia_phat, ctp.thanh_tien,
                       COALESCE(ctp.ghi_chu, '')
                FROM chi_tiet_phieu_phat ctp
                LEFT JOIN sach s ON ctp.sach_id = s.id
                WHERE ctp.phieu_phat_id = @id
                ORDER BY ctp.id
                """, conn);
            detailCmd.Parameters.AddWithValue("@id", phieuPhatId);
            using (var reader = detailCmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    data.Items.Add(new FineSlipPdfItemDTO
                    {
                        MaSach = reader.GetString(0),
                        TenSach = reader.GetString(1),
                        LyDo = reader.GetString(2),
                        SoLuong = reader.GetInt32(3),
                        DonGiaPhat = reader.GetDecimal(4),
                        ThanhTien = reader.GetDecimal(5),
                        GhiChu = reader.GetString(6)
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

    public void MarkPaid(int id)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                UPDATE phieu_phat
                SET trang_thai_thanh_toan = 'da_thanh_toan',
                    ngay_thanh_toan = CURRENT_TIMESTAMP
                WHERE id = @id
                """, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    private static List<PhieuPhatDTO> Query(string sql, params NpgsqlParameter[] parameters)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            using var reader = cmd.ExecuteReader();
            var items = new List<PhieuPhatDTO>();
            while (reader.Read())
            {
                items.Add(new PhieuPhatDTO
                {
                    Id = reader.GetInt32(0),
                    MaPhieuPhat = reader.GetString(1),
                    MaNguoiMuon = reader.GetString(2),
                    HoTenNguoiMuon = reader.GetString(3),
                    MaPhieuMuon = reader.GetString(4),
                    TongTien = reader.GetDecimal(5),
                    TrangThaiThanhToan = reader.GetString(6),
                    NgayTao = reader.GetDateTime(7)
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
