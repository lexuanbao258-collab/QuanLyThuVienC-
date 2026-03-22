using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class AuthBUS
{
    private readonly NguoiDungDAL _nguoiDungDAL = new();
    public NguoiDungDTO? Login(string username, string password) => _nguoiDungDAL.Login(username, password);
}
