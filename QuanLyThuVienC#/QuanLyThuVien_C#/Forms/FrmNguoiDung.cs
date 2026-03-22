using System.ComponentModel;
using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmNguoiDung : Form
{
    private readonly NguoiDungBUS _bus = new();

    public FrmNguoiDung()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải tài khoản");
        btnSearch.Click += (_, _) => AppGuard.Run(SearchData, "Tìm tài khoản");
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải tài khoản");
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

    private NguoiDungDTO? CurrentItem => dgvData.CurrentRow?.DataBoundItem as NguoiDungDTO;

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dialog = new NguoiDungDialog();
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(dialog.GetValue());
            LoadData();
            MessageHelper.Info("Đã thêm tài khoản.");
        }, "Thêm tài khoản");
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn tài khoản cần sửa.");
            return;
        }

        using var dialog = new NguoiDungDialog(CurrentItem);
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(dialog.GetValue());
            LoadData();
            MessageHelper.Info("Đã cập nhật tài khoản.");
        }, "Sửa tài khoản");
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn tài khoản cần xóa.");
            return;
        }

        if (CurrentItem.Id == Session.CurrentUserId)
        {
            MessageHelper.Warning("Không thể xóa tài khoản đang đăng nhập.");
            return;
        }

        if (!MessageHelper.Confirm($"Xóa tài khoản {CurrentItem.TenDangNhap}?")) return;
        AppGuard.Run(() =>
        {
            _bus.Delete(CurrentItem.Id);
            LoadData();
            MessageHelper.Info("Đã xóa tài khoản.");
        }, "Xóa tài khoản");
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
        if (dgvData.Columns[nameof(NguoiDungDTO.Id)] != null) dgvData.Columns[nameof(NguoiDungDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(NguoiDungDTO.MatKhauMaHoa)] != null) dgvData.Columns[nameof(NguoiDungDTO.MatKhauMaHoa)].Visible = false;
        if (dgvData.Columns[nameof(NguoiDungDTO.NgayTao)] != null) dgvData.Columns[nameof(NguoiDungDTO.NgayTao)].HeaderText = "Ngày tạo";
        if (dgvData.Columns[nameof(NguoiDungDTO.TenDangNhap)] != null) dgvData.Columns[nameof(NguoiDungDTO.TenDangNhap)].HeaderText = "Tên đăng nhập";
        if (dgvData.Columns[nameof(NguoiDungDTO.HoTen)] != null) dgvData.Columns[nameof(NguoiDungDTO.HoTen)].HeaderText = "Họ tên";
        if (dgvData.Columns[nameof(NguoiDungDTO.VaiTro)] != null) dgvData.Columns[nameof(NguoiDungDTO.VaiTro)].HeaderText = "Vai trò";
        if (dgvData.Columns[nameof(NguoiDungDTO.DangHoatDong)] != null) dgvData.Columns[nameof(NguoiDungDTO.DangHoatDong)].HeaderText = "Hoạt động";
    }
}
