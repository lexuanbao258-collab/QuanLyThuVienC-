namespace QuanLyThuVien.DTO;

public class PhieuMuonDTO
{
    public int PhieuMuonId { get; set; }
    public int ChiTietPhieuMuonId { get; set; }
    public string MaPhieuMuon { get; set; } = string.Empty;
    public int NguoiMuonId { get; set; }
    public string MaNguoiMuon { get; set; } = string.Empty;
    public string HoTenNguoiMuon { get; set; } = string.Empty;
    public int SachId { get; set; }
    public string MaSach { get; set; } = string.Empty;
    public string TenSach { get; set; } = string.Empty;
    public DateTime NgayMuon { get; set; }
    public DateTime HanTra { get; set; }
    public int SoLuong { get; set; }
    public string TrangThai { get; set; } = string.Empty;
}
