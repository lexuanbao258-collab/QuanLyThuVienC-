using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class PhieuMuonBUS
{
    private readonly PhieuMuonDAL _dal = new();
    public List<PhieuMuonDTO> GetAll() => _dal.GetAll();
    public List<PhieuMuonDTO> Search(string keyword) => string.IsNullOrWhiteSpace(keyword) ? GetAll() : _dal.Search(keyword);
    public string GenerateNewCode() => _dal.GenerateNewCode();

    public void Create(BorrowSlipInputDTO input, int nguoiLapId)
    {
        if (string.IsNullOrWhiteSpace(input.MaPhieuMuon)) throw new InvalidOperationException("Mã phiếu mượn không được để trống.");
        if (input.NguoiMuonId <= 0) throw new InvalidOperationException("Vui lòng chọn người mượn.");
        if (!input.Items.Any()) throw new InvalidOperationException("Phiếu mượn phải có ít nhất một cuốn sách.");
        if (input.NgayHenTra.Date < input.NgayMuon.Date) throw new InvalidOperationException("Ngày hẹn trả phải lớn hơn hoặc bằng ngày mượn.");
        _dal.Create(input, nguoiLapId);
    }

    public void DeleteBorrowSlip(int phieuMuonId) => _dal.DeleteBorrowSlip(phieuMuonId);
    public BorrowSlipPdfDTO GetSlipPdfData(int phieuMuonId) => _dal.GetSlipPdfData(phieuMuonId);
}
