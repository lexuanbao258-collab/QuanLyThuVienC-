using QuanLyThuVien.BUS;
using QuanLyThuVien.Common;
using QuanLyThuVien.DTO;

namespace QuanLyThuVien.Forms;

internal sealed class TextEditDialog : Form
{
    private readonly TextBox _txtValue = new() { Dock = DockStyle.Top, Height = 34 };
    public string ValueText => _txtValue.Text.Trim();

    public TextEditDialog(string title, string label, string value = "")
    {
        Text = title;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Width = 420;
        Height = 170;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), RowCount = 3, ColumnCount = 1 };
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        panel.Controls.Add(new Label { Text = label, AutoSize = true }, 0, 0);
        _txtValue.Text = value;
        panel.Controls.Add(_txtValue, 0, 1);

        var flow = new FlowLayoutPanel { Dock = DockStyle.Bottom, FlowDirection = FlowDirection.RightToLeft, Height = 42 };
        var btnOk = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Width = 90 };
        var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Width = 90 };
        flow.Controls.Add(btnOk);
        flow.Controls.Add(btnCancel);
        panel.Controls.Add(flow, 0, 2);

        Controls.Add(panel);
        AcceptButton = btnOk;
        CancelButton = btnCancel;
    }
}

internal sealed class NguoiMuonDialog : Form
{
    private readonly TextBox txtMa = new();
    private readonly TextBox txtHoTen = new();
    private readonly TextBox txtSdt = new();
    private readonly TextBox txtEmail = new();
    private readonly TextBox txtDiaChi = new();

    public NguoiMuonDialog(DocGiaDTO? item = null)
    {
        Text = item is null ? "Thêm người mượn" : "Sửa người mượn";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Width = 460;
        Height = 330;

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 2, RowCount = 6 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        AddRow(panel, 0, "Mã người mượn", txtMa);
        AddRow(panel, 1, "Họ tên", txtHoTen);
        AddRow(panel, 2, "Số điện thoại", txtSdt);
        AddRow(panel, 3, "Email", txtEmail);
        AddRow(panel, 4, "Địa chỉ", txtDiaChi);

        var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
        var btnOk = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Width = 90 };
        var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Width = 90 };
        flow.Controls.Add(btnOk);
        flow.Controls.Add(btnCancel);
        panel.Controls.Add(flow, 0, 5);
        panel.SetColumnSpan(flow, 2);

        Controls.Add(panel);
        AcceptButton = btnOk;
        CancelButton = btnCancel;

        if (item is not null)
        {
            txtMa.Text = item.MaNguoiMuon;
            txtHoTen.Text = item.HoTen;
            txtSdt.Text = item.SoDienThoai;
            txtEmail.Text = item.Email;
            txtDiaChi.Text = item.DiaChi;
            Tag = item.Id;
        }
    }

    public DocGiaDTO GetValue() => new()
    {
        Id = Tag is int id ? id : 0,
        MaNguoiMuon = txtMa.Text.Trim(),
        HoTen = txtHoTen.Text.Trim(),
        SoDienThoai = txtSdt.Text.Trim(),
        Email = txtEmail.Text.Trim(),
        DiaChi = txtDiaChi.Text.Trim()
    };

    private static void AddRow(TableLayoutPanel panel, int row, string label, Control control)
    {
        panel.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
        control.Dock = DockStyle.Fill;
        panel.Controls.Add(control, 1, row);
    }
}

internal sealed class NguoiDungDialog : Form
{
    private readonly TextBox txtUsername = new();
    private readonly TextBox txtPassword = new();
    private readonly TextBox txtHoTen = new();
    private readonly ComboBox cboVaiTro = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly CheckBox chkActive = new() { Text = "Đang hoạt động", Checked = true, AutoSize = true };

    public NguoiDungDialog(NguoiDungDTO? item = null)
    {
        Text = item is null ? "Thêm tài khoản" : "Sửa tài khoản";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Width = 460;
        Height = 290;

        cboVaiTro.Items.AddRange(new object[] { Constants.RoleAdmin, Constants.RoleStaff });

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 2, RowCount = 6 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        txtPassword.UseSystemPasswordChar = true;

        AddRow(panel, 0, "Tên đăng nhập", txtUsername);
        AddRow(panel, 1, "Mật khẩu", txtPassword);
        AddRow(panel, 2, "Họ tên", txtHoTen);
        AddRow(panel, 3, "Vai trò", cboVaiTro);
        panel.Controls.Add(chkActive, 1, 4);

        var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
        var btnOk = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Width = 90 };
        var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Width = 90 };
        flow.Controls.Add(btnOk);
        flow.Controls.Add(btnCancel);
        panel.Controls.Add(flow, 0, 5);
        panel.SetColumnSpan(flow, 2);

        Controls.Add(panel);
        AcceptButton = btnOk;
        CancelButton = btnCancel;

        if (item is not null)
        {
            Tag = item.Id;
            txtUsername.Text = item.TenDangNhap;
            txtPassword.Text = item.MatKhauMaHoa;
            txtHoTen.Text = item.HoTen;
            cboVaiTro.SelectedItem = item.VaiTro;
            chkActive.Checked = item.DangHoatDong;
        }
        else
        {
            cboVaiTro.SelectedIndex = 0;
        }
    }

    public NguoiDungDTO GetValue() => new()
    {
        Id = Tag is int id ? id : 0,
        TenDangNhap = txtUsername.Text.Trim(),
        MatKhauMaHoa = txtPassword.Text.Trim(),
        HoTen = txtHoTen.Text.Trim(),
        VaiTro = cboVaiTro.SelectedItem?.ToString() ?? Constants.RoleStaff,
        DangHoatDong = chkActive.Checked
    };

    private static void AddRow(TableLayoutPanel panel, int row, string label, Control control)
    {
        panel.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
        control.Dock = DockStyle.Fill;
        panel.Controls.Add(control, 1, row);
    }
}

internal sealed class SachDialog : Form
{
    private readonly TextBox txtMa = new();
    private readonly TextBox txtTen = new();
    private readonly TextBox txtTacGia = new();
    private readonly ComboBox cboTheLoai = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly ComboBox cboNxb = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly NumericUpDown nudNam = new() { Minimum = 0, Maximum = 9999 };
    private readonly TextBox txtViTri = new();
    private readonly NumericUpDown nudTong = new() { Minimum = 0, Maximum = 100000 };
    private readonly NumericUpDown nudCon = new() { Minimum = 0, Maximum = 100000 };
    private readonly CheckBox chkActive = new() { Text = "Đang hoạt động", Checked = true, AutoSize = true };
    private readonly TheLoaiBUS _theLoaiBus = new();
    private readonly NhaXuatBanBUS _nxbBus = new();

    public SachDialog(SachHienThiDTO? item = null)
    {
        Text = item is null ? "Thêm sách" : "Sửa sách";
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Width = 520;
        Height = 460;

        LoadLookups();

        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 2, RowCount = 11 };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        AddRow(panel, 0, "Mã sách", txtMa);
        AddRow(panel, 1, "Tên sách", txtTen);
        AddRow(panel, 2, "Tác giả", txtTacGia);
        AddRow(panel, 3, "Thể loại", cboTheLoai);
        AddRow(panel, 4, "Nhà xuất bản", cboNxb);
        AddRow(panel, 5, "Năm XB", nudNam);
        AddRow(panel, 6, "Vị trí kệ", txtViTri);
        AddRow(panel, 7, "Tổng số lượng", nudTong);
        AddRow(panel, 8, "Số lượng còn", nudCon);
        panel.Controls.Add(chkActive, 1, 9);

        var flow = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.RightToLeft };
        var btnOk = new Button { Text = "Lưu", DialogResult = DialogResult.OK, Width = 90 };
        var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Width = 90 };
        flow.Controls.Add(btnOk);
        flow.Controls.Add(btnCancel);
        panel.Controls.Add(flow, 0, 10);
        panel.SetColumnSpan(flow, 2);

        Controls.Add(panel);
        AcceptButton = btnOk;
        CancelButton = btnCancel;

        if (item is not null)
        {
            Tag = item;
            txtMa.Text = item.MaSach;
            txtTen.Text = item.TenSach;
            txtTacGia.Text = item.TacGia;
            txtViTri.Text = item.ViTriKe;
            nudNam.Value = item.NamXB ?? 0;
            nudTong.Value = item.TongSoLuong;
            nudCon.Value = item.SoLuongCon;
            chkActive.Checked = item.DangHoatDong;
            SelectByText(cboTheLoai, item.TheLoai);
            SelectByText(cboNxb, item.NhaXuatBan);
        }
    }

    private void LoadLookups()
    {
        cboTheLoai.Items.Clear();
        cboNxb.Items.Clear();
        cboTheLoai.Items.Add(new LookupItemDTO { Id = 0, Name = "-- Chọn thể loại --" });
        cboNxb.Items.Add(new LookupItemDTO { Id = 0, Name = "-- Chọn nhà xuất bản --" });
        foreach (var item in _theLoaiBus.GetLookup()) cboTheLoai.Items.Add(item);
        foreach (var item in _nxbBus.GetLookup()) cboNxb.Items.Add(item);
        cboTheLoai.SelectedIndex = 0;
        cboNxb.SelectedIndex = 0;
    }

    public SachDTO GetValue()
    {
        var old = Tag as SachHienThiDTO;
        var tl = cboTheLoai.SelectedItem as LookupItemDTO;
        var nxb = cboNxb.SelectedItem as LookupItemDTO;
        return new SachDTO
        {
            Id = old?.Id ?? 0,
            MaSach = txtMa.Text.Trim(),
            TenSach = txtTen.Text.Trim(),
            TacGia = txtTacGia.Text.Trim(),
            TheLoaiId = tl is not null && tl.Id > 0 ? tl.Id : null,
            NhaXuatBanId = nxb is not null && nxb.Id > 0 ? nxb.Id : null,
            NamXB = nudNam.Value > 0 ? (int)nudNam.Value : null,
            ViTriKe = txtViTri.Text.Trim(),
            TongSoLuong = (int)nudTong.Value,
            SoLuongCon = (int)nudCon.Value,
            DangHoatDong = chkActive.Checked
        };
    }

    private static void AddRow(TableLayoutPanel panel, int row, string label, Control control)
    {
        panel.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
        control.Dock = DockStyle.Fill;
        panel.Controls.Add(control, 1, row);
    }

    private static void SelectByText(ComboBox comboBox, string text)
    {
        for (var i = 0; i < comboBox.Items.Count; i++)
        {
            if (comboBox.Items[i] is LookupItemDTO item && item.Name.Equals(text, StringComparison.OrdinalIgnoreCase))
            {
                comboBox.SelectedIndex = i;
                return;
            }
        }
    }
}


internal sealed class BorrowDialog : Form
{
    private readonly PhieuMuonBUS _bus = new();
    private readonly DocGiaBUS _docGiaBus = new();
    private readonly SachBUS _sachBus = new();

    private readonly TextBox txtMaPhieu = new();
    private readonly ComboBox cboNguoiMuon = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly DateTimePicker dtNgayMuon = new() { Format = DateTimePickerFormat.Short };
    private readonly DateTimePicker dtHenTra = new() { Format = DateTimePickerFormat.Short };
    private readonly ComboBox cboSach = new() { DropDownStyle = ComboBoxStyle.DropDownList };
    private readonly NumericUpDown nudSoLuong = new() { Minimum = 1, Maximum = 1000, Value = 1 };
    private readonly DataGridView dgvItems = new() { Dock = DockStyle.Fill, AutoGenerateColumns = false };

    public BorrowDialog()
    {
        Text = "Lập phiếu mượn";
        StartPosition = FormStartPosition.CenterParent;
        Width = 900;
        Height = 560;

        txtMaPhieu.Text = _bus.GenerateNewCode();
        dtNgayMuon.Value = DateTime.Today;
        dtHenTra.Value = DateTime.Today.AddDays(7);

        LoadLookups();
        BuildUi();
    }

    private void LoadLookups()
    {
        cboNguoiMuon.Items.Clear();
        foreach (var item in _docGiaBus.GetLookup()) cboNguoiMuon.Items.Add(item);
        if (cboNguoiMuon.Items.Count > 0) cboNguoiMuon.SelectedIndex = 0;

        cboSach.Items.Clear();
        foreach (var item in _sachBus.GetLookupForBorrow()) cboSach.Items.Add(item);
        if (cboSach.Items.Count > 0) cboSach.SelectedIndex = 0;
    }

    private void BuildUi()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 1, RowCount = 4 };
        Controls.Add(root);

        var top = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 4, Height = 70 };
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        top.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
        top.Controls.Add(CreateField("Mã phiếu", txtMaPhieu), 0, 0);
        top.Controls.Add(CreateField("Người mượn", cboNguoiMuon), 1, 0);
        top.Controls.Add(CreateField("Ngày mượn", dtNgayMuon), 2, 0);
        top.Controls.Add(CreateField("Ngày hẹn trả", dtHenTra), 3, 0);

        var mid = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        var btnAddItem = new Button { Text = "Thêm sách vào phiếu", Width = 160 };
        var btnRemoveItem = new Button { Text = "Xóa dòng", Width = 100 };
        btnAddItem.Click += (_, _) => AddSelectedBook();
        btnRemoveItem.Click += (_, _) =>
        {
            if (dgvItems.CurrentRow is not null && !dgvItems.CurrentRow.IsNewRow) dgvItems.Rows.Remove(dgvItems.CurrentRow);
        };
        mid.Controls.Add(new Label { Text = "Sách", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
        mid.Controls.Add(cboSach);
        mid.Controls.Add(new Label { Text = "Số lượng", AutoSize = true, Padding = new Padding(10, 8, 0, 0) });
        mid.Controls.Add(nudSoLuong);
        mid.Controls.Add(btnAddItem);
        mid.Controls.Add(btnRemoveItem);

        ConfigureGrid();

        var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, FlowDirection = FlowDirection.RightToLeft, Height = 42 };
        var btnOk = new Button { Text = "Lưu phiếu mượn", DialogResult = DialogResult.OK, Width = 130 };
        var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Width = 90 };
        bottom.Controls.Add(btnOk);
        bottom.Controls.Add(btnCancel);

        root.Controls.Add(top);
        root.Controls.Add(mid);
        root.Controls.Add(dgvItems);
        root.Controls.Add(bottom);

        AcceptButton = btnOk;
        CancelButton = btnCancel;
    }

    private void ConfigureGrid()
    {
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "SachId", HeaderText = "Sách ID", Visible = false });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "MaSach", HeaderText = "Mã sách" });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSach", HeaderText = "Tên sách", Width = 260 });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuong", HeaderText = "Số lượng" });
        dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuongCon", HeaderText = "Số lượng còn" });
        UiFactory.StyleGrid(dgvItems);
        dgvItems.ReadOnly = true;
    }

    private void AddSelectedBook()
    {
        if (cboSach.SelectedItem is not LookupItemDTO sach) return;
        var items = _sachBus.GetAll();
        var book = items.FirstOrDefault(x => x.Id == sach.Id);
        if (book is null) return;

        if (nudSoLuong.Value > book.SoLuongCon)
        {
            MessageHelper.Warning("Số lượng mượn vượt quá số lượng còn.");
            return;
        }

        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.IsNewRow || row.Cells["SachId"].Value is null) continue;
            if (Convert.ToInt32(row.Cells["SachId"].Value) == sach.Id)
            {
                MessageHelper.Warning("Sách này đã có trong phiếu.");
                return;
            }
        }

        dgvItems.Rows.Add(sach.Id, book.MaSach, book.TenSach, (int)nudSoLuong.Value, book.SoLuongCon);
    }

    public BorrowSlipInputDTO GetValue()
    {
        var result = new BorrowSlipInputDTO
        {
            MaPhieuMuon = txtMaPhieu.Text.Trim(),
            NguoiMuonId = (cboNguoiMuon.SelectedItem as LookupItemDTO)?.Id ?? 0,
            NgayMuon = dtNgayMuon.Value.Date,
            NgayHenTra = dtHenTra.Value.Date,
            Items = new List<BorrowItemDTO>()
        };

        foreach (DataGridViewRow row in dgvItems.Rows)
        {
            if (row.IsNewRow || row.Cells["SachId"].Value is null) continue;
            result.Items.Add(new BorrowItemDTO
            {
                SachId = Convert.ToInt32(row.Cells["SachId"].Value),
                MaSach = Convert.ToString(row.Cells["MaSach"].Value) ?? string.Empty,
                TenSach = Convert.ToString(row.Cells["TenSach"].Value) ?? string.Empty,
                SoLuong = Convert.ToInt32(row.Cells["SoLuong"].Value),
                SoLuongCon = Convert.ToInt32(row.Cells["SoLuongCon"].Value)
            });
        }

        return result;
    }

    private static Control CreateField(string label, Control control)
    {
        var panel = new TableLayoutPanel { Dock = DockStyle.Fill, RowCount = 2 };
        panel.Controls.Add(new Label { Text = label, AutoSize = true }, 0, 0);
        control.Dock = DockStyle.Top;
        panel.Controls.Add(control, 0, 1);
        return panel;
    }
}

internal sealed class ReturnDialog : Form
{
    private readonly TraSachBUS _bus = new();
    private readonly int _phieuMuonId;
    private readonly TextBox txtMa = new();
    private readonly DateTimePicker dtNgayTra = new() { Format = DateTimePickerFormat.Short };
    private readonly DataGridView dgv = new() { Dock = DockStyle.Fill, AutoGenerateColumns = false };

    public ReturnDialog(int phieuMuonId)
    {
        _phieuMuonId = phieuMuonId;
        Text = "Lập phiếu trả";
        StartPosition = FormStartPosition.CenterParent;
        Width = 980;
        Height = 560;

        txtMa.Text = _bus.GenerateNewCode();
        dtNgayTra.Value = DateTime.Today;
        BuildUi();
        LoadDetails();
    }

    private void BuildUi()
    {
        var root = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16), ColumnCount = 1, RowCount = 3 };
        Controls.Add(root);

        var top = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true };
        top.Controls.Add(new Label { Text = "Mã phiếu trả", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
        txtMa.Width = 140;
        top.Controls.Add(txtMa);
        top.Controls.Add(new Label { Text = "Ngày trả", AutoSize = true, Padding = new Padding(14, 8, 0, 0) });
        top.Controls.Add(dtNgayTra);

        ConfigureGrid();

        var bottom = new FlowLayoutPanel { Dock = DockStyle.Bottom, FlowDirection = FlowDirection.RightToLeft, Height = 42 };
        var btnOk = new Button { Text = "Lưu phiếu trả", DialogResult = DialogResult.OK, Width = 130 };
        var btnCancel = new Button { Text = "Hủy", DialogResult = DialogResult.Cancel, Width = 90 };
        bottom.Controls.Add(btnOk);
        bottom.Controls.Add(btnCancel);

        root.Controls.Add(top);
        root.Controls.Add(dgv);
        root.Controls.Add(bottom);

        AcceptButton = btnOk;
        CancelButton = btnCancel;
    }

    private void ConfigureGrid()
    {
        UiFactory.StyleGrid(dgv);
        dgv.ReadOnly = false;
        dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "ChiTietPhieuMuonId", Visible = false });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "SachId", Visible = false });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "TenSach", HeaderText = "Tên sách", ReadOnly = true, Width = 260 });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuongMuon", HeaderText = "Đã mượn", ReadOnly = true });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuongTra", HeaderText = "Số lượng trả" });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuongHong", HeaderText = "Số lượng hỏng" });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "SoLuongMat", HeaderText = "Số lượng mất" });
        dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "GhiChu", HeaderText = "Ghi chú", Width = 220 });
    }

    private void LoadDetails()
    {
        dgv.Rows.Clear();
        foreach (var item in _bus.GetBorrowDetailsForReturn(_phieuMuonId))
        {
            dgv.Rows.Add(item.ChiTietPhieuMuonId, item.SachId, item.TenSach, item.SoLuongMuon, item.SoLuongTra, item.SoLuongHong, item.SoLuongMat, item.GhiChu);
        }
    }

    public ReturnSlipInputDTO GetValue()
    {
        var result = new ReturnSlipInputDTO
        {
            PhieuMuonId = _phieuMuonId,
            MaPhieuTra = txtMa.Text.Trim(),
            NgayTra = dtNgayTra.Value.Date,
            Items = new List<ReturnDetailDTO>()
        };

        foreach (DataGridViewRow row in dgv.Rows)
        {
            if (row.IsNewRow || row.Cells["ChiTietPhieuMuonId"].Value is null) continue;
            int ParseInt(string name)
            {
                var raw = Convert.ToString(row.Cells[name].Value);
                return int.TryParse(raw, out var value) ? value : 0;
            }

            result.Items.Add(new ReturnDetailDTO
            {
                ChiTietPhieuMuonId = ParseInt("ChiTietPhieuMuonId"),
                SachId = ParseInt("SachId"),
                TenSach = Convert.ToString(row.Cells["TenSach"].Value) ?? string.Empty,
                SoLuongMuon = ParseInt("SoLuongMuon"),
                SoLuongTra = ParseInt("SoLuongTra"),
                SoLuongHong = ParseInt("SoLuongHong"),
                SoLuongMat = ParseInt("SoLuongMat"),
                GhiChu = Convert.ToString(row.Cells["GhiChu"].Value) ?? string.Empty
            });
        }

        return result;
    }
}
