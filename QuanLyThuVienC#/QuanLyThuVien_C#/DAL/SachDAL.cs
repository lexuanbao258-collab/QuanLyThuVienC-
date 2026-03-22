using Npgsql;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class SachDAL
{
    public List<SachDTO> GetAll()
    {
        const string sql = BaseSelect + " ORDER BY s.id DESC";
        return Query(sql);
    }

    public List<SachDTO> Search(string keyword)
    {
        var sql = BaseSelect + """
            WHERE LOWER(s.ma_sach) LIKE LOWER(@keyword)
               OR LOWER(s.ten_sach) LIKE LOWER(@keyword)
               OR LOWER(COALESCE(s.tac_gia, '')) LIKE LOWER(@keyword)
               OR LOWER(COALESCE(tl.ten_the_loai, '')) LIKE LOWER(@keyword)
               OR LOWER(COALESCE(nxb.ten_nha_xuat_ban, '')) LIKE LOWER(@keyword)
            ORDER BY s.id DESC
            """;
        return Query(sql, new NpgsqlParameter("@keyword", $"%{keyword.Trim()}%"));
    }

    public List<LookupItemDTO> GetLookupForBorrow()
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand("""
                SELECT id, ma_sach, ten_sach
                FROM sach
                WHERE dang_hoat_dong = TRUE AND so_luong_con > 0
                ORDER BY ten_sach
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

    public void Insert(SachDTO item)
    {
        const string sql = """
            INSERT INTO sach (
                ma_sach, ten_sach, tac_gia, the_loai_id, nha_xuat_ban_id,
                nam_xuat_ban, vi_tri_ke, tong_so_luong, so_luong_con, dang_hoat_dong
            )
            VALUES (@ma, @ten, @tacGia, @theLoaiId, @nxbId, @nam, @viTri, @tong, @con, @active)
            """;
        ExecuteNonQuery(sql,
            new NpgsqlParameter("@ma", item.MaSach),
            new NpgsqlParameter("@ten", item.TenSach),
            new NpgsqlParameter("@tacGia", DbValue(item.TacGia)),
            new NpgsqlParameter("@theLoaiId", DbInt(item.TheLoaiId)),
            new NpgsqlParameter("@nxbId", DbInt(item.NhaXuatBanId)),
            new NpgsqlParameter("@nam", DbInt(item.NamXB)),
            new NpgsqlParameter("@viTri", DbValue(item.ViTriKe)),
            new NpgsqlParameter("@tong", item.TongSoLuong),
            new NpgsqlParameter("@con", item.SoLuongCon),
            new NpgsqlParameter("@active", item.DangHoatDong));
    }

    public void Update(SachDTO item)
    {
        const string sql = """
            UPDATE sach
            SET ma_sach = @ma,
                ten_sach = @ten,
                tac_gia = @tacGia,
                the_loai_id = @theLoaiId,
                nha_xuat_ban_id = @nxbId,
                nam_xuat_ban = @nam,
                vi_tri_ke = @viTri,
                tong_so_luong = @tong,
                so_luong_con = @con,
                dang_hoat_dong = @active
            WHERE id = @id
            """;
        ExecuteNonQuery(sql,
            new NpgsqlParameter("@ma", item.MaSach),
            new NpgsqlParameter("@ten", item.TenSach),
            new NpgsqlParameter("@tacGia", DbValue(item.TacGia)),
            new NpgsqlParameter("@theLoaiId", DbInt(item.TheLoaiId)),
            new NpgsqlParameter("@nxbId", DbInt(item.NhaXuatBanId)),
            new NpgsqlParameter("@nam", DbInt(item.NamXB)),
            new NpgsqlParameter("@viTri", DbValue(item.ViTriKe)),
            new NpgsqlParameter("@tong", item.TongSoLuong),
            new NpgsqlParameter("@con", item.SoLuongCon),
            new NpgsqlParameter("@active", item.DangHoatDong),
            new NpgsqlParameter("@id", item.Id));
    }

    public void Delete(int id)
    {
        ExecuteNonQuery("DELETE FROM sach WHERE id = @id", new NpgsqlParameter("@id", id));
    }

    private const string BaseSelect = """
        SELECT s.id, s.ma_sach, s.ten_sach, COALESCE(s.tac_gia, ''),
               s.the_loai_id, COALESCE(tl.ten_the_loai, ''),
               s.nha_xuat_ban_id, COALESCE(nxb.ten_nha_xuat_ban, ''),
               s.nam_xuat_ban, COALESCE(s.vi_tri_ke, ''),
               s.tong_so_luong, s.so_luong_con, s.dang_hoat_dong
        FROM sach s
        LEFT JOIN the_loai tl ON s.the_loai_id = tl.id
        LEFT JOIN nha_xuat_ban nxb ON s.nha_xuat_ban_id = nxb.id
        """;

    private static List<SachDTO> Query(string sql, params NpgsqlParameter[] parameters)
    {
        try
        {
            using var conn = DbConnectionFactory.CreateOpenConnection();
            using var cmd = new NpgsqlCommand(sql, conn);
            if (parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            using var reader = cmd.ExecuteReader();
            var items = new List<SachDTO>();
            while (reader.Read())
            {
                items.Add(new SachDTO
                {
                    Id = reader.GetInt32(0),
                    MaSach = reader.GetString(1),
                    TenSach = reader.GetString(2),
                    TacGia = reader.GetString(3),
                    TheLoaiId = reader.IsDBNull(4) ? null : reader.GetInt32(4),
                    TheLoai = reader.GetString(5),
                    NhaXuatBanId = reader.IsDBNull(6) ? null : reader.GetInt32(6),
                    NhaXuatBan = reader.GetString(7),
                    NamXB = reader.IsDBNull(8) ? null : reader.GetInt32(8),
                    ViTriKe = reader.GetString(9),
                    TongSoLuong = reader.GetInt32(10),
                    SoLuongCon = reader.GetInt32(11),
                    DangHoatDong = reader.GetBoolean(12)
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
    private static object DbInt(int? value) => value.HasValue ? value.Value : DBNull.Value;
}
