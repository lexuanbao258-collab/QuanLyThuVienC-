using System.Drawing.Drawing2D;

namespace QuanLyThuVien.Common;

public static class UiFactory
{
    public static readonly Color Navy = Color.FromArgb(20, 37, 91);
    public static readonly Color Primary = Color.FromArgb(46, 101, 230);
    public static readonly Color Background = Color.FromArgb(241, 244, 248);

    public static Button CreateMenuButton(string text)
    {
        return new Button
        {
            Text = text,
            Dock = DockStyle.Top,
            Height = 46,
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Navy,
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(24, 0, 0, 0),
            Cursor = Cursors.Hand
        };
    }

    public static Button CreatePrimaryButton(string text)
    {
        var btn = new Button
        {
            Text = text,
            Height = 34,
            FlatStyle = FlatStyle.Flat,
            BackColor = Primary,
            ForeColor = Color.White,
            Cursor = Cursors.Hand
        };
        btn.FlatAppearance.BorderSize = 0;
        return btn;
    }

    public static Panel CreateCard(string title, string? desc = null)
    {
        var panel = new Panel
        {
            BackColor = Color.White,
            Width = 250,
            Height = 120,
            Padding = new Padding(18),
            Margin = new Padding(12)
        };

        panel.Paint += (_, e) =>
        {
            using var pen = new Pen(Color.FromArgb(220, 226, 233));
            e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
        };

        if (string.IsNullOrWhiteSpace(desc))
        {
            panel.Controls.Add(new Label
            {
                Text = title,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 15, FontStyle.Bold),
                ForeColor = Color.FromArgb(23, 33, 56),
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true
            });
        }
        else
        {
            panel.Height = 150;
            panel.Controls.Add(new Label
            {
                Text = desc,
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(102, 112, 133)
            });
            panel.Controls.Add(new Label
            {
                Text = title,
                Dock = DockStyle.Top,
                Height = 34,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(23, 33, 56)
            });
        }

        return panel;
    }

    public static void StyleGrid(DataGridView dgv)
    {
        dgv.BackgroundColor = Color.White;
        dgv.BorderStyle = BorderStyle.None;
        dgv.EnableHeadersVisualStyles = false;
        dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(233, 239, 247);
        dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
        dgv.DefaultCellStyle.Font = new Font("Segoe UI", 10);
        dgv.RowHeadersWidth = 45;
        dgv.RowHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgv.AllowUserToAddRows = false;
        dgv.ReadOnly = true;
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgv.MultiSelect = false;
        dgv.RowTemplate.Height = 34;
        dgv.GridColor = Color.FromArgb(225, 230, 236);
    }
}
