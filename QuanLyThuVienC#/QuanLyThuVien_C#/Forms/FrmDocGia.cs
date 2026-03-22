using System.ComponentModel;
using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmDocGia : Form
{
    private readonly DocGiaBUS _bus = new();

    public FrmDocGia()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải danh sách người mượn");
        btnSearch.Click += (_, _) => AppGuard.Run(SearchData, "Tìm người mượn");
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải danh sách người mượn");
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

    private DocGiaDTO? CurrentItem => dgvData.CurrentRow?.DataBoundItem as DocGiaDTO;

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dialog = new NguoiMuonDialog();
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(dialog.GetValue());
            LoadData();
            MessageHelper.Info("Đã thêm người mượn.");
        }, "Thêm người mượn");
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn người mượn cần sửa.");
            return;
        }

        using var dialog = new NguoiMuonDialog(CurrentItem);
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(dialog.GetValue());
            LoadData();
            MessageHelper.Info("Đã cập nhật người mượn.");
        }, "Sửa người mượn");
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn người mượn cần xóa.");
            return;
        }

        if (!MessageHelper.Confirm($"Xóa người mượn {CurrentItem.MaNguoiMuon} - {CurrentItem.HoTen}?")) return;
        AppGuard.Run(() =>
        {
            _bus.Delete(CurrentItem.Id);
            LoadData();
            MessageHelper.Info("Đã xóa người mượn.");
        }, "Xóa người mượn");
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
        if (dgvData.Columns[nameof(DocGiaDTO.Id)] != null) dgvData.Columns[nameof(DocGiaDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(DocGiaDTO.MaNguoiMuon)] != null) dgvData.Columns[nameof(DocGiaDTO.MaNguoiMuon)].HeaderText = "Mã người mượn";
        if (dgvData.Columns[nameof(DocGiaDTO.HoTen)] != null) dgvData.Columns[nameof(DocGiaDTO.HoTen)].HeaderText = "Họ tên";
        if (dgvData.Columns[nameof(DocGiaDTO.SoDienThoai)] != null) dgvData.Columns[nameof(DocGiaDTO.SoDienThoai)].HeaderText = "Số điện thoại";
        if (dgvData.Columns[nameof(DocGiaDTO.Email)] != null) dgvData.Columns[nameof(DocGiaDTO.Email)].HeaderText = "Email";
        if (dgvData.Columns[nameof(DocGiaDTO.DiaChi)] != null) dgvData.Columns[nameof(DocGiaDTO.DiaChi)].HeaderText = "Địa chỉ";
    }
}
