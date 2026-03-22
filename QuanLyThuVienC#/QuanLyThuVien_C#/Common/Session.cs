using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Common;

public static class Session
{
    public static NguoiDungDTO? CurrentUser { get; set; }
    public static int CurrentUserId => CurrentUser?.Id ?? 0;
}
