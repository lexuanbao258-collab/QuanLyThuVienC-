using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class NhaXuatBanDAL
{
    public List<NhaXuatBanDTO> GetAll()
    {
        const string sql = "SELECT id, ten_nha_xuat_ban FROM nha_xuat_ban ORDER BY ten_nha_xuat_ban";
        return Query(sql);
    }

    public List<NhaXuatBanDTO> Search(string keyword)
    {
        const string sql = """
            SELECT id, ten_nha_xuat_ban
            FROM nha_xuat_ban
            WHERE LOWER(ten_nha_xuat_ban) LIKE LOWER(@keyword)
            ORDER BY ten_nha_xuat_ban
            """;
        return Query(sql, new NpgsqlParameter("@keyword", $"%{keyword.Trim()}%"));
    }

    public void Insert(NhaXuatBanDTO item)
    {
        ExecuteNonQuery("INSERT INTO nha_xuat_ban (ten_nha_xuat_ban) VALUES (@name)", new NpgsqlParameter("@name", item.TenNhaXuatBan));
    }

    public void Update(NhaXuatBanDTO item)
    {
        ExecuteNonQuery("UPDATE nha_xuat_ban SET ten_nha_xuat_ban = @name WHERE id = @id", new NpgsqlParameter("@name", item.TenNhaXuatBan), new NpgsqlParameter("@id", item.Id));
    }

    public void Delete(int id)
    {
        ExecuteNonQuery("DELETE FROM nha_xuat_ban WHERE id = @id", new NpgsqlParameter("@id", id));
    }

    public List<LookupItemDTO> GetLookup() => GetAll().Select(x => new LookupItemDTO { Id = x.Id, Name = x.TenNhaXuatBan }).ToList();

    private static List<NhaXuatBanDTO> Query(string sql, params NpgsqlParameter[] parameters)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            using var reader = cmd.ExecuteReader();
            var items = new List<NhaXuatBanDTO>();
            while (reader.Read())
            {
                items.Add(new NhaXuatBanDTO
                {
                    Id = reader.GetInt32(0),
                    TenNhaXuatBan = reader.GetString(1)
                });
            }
            return items;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    private static void ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddRange(parameters);
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }
}
