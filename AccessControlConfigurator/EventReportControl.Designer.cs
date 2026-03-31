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
        private Panel panelPagination;
        private Button btnPrevPage;
        private Button btnNextPage;
        private Label lblPageInfo;
        private Label lblPageSize;
        private ComboBox cmbPageSize;
        private CheckBox chkFilterByCreatedDate;
        private Label lblStartDate;
        private Label lblEndDate;
        private DateTimePicker dtStartDate;
        private DateTimePicker dtEndDate;
        private Button btnApplyFilters;
        private Label lblCardNumbers;
        private TextBox txtCardNumbers;
        private Button btnSearchCardNumbers;
        private Button btnClearFilters;

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
            panelPagination = new Panel();
            btnPrevPage = new Button();
            btnNextPage = new Button();
            lblPageInfo = new Label();
            lblPageSize = new Label();
            cmbPageSize = new ComboBox();
            chkFilterByCreatedDate = new CheckBox();
            lblStartDate = new Label();
            lblEndDate = new Label();
            dtStartDate = new DateTimePicker();
            dtEndDate = new DateTimePicker();
            btnApplyFilters = new Button();
            lblCardNumbers = new Label();
            txtCardNumbers = new TextBox();
            btnSearchCardNumbers = new Button();
            btnClearFilters = new Button();
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
            panelHeader.Height = 156;
            panelHeader.Padding = new Padding(14, 8, 14, 8);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblChooseColumns);
            panelHeader.Controls.Add(panelColumnChooser);
            panelHeader.Controls.Add(panelActions);
            panelHeader.Controls.Add(panelPagination);
            panelHeader.Controls.Add(chkFilterByCreatedDate);
            panelHeader.Controls.Add(lblStartDate);
            panelHeader.Controls.Add(dtStartDate);
            panelHeader.Controls.Add(lblEndDate);
            panelHeader.Controls.Add(dtEndDate);
            panelHeader.Controls.Add(btnApplyFilters);
            panelHeader.Controls.Add(lblCardNumbers);
            panelHeader.Controls.Add(txtCardNumbers);
            panelHeader.Controls.Add(btnSearchCardNumbers);
            panelHeader.Controls.Add(btnClearFilters);

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
            panelActions.Anchor = AnchorStyles.Top | AnchorStyles.Left;
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

            // ── panelPagination ────────────────────────────────────────────
            panelPagination.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            panelPagination.Location = new Point(540, 76);
            panelPagination.Size = new Size(350, 36);

            btnPrevPage.Text = "Prev";
            btnPrevPage.Size = new Size(60, 30);
            btnPrevPage.Location = new Point(0, 3);
            btnPrevPage.FlatStyle = FlatStyle.Flat;
            btnPrevPage.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 220);

            btnNextPage.Text = "Next";
            btnNextPage.Size = new Size(60, 30);
            btnNextPage.Location = new Point(68, 3);
            btnNextPage.FlatStyle = FlatStyle.Flat;
            btnNextPage.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 220);

            lblPageInfo.AutoSize = true;
            lblPageInfo.Location = new Point(138, 8);
            lblPageInfo.Font = new Font("Segoe UI", 9F);
            lblPageInfo.Text = "Page 1 of 1";

            lblPageSize.AutoSize = true;
            lblPageSize.Location = new Point(232, 8);
            lblPageSize.Font = new Font("Segoe UI", 9F);
            lblPageSize.Text = "Size";

            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.Location = new Point(270, 4);
            cmbPageSize.Size = new Size(70, 27);

            panelPagination.Controls.Add(btnPrevPage);
            panelPagination.Controls.Add(btnNextPage);
            panelPagination.Controls.Add(lblPageInfo);
            panelPagination.Controls.Add(lblPageSize);
            panelPagination.Controls.Add(cmbPageSize);

            // ── Filters row ────────────────────────────────────────────────
            chkFilterByCreatedDate.AutoSize = true;
            chkFilterByCreatedDate.Location = new Point(14, 120);
            chkFilterByCreatedDate.Text = "Filter by Created Date";

            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(190, 121);
            lblStartDate.Text = "Start";

            dtStartDate.Format = DateTimePickerFormat.Custom;
            dtStartDate.CustomFormat = "yyyy-MM-dd HH:mm";
            dtStartDate.ShowUpDown = true;
            dtStartDate.Location = new Point(230, 118);
            dtStartDate.Size = new Size(150, 27);

            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(390, 121);
            lblEndDate.Text = "End";

            dtEndDate.Format = DateTimePickerFormat.Custom;
            dtEndDate.CustomFormat = "yyyy-MM-dd HH:mm";
            dtEndDate.ShowUpDown = true;
            dtEndDate.Location = new Point(425, 118);
            dtEndDate.Size = new Size(150, 27);

            btnApplyFilters.Text = "Apply";
            btnApplyFilters.Size = new Size(70, 28);
            btnApplyFilters.Location = new Point(585, 118);
            btnApplyFilters.FlatStyle = FlatStyle.Flat;
            btnApplyFilters.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 220);

            lblCardNumbers.AutoSize = true;
            lblCardNumbers.Location = new Point(665, 121);
            lblCardNumbers.Text = "Card Number";

            txtCardNumbers.Location = new Point(715, 118);
            txtCardNumbers.Size = new Size(180, 27);

            btnSearchCardNumbers.Text = "🔍";
            btnSearchCardNumbers.Size = new Size(34, 28);
            btnSearchCardNumbers.Location = new Point(900, 118);
            btnSearchCardNumbers.FlatStyle = FlatStyle.Flat;
            btnSearchCardNumbers.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 220);

            btnClearFilters.Text = "Clear";
            btnClearFilters.Size = new Size(60, 28);
            btnClearFilters.Location = new Point(940, 118);
            btnClearFilters.FlatStyle = FlatStyle.Flat;
            btnClearFilters.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 220);

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
            panelPagination.ResumeLayout(false);
            panelPagination.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
        }
    }
}
