namespace QuanLyThuVien.Forms;

partial class FrmTraSach
{
    private System.ComponentModel.IContainer components = null;
    private Label lblTitle;
    private Label lblSection;
    private Panel pnlSearch;
    private TextBox txtSearch;
    private Button btnSearch;
    private Button btnRefresh;
    private Panel pnlActions;
    private Button btnAdd;
    private Button btnEdit;
    private Button btnDelete;
    private Panel pnlGridHost;
    private DataGridView dgvData;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblTitle = new Label();
        lblSection = new Label();
        pnlSearch = new Panel();
        txtSearch = new TextBox();
        btnSearch = new Button();
        btnRefresh = new Button();
        pnlActions = new Panel();
        btnAdd = new Button();
        btnEdit = new Button();
        btnDelete = new Button();
        pnlGridHost = new Panel();
        dgvData = new DataGridView();
        pnlSearch.SuspendLayout();
        pnlActions.SuspendLayout();
        pnlGridHost.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
        SuspendLayout();
        // lblTitle
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(23, 33, 56);
        lblTitle.Location = new Point(20, 14);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(180, 32);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Phiếu trả";
        // lblSection
        lblSection.AutoSize = true;
        lblSection.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblSection.Location = new Point(20, 58);
        lblSection.Name = "lblSection";
        lblSection.Size = new Size(220, 25);
        lblSection.TabIndex = 1;
        lblSection.Text = "DANH SÁCH PHIẾU TRẢ";
        // pnlSearch
        pnlSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlSearch.Controls.Add(txtSearch);
        pnlSearch.Controls.Add(btnSearch);
        pnlSearch.Controls.Add(btnRefresh);
        pnlSearch.Location = new Point(20, 94);
        pnlSearch.Name = "pnlSearch";
        pnlSearch.Size = new Size(1120, 40);
        pnlSearch.TabIndex = 2;
        // txtSearch
        txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSearch.Font = new Font("Segoe UI", 10F);
        txtSearch.Location = new Point(0, 5);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Tìm theo mã phiếu / tên sách";
        txtSearch.Size = new Size(898, 25);
        txtSearch.TabIndex = 0;
        // btnSearch
        btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSearch.BackColor = Color.FromArgb(46, 101, 230);
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.ForeColor = Color.White;
        btnSearch.Location = new Point(904, 4);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(100, 29);
        btnSearch.TabIndex = 1;
        btnSearch.Text = "Tìm kiếm";
        btnSearch.UseVisualStyleBackColor = false;
        // btnRefresh
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.BackColor = Color.FromArgb(46, 101, 230);
        btnRefresh.FlatAppearance.BorderSize = 0;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.ForeColor = Color.White;
        btnRefresh.Location = new Point(1010, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(110, 29);
        btnRefresh.TabIndex = 2;
        btnRefresh.Text = "Làm mới";
        btnRefresh.UseVisualStyleBackColor = false;
        // pnlActions
        pnlActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlActions.Controls.Add(btnAdd);
        pnlActions.Controls.Add(btnEdit);
        pnlActions.Controls.Add(btnDelete);
        pnlActions.Location = new Point(20, 141);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(1120, 36);
        pnlActions.TabIndex = 3;
        // btnAdd
        btnAdd.BackColor = Color.FromArgb(46, 101, 230);
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(0, 3);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(370, 30);
        btnAdd.TabIndex = 0;
        btnAdd.Text = "Thêm";
        btnAdd.UseVisualStyleBackColor = false;
        // btnEdit
        btnEdit.Anchor = AnchorStyles.Top;
        btnEdit.BackColor = Color.FromArgb(46, 101, 230);
        btnEdit.FlatAppearance.BorderSize = 0;
        btnEdit.FlatStyle = FlatStyle.Flat;
        btnEdit.ForeColor = Color.White;
        btnEdit.Location = new Point(376, 3);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(370, 30);
        btnEdit.TabIndex = 1;
        btnEdit.Text = "Sửa";
        btnEdit.UseVisualStyleBackColor = false;
        // btnDelete
        btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDelete.BackColor = Color.FromArgb(46, 101, 230);
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(752, 3);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(368, 30);
        btnDelete.TabIndex = 2;
        btnDelete.Text = "Xóa";
        btnDelete.UseVisualStyleBackColor = false;
        // pnlGridHost
        pnlGridHost.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pnlGridHost.BackColor = Color.White;
        pnlGridHost.Controls.Add(dgvData);
        pnlGridHost.Location = new Point(20, 183);
        pnlGridHost.Name = "pnlGridHost";
        pnlGridHost.Padding = new Padding(10);
        pnlGridHost.Size = new Size(1120, 487);
        pnlGridHost.TabIndex = 4;
        // dgvData
        dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvData.Dock = DockStyle.Fill;
        dgvData.Location = new Point(10, 10);
        dgvData.Name = "dgvData";
        dgvData.RowTemplate.Height = 25;
        dgvData.Size = new Size(1100, 467);
        dgvData.TabIndex = 0;
        // FrmTraSach
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 244, 248);
        ClientSize = new Size(1160, 690);
        Controls.Add(pnlGridHost);
        Controls.Add(pnlActions);
        Controls.Add(pnlSearch);
        Controls.Add(lblSection);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.None;
        Name = "FrmTraSach";
        Text = "Phiếu trả";
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        pnlActions.ResumeLayout(false);
        pnlGridHost.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
