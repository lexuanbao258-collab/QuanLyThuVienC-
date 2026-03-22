using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class ThongKeBUS
{
    private readonly ThongKeDAL _dal = new();
    public List<ThongKeDTO> GetTongQuan() => _dal.GetTongQuan();
}
