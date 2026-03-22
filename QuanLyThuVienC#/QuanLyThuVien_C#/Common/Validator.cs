namespace QuanLyThuVien.Common;

public static class Validator
{
    public static bool HasText(string? value) => !string.IsNullOrWhiteSpace(value);
}
