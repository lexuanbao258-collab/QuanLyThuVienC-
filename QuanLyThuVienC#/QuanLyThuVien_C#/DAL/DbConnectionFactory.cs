using System.Configuration;
using Npgsql;

namespace QuanLyThuVien.DAL;

public static class DbConnectionFactory
{
    public static string ConnectionString
    {
        get
        {
            var connection = ConfigurationManager.ConnectionStrings["LibraryDb"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new InvalidOperationException("Chưa tìm thấy chuỗi kết nối 'LibraryDb' trong App.config.");
            }

            return connection;
        }
    }

    public static NpgsqlConnection CreateConnection() => new(ConnectionString);

    public static NpgsqlConnection CreateOpenConnection()
    {
        var connection = CreateConnection();
        connection.Open();
        return connection;
    }

    public static InvalidOperationException BuildFriendlyException(Exception ex)
    {
        return new InvalidOperationException(
            "Không kết nối được PostgreSQL. Hãy kiểm tra PostgreSQL đã chạy, database đã tạo bằng file database/setup_postgresql.sql, và App.config đúng Host/Port/Database/Username/Password.\n\nChi tiết kỹ thuật: " + ex.Message,
            ex);
    }
}
