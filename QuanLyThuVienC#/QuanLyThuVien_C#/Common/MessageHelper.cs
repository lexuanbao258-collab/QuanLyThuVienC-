namespace QuanLyThuVien.Common;

public static class MessageHelper
{
    public static void Info(string text) => MessageBox.Show(text, Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
    public static void Warning(string text) => MessageBox.Show(text, Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    public static void Error(string text) => MessageBox.Show(text, Constants.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
    public static bool Confirm(string text) => MessageBox.Show(text, Constants.AppTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
}
