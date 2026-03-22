namespace QuanLyThuVien.DTO;

public class SachDTO
{
    public int Id { get; set; }
    public string MaSach { get; set; } = string.Empty;
    public string TenSach { get; set; } = string.Empty;
    public string TacGia { get; set; } = string.Empty;
    public int? TheLoaiId { get; set; }
    public string TheLoai { get; set; } = string.Empty;
    public int? NhaXuatBanId { get; set; }
    public string NhaXuatBan { get; set; } = string.Empty;
    public int? NamXB { get; set; }
    public string ViTriKe { get; set; } = string.Empty;
    public int TongSoLuong { get; set; }
    public int SoLuongCon { get; set; }
    public bool DangHoatDong { get; set; }
}
