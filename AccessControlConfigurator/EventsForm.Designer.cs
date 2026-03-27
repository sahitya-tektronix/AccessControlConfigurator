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

            SuspendLayout();

            // =========================
            // 🔷 TOP PANEL
            // =========================
            topPanel.BackColor = Color.WhiteSmoke;
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 50;

            // Title
            lblTitle.Text = "Live Controller Events";
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(10, 12);

            // Buttons (LEFT)
            btnclr.Text = "Clear";
            btnclr.Size = new Size(100, 30);
            btnclr.Location = new Point(220, 10);

            btnrefresh.Text = "Refresh";
            btnrefresh.Size = new Size(100, 30);
            btnrefresh.Location = new Point(330, 10);

            btnDelete.Text = "Delete";
            btnDelete.Size = new Size(100, 30);
            btnDelete.Location = new Point(440, 10);

            btnBack.Text = "Back";
            btnBack.Size = new Size(100, 30);
            btnBack.Location = new Point(550, 10);

            // =========================
            // 🔍 SEARCH (RIGHT SIDE)
            // =========================
            FlowLayoutPanel searchPanel = new FlowLayoutPanel();
            searchPanel.Dock = DockStyle.Right;
            searchPanel.FlowDirection = FlowDirection.RightToLeft;
            searchPanel.WrapContents = false;
            searchPanel.Padding = new Padding(0, 10, 10, 0);
            searchPanel.AutoSize = true;

            btnSearch.Text = "🔍";
            btnSearch.Size = new Size(40, 28);

            txtSearch.Size = new Size(200, 27);
            txtSearch.PlaceholderText = "Search here";

            lblSearchRight.Text = "Search";
            lblSearchRight.AutoSize = true;
            lblSearchRight.Padding = new Padding(5, 5, 5, 0);

            searchPanel.Controls.Add(btnSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(lblSearchRight);

            // Add to topPanel
            topPanel.Controls.Add(lblTitle);
            topPanel.Controls.Add(btnclr);
            topPanel.Controls.Add(btnrefresh);
            topPanel.Controls.Add(btnDelete);
            topPanel.Controls.Add(btnBack);
            topPanel.Controls.Add(searchPanel);

            // =========================
            // 🔷 FILTER PANEL (RIGHT ALIGNED)
            // =========================
            filterPanel.BackColor = Color.WhiteSmoke;
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Height = 40;

            FlowLayoutPanel flowRight = new FlowLayoutPanel();
            flowRight.Dock = DockStyle.Right;
            flowRight.FlowDirection = FlowDirection.RightToLeft;
            flowRight.WrapContents = false;
            flowRight.Padding = new Padding(0, 5, 10, 0);
            flowRight.AutoSize = true;

            // SCP ID
            cmbScpFilter.Size = new Size(100, 28);
            cmbScpFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            lblScpFilter.Text = "SCP ID";
            lblScpFilter.AutoSize = true;
            lblScpFilter.Padding = new Padding(10, 5, 5, 0);

            // Event Type
            cmbEventTypeFilter.Size = new Size(150, 28);
            cmbEventTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            lblEventTypeFilter.Text = "Event Type";
            lblEventTypeFilter.AutoSize = true;
            lblEventTypeFilter.Padding = new Padding(10, 5, 5, 0);

            // Add controls (RIGHT → LEFT order)
            flowRight.Controls.Add(cmbScpFilter);
            flowRight.Controls.Add(lblScpFilter);
            flowRight.Controls.Add(cmbEventTypeFilter);
            flowRight.Controls.Add(lblEventTypeFilter);

            filterPanel.Controls.Add(flowRight);

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
    }
}
