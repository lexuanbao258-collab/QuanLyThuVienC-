using System.ComponentModel;
using QuanLyThuVien.Common;
using QuanLyThuVien.DAL;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmPhieuPhat : Form
{
    private readonly PhieuPhatDAL _dal = new();

    public FrmPhieuPhat()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải phiếu phạt");
        btnSearch.Click += (_, _) => AppGuard.Run(SearchData, "Tìm phiếu phạt");
        btnAdd.Enabled = true;
        btnAdd.Text = "Xuất PDF";
        btnAdd.Click += BtnExportPdf_Click;
        btnEdit.Text = "Đánh dấu đã thanh toán";
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Enabled = false;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải phiếu phạt");
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
    }

    private PhieuPhatDTO? CurrentItem => dgvData.CurrentRow?.DataBoundItem as PhieuPhatDTO;


    private void BtnExportPdf_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn phiếu phạt cần xuất PDF.");
            return;
        }

        using var saveDialog = new SaveFileDialog
        {
            Filter = "PDF file (*.pdf)|*.pdf",
            FileName = $"{CurrentItem.MaPhieuPhat}.pdf",
            Title = "Xuất phiếu phạt PDF"
        };
        if (saveDialog.ShowDialog(this) != DialogResult.OK) return;

        AppGuard.Run(() =>
        {
            var data = _dal.GetSlipPdfData(CurrentItem.Id);
            PdfExporter.ExportFineSlip(saveDialog.FileName, data);
            MessageHelper.Info("Đã xuất phiếu phạt ra PDF.");
        }, "Xuất PDF phiếu phạt");
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn phiếu phạt.");
            return;
        }

        if (string.Equals(CurrentItem.TrangThaiThanhToan, "da_thanh_toan", StringComparison.OrdinalIgnoreCase))
        {
            MessageHelper.Info("Phiếu phạt này đã được thanh toán.");
            return;
        }

        if (!MessageHelper.Confirm($"Đánh dấu phiếu phạt {CurrentItem.MaPhieuPhat} đã thanh toán?")) return;
        AppGuard.Run(() =>
        {
            _dal.MarkPaid(CurrentItem.Id);
            LoadData();
            MessageHelper.Info("Đã cập nhật trạng thái thanh toán.");
        }, "Thanh toán phiếu phạt");
    }

    private void LoadData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _dal.GetAll();
        ApplyHeaders();
    }

    private void SearchData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _dal.Search(txtSearch.Text);
        ApplyHeaders();
    }

    private void ApplyHeaders()
    {
        if (dgvData.Columns[nameof(PhieuPhatDTO.Id)] != null) dgvData.Columns[nameof(PhieuPhatDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(PhieuPhatDTO.MaPhieuPhat)] != null) dgvData.Columns[nameof(PhieuPhatDTO.MaPhieuPhat)].HeaderText = "Mã phiếu phạt";
        if (dgvData.Columns[nameof(PhieuPhatDTO.MaNguoiMuon)] != null) dgvData.Columns[nameof(PhieuPhatDTO.MaNguoiMuon)].HeaderText = "Mã người mượn";
        if (dgvData.Columns[nameof(PhieuPhatDTO.HoTenNguoiMuon)] != null) dgvData.Columns[nameof(PhieuPhatDTO.HoTenNguoiMuon)].HeaderText = "Người mượn";
        if (dgvData.Columns[nameof(PhieuPhatDTO.MaPhieuMuon)] != null) dgvData.Columns[nameof(PhieuPhatDTO.MaPhieuMuon)].HeaderText = "Mã phiếu mượn";
        if (dgvData.Columns[nameof(PhieuPhatDTO.TongTien)] != null) dgvData.Columns[nameof(PhieuPhatDTO.TongTien)].HeaderText = "Tổng tiền phạt";
        if (dgvData.Columns[nameof(PhieuPhatDTO.TrangThaiThanhToan)] != null) dgvData.Columns[nameof(PhieuPhatDTO.TrangThaiThanhToan)].HeaderText = "Trạng thái thanh toán";
        if (dgvData.Columns[nameof(PhieuPhatDTO.NgayTao)] != null) dgvData.Columns[nameof(PhieuPhatDTO.NgayTao)].HeaderText = "Ngày tạo";
    }
}
