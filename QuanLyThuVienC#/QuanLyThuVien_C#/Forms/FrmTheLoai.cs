using System.ComponentModel;
using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmTheLoai : Form
{
    private readonly TheLoaiBUS _bus = new();

    public FrmTheLoai()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải thể loại");
        btnSearch.Click += (_, _) => AppGuard.Run(SearchData, "Tìm thể loại");
        btnAdd.Click += BtnAdd_Click;
        btnEdit.Click += BtnEdit_Click;
        btnDelete.Click += BtnDelete_Click;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải thể loại");
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

    private TheLoaiDTO? CurrentItem => dgvData.CurrentRow?.DataBoundItem as TheLoaiDTO;

    private void BtnAdd_Click(object? sender, EventArgs e)
    {
        using var dialog = new TextEditDialog("Thêm thể loại", "Tên thể loại");
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(new TheLoaiDTO { TenTheLoai = dialog.ValueText });
            LoadData();
            MessageHelper.Info("Đã thêm thể loại.");
        }, "Thêm thể loại");
    }

    private void BtnEdit_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn thể loại cần sửa.");
            return;
        }

        using var dialog = new TextEditDialog("Sửa thể loại", "Tên thể loại", CurrentItem.TenTheLoai);
        if (dialog.ShowDialog(this) != DialogResult.OK) return;
        AppGuard.Run(() =>
        {
            _bus.Save(new TheLoaiDTO { Id = CurrentItem.Id, TenTheLoai = dialog.ValueText });
            LoadData();
            MessageHelper.Info("Đã cập nhật thể loại.");
        }, "Sửa thể loại");
    }

    private void BtnDelete_Click(object? sender, EventArgs e)
    {
        if (CurrentItem is null)
        {
            MessageHelper.Warning("Hãy chọn thể loại cần xóa.");
            return;
        }

        if (!MessageHelper.Confirm($"Xóa thể loại {CurrentItem.TenTheLoai}?")) return;
        AppGuard.Run(() =>
        {
            _bus.Delete(CurrentItem.Id);
            LoadData();
            MessageHelper.Info("Đã xóa thể loại.");
        }, "Xóa thể loại");
    }

    private void LoadData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _bus.GetAll();
        if (dgvData.Columns[nameof(TheLoaiDTO.Id)] != null) dgvData.Columns[nameof(TheLoaiDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(TheLoaiDTO.TenTheLoai)] != null) dgvData.Columns[nameof(TheLoaiDTO.TenTheLoai)].HeaderText = "Tên thể loại";
    }

    private void SearchData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _bus.Search(txtSearch.Text);
        if (dgvData.Columns[nameof(TheLoaiDTO.Id)] != null) dgvData.Columns[nameof(TheLoaiDTO.Id)].Visible = false;
        if (dgvData.Columns[nameof(TheLoaiDTO.TenTheLoai)] != null) dgvData.Columns[nameof(TheLoaiDTO.TenTheLoai)].HeaderText = "Tên thể loại";
    }
}
