using System.ComponentModel;
using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmMuonSach : Form
{
    private readonly PhieuMuonBUS _bus = new();

    public FrmMuonSach()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải phiếu mượn");
        btnSearch.Click += (_, _) => AppGuard.Run(SearchData, "Tìm phiếu mượn");
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Enabled = true;
        btnEdit.Text = "Xuất PDF";
        btnEdit.Click += BtnExportPdf_Click;
        btnDelete.Click += BtnDelete_Click;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải phiếu mượn");
    }

    private void ConfigurePage()
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        TopLevel = false;
        FormBorderStyle = FormBorderStyle.None;
        Dock = DockStyle.Fill;
    }

    private void ConfigureGrid()
    {
        UiFactory.StyleGrid(dgvData);
        dgvData.RowPostPaint += (_, e) => dgvData.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
    }

    private PhieuMuonDTO? CurrentItem => dgvData.CurrentRow?.DataBoundItem as PhieuMuonDTO;

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dialog = new BorrowDialog();
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Create(dialog.GetValue(), Session.CurrentUserId);
            LoadData();
            MessageHelper.Info("Đã lập phiếu mượn.");
        }, "Lập phiếu mượn");
    }


    private void BtnExportPdf_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn phiếu mượn cần xuất PDF.");
            return;
        }

        using var saveDialog = new SaveFileDialog
        {
            Filter = "PDF file (*.pdf)|*.pdf",
            FileName = $"{CurrentItem.MaPhieuMuon}.pdf",
            Title = "Xuất phiếu mượn PDF"
        };
        if (saveDialog.ShowDialog(this) != DialogResult.OK) return;

        AppGuard.Run(() =>
        {
            var data = _bus.GetSlipPdfData(CurrentItem.PhieuMuonId);
            PdfExporter.ExportBorrowSlip(saveDialog.FileName, data);
            MessageHelper.Info("Đã xuất phiếu mượn ra PDF.");
        }, "Xuất PDF phiếu mượn");
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn phiếu mượn cần xóa.");
            return;
        }

        if (!MessageHelper.Confirm($"Xóa phiếu mượn {CurrentItem.MaPhieuMuon}?")) return;
        AppGuard.Run(() =>
        {
            _bus.DeleteBorrowSlip(CurrentItem.PhieuMuonId);
            LoadData();
            MessageHelper.Info("Đã xóa phiếu mượn.");
        }, "Xóa phiếu mượn");
    }

    private void LoadData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _bus.GetAll();
        ApplyHeaders();
    }

    private void SearchData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _bus.Search(txtSearch.Text);
        ApplyHeaders();
    }

    private void ApplyHeaders()
    {
        if (dgvData.Columns[nameof(PhieuMuonDTO.PhieuMuonId)] != null) dgvData.Columns[nameof(PhieuMuonDTO.PhieuMuonId)].Visible = false;
        if (dgvData.Columns[nameof(PhieuMuonDTO.ChiTietPhieuMuonId)] != null) dgvData.Columns[nameof(PhieuMuonDTO.ChiTietPhieuMuonId)].Visible = false;
        if (dgvData.Columns[nameof(PhieuMuonDTO.NguoiMuonId)] != null) dgvData.Columns[nameof(PhieuMuonDTO.NguoiMuonId)].Visible = false;
        if (dgvData.Columns[nameof(PhieuMuonDTO.SachId)] != null) dgvData.Columns[nameof(PhieuMuonDTO.SachId)].Visible = false;
        if (dgvData.Columns[nameof(PhieuMuonDTO.MaPhieuMuon)] != null) dgvData.Columns[nameof(PhieuMuonDTO.MaPhieuMuon)].HeaderText = "Mã phiếu mượn";
        if (dgvData.Columns[nameof(PhieuMuonDTO.MaNguoiMuon)] != null) dgvData.Columns[nameof(PhieuMuonDTO.MaNguoiMuon)].HeaderText = "Mã người mượn";
        if (dgvData.Columns[nameof(PhieuMuonDTO.HoTenNguoiMuon)] != null) dgvData.Columns[nameof(PhieuMuonDTO.HoTenNguoiMuon)].HeaderText = "Họ tên";
        if (dgvData.Columns[nameof(PhieuMuonDTO.MaSach)] != null) dgvData.Columns[nameof(PhieuMuonDTO.MaSach)].HeaderText = "Mã sách";
        if (dgvData.Columns[nameof(PhieuMuonDTO.TenSach)] != null) dgvData.Columns[nameof(PhieuMuonDTO.TenSach)].HeaderText = "Tên sách";
        if (dgvData.Columns[nameof(PhieuMuonDTO.NgayMuon)] != null) dgvData.Columns[nameof(PhieuMuonDTO.NgayMuon)].HeaderText = "Ngày mượn";
        if (dgvData.Columns[nameof(PhieuMuonDTO.HanTra)] != null) dgvData.Columns[nameof(PhieuMuonDTO.HanTra)].HeaderText = "Ngày hẹn trả";
        if (dgvData.Columns[nameof(PhieuMuonDTO.SoLuong)] != null) dgvData.Columns[nameof(PhieuMuonDTO.SoLuong)].HeaderText = "Số lượng";
        if (dgvData.Columns[nameof(PhieuMuonDTO.TrangThai)] != null) dgvData.Columns[nameof(PhieuMuonDTO.TrangThai)].HeaderText = "Trạng thái";
    }
}
