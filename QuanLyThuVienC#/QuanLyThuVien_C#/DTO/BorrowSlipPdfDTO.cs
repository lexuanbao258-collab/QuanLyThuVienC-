namespace QuanLyThuVien.DTO;

public class BorrowSlipPdfDTO
{
    public string MaPhieuMuon { get; set; } = string.Empty;
    public string MaNguoiMuon { get; set; } = string.Empty;
    public string HoTenNguoiMuon { get; set; } = string.Empty;
    public DateTime NgayMuon { get; set; }
    public DateTime NgayHenTra { get; set; }
    public string TrangThai { get; set; } = string.Empty;
    public string NguoiLap { get; set; } = string.Empty;
    public List<BorrowSlipPdfItemDTO> Items { get; set; } = new();
}

public class BorrowSlipPdfItemDTO
{
    public string MaSach { get; set; } = string.Empty;
    public string TenSach { get; set; } = string.Empty;
    public int SoLuong { get; set; }
}
