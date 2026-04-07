namespace AccessControlConfigurator
{
    partial class EventsControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.FlowLayoutPanel filterRightPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvEvents;

        private void InitializeComponent()
        {
            FlowLayoutPanel actionPanel = new FlowLayoutPanel();
            FlowLayoutPanel searchPanel = new FlowLayoutPanel();
            filterRightPanel = new FlowLayoutPanel();
            topPanel = new Panel();
            filterPanel = new Panel();
            dgvEvents = new DataGridView();

            btnBack = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearchRight = new Label();

            btnclr = new Button();
            btnrefresh = new Button();
            btnDelete = new Button();
            lblTitle = new Label();

            lblEventTypeFilter = new Label();
            cmbEventTypeFilter = new ComboBox();
            lblScpFilter = new Label();
            cmbScpFilter = new ComboBox();
            btnClearFilters = new Button();
            lblTimeDisplay = new Label();
            cmbTimeDisplay = new ComboBox();

            SuspendLayout();

            // =========================
            // 🔷 TOP PANEL
            // =========================
            topPanel.BackColor = Color.WhiteSmoke;
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 56;

            // Title — now a separate DockStyle.Top label above topPanel
            lblTitle.Text = "Live Controller Events";
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Top;
            lblTitle.Height = 40;
            lblTitle.Padding = new Padding(14, 0, 0, 0);
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;

            actionPanel.AutoSize = true;
            actionPanel.Location = new Point(10, 11);
            actionPanel.Margin = new Padding(0);
            actionPanel.Name = "actionPanel";
            actionPanel.Size = new Size(410, 34);
            actionPanel.WrapContents = false;

            // ==================== STANDARDIZE BUTTONS ====================
            btnclr.Text = "Clear";
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnclr, 80);

            btnrefresh.Text = "Refresh";
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnrefresh, 80);

            btnDelete.Text = "Delete";
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnDelete, 80);

            btnBack.Text = "\u2190 Back";
            Helpers.UIStyleHelper.StyleNeutralToolbarButton(btnBack, 90);
            btnBack.Margin = new Padding(0);
            btnBack.Visible = false;

            actionPanel.Controls.Add(btnclr);
            actionPanel.Controls.Add(btnrefresh);
            actionPanel.Controls.Add(btnDelete);
            actionPanel.Controls.Add(btnBack);

            // =========================
            // 🔍 SEARCH (RIGHT SIDE)
            // =========================
            searchPanel.Dock = DockStyle.Right;
            searchPanel.FlowDirection = FlowDirection.RightToLeft;
            searchPanel.WrapContents = false;
            searchPanel.Padding = new Padding(0, 12, 12, 0);
            searchPanel.AutoSize = true;
            searchPanel.Margin = new Padding(0);

            btnSearch.Text = "🔍";
            btnSearch.Size = new Size(40, 28);
            btnSearch.Margin = new Padding(8, 0, 0, 0);
            btnSearch.Font = Helpers.UIStyleHelper.StandardFonts.ButtonFont;
            btnSearch.FlatStyle = FlatStyle.Flat;

            txtSearch.Size = new Size(200, 27);
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Margin = new Padding(8, 0, 0, 0);

            lblSearchRight.Text = "Search";
            lblSearchRight.AutoSize = true;
            lblSearchRight.Padding = new Padding(5, 4, 5, 0);

            searchPanel.Controls.Add(btnSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(lblSearchRight);

            // Add to topPanel (title is now separate above topPanel)
            topPanel.Controls.Add(actionPanel);
            topPanel.Controls.Add(searchPanel);

            // =========================
            // 🔷 FILTER PANEL (RIGHT ALIGNED)
            // =========================
            filterPanel.BackColor = Color.WhiteSmoke;
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 72;

            filterRightPanel.Dock = DockStyle.Fill;
            filterRightPanel.FlowDirection = FlowDirection.RightToLeft;
            filterRightPanel.WrapContents = true;
            filterRightPanel.Padding = new Padding(0, 7, 12, 0);
            filterRightPanel.AutoSize = false;
            filterRightPanel.Margin = new Padding(0);

            // SCP ID
            cmbScpFilter.Size = new Size(110, 28);
            cmbScpFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScpFilter.Margin = new Padding(8, 0, 0, 0);

            lblScpFilter.Text = "SCP ID";
            lblScpFilter.AutoSize = true;
            lblScpFilter.Padding = new Padding(10, 4, 5, 0);

            // Event Type
            cmbEventTypeFilter.Size = new Size(170, 28);
            cmbEventTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEventTypeFilter.Margin = new Padding(8, 0, 0, 0);

            lblEventTypeFilter.Text = "Event Category";
            lblEventTypeFilter.AutoSize = true;
            lblEventTypeFilter.Padding = new Padding(10, 4, 5, 0);

            // Add controls (RIGHT → LEFT order)
            btnClearFilters.Text = "Clear";
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnClearFilters, 70);
            btnClearFilters.Height = 28;
            btnClearFilters.Margin = new Padding(8, 0, 0, 0);

            cmbTimeDisplay.Size = new Size(110, 28);
            cmbTimeDisplay.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeDisplay.Margin = new Padding(8, 0, 0, 0);

            lblTimeDisplay.Text = "Time Display";
            lblTimeDisplay.AutoSize = true;
            lblTimeDisplay.Padding = new Padding(10, 4, 5, 0);

            filterRightPanel.Controls.Add(btnClearFilters);
            filterRightPanel.Controls.Add(cmbTimeDisplay);
            filterRightPanel.Controls.Add(lblTimeDisplay);
            filterRightPanel.Controls.Add(cmbScpFilter);
            filterRightPanel.Controls.Add(lblScpFilter);
            filterRightPanel.Controls.Add(cmbEventTypeFilter);
            filterRightPanel.Controls.Add(lblEventTypeFilter);

            filterPanel.Controls.Add(filterRightPanel);

            // =========================
            // 📊 GRID
            // =========================
            dgvEvents.Dock = DockStyle.Fill;
            dgvEvents.BackgroundColor = Color.White;
            dgvEvents.BorderStyle = BorderStyle.None;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.ReadOnly = true;
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToDeleteRows = false;
            dgvEvents.AllowUserToResizeRows = false;

            // =========================
            // WebSocket status bar (bottom-left)
            // =========================
            pnlWsStatus = new Panel();
            pnlWsStatus.Dock      = DockStyle.Bottom;
            pnlWsStatus.Height    = 26;
            pnlWsStatus.BackColor = Color.FromArgb(240, 244, 250);

            lblWsStatus = new Label();
            lblWsStatus.AutoSize  = false;
            lblWsStatus.Dock      = DockStyle.Fill;
            lblWsStatus.Font      = new Font("Segoe UI", 8.5F);
            lblWsStatus.ForeColor = Color.FromArgb(60, 90, 140);
            lblWsStatus.TextAlign = ContentAlignment.MiddleLeft;
            lblWsStatus.Padding   = new Padding(10, 0, 0, 0);
            lblWsStatus.Text      = "WebSocket: not connected";
            pnlWsStatus.Controls.Add(lblWsStatus);

            // =========================
            // FINAL ADD
            // =========================
            Controls.Add(dgvEvents);
            Controls.Add(filterPanel);
            Controls.Add(topPanel);
            Controls.Add(lblTitle);      // DockStyle.Top, added after topPanel → appears above topPanel
            Controls.Add(pnlWsStatus);   // DockStyle.Bottom must be added last so it sits below Fill

            BackColor = Color.White;
            Size = new Size(1200, 700);

            ResumeLayout(false);
        }
        private Button btnDelete;
        private Button btnrefresh;
        private Button btnclr;
        private Label lblSearchRight;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnBack;
        private Label lblEventTypeFilter;
        private ComboBox cmbEventTypeFilter;
        private Label lblScpFilter;
        private ComboBox cmbScpFilter;
        private Button btnClearFilters;
        private Label lblTimeDisplay;
        private ComboBox cmbTimeDisplay;
        private Panel pnlWsStatus;
        private Label lblWsStatus;
    }
}

