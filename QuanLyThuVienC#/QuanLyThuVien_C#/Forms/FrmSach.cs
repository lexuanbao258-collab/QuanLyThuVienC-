using System.ComponentModel;
using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmSach : Form
{
    private readonly SachBUS _bus = new();

    public FrmSach()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải danh sách sách");
        btnSearch.Click += (_, _) => AppGuard.Run(SearchData, "Tìm sách");
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải danh sách sách");
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

    private SachHienThiDTO? CurrentItem => dgvData.CurrentRow?.DataBoundItem as SachHienThiDTO;

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dialog = new SachDialog();
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(dialog.GetValue());
            LoadData();
            MessageHelper.Info("Đã thêm sách.");
        }, "Thêm sách");
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn sách cần sửa.");
            return;
        }

        using var dialog = new SachDialog(CurrentItem);
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(dialog.GetValue());
            LoadData();
            MessageHelper.Info("Đã cập nhật sách.");
        }, "Sửa sách");
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn sách cần xóa.");
            return;
        }

        if (!MessageHelper.Confirm($"Xóa sách {CurrentItem.MaSach} - {CurrentItem.TenSach}?")) return;
        AppGuard.Run(() =>
        {
            _bus.Delete(CurrentItem.Id);
            LoadData();
            MessageHelper.Info("Đã xóa sách.");
        }, "Xóa sách");
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
        if (dgvData.Columns[nameof(SachHienThiDTO.Id)] != null) dgvData.Columns[nameof(SachHienThiDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(SachHienThiDTO.DangHoatDong)] != null) dgvData.Columns[nameof(SachHienThiDTO.DangHoatDong)].HeaderText = "Hoạt động";
        if (dgvData.Columns[nameof(SachHienThiDTO.MaSach)] != null) dgvData.Columns[nameof(SachHienThiDTO.MaSach)].HeaderText = "Mã sách";
        if (dgvData.Columns[nameof(SachHienThiDTO.TenSach)] != null) dgvData.Columns[nameof(SachHienThiDTO.TenSach)].HeaderText = "Tên sách";
        if (dgvData.Columns[nameof(SachHienThiDTO.TacGia)] != null) dgvData.Columns[nameof(SachHienThiDTO.TacGia)].HeaderText = "Tác giả";
        if (dgvData.Columns[nameof(SachHienThiDTO.TheLoai)] != null) dgvData.Columns[nameof(SachHienThiDTO.TheLoai)].HeaderText = "Thể loại";
        if (dgvData.Columns[nameof(SachHienThiDTO.NhaXuatBan)] != null) dgvData.Columns[nameof(SachHienThiDTO.NhaXuatBan)].HeaderText = "Nhà xuất bản";
        if (dgvData.Columns[nameof(SachHienThiDTO.NamXB)] != null) dgvData.Columns[nameof(SachHienThiDTO.NamXB)].HeaderText = "Năm XB";
        if (dgvData.Columns[nameof(SachHienThiDTO.ViTriKe)] != null) dgvData.Columns[nameof(SachHienThiDTO.ViTriKe)].HeaderText = "Vị trí kệ";
        if (dgvData.Columns[nameof(SachHienThiDTO.TongSoLuong)] != null) dgvData.Columns[nameof(SachHienThiDTO.TongSoLuong)].HeaderText = "Tổng số lượng";
        if (dgvData.Columns[nameof(SachHienThiDTO.SoLuongCon)] != null) dgvData.Columns[nameof(SachHienThiDTO.SoLuongCon)].HeaderText = "Số lượng còn";
    }
}
