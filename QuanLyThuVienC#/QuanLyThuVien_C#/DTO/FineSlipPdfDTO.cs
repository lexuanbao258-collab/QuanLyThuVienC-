namespace QuanLyThuVien.DTO;

public class FineSlipPdfDTO
{
    public string MaPhieuPhat { get; set; } = string.Empty;
    public string MaPhieuMuon { get; set; } = string.Empty;
    public string MaPhieuTra { get; set; } = string.Empty;
    public string MaNguoiMuon { get; set; } = string.Empty;
    public string HoTenNguoiMuon { get; set; } = string.Empty;
    public decimal TongTien { get; set; }
    public string TrangThaiThanhToan { get; set; } = string.Empty;
    public DateTime NgayTao { get; set; }
    public string NguoiLap { get; set; } = string.Empty;
    public List<FineSlipPdfItemDTO> Items { get; set; } = new();
}

public class FineSlipPdfItemDTO
{
    public string MaSach { get; set; } = string.Empty;
    public string TenSach { get; set; } = string.Empty;
    public string LyDo { get; set; } = string.Empty;
    public int SoLuong { get; set; }
    public decimal DonGiaPhat { get; set; }
    public decimal ThanhTien { get; set; }
    public string GhiChu { get; set; } = string.Empty;
}
