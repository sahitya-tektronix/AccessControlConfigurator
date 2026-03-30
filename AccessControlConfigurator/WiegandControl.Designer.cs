namespace AccessControlConfigurator
{
    partial class WiegandControl
    {
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSearchRight;
        private System.Windows.Forms.Label lblFormatNumber;
        private System.Windows.Forms.Label lblBits;
        private System.Windows.Forms.Label lblFacilityCode;
        private System.Windows.Forms.DataGridView dgvFormats;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtFormatNumberFilter;
        private System.Windows.Forms.TextBox txtBitsFilter;
        private System.Windows.Forms.TextBox txtFacilityCodeFilter;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnClearFilters;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        private void InitializeComponent()
        {
            topPanel = new Panel();
            lblTitle = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnRefresh = new Button();
            searchPanel = new FlowLayoutPanel();
            btnSearch = new Button();
            btnClearFilters = new Button();
            txtSearch = new TextBox();
            lblSearchRight = new Label();
            filterPanel = new Panel();
            flowRight = new FlowLayoutPanel();
            txtFacilityCodeFilter = new TextBox();
            lblFacilityCode = new Label();
            txtBitsFilter = new TextBox();
            lblBits = new Label();
            txtFormatNumberFilter = new TextBox();
            lblFormatNumber = new Label();
            dgvFormats = new DataGridView();
            topPanel.SuspendLayout();
            searchPanel.SuspendLayout();
            filterPanel.SuspendLayout();
            flowRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFormats).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.WhiteSmoke;
            topPanel.Controls.Add(lblTitle);
            topPanel.Controls.Add(btnAdd);
            topPanel.Controls.Add(btnEdit);
            topPanel.Controls.Add(btnDelete);
            topPanel.Controls.Add(btnRefresh);
            topPanel.Controls.Add(searchPanel);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1032, 50);
            topPanel.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(169, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Wiegand Formats";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(220, 10);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 28);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(310, 10);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 28);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(400, 10);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 28);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(490, 10);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 28);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // searchPanel
            // 
            searchPanel.AutoSize = true;
            searchPanel.Controls.Add(btnClearFilters);
            searchPanel.Controls.Add(btnSearch);
            searchPanel.Controls.Add(txtSearch);
            searchPanel.Controls.Add(lblSearchRight);
            searchPanel.Dock = DockStyle.None;
            searchPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchPanel.FlowDirection = FlowDirection.RightToLeft;
            searchPanel.Location = new Point(659, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.Padding = new Padding(0, 10, 10, 0);
            searchPanel.Size = new Size(373, 50);
            searchPanel.TabIndex = 5;
            searchPanel.WrapContents = false;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(244, 13);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(40, 28);
            btnSearch.TabIndex = 0;
            btnSearch.Text = "🔍";
            btnSearch.Click += btnSearch_Click;
            // 
            // btnClearFilters
            // 
            btnClearFilters.Location = new Point(290, 13);
            btnClearFilters.Name = "btnClearFilters";
            btnClearFilters.Size = new Size(70, 28);
            btnClearFilters.TabIndex = 0;
            btnClearFilters.Text = "Clear";
            btnClearFilters.Click += btnClearFilters_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(72, 13);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search name";
            txtSearch.Size = new Size(166, 27);
            txtSearch.TabIndex = 1;
            // 
            // lblSearchRight
            // 
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(3, 10);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Padding = new Padding(5, 5, 5, 0);
            lblSearchRight.Size = new Size(63, 25);
            lblSearchRight.TabIndex = 2;
            lblSearchRight.Text = "Search";
            // 
            // filterPanel
            // 
            filterPanel.BackColor = Color.WhiteSmoke;
            filterPanel.Controls.Add(flowRight);
            filterPanel.Dock = DockStyle.Top;
            filterPanel.Location = new Point(0, 50);
            filterPanel.Name = "filterPanel";
            filterPanel.Size = new Size(1032, 40);
            filterPanel.TabIndex = 1;
            // 
            // flowRight
            // 
            flowRight.AutoSize = true;
            flowRight.Controls.Add(txtFacilityCodeFilter);
            flowRight.Controls.Add(lblFacilityCode);
            flowRight.Controls.Add(txtBitsFilter);
            flowRight.Controls.Add(lblBits);
            flowRight.Controls.Add(txtFormatNumberFilter);
            flowRight.Controls.Add(lblFormatNumber);
            flowRight.Dock = DockStyle.Right;
            flowRight.FlowDirection = FlowDirection.RightToLeft;
            flowRight.Location = new Point(526, 0);
            flowRight.Name = "flowRight";
            flowRight.Padding = new Padding(0, 5, 10, 0);
            flowRight.Size = new Size(506, 40);
            flowRight.TabIndex = 0;
            flowRight.WrapContents = false;
            // 
            // txtFacilityCodeFilter
            // 
            txtFacilityCodeFilter.Location = new Point(413, 8);
            txtFacilityCodeFilter.Name = "txtFacilityCodeFilter";
            txtFacilityCodeFilter.PlaceholderText = "e.g., 0";
            txtFacilityCodeFilter.Size = new Size(80, 27);
            txtFacilityCodeFilter.TabIndex = 1;
            // 
            // lblFacilityCode
            // 
            lblFacilityCode.AutoSize = true;
            lblFacilityCode.Location = new Point(299, 5);
            lblFacilityCode.Name = "lblFacilityCode";
            lblFacilityCode.Padding = new Padding(10, 5, 5, 0);
            lblFacilityCode.Size = new Size(108, 25);
            lblFacilityCode.TabIndex = 2;
            lblFacilityCode.Text = "Facility Code";
            // 
            // txtBitsFilter
            // 
            txtBitsFilter.Location = new Point(233, 8);
            txtBitsFilter.Name = "txtBitsFilter";
            txtBitsFilter.PlaceholderText = "e.g., 26";
            txtBitsFilter.Size = new Size(60, 27);
            txtBitsFilter.TabIndex = 3;
            // 
            // lblBits
            // 
            lblBits.AutoSize = true;
            lblBits.Location = new Point(179, 5);
            lblBits.Name = "lblBits";
            lblBits.Padding = new Padding(10, 5, 5, 0);
            lblBits.Size = new Size(48, 25);
            lblBits.TabIndex = 4;
            lblBits.Text = "Bits";
            // 
            // txtFormatNumberFilter
            // 
            txtFormatNumberFilter.Location = new Point(93, 8);
            txtFormatNumberFilter.Name = "txtFormatNumberFilter";
            txtFormatNumberFilter.PlaceholderText = "0-7";
            txtFormatNumberFilter.Size = new Size(80, 27);
            txtFormatNumberFilter.TabIndex = 5;
            // 
            // lblFormatNumber
            // 
            lblFormatNumber.AutoSize = true;
            lblFormatNumber.Location = new Point(3, 5);
            lblFormatNumber.Name = "lblFormatNumber";
            lblFormatNumber.Padding = new Padding(10, 5, 5, 0);
            lblFormatNumber.Size = new Size(84, 25);
            lblFormatNumber.TabIndex = 6;
            lblFormatNumber.Text = "Format #";
            // 
            // dgvFormats
            // 
            dgvFormats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFormats.ColumnHeadersHeight = 29;
            dgvFormats.Dock = DockStyle.Fill;
            dgvFormats.Location = new Point(0, 90);
            dgvFormats.Name = "dgvFormats";
            dgvFormats.RowHeadersWidth = 51;
            dgvFormats.Size = new Size(1032, 363);
            dgvFormats.TabIndex = 0;
            // 
            // WiegandControl
            // 
            Controls.Add(dgvFormats);
            Controls.Add(filterPanel);
            Controls.Add(topPanel);
            Name = "WiegandControl";
            Size = new Size(1032, 453);
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            flowRight.ResumeLayout(false);
            flowRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFormats).EndInit();
            ResumeLayout(false);
        }
        private FlowLayoutPanel searchPanel;
        private FlowLayoutPanel flowRight;
    }
}
