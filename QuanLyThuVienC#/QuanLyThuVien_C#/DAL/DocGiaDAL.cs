using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class DocGiaDAL
{
    public List<DocGiaDTO> GetAll()
    {
        const string sql = """
            SELECT id, ma_nguoi_muon, ho_ten, so_dien_thoai, email, dia_chi
            FROM nguoi_muon
            ORDER BY id DESC
            """;
        return Query(sql);
    }

    public List<DocGiaDTO> Search(string keyword)
    {
        const string sql = """
            SELECT id, ma_nguoi_muon, ho_ten, so_dien_thoai, email, dia_chi
            FROM nguoi_muon
            WHERE LOWER(ma_nguoi_muon) LIKE LOWER(@keyword)
               OR LOWER(ho_ten) LIKE LOWER(@keyword)
               OR LOWER(COALESCE(so_dien_thoai, '')) LIKE LOWER(@keyword)
            ORDER BY id DESC
            """;
        return Query(sql, new NpgsqlParameter("@keyword", $"%{keyword.Trim()}%"));
    }

    public void Insert(DocGiaDTO item)
    {
        const string sql = """
            INSERT INTO nguoi_muon (ma_nguoi_muon, ho_ten, so_dien_thoai, email, dia_chi)
            VALUES (@ma, @hoTen, @sdt, @email, @diaChi)
            """;
        ExecuteNonQuery(sql,
            new NpgsqlParameter("@ma", item.MaNguoiMuon),
            new NpgsqlParameter("@hoTen", item.HoTen),
            new NpgsqlParameter("@sdt", DbValue(item.SoDienThoai)),
            new NpgsqlParameter("@email", DbValue(item.Email)),
            new NpgsqlParameter("@diaChi", DbValue(item.DiaChi)));
    }

    public void Update(DocGiaDTO item)
    {
        const string sql = """
            UPDATE nguoi_muon
            SET ma_nguoi_muon = @ma,
                ho_ten = @hoTen,
                so_dien_thoai = @sdt,
                email = @email,
                dia_chi = @diaChi
            WHERE id = @id
            """;
        ExecuteNonQuery(sql,
            new NpgsqlParameter("@ma", item.MaNguoiMuon),
            new NpgsqlParameter("@hoTen", item.HoTen),
            new NpgsqlParameter("@sdt", DbValue(item.SoDienThoai)),
            new NpgsqlParameter("@email", DbValue(item.Email)),
            new NpgsqlParameter("@diaChi", DbValue(item.DiaChi)),
            new NpgsqlParameter("@id", item.Id));
    }

    public void Delete(int id)
    {
        ExecuteNonQuery("DELETE FROM nguoi_muon WHERE id = @id", new NpgsqlParameter("@id", id));
    }

    public List<LookupItemDTO> GetLookup()
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("SELECT id, ma_nguoi_muon, ho_ten FROM nguoi_muon ORDER BY ho_ten", conn);
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

    private static List<DocGiaDTO> Query(string sql, params NpgsqlParameter[] parameters)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            using var reader = cmd.ExecuteReader();
            var items = new List<DocGiaDTO>();
            while (reader.Read())
            {
                items.Add(new DocGiaDTO
                {
                    Id = reader.GetInt32(0),
                    MaNguoiMuon = reader.GetString(1),
                    HoTen = reader.GetString(2),
                    SoDienThoai = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                    Email = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                    DiaChi = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
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

    private static object DbValue(string? value) => string.IsNullOrWhiteSpace(value) ? DBNull.Value : value.Trim();
}
