using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class TheLoaiDAL
{
    public List<TheLoaiDTO> GetAll()
    {
        const string sql = "SELECT id, ten_the_loai FROM the_loai ORDER BY ten_the_loai";
        return Query(sql);
    }

    public List<TheLoaiDTO> Search(string keyword)
    {
        const string sql = """
            SELECT id, ten_the_loai
            FROM the_loai
            WHERE LOWER(ten_the_loai) LIKE LOWER(@keyword)
            ORDER BY ten_the_loai
            """;
        return Query(sql, new NpgsqlParameter("@keyword", $"%{keyword.Trim()}%"));
    }

    public void Insert(TheLoaiDTO item)
    {
        ExecuteNonQuery("INSERT INTO the_loai (ten_the_loai) VALUES (@name)", new NpgsqlParameter("@name", item.TenTheLoai));
    }

    public void Update(TheLoaiDTO item)
    {
        ExecuteNonQuery("UPDATE the_loai SET ten_the_loai = @name WHERE id = @id", new NpgsqlParameter("@name", item.TenTheLoai), new NpgsqlParameter("@id", item.Id));
    }

    public void Delete(int id)
    {
        ExecuteNonQuery("DELETE FROM the_loai WHERE id = @id", new NpgsqlParameter("@id", id));
    }

    public List<LookupItemDTO> GetLookup() => GetAll().Select(x => new LookupItemDTO { Id = x.Id, Name = x.TenTheLoai }).ToList();

    private static List<TheLoaiDTO> Query(string sql, params NpgsqlParameter[] parameters)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            using var reader = cmd.ExecuteReader();
            var items = new List<TheLoaiDTO>();
            while (reader.Read())
            {
                items.Add(new TheLoaiDTO
                {
                    Id = reader.GetInt32(0),
                    TenTheLoai = reader.GetString(1)
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
