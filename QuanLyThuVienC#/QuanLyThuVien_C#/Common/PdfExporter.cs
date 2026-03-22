using System.Globalization;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Common;

public static class PdfExporter
{
    public static void ExportBorrowSlip(string filePath, BorrowSlipPdfDTO data)
    {
        var doc = CreateDocument($"Phiếu mượn {data.MaPhieuMuon}");
        var ctx = new PdfRenderContext(doc);
        WriteTitle(ctx, "PHIẾU MƯỢN SÁCH");
        WriteKeyValue(ctx, "Mã phiếu mượn", data.MaPhieuMuon);
        WriteKeyValue(ctx, "Mã người mượn", data.MaNguoiMuon);
        WriteKeyValue(ctx, "Họ tên người mượn", data.HoTenNguoiMuon);
        WriteKeyValue(ctx, "Ngày mượn", data.NgayMuon.ToString("dd/MM/yyyy"));
        WriteKeyValue(ctx, "Ngày hẹn trả", data.NgayHenTra.ToString("dd/MM/yyyy"));
        WriteKeyValue(ctx, "Trạng thái", FormatBorrowStatus(data.TrangThai));
        WriteKeyValue(ctx, "Người lập", string.IsNullOrWhiteSpace(data.NguoiLap) ? "-" : data.NguoiLap);
        WriteSection(ctx, "Danh sách sách mượn");
        for (var i = 0; i < data.Items.Count; i++)
        {
            var item = data.Items[i];
            WriteBullet(ctx, $"{i + 1}. {item.MaSach} - {item.TenSach} | Số lượng: {item.SoLuong}");
        }
        Save(doc, filePath);
    }

    public static void ExportReturnSlip(string filePath, ReturnSlipPdfDTO data)
    {
        var doc = CreateDocument($"Phiếu trả {data.MaPhieuTra}");
        var ctx = new PdfRenderContext(doc);
        WriteTitle(ctx, "PHIẾU TRẢ SÁCH");
        WriteKeyValue(ctx, "Mã phiếu trả", data.MaPhieuTra);
        WriteKeyValue(ctx, "Mã phiếu mượn", data.MaPhieuMuon);
        WriteKeyValue(ctx, "Mã người mượn", data.MaNguoiMuon);
        WriteKeyValue(ctx, "Họ tên người mượn", data.HoTenNguoiMuon);
        WriteKeyValue(ctx, "Ngày trả", data.NgayTra.ToString("dd/MM/yyyy"));
        WriteKeyValue(ctx, "Số ngày trễ", data.SoNgayTre.ToString());
        WriteKeyValue(ctx, "Người xử lý", string.IsNullOrWhiteSpace(data.NguoiXuLy) ? "-" : data.NguoiXuLy);
        WriteKeyValue(ctx, "Ghi chú", string.IsNullOrWhiteSpace(data.GhiChu) ? "-" : data.GhiChu);
        WriteSection(ctx, "Chi tiết trả sách");
        for (var i = 0; i < data.Items.Count; i++)
        {
            var item = data.Items[i];
            WriteBullet(ctx,
                $"{i + 1}. {item.MaSach} - {item.TenSach} | Mượn: {item.SoLuongMuon} | Trả: {item.SoLuongTra} | Hỏng: {item.SoLuongHong} | Mất: {item.SoLuongMat}");
            if (!string.IsNullOrWhiteSpace(item.GhiChuTinhTrang))
            {
                WriteSubLine(ctx, $"   Ghi chú: {item.GhiChuTinhTrang}");
            }
        }
        Save(doc, filePath);
    }

    public static void ExportFineSlip(string filePath, FineSlipPdfDTO data)
    {
        var doc = CreateDocument($"Phiếu phạt {data.MaPhieuPhat}");
        var ctx = new PdfRenderContext(doc);
        WriteTitle(ctx, "PHIẾU PHẠT");
        WriteKeyValue(ctx, "Mã phiếu phạt", data.MaPhieuPhat);
        WriteKeyValue(ctx, "Mã phiếu mượn", data.MaPhieuMuon);
        WriteKeyValue(ctx, "Mã phiếu trả", string.IsNullOrWhiteSpace(data.MaPhieuTra) ? "-" : data.MaPhieuTra);
        WriteKeyValue(ctx, "Mã người mượn", data.MaNguoiMuon);
        WriteKeyValue(ctx, "Họ tên người mượn", data.HoTenNguoiMuon);
        WriteKeyValue(ctx, "Ngày tạo", data.NgayTao.ToString("dd/MM/yyyy HH:mm"));
        WriteKeyValue(ctx, "Người lập", string.IsNullOrWhiteSpace(data.NguoiLap) ? "-" : data.NguoiLap);
        WriteKeyValue(ctx, "Trạng thái thanh toán", FormatPaymentStatus(data.TrangThaiThanhToan));
        WriteKeyValue(ctx, "Tổng tiền", FormatCurrency(data.TongTien));
        WriteSection(ctx, "Chi tiết tiền phạt");
        for (var i = 0; i < data.Items.Count; i++)
        {
            var item = data.Items[i];
            WriteBullet(ctx,
                $"{i + 1}. {item.MaSach} - {item.TenSach} | Lý do: {FormatReason(item.LyDo)} | SL: {item.SoLuong} | Đơn giá: {FormatCurrency(item.DonGiaPhat)} | Thành tiền: {FormatCurrency(item.ThanhTien)}");
            if (!string.IsNullOrWhiteSpace(item.GhiChu))
            {
                WriteSubLine(ctx, $"   Ghi chú: {item.GhiChu}");
            }
        }
        Save(doc, filePath);
    }

    private static PdfDocument CreateDocument(string title)
    {
        var document = new PdfDocument();
        document.Info.Title = title;
        document.Info.Author = "QuanLyThuVien";
        return document;
    }

    private static void Save(PdfDocument document, string filePath)
    {
        var dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrWhiteSpace(dir)) Directory.CreateDirectory(dir);
        document.Save(filePath);
    }

    private static void WriteTitle(PdfRenderContext ctx, string text)
    {
        ctx.EnsureSpace(36);
        ctx.Gfx.DrawString(text, ctx.TitleFont, XBrushes.DarkBlue,
            new XRect(40, ctx.Y, ctx.Page.Width.Point - 80, 24), XStringFormats.TopCenter);
        ctx.Y += 36;
    }

    private static void WriteSection(PdfRenderContext ctx, string text)
    {
        ctx.EnsureSpace(28);
        ctx.Gfx.DrawString(text, ctx.SectionFont, XBrushes.Black,
            new XRect(40, ctx.Y, ctx.Page.Width.Point - 80, 20), XStringFormats.TopLeft);
        ctx.Y += 24;
    }

    private static void WriteKeyValue(PdfRenderContext ctx, string label, string value)
    {
        var line = $"{label}: {value}";
        WriteParagraph(ctx, line, ctx.NormalFont);
    }

    private static void WriteBullet(PdfRenderContext ctx, string text)
    {
        WriteParagraph(ctx, text, ctx.NormalFont);
    }

    private static void WriteSubLine(PdfRenderContext ctx, string text)
    {
        WriteParagraph(ctx, text, ctx.SmallFont);
    }

    private static void WriteParagraph(PdfRenderContext ctx, string text, XFont font)
    {
        var height = EstimateHeight(text);
        ctx.EnsureSpace(height);
        var rect = new XRect(40, ctx.Y, ctx.Page.Width.Point - 80, height);
        ctx.Gfx.DrawString(text, font, XBrushes.Black, rect, XStringFormats.TopLeft);
        ctx.Y += height;
    }

    private static double EstimateHeight(string text)
    {
        var lines = Math.Max(1, (int)Math.Ceiling(text.Length / 85d));
        return 18 * lines + 4;
    }

    private static string FormatBorrowStatus(string status) => status switch
    {
        "dang_muon" => "Đang mượn",
        "da_tra" => "Đã trả",
        "qua_han" => "Quá hạn",
        _ => status
    };

    private static string FormatPaymentStatus(string status) => status switch
    {
        "chua_thanh_toan" => "Chưa thanh toán",
        "da_thanh_toan" => "Đã thanh toán",
        _ => status
    };

    private static string FormatReason(string reason) => reason switch
    {
        "tra_tre" => "Trả trễ",
        "lam_hong" => "Làm hỏng",
        "lam_mat" => "Làm mất",
        _ => reason
    };

    private static string FormatCurrency(decimal amount)
        => string.Format(CultureInfo.GetCultureInfo("vi-VN"), "{0:N0} đ", amount);

    private sealed class PdfRenderContext
    {
        public PdfDocument Document { get; }
        public PdfPage Page { get; private set; }
        public XGraphics Gfx { get; private set; }
        public double Y { get; set; }
        public XFont TitleFont { get; } = new("Arial", 18, XFontStyleEx.Bold);
        public XFont SectionFont { get; } = new("Arial", 12, XFontStyleEx.Bold);
        public XFont NormalFont { get; } = new("Arial", 11, XFontStyleEx.Regular);
        public XFont SmallFont { get; } = new("Arial", 10, XFontStyleEx.Regular);

        public PdfRenderContext(PdfDocument document)
        {
            Document = document;
            Page = default!;
            Gfx = default!;
            AddPage();
        }

        public void EnsureSpace(double neededHeight)
        {
            if (Y + neededHeight > Page.Height.Point - 40)
            {
                AddPage();
            }
        }

        private void AddPage()
        {
            Page = Document.AddPage();
            Page.Size = PdfSharp.PageSize.A4;
            Gfx = XGraphics.FromPdfPage(Page);
            Y = 40;
        }
    }
}
