namespace QuanLyThuVien.DTO;

public class ReturnDetailDTO
{
    public int ChiTietPhieuMuonId { get; set; }
    public int SachId { get; set; }
    public string TenSach { get; set; } = string.Empty;
    public int SoLuongMuon { get; set; }
    public int SoLuongTra { get; set; }
    public int SoLuongHong { get; set; }
    public int SoLuongMat { get; set; }
    public string GhiChu { get; set; } = string.Empty;
}
