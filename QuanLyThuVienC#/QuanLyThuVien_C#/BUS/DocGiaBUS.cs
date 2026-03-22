using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class DocGiaBUS
{
    private readonly DocGiaDAL _dal = new();
    public List<DocGiaDTO> GetAll() => _dal.GetAll();
    public List<DocGiaDTO> Search(string keyword) => string.IsNullOrWhiteSpace(keyword) ? GetAll() : _dal.Search(keyword);

    public void Save(DocGiaDTO item)
    {
        if (string.IsNullOrWhiteSpace(item.MaNguoiMuon)) throw new InvalidOperationException("Mã người mượn không được để trống.");
        if (string.IsNullOrWhiteSpace(item.HoTen)) throw new InvalidOperationException("Họ tên không được để trống.");
        if (item.Id == 0) _dal.Insert(item); else _dal.Update(item);
    }

    public void Delete(int id) => _dal.Delete(id);
    public List<LookupItemDTO> GetLookup() => _dal.GetLookup();
}
