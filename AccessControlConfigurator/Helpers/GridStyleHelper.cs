using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator.Helpers
{
    internal static class GridStyleHelper
    {
        // HID brand colours
        private static readonly Color HeaderBack   = Color.FromArgb(30, 58, 95);   // deep navy
        private static readonly Color HeaderFore   = Color.White;
        private static readonly Color SelectBack   = Color.FromArgb(0, 120, 215);  // HID blue
        private static readonly Color SelectFore   = Color.White;
        private static readonly Color AltRowBack   = Color.FromArgb(244, 247, 252); // very light blue-grey
        private static readonly Color GridBorder   = Color.FromArgb(213, 220, 232);

        public static void ApplyStandardStyle(
            DataGridView grid,
            bool fillColumns = true,
            bool allowColumnResize = false,
            bool allowColumnOrder = false)
        {
            if (grid == null)
                return;

            // --- grid chrome ---
            grid.BackgroundColor    = Color.White;
            grid.BorderStyle        = BorderStyle.None;
            grid.GridColor          = GridBorder;
            grid.Padding            = new Padding(0);

            // --- behaviour ---
            grid.ReadOnly                = true;
            grid.MultiSelect             = false;
            grid.RowHeadersVisible       = false;
            grid.AllowUserToAddRows      = false;
            grid.AllowUserToDeleteRows   = false;
            grid.AllowUserToResizeRows   = false;
            grid.AllowUserToResizeColumns = allowColumnResize;
            grid.AllowUserToOrderColumns  = allowColumnOrder;
            grid.SelectionMode            = DataGridViewSelectionMode.FullRowSelect;
            grid.CellBorderStyle          = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            grid.EnableHeadersVisualStyles = false;

            grid.AutoSizeColumnsMode = fillColumns
                ? DataGridViewAutoSizeColumnsMode.Fill
                : DataGridViewAutoSizeColumnsMode.None;

            // --- header ---
            grid.ColumnHeadersHeight = 42;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            grid.ColumnHeadersDefaultCellStyle.BackColor  = HeaderBack;
            grid.ColumnHeadersDefaultCellStyle.ForeColor  = HeaderFore;
            grid.ColumnHeadersDefaultCellStyle.Font       = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.Alignment  = DataGridViewContentAlignment.MiddleLeft;
            grid.ColumnHeadersDefaultCellStyle.WrapMode   = DataGridViewTriState.False;
            grid.ColumnHeadersDefaultCellStyle.Padding    = new Padding(10, 0, 0, 0);

            // --- rows ---
            grid.RowTemplate.Height = 34;

            grid.DefaultCellStyle.Font             = new Font("Segoe UI", 9F, FontStyle.Regular);
            grid.DefaultCellStyle.BackColor        = Color.White;
            grid.DefaultCellStyle.ForeColor        = Color.FromArgb(30, 30, 30);
            grid.DefaultCellStyle.SelectionBackColor = SelectBack;
            grid.DefaultCellStyle.SelectionForeColor = SelectFore;
            grid.DefaultCellStyle.Padding          = new Padding(10, 0, 4, 0);
            grid.DefaultCellStyle.Alignment        = DataGridViewContentAlignment.MiddleLeft;

            grid.AlternatingRowsDefaultCellStyle.BackColor = AltRowBack;
            grid.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 30);
        }
    }
}
