namespace QuanLyThuVien.DTO;

public class PhieuTraDTO
{
    public int Id { get; set; }
    public string MaPhieuTra { get; set; } = string.Empty;
    public string MaPhieuMuon { get; set; } = string.Empty;
    public string HoTenNguoiMuon { get; set; } = string.Empty;
    public DateTime NgayTra { get; set; }
    public int SoNgayTre { get; set; }
    public string GhiChu { get; set; } = string.Empty;
}
