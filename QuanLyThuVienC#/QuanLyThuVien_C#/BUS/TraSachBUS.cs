using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.BUS;

public class TraSachBUS
{
    private readonly PhieuTraDAL _dal = new();
    public List<PhieuTraDTO> GetAll() => _dal.GetAll();
    public List<PhieuTraDTO> Search(string keyword) => string.IsNullOrWhiteSpace(keyword) ? GetAll() : _dal.Search(keyword);
    public string GenerateNewCode() => _dal.GenerateNewCode();
    public List<LookupItemDTO> GetPendingBorrowSlips() => _dal.GetPendingBorrowSlips();
    public List<ReturnDetailDTO> GetBorrowDetailsForReturn(int phieuMuonId) => _dal.GetBorrowDetailsForReturn(phieuMuonId);
    public ReturnSlipPdfDTO GetSlipPdfData(int phieuTraId) => _dal.GetSlipPdfData(phieuTraId);

    public void Create(ReturnSlipInputDTO input, int nguoiXuLyId)
    {
        if (string.IsNullOrWhiteSpace(input.MaPhieuTra)) throw new InvalidOperationException("Mã phiếu trả không được để trống.");
        if (input.PhieuMuonId <= 0) throw new InvalidOperationException("Vui lòng chọn phiếu mượn.");
        if (!input.Items.Any()) throw new InvalidOperationException("Phiếu trả không có chi tiết.");
        _dal.Create(input, nguoiXuLyId);
    }
}
