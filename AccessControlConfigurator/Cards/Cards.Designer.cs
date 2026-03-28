namespace AccessControlConfigurator
{
    partial class Cards
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSync;
        private Button btnRefresh;
        private Button btnBack;


        private DataGridView dgvCards;

        private TableLayoutPanel mainLayout;
        private Panel headerPanel;
        private Panel filterPanel;
        private Label lblCardNumberFilter;
        private TextBox txtCardNumberFilter;
        private Label lblActTimeFilter;
        private ComboBox cmbActTimeFilter;
        private Label lblDeactTimeFilter;
        private ComboBox cmbDeactTimeFilter;
        private Button btnClearFilters;


        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSync = new Button();
            btnRefresh = new Button();
            btnBack = new Button();
            dgvCards = new DataGridView();
            mainLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearchRight = new Label();
            filterPanel = new Panel();
            lblCardNumberFilter = new Label();
            txtCardNumberFilter = new TextBox();
            lblActTimeFilter = new Label();
            cmbActTimeFilter = new ComboBox();
            lblDeactTimeFilter = new Label();
            cmbDeactTimeFilter = new ComboBox();
            btnClearFilters = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvCards).BeginInit();
            mainLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            filterPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(78, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Cards";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(158, 12);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(244, 12);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 30);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(330, 12);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSync
            // 
            btnSync.Location = new Point(420, 12);
            btnSync.Name = "btnSync";
            btnSync.Size = new Size(80, 30);
            btnSync.TabIndex = 4;
            btnSync.Text = "Sync";
            btnSync.Click += btnSync_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(510, 12);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 30);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(600, 12);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 30);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            // 
            // dgvCards
            // 
            dgvCards.AllowUserToAddRows = false;
            dgvCards.AllowUserToOrderColumns = false;
            dgvCards.BackgroundColor = Color.White;
            dgvCards.BorderStyle = BorderStyle.None;
            dgvCards.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCards.Dock = DockStyle.Fill;
            dgvCards.Location = new Point(3, 99);
            dgvCards.Name = "dgvCards";
            dgvCards.RowHeadersVisible = false;
            dgvCards.RowHeadersWidth = 51;
            dgvCards.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCards.Size = new Size(1094, 548);
            dgvCards.TabIndex = 1;
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(filterPanel, 0, 1);
            mainLayout.Controls.Add(dgvCards, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(1100, 650);
            mainLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(btnSearch);
            headerPanel.Controls.Add(txtSearch);
            headerPanel.Controls.Add(lblSearchRight);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(btnAdd);
            headerPanel.Controls.Add(btnEdit);
            headerPanel.Controls.Add(btnClearFilters);
            headerPanel.Controls.Add(btnDelete);
            headerPanel.Controls.Add(btnSync);
            headerPanel.Controls.Add(btnRefresh);
            headerPanel.Controls.Add(btnBack);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(3, 3);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(10);
            headerPanel.Size = new Size(1094, 54);
            headerPanel.TabIndex = 0;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = SystemColors.ControlDark;
            btnSearch.Location = new Point(964, 13);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(36, 29);
            btnSearch.TabIndex = 11;
            btnSearch.Text = "🔍";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(821, 15);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Size = new Size(137, 27);
            txtSearch.TabIndex = 10;
            // 
            // lblSearchRight
            // 
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(762, 19);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Size = new Size(53, 20);
            lblSearchRight.TabIndex = 9;
            lblSearchRight.Text = "Search";
            // 
            // filterPanel
            // 
            filterPanel.Controls.Add(lblCardNumberFilter);
            filterPanel.Controls.Add(txtCardNumberFilter);
            filterPanel.Controls.Add(lblActTimeFilter);
            filterPanel.Controls.Add(cmbActTimeFilter);
            filterPanel.Controls.Add(lblDeactTimeFilter);
            filterPanel.Controls.Add(cmbDeactTimeFilter);
            filterPanel.Dock = DockStyle.Fill;
            filterPanel.Location = new Point(3, 63);
            filterPanel.Name = "filterPanel";
            filterPanel.Padding = new Padding(10, 4, 10, 4);
            filterPanel.Size = new Size(1094, 30);
            filterPanel.TabIndex = 2;
            // 
            // lblCardNumberFilter
            // 
            lblCardNumberFilter.AutoSize = true;
            lblCardNumberFilter.Location = new Point(420, 8);
            lblCardNumberFilter.Name = "lblCardNumberFilter";
            lblCardNumberFilter.Size = new Size(98, 20);
            lblCardNumberFilter.TabIndex = 12;
            lblCardNumberFilter.Text = "Card Number";
            // 
            // txtCardNumberFilter
            // 
            txtCardNumberFilter.Location = new Point(520, 4);
            txtCardNumberFilter.Name = "txtCardNumberFilter";
            txtCardNumberFilter.PlaceholderText = "Filter card no";
            txtCardNumberFilter.Size = new Size(160, 27);
            txtCardNumberFilter.TabIndex = 13;
            // 
            // lblActTimeFilter
            // 
            lblActTimeFilter.AutoSize = true;
            lblActTimeFilter.Location = new Point(690, 8);
            lblActTimeFilter.Name = "lblActTimeFilter";
            lblActTimeFilter.Size = new Size(68, 20);
            lblActTimeFilter.TabIndex = 14;
            lblActTimeFilter.Text = "Act Time";
            // 
            // cmbActTimeFilter
            // 
            cmbActTimeFilter.Location = new Point(760, 4);
            cmbActTimeFilter.Name = "cmbActTimeFilter";
            cmbActTimeFilter.Size = new Size(110, 28);
            cmbActTimeFilter.TabIndex = 15;
            // 
            // lblDeactTimeFilter
            // 
            lblDeactTimeFilter.AutoSize = true;
            lblDeactTimeFilter.Location = new Point(880, 8);
            lblDeactTimeFilter.Name = "lblDeactTimeFilter";
            lblDeactTimeFilter.Size = new Size(85, 20);
            lblDeactTimeFilter.TabIndex = 16;
            lblDeactTimeFilter.Text = "Deact Time";
            // 
            // cmbDeactTimeFilter
            // 
            cmbDeactTimeFilter.Location = new Point(970, 4);
            cmbDeactTimeFilter.Name = "cmbDeactTimeFilter";
            cmbDeactTimeFilter.Size = new Size(110, 28);
            cmbDeactTimeFilter.TabIndex = 17;
            // 
            // btnClearFilters
            // 
            btnClearFilters.Location = new Point(1006, 12);
            btnClearFilters.Name = "btnClearFilters";
            btnClearFilters.Size = new Size(80, 27);
            btnClearFilters.TabIndex = 18;
            btnClearFilters.Text = "Clear";
            btnClearFilters.UseVisualStyleBackColor = true;
            btnClearFilters.Click += btnClearFilters_Click;
            // 
            // Cards
            // 
            Controls.Add(mainLayout);
            Name = "Cards";
            Size = new Size(1100, 650);
            ((System.ComponentModel.ISupportInitialize)dgvCards).EndInit();
            mainLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            ResumeLayout(false);
        }
        private Label lblSearchRight;
        private TextBox txtSearch;
        private Button btnSearch;
        //private Label lblCardNumberFilter;
        //private TextBox txtCardNumberFilter;
        //private Label lblActTimeFilter;
        //private ComboBox cmbActTimeFilter;
        //private Label lblDeactTimeFilter;
        //private ComboBox cmbDeactTimeFilter;

    }
}
