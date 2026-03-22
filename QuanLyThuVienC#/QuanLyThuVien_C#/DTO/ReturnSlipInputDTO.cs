namespace QuanLyThuVien.DTO;

public class ReturnSlipInputDTO
{
    public int PhieuMuonId { get; set; }
    public string MaPhieuTra { get; set; } = string.Empty;
    public DateTime NgayTra { get; set; }
    public List<ReturnDetailDTO> Items { get; set; } = new();
}
