using System.ComponentModel;
using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmTraSach : Form
{
    private readonly TraSachBUS _bus = new();

    public FrmTraSach()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải phiếu trả");
        btnSearch.Click += (_, _) => AppGuard.Run(SearchData, "Tìm phiếu trả");
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Enabled = true;
        btnEdit.Text = "Xuất PDF";
        btnEdit.Click += BtnExportPdf_Click;
        btnDelete.Enabled = false;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải phiếu trả");
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


    private PhieuTraDTO? CurrentItem => dgvData.CurrentRow?.DataBoundItem as PhieuTraDTO;

    private void BtnExportPdf_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn phiếu trả cần xuất PDF.");
            return;
        }

        using var saveDialog = new SaveFileDialog
        {
            Filter = "PDF file (*.pdf)|*.pdf",
            FileName = $"{CurrentItem.MaPhieuTra}.pdf",
            Title = "Xuất phiếu trả PDF"
        };
        if (saveDialog.ShowDialog(this) != DialogResult.OK) return;

        AppGuard.Run(() =>
        {
            var data = _bus.GetSlipPdfData(CurrentItem.Id);
            PdfExporter.ExportReturnSlip(saveDialog.FileName, data);
            MessageHelper.Info("Đã xuất phiếu trả ra PDF.");
        }, "Xuất PDF phiếu trả");
    }

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        AppGuard.Run(() =>
        {
            var pending = _bus.GetPendingBorrowSlips();
            if (pending.Count == 0)
            {
                MessageHelper.Info("Không có phiếu mượn nào đang chờ trả.");
                return;
            }

            using var picker = new Form
            {
                Text = "Chọn phiếu mượn để trả",
                Width = 520,
                Height = 180,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var cbo = new ComboBox { Dock = DockStyle.Top, DropDownStyle = ComboBoxStyle.DropDownList, Height = 34 };
            foreach (var item in pending) cbo.Items.Add(item);
            cbo.SelectedIndex = 0;

            var btnOk = new Button { Text = "Tiếp tục", DialogResult = DialogResult.OK, Width = 100 };
            var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Width = 100 };
            var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, FlowDirection = FlowDirection.RightToLeft, Height = 42 };
            buttons.Controls.Add(btnOk);
            buttons.Controls.Add(btnCancel);

            var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), RowCount = 3, ColumnCount = 1 };
            root.Controls.Add(new Label { Text = "Phiếu mượn", AutoSize = true }, 0, 0);
            root.Controls.Add(cbo, 0, 1);
            root.Controls.Add(buttons, 0, 2);

            picker.Controls.Add(root);
            picker.AcceptButton = btnOk;
            picker.CancelButton = btnCancel;

            if (picker.ShowDialog(this) != DialogResult.OK) return;
            var selected = cbo.SelectedItem as LookupItemDTO;
            if (selected is null || selected.Id <= 0) return;

            using var dialog = new ReturnDialog(selected.Id);
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            _bus.Create(dialog.GetValue(), Session.CurrentUserId);
            LoadData();
            MessageHelper.Info("Đã lập phiếu trả.");
        }, "Lập phiếu trả");
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
        if (dgvData.Columns[nameof(PhieuTraDTO.Id)] != null) dgvData.Columns[nameof(PhieuTraDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(PhieuTraDTO.MaPhieuTra)] != null) dgvData.Columns[nameof(PhieuTraDTO.MaPhieuTra)].HeaderText = "Mã phiếu trả";
        if (dgvData.Columns[nameof(PhieuTraDTO.MaPhieuMuon)] != null) dgvData.Columns[nameof(PhieuTraDTO.MaPhieuMuon)].HeaderText = "Mã phiếu mượn";
        if (dgvData.Columns[nameof(PhieuTraDTO.HoTenNguoiMuon)] != null) dgvData.Columns[nameof(PhieuTraDTO.HoTenNguoiMuon)].HeaderText = "Người mượn";
        if (dgvData.Columns[nameof(PhieuTraDTO.NgayTra)] != null) dgvData.Columns[nameof(PhieuTraDTO.NgayTra)].HeaderText = "Ngày trả";
        if (dgvData.Columns[nameof(PhieuTraDTO.SoNgayTre)] != null) dgvData.Columns[nameof(PhieuTraDTO.SoNgayTre)].HeaderText = "Số ngày trễ";
        if (dgvData.Columns[nameof(PhieuTraDTO.GhiChu)] != null) dgvData.Columns[nameof(PhieuTraDTO.GhiChu)].HeaderText = "Ghi chú";
    }
}
