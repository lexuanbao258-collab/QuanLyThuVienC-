namespace QuanLyThuVien.DTO;

public class ReturnSlipPdfDTO
{
    public string MaPhieuTra { get; set; } = string.Empty;
    public string MaPhieuMuon { get; set; } = string.Empty;
    public string MaNguoiMuon { get; set; } = string.Empty;
    public string HoTenNguoiMuon { get; set; } = string.Empty;
    public DateTime NgayTra { get; set; }
    public int SoNgayTre { get; set; }
    public string GhiChu { get; set; } = string.Empty;
    public string NguoiXuLy { get; set; } = string.Empty;
    public List<ReturnSlipPdfItemDTO> Items { get; set; } = new();
}

public class ReturnSlipPdfItemDTO
{
    public string MaSach { get; set; } = string.Empty;
    public string TenSach { get; set; } = string.Empty;
    public int SoLuongMuon { get; set; }
    public int SoLuongTra { get; set; }
    public int SoLuongHong { get; set; }
    public int SoLuongMat { get; set; }
    public string GhiChuTinhTrang { get; set; } = string.Empty;
}
