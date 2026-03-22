using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class SachBUS
{
    private readonly SachDAL _dal = new();
    public List<SachHienThiDTO> GetAll() => _dal.GetAll().Select(Map).ToList();
    public List<SachHienThiDTO> Search(string keyword) => (string.IsNullOrWhiteSpace(keyword) ? _dal.GetAll() : _dal.Search(keyword)).Select(Map).ToList();

    public void Save(SachDTO item)
    {
        if (string.IsNullOrWhiteSpace(item.MaSach)) throw new InvalidOperationException("Mã sách không được để trống.");
        if (string.IsNullOrWhiteSpace(item.TenSach)) throw new InvalidOperationException("Tên sách không được để trống.");
        if (item.SoLuongCon > item.TongSoLuong) throw new InvalidOperationException("Số lượng còn không được lớn hơn tổng số lượng.");
        if (item.Id == 0) _dal.Insert(item); else _dal.Update(item);
    }

    public void Delete(int id) => _dal.Delete(id);
    public List<LookupItemDTO> GetLookupForBorrow() => _dal.GetLookupForBorrow();

    private static SachHienThiDTO Map(SachDTO x) => new()
    {
        Id = x.Id,
        MaSach = x.MaSach,
        TenSach = x.TenSach,
        TacGia = x.TacGia,
        TheLoai = x.TheLoai,
        NhaXuatBan = x.NhaXuatBan,
        NamXB = x.NamXB,
        ViTriKe = x.ViTriKe,
        TongSoLuong = x.TongSoLuong,
        SoLuongCon = x.SoLuongCon,
        DangHoatDong = x.DangHoatDong
    };
}
