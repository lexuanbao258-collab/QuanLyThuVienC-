using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class TheLoaiBUS
{
    private readonly TheLoaiDAL _dal = new();
    public List<TheLoaiDTO> GetAll() => _dal.GetAll();
    public List<TheLoaiDTO> Search(string keyword) => string.IsNullOrWhiteSpace(keyword) ? GetAll() : _dal.Search(keyword);

    public void Save(TheLoaiDTO item)
    {
        if (string.IsNullOrWhiteSpace(item.TenTheLoai)) throw new InvalidOperationException("Tên thể loại không được để trống.");
        if (item.Id == 0) _dal.Insert(item); else _dal.Update(item);
    }

    public void Delete(int id) => _dal.Delete(id);
    public List<LookupItemDTO> GetLookup() => _dal.GetLookup();
}
