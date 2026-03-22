namespace QuanLyThuVien.Forms;

partial class FrmLogin
{
    private System.ComponentModel.IContainer components = null;
    private Panel pnlCard;
    private Label lblLoginTitle;
    private Label lblUser;
    private Label lblPass;
    private TextBox txtUser;
    private TextBox txtPass;
    private Button btnLogin;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null)) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        pnlCard = new Panel();
        btnLogin = new Button();
        txtPass = new TextBox();
        lblPass = new Label();
        txtUser = new TextBox();
        lblUser = new Label();
        lblLoginTitle = new Label();
        pnlCard.SuspendLayout();
        SuspendLayout();
        // 
        // pnlCard
        // 
        pnlCard.BackColor = Color.White;
        pnlCard.Controls.Add(btnLogin);
        pnlCard.Controls.Add(txtPass);
        pnlCard.Controls.Add(lblPass);
        pnlCard.Controls.Add(txtUser);
        pnlCard.Controls.Add(lblUser);
        pnlCard.Controls.Add(lblLoginTitle);
        pnlCard.Location = new Point(50, 43);
        pnlCard.Margin = new Padding(3, 4, 3, 4);
        pnlCard.Name = "pnlCard";
        pnlCard.Size = new Size(411, 307);
        pnlCard.TabIndex = 0;
        // 
        // btnLogin
        // 
        btnLogin.BackColor = Color.FromArgb(46, 101, 230);
        btnLogin.FlatAppearance.BorderSize = 0;
        btnLogin.FlatStyle = FlatStyle.Flat;
        btnLogin.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnLogin.ForeColor = Color.White;
        btnLogin.Location = new Point(32, 248);
        btnLogin.Margin = new Padding(3, 4, 3, 4);
        btnLogin.Name = "btnLogin";
        btnLogin.Size = new Size(347, 43);
        btnLogin.TabIndex = 5;
        btnLogin.Text = "Đăng nhập";
        btnLogin.UseVisualStyleBackColor = false;
        btnLogin.Click += btnLogin_Click_1;
        // 
        // txtPass
        // 
        txtPass.Location = new Point(32, 197);
        txtPass.Margin = new Padding(3, 4, 3, 4);
        txtPass.Name = "txtPass";
        txtPass.PlaceholderText = "Mật khẩu";
        txtPass.Size = new Size(347, 27);
        txtPass.TabIndex = 4;
        txtPass.UseSystemPasswordChar = true;
        // 
        // lblPass
        // 
        lblPass.AutoSize = true;
        lblPass.BackColor = Color.FromArgb(192, 255, 255);
        lblPass.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblPass.Location = new Point(32, 164);
        lblPass.Name = "lblPass";
        lblPass.Size = new Size(86, 23);
        lblPass.TabIndex = 3;
        lblPass.Text = "Mật khẩu";
        // 
        // txtUser
        // 
        txtUser.Location = new Point(32, 117);
        txtUser.Margin = new Padding(3, 4, 3, 4);
        txtUser.Name = "txtUser";
        txtUser.PlaceholderText = "Tên đăng nhập";
        txtUser.Size = new Size(347, 27);
        txtUser.TabIndex = 2;
        // 
        // lblUser
        // 
        lblUser.AutoSize = true;
        lblUser.BackColor = Color.FromArgb(192, 255, 255);
        lblUser.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        lblUser.Location = new Point(32, 84);
        lblUser.Name = "lblUser";
        lblUser.Size = new Size(128, 23);
        lblUser.TabIndex = 1;
        lblUser.Text = "Tên đăng nhập";
        // 
        // lblLoginTitle
        // 
        lblLoginTitle.BackColor = Color.Blue;
        lblLoginTitle.Dock = DockStyle.Top;
        lblLoginTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblLoginTitle.Location = new Point(0, 0);
        lblLoginTitle.Name = "lblLoginTitle";
        lblLoginTitle.Size = new Size(411, 64);
        lblLoginTitle.TabIndex = 0;
        lblLoginTitle.Text = "ĐĂNG NHẬP HỆ THỐNG";
        lblLoginTitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // FrmLogin
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(512, 392);
        Controls.Add(pnlCard);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Margin = new Padding(3, 4, 3, 4);
        MaximizeBox = false;
        Name = "FrmLogin";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Hệ thống quản lý thư viện";
        pnlCard.ResumeLayout(false);
        pnlCard.PerformLayout();
        ResumeLayout(false);
    }
}
