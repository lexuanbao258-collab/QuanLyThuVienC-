namespace QuanLyThuVien.DTO;

public class BorrowSlipInputDTO
{
    public string MaPhieuMuon { get; set; } = string.Empty;
    public int NguoiMuonId { get; set; }
    public DateTime NgayMuon { get; set; }
    public DateTime NgayHenTra { get; set; }
    public List<BorrowItemDTO> Items { get; set; } = new();
}
