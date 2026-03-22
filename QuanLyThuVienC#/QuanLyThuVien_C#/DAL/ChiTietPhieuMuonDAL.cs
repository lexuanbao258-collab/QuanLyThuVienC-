using QuanLyThuVien.DTO;

namespace QuanLyThuVien.DAL;

public class ChiTietPhieuMuonDAL
{
    private readonly PhieuMuonDAL _phieuMuonDAL = new();

    public List<PhieuMuonDTO> GetAll() => _phieuMuonDAL.GetAll();
}
