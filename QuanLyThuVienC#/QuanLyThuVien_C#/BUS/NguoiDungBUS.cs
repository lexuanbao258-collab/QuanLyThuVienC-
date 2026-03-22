using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class NguoiDungBUS
{
    private readonly NguoiDungDAL _dal = new();
    public List<NguoiDungDTO> GetAll() => _dal.GetAll();

    public List<NguoiDungDTO> Search(string keyword)
    {
        keyword = (keyword ?? string.Empty).Trim().ToLower();
        return string.IsNullOrWhiteSpace(keyword)
            ? GetAll()
            : GetAll().Where(x =>
                x.TenDangNhap.ToLower().Contains(keyword) ||
                x.HoTen.ToLower().Contains(keyword) ||
                x.VaiTro.ToLower().Contains(keyword)).ToList();
    }

    public void Save(NguoiDungDTO item)
    {
        if (string.IsNullOrWhiteSpace(item.TenDangNhap)) throw new InvalidOperationException("Tên đăng nhập không được để trống.");
        if (string.IsNullOrWhiteSpace(item.MatKhauMaHoa)) throw new InvalidOperationException("Mật khẩu không được để trống.");
        if (string.IsNullOrWhiteSpace(item.HoTen)) throw new InvalidOperationException("Họ tên không được để trống.");
        if (string.IsNullOrWhiteSpace(item.VaiTro)) throw new InvalidOperationException("Vai trò không được để trống.");
        if (item.Id == 0) _dal.Insert(item); else _dal.Update(item);
    }

    public void Delete(int id) => _dal.Delete(id);
}
