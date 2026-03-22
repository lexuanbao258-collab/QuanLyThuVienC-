using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;

namespace QuanLyThuVien.Forms;

public partial class FrmLogin : Form
{
    private readonly AuthBUS _authBUS = new();

    public FrmLogin()
    {
        InitializeComponent();
        BackColor = UiFactory.Background;
        txtUser.Text = string.Empty;
        txtPass.Text = string.Empty;
        Shown += (_, _) => txtUser.Focus();
        btnLogin.Click += BtnLogin_Click;
        Load += (_, _) => CenterCard();
        Resize += (_, _) => CenterCard();
    }

    private void CenterCard()
    {
        pnlCard.Left = (ClientSize.Width - pnlCard.Width) / 2;
        pnlCard.Top = (ClientSize.Height - pnlCard.Height) / 2;
    }

    private void BtnLogin_Click(object? sender, EventArgs e)
    {
        try
        {
            var user = _authBUS.Login(txtUser.Text.Trim(), txtPass.Text.Trim());
            if (user is null)
            {
                MessageHelper.Error("Sai tên đăng nhập hoặc mật khẩu, hoặc tài khoản đã bị khóa.");
                return;
            }

            Session.CurrentUser = user;
            Hide();
            using var frmMain = new FrmMain();
            frmMain.ShowDialog();
            Close();
        }
        catch (Exception ex)
        {
            MessageHelper.Error(ex.Message);
        }
    }

    private void btnLogin_Click_1(object sender, EventArgs e)
    {
      
    }
}
