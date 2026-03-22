namespace QuanLyThuVien.Common;

public static class AppGuard
{
    public static void Run(Action action, string actionName)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            MessageHelper.Error($"{actionName} thất bại.\n\n{ex.Message}");
        }
    }

    public static T Run<T>(Func<T> func, string actionName)
    {
        try
        {
            return func();
        }
        catch (Exception ex)
        {
            MessageHelper.Error($"{actionName} thất bại.\n\n{ex.Message}");
            return default!;
        }
    }
}
