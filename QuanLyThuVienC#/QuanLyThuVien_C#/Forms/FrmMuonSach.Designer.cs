namespace QuanLyThuVien.Forms;

partial class FrmMuonSach
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
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(23, 33, 56);
        lblTitle.Location = new Point(23, 19);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(189, 41);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "Phiếu mượn";
        // 
        // lblSection
        // 
        lblSection.AutoSize = true;
        lblSection.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblSection.Location = new Point(23, 77);
        lblSection.Name = "lblSection";
        lblSection.Size = new Size(321, 32);
        lblSection.TabIndex = 1;
        lblSection.Text = "DANH SÁCH PHIẾU MƯỢN";
        // 
        // pnlSearch
        // 
        pnlSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlSearch.Controls.Add(txtSearch);
        pnlSearch.Controls.Add(btnSearch);
        pnlSearch.Controls.Add(btnRefresh);
        pnlSearch.Location = new Point(23, 125);
        pnlSearch.Margin = new Padding(3, 4, 3, 4);
        pnlSearch.Name = "pnlSearch";
        pnlSearch.Size = new Size(1280, 53);
        pnlSearch.TabIndex = 2;
        // 
        // txtSearch
        // 
        txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSearch.Font = new Font("Segoe UI", 10F);
        txtSearch.Location = new Point(0, 7);
        txtSearch.Margin = new Padding(3, 4, 3, 4);
        txtSearch.Name = "txtSearch";
        txtSearch.PlaceholderText = "Tìm theo mã phiếu / mã người mượn / tên sách";
        txtSearch.Size = new Size(1026, 30);
        txtSearch.TabIndex = 0;
        // 
        // btnSearch
        // 
        btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSearch.BackColor = Color.FromArgb(46, 101, 230);
        btnSearch.FlatAppearance.BorderSize = 0;
        btnSearch.FlatStyle = FlatStyle.Flat;
        btnSearch.ForeColor = Color.White;
        btnSearch.Location = new Point(1033, 5);
        btnSearch.Margin = new Padding(3, 4, 3, 4);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(114, 39);
        btnSearch.TabIndex = 1;
        btnSearch.Text = "Tìm kiếm";
        btnSearch.UseVisualStyleBackColor = false;
        // 
        // btnRefresh
        // 
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.BackColor = Color.FromArgb(46, 101, 230);
        btnRefresh.FlatAppearance.BorderSize = 0;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.ForeColor = Color.White;
        btnRefresh.Location = new Point(1154, 5);
        btnRefresh.Margin = new Padding(3, 4, 3, 4);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(126, 39);
        btnRefresh.TabIndex = 2;
        btnRefresh.Text = "Làm mới";
        btnRefresh.UseVisualStyleBackColor = false;
        // 
        // pnlActions
        // 
        pnlActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        pnlActions.Controls.Add(btnAdd);
        pnlActions.Controls.Add(btnEdit);
        pnlActions.Controls.Add(btnDelete);
        pnlActions.Location = new Point(23, 188);
        pnlActions.Margin = new Padding(3, 4, 3, 4);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(1280, 48);
        pnlActions.TabIndex = 3;
        // 
        // btnAdd
        // 
        btnAdd.BackColor = Color.FromArgb(46, 101, 230);
        btnAdd.FlatAppearance.BorderSize = 0;
        btnAdd.FlatStyle = FlatStyle.Flat;
        btnAdd.ForeColor = Color.White;
        btnAdd.Location = new Point(0, 4);
        btnAdd.Margin = new Padding(3, 4, 3, 4);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(423, 40);
        btnAdd.TabIndex = 0;
        btnAdd.Text = "Thêm";
        btnAdd.UseVisualStyleBackColor = false;
        // 
        // btnEdit
        // 
        btnEdit.Anchor = AnchorStyles.Top;
        btnEdit.BackColor = Color.FromArgb(46, 101, 230);
        btnEdit.FlatAppearance.BorderSize = 0;
        btnEdit.FlatStyle = FlatStyle.Flat;
        btnEdit.ForeColor = Color.White;
        btnEdit.Location = new Point(430, 4);
        btnEdit.Margin = new Padding(3, 4, 3, 4);
        btnEdit.Name = "btnEdit";
        btnEdit.Size = new Size(423, 40);
        btnEdit.TabIndex = 1;
        btnEdit.Text = "Sửa";
        btnEdit.UseVisualStyleBackColor = false;
        // 
        // btnDelete
        // 
        btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDelete.BackColor = Color.FromArgb(46, 101, 230);
        btnDelete.FlatAppearance.BorderSize = 0;
        btnDelete.FlatStyle = FlatStyle.Flat;
        btnDelete.ForeColor = Color.White;
        btnDelete.Location = new Point(859, 4);
        btnDelete.Margin = new Padding(3, 4, 3, 4);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(421, 40);
        btnDelete.TabIndex = 2;
        btnDelete.Text = "Xóa";
        btnDelete.UseVisualStyleBackColor = false;
        // 
        // pnlGridHost
        // 
        pnlGridHost.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        pnlGridHost.BackColor = Color.White;
        pnlGridHost.Controls.Add(dgvData);
        pnlGridHost.Location = new Point(23, 244);
        pnlGridHost.Margin = new Padding(3, 4, 3, 4);
        pnlGridHost.Name = "pnlGridHost";
        pnlGridHost.Padding = new Padding(11, 13, 11, 13);
        pnlGridHost.Size = new Size(1280, 649);
        pnlGridHost.TabIndex = 4;
        // 
        // dgvData
        // 
        dgvData.BackgroundColor = SystemColors.ActiveCaption;
        dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvData.Dock = DockStyle.Fill;
        dgvData.Location = new Point(11, 13);
        dgvData.Margin = new Padding(3, 4, 3, 4);
        dgvData.Name = "dgvData";
        dgvData.RowHeadersWidth = 51;
        dgvData.RowTemplate.Height = 25;
        dgvData.Size = new Size(1258, 623);
        dgvData.TabIndex = 0;
        // 
        // FrmMuonSach
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(241, 244, 248);
        ClientSize = new Size(1326, 920);
        Controls.Add(pnlGridHost);
        Controls.Add(pnlActions);
        Controls.Add(pnlSearch);
        Controls.Add(lblSection);
        Controls.Add(lblTitle);
        FormBorderStyle = FormBorderStyle.None;
        Margin = new Padding(3, 4, 3, 4);
        Name = "FrmMuonSach";
        Text = "Phiếu mượn";
        pnlSearch.ResumeLayout(false);
        pnlSearch.PerformLayout();
        pnlActions.ResumeLayout(false);
        pnlGridHost.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
}
