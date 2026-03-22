using QuanLyThuVien.Common;

namespace QuanLyThuVien.Forms;

public partial class FrmMain : Form
{
    private readonly List<Button> _menuButtons = new();

    public FrmMain()
    {
        InitializeComponent();
        BackColor = UiFactory.Background;
        WindowState = FormWindowState.Maximized;
        MinimumSize = new Size(1365, 768);
        ConfigureMenuButtons();
        BuildDashboardCards();
        Load += FrmMain_Load;
        Resize += (_, _) => UpdateHeaderUserPosition();
    }

    private void FrmMain_Load(object? sender, EventArgs e)
    {
        lblSidebarUser.Text = Session.CurrentUser?.HoTen ?? string.Empty;
        lblSidebarRole.Text = Session.CurrentUser?.VaiTro ?? string.Empty;
        lblHeaderUser.Text = $"Người dùng: {Session.CurrentUser?.HoTen}";
        btnMenuNguoiDung.Visible = Session.CurrentUser?.VaiTro == Constants.RoleAdmin;
        UpdateHeaderUserPosition();
        ShowDashboard();
    }

    private void ConfigureMenuButtons()
    {
        _menuButtons.AddRange(new[]
        {
            btnMenuHome, btnMenuSach, btnMenuDocGia, btnMenuTheLoai, btnMenuNxb,
            btnMenuNguoiDung, btnMenuMuonSach, btnMenuTraSach, btnMenuPhieuPhat, btnMenuThongKe
        });

        foreach (var btn in _menuButtons)
        {
            btn.FlatAppearance.BorderSize = 0;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(24, 0, 0, 0);
            btn.BackColor = UiFactory.Navy;
            btn.ForeColor = Color.White;
        }

        btnBrand.FlatAppearance.BorderSize = 0;
        btnLogout.FlatAppearance.BorderSize = 0;

        btnMenuHome.Click += (_, _) => ShowDashboard();
        btnMenuSach.Click += (_, _) => OpenPage(new FrmSach(), "Quản lý sách");
        btnMenuDocGia.Click += (_, _) => OpenPage(new FrmDocGia(), "Người mượn");
        btnMenuTheLoai.Click += (_, _) => OpenPage(new FrmTheLoai(), "Thể loại sách");
        btnMenuNxb.Click += (_, _) => OpenPage(new FrmNhaXuatBan(), "Nhà xuất bản");
        btnMenuNguoiDung.Click += (_, _) => OpenPage(new FrmNguoiDung(), "Quản lý tài khoản");
        btnMenuMuonSach.Click += (_, _) => OpenPage(new FrmMuonSach(), "Phiếu mượn");
        btnMenuTraSach.Click += (_, _) => OpenPage(new FrmTraSach(), "Phiếu trả");
        btnMenuPhieuPhat.Click += (_, _) => OpenPage(new FrmPhieuPhat(), "Phiếu phạt");
        btnMenuThongKe.Click += (_, _) => OpenPage(new FrmThongKe(), "Thống kê");
        btnLogout.Click += (_, _) => Close();
    }

    private void BuildDashboardCards()
    {
        flowDashboardCards.Controls.Clear();
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Quản lý sách"));
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Người mượn"));
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Thể loại sách"));
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Nhà xuất bản"));
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Quản lý tài khoản"));
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Phiếu mượn"));
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Phiếu trả"));
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Phiếu phạt"));
        flowDashboardCards.Controls.Add(UiFactory.CreateCard("Thống kê"));
    }

    private void OpenPage(Form form, string title)
    {
        lblTopTitle.Text = title;

        foreach (Control control in pnlContent.Controls.OfType<Form>().ToList())
        {
            control.Dispose();
        }

        pnlDashboard.Visible = false;

        form.TopLevel = false;
        form.FormBorderStyle = FormBorderStyle.None;
        form.Dock = DockStyle.Fill;
        pnlContent.Controls.Add(form);
        form.BringToFront();
        form.Show();
    }

    private void ShowDashboard()
    {
        lblTopTitle.Text = "Trang chủ";

        foreach (Control control in pnlContent.Controls.OfType<Form>().ToList())
        {
            control.Dispose();
        }

        if (!pnlContent.Controls.Contains(pnlDashboard))
        {
            pnlContent.Controls.Add(pnlDashboard);
        }

        pnlDashboard.Visible = true;
        pnlDashboard.Dock = DockStyle.Fill;
        pnlDashboard.BringToFront();
        lblDashboardWelcome.Text = $"Xin chào {Session.CurrentUser?.HoTen} | Vai trò: {Session.CurrentUser?.VaiTro}";
    }

    private void UpdateHeaderUserPosition()
    {
        lblHeaderUser.Left = Math.Max(panelHeader.Width - lblHeaderUser.Width - 24, 850);
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show(
         "Bạn có chắc chắn muốn đăng xuất?",
         "Xác nhận",
         MessageBoxButtons.YesNo,
         MessageBoxIcon.Question);
        FrmLogin frmLogin = new FrmLogin();
        if (result == DialogResult.Yes)
        {
            Session.CurrentUser = null;
            Hide();
        }
        frmLogin.ShowDialog();

       

    }
}
