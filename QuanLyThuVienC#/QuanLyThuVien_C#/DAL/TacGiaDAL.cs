using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class TacGiaDAL
{
    public List<TacGiaDTO> GetAll()
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                SELECT DISTINCT COALESCE(tac_gia, '')
                FROM sach
                WHERE COALESCE(tac_gia, '') <> ''
                ORDER BY tac_gia
                """, conn);
            using var reader = cmd.ExecuteReader();
            var items = new List<TacGiaDTO>();
            while (reader.Read())
            {
                items.Add(new TacGiaDTO { TenTacGia = reader.GetString(0) });
            }
            return items;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }
}
