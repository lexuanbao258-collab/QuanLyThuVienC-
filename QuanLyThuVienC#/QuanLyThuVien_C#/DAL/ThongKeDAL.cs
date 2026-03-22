using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class ThongKeDAL
{
    public List<ThongKeDTO> GetTongQuan()
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            var items = new List<ThongKeDTO>
            {
                new() { ChiSo = "Tổng số đầu sách", GiaTri = ExecuteScalar(conn, "SELECT COUNT(*) FROM sach") },
                new() { ChiSo = "Tổng số sách đang mượn", GiaTri = ExecuteScalar(conn, """
                    SELECT COALESCE(SUM(ctm.so_luong), 0)
                    FROM chi_tiet_phieu_muon ctm
                    JOIN phieu_muon pm ON ctm.phieu_muon_id = pm.id
                    WHERE pm.trang_thai = 'dang_muon'
                    """) },
                new() { ChiSo = "Số phiếu quá hạn", GiaTri = ExecuteScalar(conn, """
                    SELECT COUNT(*)
                    FROM phieu_muon
                    WHERE trang_thai = 'dang_muon'
                      AND ngay_hen_tra < CURRENT_DATE
                    """) },
                new() { ChiSo = "Tổng tiền phạt chưa thanh toán", GiaTri = ExecuteScalar(conn, """
                    SELECT COALESCE(SUM(tong_tien), 0)
                    FROM phieu_phat
                    WHERE trang_thai_thanh_toan = 'chua_thanh_toan'
                    """) },
                new() { ChiSo = "Người mượn", GiaTri = ExecuteScalar(conn, "SELECT COUNT(*) FROM nguoi_muon") },
                new() { ChiSo = "Tài khoản đang hoạt động", GiaTri = ExecuteScalar(conn, "SELECT COUNT(*) FROM nguoi_dung WHERE dang_hoat_dong = TRUE") }
            };

            using var topCmd = new NpgsqlCommand("""
                SELECT s.ten_sach, COALESCE(SUM(ctm.so_luong), 0) AS tong_luot_muon
                FROM sach s
                LEFT JOIN chi_tiet_phieu_muon ctm ON s.id = ctm.sach_id
                GROUP BY s.id, s.ten_sach
                ORDER BY tong_luot_muon DESC, s.ten_sach
                LIMIT 5
                """, conn);
            using var reader = topCmd.ExecuteReader();
            var rank = 1;
            while (reader.Read())
            {
                items.Add(new ThongKeDTO
                {
                    ChiSo = $"Top {rank}",
                    GiaTri = $"{reader.GetString(0)} - {reader.GetInt64(1)} lượt"
                });
                rank++;
            }

            return items;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    private static string ExecuteScalar(NpgsqlConnection conn, string sql)
    {
        using var cmd = new NpgsqlCommand(sql, conn);
        return Convert.ToString(cmd.ExecuteScalar()) ?? "0";
    }
}
