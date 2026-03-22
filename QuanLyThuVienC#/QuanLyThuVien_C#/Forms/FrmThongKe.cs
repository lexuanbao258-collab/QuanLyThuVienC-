using System.ComponentModel;
using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

public partial class FrmThongKe : Form
{
    private readonly ThongKeBUS _bus = new();

    public FrmThongKe()
    {
        InitializeComponent();
        ConfigurePage();
        ConfigureGrid();
        btnRefresh.Click += (_, _) => AppGuard.Run(LoadData, "Tải thống kê");
        btnSearch.Enabled = false;
        btnAdd.Enabled = false;
        btnEdit.Enabled = false;
        btnDelete.Enabled = false;
        Load += (_, _) => AppGuard.Run(LoadData, "Tải thống kê");
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

    private void LoadData()
    {
        dgvData.DataSource = null;
        dgvData.DataSource = _bus.GetTongQuan();
        if (dgvData.Columns[nameof(ThongKeDTO.ChiSo)] != null) dgvData.Columns[nameof(ThongKeDTO.ChiSo)].HeaderText = "Chỉ số";
        if (dgvData.Columns[nameof(ThongKeDTO.GiaTri)] != null) dgvData.Columns[nameof(ThongKeDTO.GiaTri)].HeaderText = "Giá trị";
    }
}
