using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class EventReportControl
    {
        // ── Header ──────────────────────────────────────────────────────────
        private Panel panelHeader;
        private Label lblTitle;

        // Row 1: "Choose Columns:" label + checkboxes
        private Label lblChooseColumns;
        private FlowLayoutPanel panelColumnChooser;
        private CheckBox chkColEventTime;
        private CheckBox chkColCardNumber;
        private CheckBox chkColControllerName;
        private CheckBox chkColScpId;
        private CheckBox chkColEventDescription;
        private CheckBox chkColCreatedAt;

        // Row 2: action buttons
        private FlowLayoutPanel panelActions;
        private Button btnExportPdf;
        private Button btnExportExcel;
        private Button btnReload;

        // ── Body ─────────────────────────────────────────────────────────────
        private DataGridView dgvEvents;

        // ── Required by the designer infrastructure ──────────────────────────
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            // ── Instantiate controls ────────────────────────────────────────
            panelHeader = new Panel();
            lblTitle = new Label();
            lblChooseColumns = new Label();
            panelColumnChooser = new FlowLayoutPanel();
            chkColEventTime = new CheckBox();
            chkColCardNumber = new CheckBox();
            chkColControllerName = new CheckBox();
            chkColScpId = new CheckBox();
            chkColEventDescription = new CheckBox();
            chkColCreatedAt = new CheckBox();
            panelActions = new FlowLayoutPanel();
            btnExportPdf = new Button();
            btnExportExcel = new Button();
            btnReload = new Button();
            dgvEvents = new DataGridView();

            // ── Suspend layout ──────────────────────────────────────────────
            panelHeader.SuspendLayout();
            panelColumnChooser.SuspendLayout();
            panelActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();

            // ════════════════════════════════════════════════════════════════
            // panelHeader  (top bar, fixed height, anchors L+R)
            // ════════════════════════════════════════════════════════════════
            panelHeader.BackColor = Color.FromArgb(245, 246, 248);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 120;
            panelHeader.Padding = new Padding(14, 8, 14, 8);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblChooseColumns);
            panelHeader.Controls.Add(panelColumnChooser);
            panelHeader.Controls.Add(panelActions);

            // ── lblTitle ────────────────────────────────────────────────────
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 60);
            lblTitle.Location = new Point(14, 10);
            lblTitle.Text = "Event Report";

            // ── lblChooseColumns ────────────────────────────────────────────
            // Sits on the same row as the checkboxes, aligned to the left
            lblChooseColumns.AutoSize = true;
            lblChooseColumns.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblChooseColumns.ForeColor = Color.FromArgb(70, 70, 120);
            lblChooseColumns.Location = new Point(14, 46);   // Y = same baseline as checkboxes
            lblChooseColumns.Text = "Choose Columns:";

            // ── panelColumnChooser ──────────────────────────────────────────
            panelColumnChooser.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelColumnChooser.AutoSize = false;
            panelColumnChooser.FlowDirection = FlowDirection.LeftToRight;
            panelColumnChooser.WrapContents = false;
            panelColumnChooser.Location = new Point(130, 42);   // offset right of the label
            panelColumnChooser.Size = new Size(760, 26);
            panelColumnChooser.Controls.Add(chkColEventTime);
            panelColumnChooser.Controls.Add(chkColCardNumber);
            panelColumnChooser.Controls.Add(chkColControllerName);
            panelColumnChooser.Controls.Add(chkColScpId);
            panelColumnChooser.Controls.Add(chkColEventDescription);
            panelColumnChooser.Controls.Add(chkColCreatedAt);

            // helper: shared checkbox style
            void StyleCheckBox(CheckBox c, string text)
            {
                c.AutoSize = true;
                c.Checked = true;
                c.Font = new Font("Segoe UI", 9F);
                c.ForeColor = Color.FromArgb(40, 40, 80);
                c.Margin = new Padding(0, 3, 16, 0);
                c.Text = text;
            }

            StyleCheckBox(chkColEventTime, "Event Time");
            StyleCheckBox(chkColCardNumber, "Card Number");
            StyleCheckBox(chkColControllerName, "Controller Name");
            StyleCheckBox(chkColScpId, "SCP ID");
            StyleCheckBox(chkColEventDescription, "Event Description");
            StyleCheckBox(chkColCreatedAt, "Created At");

            // ── panelActions ────────────────────────────────────────────────
            panelActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelActions.AutoSize = false;
            panelActions.FlowDirection = FlowDirection.LeftToRight;
            panelActions.WrapContents = false;
            panelActions.Location = new Point(14, 76);
            panelActions.Size = new Size(876, 36);
            panelActions.Controls.Add(btnExportPdf);
            panelActions.Controls.Add(btnExportExcel);
            panelActions.Controls.Add(btnReload);

            // helper: shared button style
            void StyleButton(Button b, string text, Color back, Color fore)
            {
                b.AutoSize = false;
                b.Cursor = Cursors.Hand;
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 1;
                b.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 220);
                b.BackColor = back;
                b.ForeColor = fore;
                b.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                b.Height = 30;
                b.Width = 110;
                b.Margin = new Padding(0, 0, 8, 0);
                b.Text = text;
            }

            StyleButton(btnExportPdf, "Export PDF", Color.FromArgb(220, 53, 69), Color.White);
            StyleButton(btnExportExcel, "Export Excel", Color.FromArgb(25, 135, 84), Color.White);
            StyleButton(btnReload, "Reload", Color.FromArgb(13, 110, 253), Color.White);

            // ════════════════════════════════════════════════════════════════
            // dgvEvents  (fills remaining space under the header)
            // ════════════════════════════════════════════════════════════════
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AllowUserToResizeRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.BackgroundColor = Color.White;
            dgvEvents.BorderStyle = BorderStyle.None;
            dgvEvents.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvEvents.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvEvents.Dock = DockStyle.Fill;
            dgvEvents.GridColor = Color.FromArgb(220, 220, 235);
            dgvEvents.ReadOnly = true;
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Column header style
            dgvEvents.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(235, 237, 245);
            dgvEvents.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 30, 80);
            dgvEvents.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dgvEvents.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Alternating row colours
            dgvEvents.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 255);
            dgvEvents.DefaultCellStyle.SelectionBackColor = Color.FromArgb(13, 110, 253);
            dgvEvents.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvEvents.DefaultCellStyle.Font = new Font("Segoe UI", 9F);

            // ════════════════════════════════════════════════════════════════
            // EventReportControl (UserControl root)
            // ════════════════════════════════════════════════════════════════
            BackColor = Color.White;
            Controls.Add(dgvEvents);       // fills remaining area
            Controls.Add(panelHeader);     // Dock=Top sits above dgvEvents
            Name = "EventReportControl";
            Size = new Size(900, 540);

            // ── Resume layout ───────────────────────────────────────────────
            panelColumnChooser.ResumeLayout(false);
            panelColumnChooser.PerformLayout();
            panelActions.ResumeLayout(false);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
        }
    }
}
