using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class NguoiDungDAL
{
    public NguoiDungDTO? Login(string username, string password)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                SELECT id, ten_dang_nhap, mat_khau_ma_hoa, ho_ten, vai_tro, dang_hoat_dong, ngay_tao
                FROM nguoi_dung
                WHERE ten_dang_nhap = @username
                  AND mat_khau_ma_hoa = @password
                  AND dang_hoat_dong = TRUE
                LIMIT 1
                """, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            using var reader = cmd.ExecuteReader();
            return reader.Read() ? Map(reader) : null;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    public List<NguoiDungDTO> GetAll()
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                SELECT id, ten_dang_nhap, mat_khau_ma_hoa, ho_ten, vai_tro, dang_hoat_dong, ngay_tao
                FROM nguoi_dung
                ORDER BY id DESC
                """, conn);
            using var reader = cmd.ExecuteReader();
            var items = new List<NguoiDungDTO>();
            while (reader.Read()) items.Add(Map(reader));
            return items;
        }
        catch (Exception ex)
        {
            throw DbConnectionFactory.BuildFriendlyException(ex);
        }
    }

    public void Insert(NguoiDungDTO item)
    {
        const string sql = """
            INSERT INTO nguoi_dung (ten_dang_nhap, mat_khau_ma_hoa, ho_ten, vai_tro, dang_hoat_dong)
            VALUES (@username, @password, @hoTen, @vaiTro, @active)
            """;
        ExecuteNonQuery(sql,
            new NpgsqlParameter("@username", item.TenDangNhap),
            new NpgsqlParameter("@password", item.MatKhauMaHoa),
            new NpgsqlParameter("@hoTen", item.HoTen),
            new NpgsqlParameter("@vaiTro", item.VaiTro),
            new NpgsqlParameter("@active", item.DangHoatDong));
    }

    public void Update(NguoiDungDTO item)
    {
        const string sql = """
            UPDATE nguoi_dung
            SET ten_dang_nhap = @username,
                mat_khau_ma_hoa = @password,
                ho_ten = @hoTen,
                vai_tro = @vaiTro,
                dang_hoat_dong = @active
            WHERE id = @id
            """;
        ExecuteNonQuery(sql,
            new NpgsqlParameter("@username", item.TenDangNhap),
            new NpgsqlParameter("@password", item.MatKhauMaHoa),
            new NpgsqlParameter("@hoTen", item.HoTen),
            new NpgsqlParameter("@vaiTro", item.VaiTro),
            new NpgsqlParameter("@active", item.DangHoatDong),
            new NpgsqlParameter("@id", item.Id));
    }

    public void Delete(int id)
    {
        ExecuteNonQuery("DELETE FROM nguoi_dung WHERE id = @id", new NpgsqlParameter("@id", id));
    }

    private static NguoiDungDTO Map(NpgsqlDataReader reader) => new()
    {
        Id = reader.GetInt32(0),
        TenDangNhap = reader.GetString(1),
        MatKhauMaHoa = reader.GetString(2),
        HoTen = reader.GetString(3),
        VaiTro = reader.GetString(4),
        DangHoatDong = reader.GetBoolean(5),
        NgayTao = reader.GetDateTime(6)
    };

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
