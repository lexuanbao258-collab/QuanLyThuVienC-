using System.ComponentModel;
using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmNhaXuatBan : Form
{
    private readonly NhaXuatBanBUS _bus = new();

    public FrmNhaXuatBan()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải nhà xuất bản");
        btnSearch.Click += (_, _) => AppGuard.Run(SearchData, "Tìm nhà xuất bản");
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải nhà xuất bản");
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

    private NhaXuatBanDTO? CurrentItem => dgvData.CurrentRow?.DataBoundItem as NhaXuatBanDTO;

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dialog = new TextEditDialog("Thêm nhà xuất bản", "Tên nhà xuất bản");
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(new NhaXuatBanDTO { TenNhaXuatBan = dialog.ValueText });
            LoadData();
            MessageHelper.Info("Đã thêm nhà xuất bản.");
        }, "Thêm nhà xuất bản");
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn nhà xuất bản cần sửa.");
            return;
        }

        using var dialog = new TextEditDialog("Sửa nhà xuất bản", "Tên nhà xuất bản", CurrentItem.TenNhaXuatBan);
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(new NhaXuatBanDTO { Id = CurrentItem.Id, TenNhaXuatBan = dialog.ValueText });
            LoadData();
            MessageHelper.Info("Đã cập nhật nhà xuất bản.");
        }, "Sửa nhà xuất bản");
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn nhà xuất bản cần xóa.");
            return;
        }

        if (!MessageHelper.Confirm($"Xóa nhà xuất bản {CurrentItem.TenNhaXuatBan}?")) return;
        AppGuard.Run(() =>
        {
            _bus.Delete(CurrentItem.Id);
            LoadData();
            MessageHelper.Info("Đã xóa nhà xuất bản.");
        }, "Xóa nhà xuất bản");
    }

    private void LoadData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _bus.GetAll();
        if (dgvData.Columns[nameof(NhaXuatBanDTO.Id)] != null) dgvData.Columns[nameof(NhaXuatBanDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(NhaXuatBanDTO.TenNhaXuatBan)] != null) dgvData.Columns[nameof(NhaXuatBanDTO.TenNhaXuatBan)].HeaderText = "Tên nhà xuất bản";
    }

    private void SearchData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _bus.Search(txtSearch.Text);
        if (dgvData.Columns[nameof(NhaXuatBanDTO.Id)] != null) dgvData.Columns[nameof(NhaXuatBanDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(NhaXuatBanDTO.TenNhaXuatBan)] != null) dgvData.Columns[nameof(NhaXuatBanDTO.TenNhaXuatBan)].HeaderText = "Tên nhà xuất bản";
    }
}
