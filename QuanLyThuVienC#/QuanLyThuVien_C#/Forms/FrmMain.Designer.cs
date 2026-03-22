namespace QuanLyThuVien.Forms;

partial class FrmMain
{
    private System.ComponentModel.IContainer components = null;
    private Panel panelSidebar;
    private Button btnBrand;
    private Label lblSidebarUser;
    private Label lblSidebarRole;
    private Button btnMenuHome;
    private Button btnMenuSach;
    private Button btnMenuDocGia;
    private Button btnMenuTheLoai;
    private Button btnMenuNxb;
    private Button btnMenuNguoiDung;
    private Button btnMenuMuonSach;
    private Button btnMenuTraSach;
    private Button btnMenuPhieuPhat;
    private Button btnMenuThongKe;
    private Button btnLogout;
    private Panel panelHeader;
    private Label lblTopTitle;
    private Label lblHeaderUser;
    private Panel pnlContent;
    private Panel pnlDashboard;
    private Label lblDashboardTitle;
    private Label lblDashboardWelcome;
    private FlowLayoutPanel flowDashboardCards;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelSidebar = new Panel();
        btnMenuThongKe = new Button();
        btnMenuPhieuPhat = new Button();
        btnMenuTraSach = new Button();
        btnMenuMuonSach = new Button();
        btnMenuNguoiDung = new Button();
        btnMenuNxb = new Button();
        btnMenuTheLoai = new Button();
        btnMenuDocGia = new Button();
        btnMenuSach = new Button();
        btnMenuHome = new Button();
        lblSidebarRole = new Label();
        lblSidebarUser = new Label();
        btnBrand = new Button();
        btnLogout = new Button();
        panelHeader = new Panel();
        lblHeaderUser = new Label();
        lblTopTitle = new Label();
        pnlContent = new Panel();
        pnlDashboard = new Panel();
        flowDashboardCards = new FlowLayoutPanel();
        lblDashboardWelcome = new Label();
        lblDashboardTitle = new Label();
        panelSidebar.SuspendLayout();
        panelHeader.SuspendLayout();
        pnlContent.SuspendLayout();
        pnlDashboard.SuspendLayout();
        SuspendLayout();
        // 
        // panelSidebar
        // 
        panelSidebar.BackColor = Color.FromArgb(20, 37, 91);
        panelSidebar.Controls.Add(btnMenuThongKe);
        panelSidebar.Controls.Add(btnMenuPhieuPhat);
        panelSidebar.Controls.Add(btnMenuTraSach);
        panelSidebar.Controls.Add(btnMenuMuonSach);
        panelSidebar.Controls.Add(btnMenuNguoiDung);
        panelSidebar.Controls.Add(btnMenuNxb);
        panelSidebar.Controls.Add(btnMenuTheLoai);
        panelSidebar.Controls.Add(btnMenuDocGia);
        panelSidebar.Controls.Add(btnMenuSach);
        panelSidebar.Controls.Add(btnMenuHome);
        panelSidebar.Controls.Add(lblSidebarRole);
        panelSidebar.Controls.Add(lblSidebarUser);
        panelSidebar.Controls.Add(btnBrand);
        panelSidebar.Controls.Add(btnLogout);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Location = new Point(0, 0);
        panelSidebar.Margin = new Padding(3, 4, 3, 4);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Size = new Size(257, 1015);
        panelSidebar.TabIndex = 0;
        // 
        // btnMenuThongKe
        // 
        btnMenuThongKe.Location = new Point(0, 752);
        btnMenuThongKe.Margin = new Padding(3, 4, 3, 4);
        btnMenuThongKe.Name = "btnMenuThongKe";
        btnMenuThongKe.Size = new Size(257, 61);
        btnMenuThongKe.TabIndex = 12;
        btnMenuThongKe.Text = "Thống kê";
        btnMenuThongKe.UseVisualStyleBackColor = true;
        // 
        // btnMenuPhieuPhat
        // 
        btnMenuPhieuPhat.Location = new Point(0, 691);
        btnMenuPhieuPhat.Margin = new Padding(3, 4, 3, 4);
        btnMenuPhieuPhat.Name = "btnMenuPhieuPhat";
        btnMenuPhieuPhat.Size = new Size(257, 61);
        btnMenuPhieuPhat.TabIndex = 11;
        btnMenuPhieuPhat.Text = "Phiếu phạt";
        btnMenuPhieuPhat.UseVisualStyleBackColor = true;
        // 
        // btnMenuTraSach
        // 
        btnMenuTraSach.Location = new Point(0, 629);
        btnMenuTraSach.Margin = new Padding(3, 4, 3, 4);
        btnMenuTraSach.Name = "btnMenuTraSach";
        btnMenuTraSach.Size = new Size(257, 61);
        btnMenuTraSach.TabIndex = 10;
        btnMenuTraSach.Text = "Phiếu trả";
        btnMenuTraSach.UseVisualStyleBackColor = true;
        // 
        // btnMenuMuonSach
        // 
        btnMenuMuonSach.Location = new Point(0, 568);
        btnMenuMuonSach.Margin = new Padding(3, 4, 3, 4);
        btnMenuMuonSach.Name = "btnMenuMuonSach";
        btnMenuMuonSach.Size = new Size(257, 61);
        btnMenuMuonSach.TabIndex = 9;
        btnMenuMuonSach.Text = "Phiếu mượn";
        btnMenuMuonSach.UseVisualStyleBackColor = true;
        // 
        // btnMenuNguoiDung
        // 
        btnMenuNguoiDung.Location = new Point(0, 507);
        btnMenuNguoiDung.Margin = new Padding(3, 4, 3, 4);
        btnMenuNguoiDung.Name = "btnMenuNguoiDung";
        btnMenuNguoiDung.Size = new Size(257, 61);
        btnMenuNguoiDung.TabIndex = 8;
        btnMenuNguoiDung.Text = "Quản lý tài khoản";
        btnMenuNguoiDung.UseVisualStyleBackColor = true;
        // 
        // btnMenuNxb
        // 
        btnMenuNxb.Location = new Point(0, 445);
        btnMenuNxb.Margin = new Padding(3, 4, 3, 4);
        btnMenuNxb.Name = "btnMenuNxb";
        btnMenuNxb.Size = new Size(257, 61);
        btnMenuNxb.TabIndex = 7;
        btnMenuNxb.Text = "Nhà xuất bản";
        btnMenuNxb.UseVisualStyleBackColor = true;
        // 
        // btnMenuTheLoai
        // 
        btnMenuTheLoai.Location = new Point(0, 384);
        btnMenuTheLoai.Margin = new Padding(3, 4, 3, 4);
        btnMenuTheLoai.Name = "btnMenuTheLoai";
        btnMenuTheLoai.Size = new Size(257, 61);
        btnMenuTheLoai.TabIndex = 6;
        btnMenuTheLoai.Text = "Thể loại sách";
        btnMenuTheLoai.UseVisualStyleBackColor = true;
        // 
        // btnMenuDocGia
        // 
        btnMenuDocGia.Location = new Point(0, 323);
        btnMenuDocGia.Margin = new Padding(3, 4, 3, 4);
        btnMenuDocGia.Name = "btnMenuDocGia";
        btnMenuDocGia.Size = new Size(257, 61);
        btnMenuDocGia.TabIndex = 5;
        btnMenuDocGia.Text = "Người mượn";
        btnMenuDocGia.UseVisualStyleBackColor = true;
        // 
        // btnMenuSach
        // 
        btnMenuSach.Location = new Point(0, 261);
        btnMenuSach.Margin = new Padding(3, 4, 3, 4);
        btnMenuSach.Name = "btnMenuSach";
        btnMenuSach.Size = new Size(257, 61);
        btnMenuSach.TabIndex = 4;
        btnMenuSach.Text = "Quản lý sách";
        btnMenuSach.UseVisualStyleBackColor = true;
        // 
        // btnMenuHome
        // 
        btnMenuHome.Location = new Point(0, 200);
        btnMenuHome.Margin = new Padding(3, 4, 3, 4);
        btnMenuHome.Name = "btnMenuHome";
        btnMenuHome.Size = new Size(257, 61);
        btnMenuHome.TabIndex = 3;
        btnMenuHome.Text = "Trang chủ";
        btnMenuHome.UseVisualStyleBackColor = true;
        // 
        // lblSidebarRole
        // 
        lblSidebarRole.Font = new Font("Segoe UI", 10F);
        lblSidebarRole.ForeColor = Color.White;
        lblSidebarRole.Location = new Point(18, 147);
        lblSidebarRole.Name = "lblSidebarRole";
        lblSidebarRole.Size = new Size(217, 32);
        lblSidebarRole.TabIndex = 2;
        lblSidebarRole.Text = "quan_tri";
        lblSidebarRole.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblSidebarUser
        // 
        lblSidebarUser.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
        lblSidebarUser.ForeColor = Color.White;
        lblSidebarUser.Location = new Point(18, 104);
        lblSidebarUser.Name = "lblSidebarUser";
        lblSidebarUser.Size = new Size(217, 51);
        lblSidebarUser.TabIndex = 1;
        lblSidebarUser.Text = "Quản Trị Hệ Thống";
        lblSidebarUser.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnBrand
        // 
        btnBrand.BackColor = Color.FromArgb(46, 101, 230);
        btnBrand.FlatStyle = FlatStyle.Flat;
        btnBrand.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        btnBrand.ForeColor = Color.White;
        btnBrand.Location = new Point(18, 27);
        btnBrand.Margin = new Padding(3, 4, 3, 4);
        btnBrand.Name = "btnBrand";
        btnBrand.Size = new Size(217, 59);
        btnBrand.TabIndex = 0;
        btnBrand.Text = "THƯ VIỆN";
        btnBrand.UseVisualStyleBackColor = false;
        // 
        // btnLogout
        // 
        btnLogout.BackColor = Color.FromArgb(20, 37, 91);
        btnLogout.Dock = DockStyle.Bottom;
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.ForeColor = Color.White;
        btnLogout.Location = new Point(0, 954);
        btnLogout.Margin = new Padding(3, 4, 3, 4);
        btnLogout.Name = "btnLogout";
        btnLogout.Padding = new Padding(27, 0, 0, 0);
        btnLogout.Size = new Size(257, 61);
        btnLogout.TabIndex = 13;
        btnLogout.Text = "Đăng xuất";
        btnLogout.TextAlign = ContentAlignment.MiddleLeft;
        btnLogout.UseVisualStyleBackColor = false;
        btnLogout.Click += btnLogout_Click;
        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.FromArgb(255, 255, 128);
        panelHeader.Controls.Add(lblHeaderUser);
        panelHeader.Controls.Add(lblTopTitle);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Location = new Point(257, 0);
        panelHeader.Margin = new Padding(3, 4, 3, 4);
        panelHeader.Name = "panelHeader";
        panelHeader.Size = new Size(1297, 109);
        panelHeader.TabIndex = 1;
        // 
        // lblHeaderUser
        // 
        lblHeaderUser.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblHeaderUser.AutoSize = true;
        lblHeaderUser.Font = new Font("Segoe UI Semibold", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 163);
        lblHeaderUser.ForeColor = Color.FromArgb(87, 96, 117);
        lblHeaderUser.Location = new Point(1058, 37);
        lblHeaderUser.Name = "lblHeaderUser";
        lblHeaderUser.Size = new Size(191, 25);
        lblHeaderUser.TabIndex = 1;
        lblHeaderUser.Text = "Người dùng: Quản trị";
        // 
        // lblTopTitle
        // 
        lblTopTitle.AutoSize = true;
        lblTopTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTopTitle.ForeColor = Color.FromArgb(23, 33, 56);
        lblTopTitle.Location = new Point(23, 29);
        lblTopTitle.Name = "lblTopTitle";
        lblTopTitle.Size = new Size(157, 41);
        lblTopTitle.TabIndex = 0;
        lblTopTitle.Text = "Trang chủ";
        // 
        // pnlContent
        // 
        pnlContent.BackColor = Color.FromArgb(241, 244, 248);
        pnlContent.Controls.Add(pnlDashboard);
        pnlContent.Dock = DockStyle.Fill;
        pnlContent.Location = new Point(257, 109);
        pnlContent.Margin = new Padding(3, 4, 3, 4);
        pnlContent.Name = "pnlContent";
        pnlContent.Size = new Size(1297, 906);
        pnlContent.TabIndex = 2;
        // 
        // pnlDashboard
        // 
        pnlDashboard.AutoScroll = true;
        pnlDashboard.Controls.Add(flowDashboardCards);
        pnlDashboard.Controls.Add(lblDashboardWelcome);
        pnlDashboard.Controls.Add(lblDashboardTitle);
        pnlDashboard.Dock = DockStyle.Fill;
        pnlDashboard.Location = new Point(0, 0);
        pnlDashboard.Margin = new Padding(3, 4, 3, 4);
        pnlDashboard.Name = "pnlDashboard";
        pnlDashboard.Padding = new Padding(27, 53, 27, 32);
        pnlDashboard.Size = new Size(1297, 906);
        pnlDashboard.TabIndex = 0;
        // 
        // flowDashboardCards
        // 
        flowDashboardCards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        flowDashboardCards.Location = new Point(27, 280);
        flowDashboardCards.Margin = new Padding(3, 4, 3, 4);
        flowDashboardCards.Name = "flowDashboardCards";
        flowDashboardCards.Size = new Size(1242, 573);
        flowDashboardCards.TabIndex = 2;
        // 
        // lblDashboardWelcome
        // 
        lblDashboardWelcome.Anchor = AnchorStyles.Top;
        lblDashboardWelcome.AutoSize = true;
        lblDashboardWelcome.Font = new Font("Segoe UI", 12F);
        lblDashboardWelcome.ForeColor = Color.FromArgb(87, 96, 117);
        lblDashboardWelcome.Location = new Point(478, 187);
        lblDashboardWelcome.Name = "lblDashboardWelcome";
        lblDashboardWelcome.Size = new Size(326, 28);
        lblDashboardWelcome.TabIndex = 1;
        lblDashboardWelcome.Text = "Xin chào Quản Trị Hệ Thống | Vai trò";
        // 
        // lblDashboardTitle
        // 
        lblDashboardTitle.Anchor = AnchorStyles.Top;
        lblDashboardTitle.AutoSize = true;
        lblDashboardTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
        lblDashboardTitle.ForeColor = Color.FromArgb(23, 33, 56);
        lblDashboardTitle.Location = new Point(361, 117);
        lblDashboardTitle.Name = "lblDashboardTitle";
        lblDashboardTitle.Size = new Size(613, 54);
        lblDashboardTitle.TabIndex = 0;
        lblDashboardTitle.Text = "HỆ THỐNG QUẢN LÝ THƯ VIỆN";
        // 
        // FrmMain
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1554, 1015);
        Controls.Add(pnlContent);
        Controls.Add(panelHeader);
        Controls.Add(panelSidebar);
        Margin = new Padding(3, 4, 3, 4);
        Name = "FrmMain";
        Text = "Hệ thống quản lý thư viện";
        panelSidebar.ResumeLayout(false);
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        pnlContent.ResumeLayout(false);
        pnlDashboard.ResumeLayout(false);
        pnlDashboard.PerformLayout();
        ResumeLayout(false);
    }
}
