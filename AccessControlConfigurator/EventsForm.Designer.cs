namespace AccessControlConfigurator
{
    partial class EventsControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvEvents;

        private void InitializeComponent()
        {
            FlowLayoutPanel actionPanel = new FlowLayoutPanel();
            FlowLayoutPanel searchPanel = new FlowLayoutPanel();
            FlowLayoutPanel filterRightPanel = new FlowLayoutPanel();
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

            SuspendLayout();

            // =========================
            // 🔷 TOP PANEL
            // =========================
            topPanel.BackColor = Color.WhiteSmoke;
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 56;

            // Title
            lblTitle.Text = "Live Controller Events";
            lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(14, 16);

            actionPanel.AutoSize = true;
            actionPanel.Location = new Point(240, 11);
            actionPanel.Margin = new Padding(0);
            actionPanel.Name = "actionPanel";
            actionPanel.Size = new Size(380, 34);
            actionPanel.WrapContents = false;

            btnclr.Text = "Clear";
            btnclr.Size = new Size(90, 30);
            btnclr.Margin = new Padding(0, 0, 10, 0);

            btnrefresh.Text = "Refresh";
            btnrefresh.Size = new Size(90, 30);
            btnrefresh.Margin = new Padding(0, 0, 10, 0);

            btnDelete.Text = "Delete";
            btnDelete.Size = new Size(90, 30);
            btnDelete.Margin = new Padding(0, 0, 10, 0);

            btnBack.Text = "Back";
            btnBack.Size = new Size(90, 30);
            btnBack.Margin = new Padding(0);

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

            txtSearch.Size = new Size(200, 27);
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Margin = new Padding(8, 0, 0, 0);

            lblSearchRight.Text = "Search";
            lblSearchRight.AutoSize = true;
            lblSearchRight.Padding = new Padding(5, 4, 5, 0);

            searchPanel.Controls.Add(btnSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(lblSearchRight);

            // Add to topPanel
            topPanel.Controls.Add(lblTitle);
            topPanel.Controls.Add(actionPanel);
            topPanel.Controls.Add(searchPanel);

            // =========================
            // 🔷 FILTER PANEL (RIGHT ALIGNED)
            // =========================
            filterPanel.BackColor = Color.WhiteSmoke;
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 44;

            filterRightPanel.Dock = DockStyle.Right;
            filterRightPanel.FlowDirection = FlowDirection.RightToLeft;
            filterRightPanel.WrapContents = false;
            filterRightPanel.Padding = new Padding(0, 7, 12, 0);
            filterRightPanel.AutoSize = true;
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
            btnClearFilters.Size = new Size(70, 28);
            btnClearFilters.Margin = new Padding(8, 0, 0, 0);

            filterRightPanel.Controls.Add(btnClearFilters);
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
            // FINAL ADD
            // =========================
            Controls.Add(dgvEvents);
            Controls.Add(filterPanel);
            Controls.Add(topPanel);

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
    }
}
