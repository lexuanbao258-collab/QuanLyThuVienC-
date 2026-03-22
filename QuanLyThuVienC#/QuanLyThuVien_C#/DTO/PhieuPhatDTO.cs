namespace QuanLyThuVien.DTO;

public class PhieuPhatDTO
{
    public int Id { get; set; }
    public string MaPhieuPhat { get; set; } = string.Empty;
    public string MaNguoiMuon { get; set; } = string.Empty;
    public string HoTenNguoiMuon { get; set; } = string.Empty;
    public string MaPhieuMuon { get; set; } = string.Empty;
    public decimal TongTien { get; set; }
    public string TrangThaiThanhToan { get; set; } = string.Empty;
    public DateTime NgayTao { get; set; }
}
