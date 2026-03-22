using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class NhaXuatBanBUS
{
    private readonly NhaXuatBanDAL _dal = new();
    public List<NhaXuatBanDTO> GetAll() => _dal.GetAll();
    public List<NhaXuatBanDTO> Search(string keyword) => string.IsNullOrWhiteSpace(keyword) ? GetAll() : _dal.Search(keyword);

    public void Save(NhaXuatBanDTO item)
    {
        if (string.IsNullOrWhiteSpace(item.TenNhaXuatBan)) throw new InvalidOperationException("Tên nhà xuất bản không được để trống.");
        if (item.Id == 0) _dal.Insert(item); else _dal.Update(item);
    }

    public void Delete(int id) => _dal.Delete(id);
    public List<LookupItemDTO> GetLookup() => _dal.GetLookup();
}
