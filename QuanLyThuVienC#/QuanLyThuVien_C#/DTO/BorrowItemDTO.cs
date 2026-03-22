namespace QuanLyThuVien.DTO;

public class BorrowItemDTO
{
    public int SachId { get; set; }
    public string MaSach { get; set; } = string.Empty;
    public string TenSach { get; set; } = string.Empty;
    public int SoLuong { get; set; }
    public int SoLuongCon { get; set; }
}
