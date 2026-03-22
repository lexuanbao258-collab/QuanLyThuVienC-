namespace QuanLyThuVien.DTO;

public class NguoiDungDTO
{
    public int Id { get; set; }
    public string TenDangNhap { get; set; } = string.Empty;
    public string MatKhauMaHoa { get; set; } = string.Empty;
    public string HoTen { get; set; } = string.Empty;
    public string VaiTro { get; set; } = string.Empty;
    public bool DangHoatDong { get; set; }
    public DateTime NgayTao { get; set; }
}
