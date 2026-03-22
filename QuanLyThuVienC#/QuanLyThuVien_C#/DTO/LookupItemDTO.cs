namespace QuanLyThuVien.DTO;

public class LookupItemDTO
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public override string ToString() => string.IsNullOrWhiteSpace(Code) ? Name : $"{Code} - {Name}";
}
