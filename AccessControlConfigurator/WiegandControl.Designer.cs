namespace AccessControlConfigurator
{
    partial class WiegandControl
    {
        private System.Windows.Forms.DataGridView dgvFormats;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;

        private void InitializeComponent()
        {
            dgvFormats = new DataGridView();
            txtSearch = new TextBox();
            btnSearch = new Button();
            btnRefresh = new Button();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvFormats).BeginInit();
            SuspendLayout();
            // 
            // dgvFormats
            // 
            dgvFormats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFormats.ColumnHeadersHeight = 29;
            dgvFormats.Location = new Point(20, 60);
            dgvFormats.Name = "dgvFormats";
            dgvFormats.RowHeadersWidth = 51;
            dgvFormats.Size = new Size(1000, 400);
            dgvFormats.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(700, 20);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 27);
            txtSearch.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(910, 18);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(75, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "🔍";
            btnSearch.Click += btnSearch_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(544, 20);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(75, 27);
            btnRefresh.TabIndex = 6;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(301, 20);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 27);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add";
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(382, 19);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(75, 27);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "Edit";
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(463, 20);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(75, 27);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "Delete";
            // 
            // WiegandControl
            // 
            ClientSize = new Size(1032, 453);
            Controls.Add(dgvFormats);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(btnAdd);
            Controls.Add(btnEdit);
            Controls.Add(btnDelete);
            Controls.Add(btnRefresh);
            Name = "WiegandControl";
            ((System.ComponentModel.ISupportInitialize)dgvFormats).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}