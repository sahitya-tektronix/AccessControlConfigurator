using System.Windows.Forms;

namespace AccessControlConfigurator.Controls
{
    public partial class CardholdersControl : UserControl
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;

        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnRefresh;
        private Button btnBack;

        private DataGridView dgvCardholders;

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
            btnRefresh = new Button();
            btnBack = new Button();
            dgvCardholders = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            lblSearchRight = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            topPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvCardholders).BeginInit();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(0, 2);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(152, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Cardholders";
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(164, 2);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 30);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(250, 2);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 30);
            btnEdit.TabIndex = 1;
            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(336, 2);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(422, 2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 30);
            btnRefresh.TabIndex = 3;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(508, 2);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 30);
            btnBack.TabIndex = 4;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            // 
            // dgvCardholders
            // 
            dgvCardholders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCardholders.BackgroundColor = Color.White;
            dgvCardholders.BorderStyle = BorderStyle.None;
            dgvCardholders.ColumnHeadersHeight = 29;
            dgvCardholders.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7 });
            dgvCardholders.Dock = DockStyle.Fill;
            dgvCardholders.Location = new Point(0, 32);
            dgvCardholders.MultiSelect = false;
            dgvCardholders.Name = "dgvCardholders";
            dgvCardholders.ReadOnly = true;
            dgvCardholders.RowHeadersWidth = 51;
            dgvCardholders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCardholders.Size = new Size(960, 518);
            dgvCardholders.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "ID";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "First Name";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Last Name";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "User Name";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.Visible = false;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Email";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Mobile";
            dataGridViewTextBoxColumn6.MinimumWidth = 6;
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "Active";
            dataGridViewTextBoxColumn7.MinimumWidth = 6;
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // lblSearchRight
            // 
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(691, 9);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Size = new Size(53, 20);
            lblSearchRight.TabIndex = 10;
            lblSearchRight.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(750, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Size = new Size(137, 27);
            txtSearch.TabIndex = 11;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(893, 5);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(36, 29);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "🔍";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.White;
            topPanel.Controls.Add(btnAdd);
            topPanel.Controls.Add(lblTitle);
            topPanel.Controls.Add(btnSearch);
            topPanel.Controls.Add(btnEdit);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnDelete);
            topPanel.Controls.Add(lblSearchRight);
            topPanel.Controls.Add(btnRefresh);
            topPanel.Controls.Add(btnBack);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(960, 32);
            topPanel.TabIndex = 3;
            // 
            // CardholdersControl
            // 
            Controls.Add(dgvCardholders);
            Controls.Add(topPanel);
            Name = "CardholdersControl";
            Size = new Size(960, 550);
            ((System.ComponentModel.ISupportInitialize)dgvCardholders).EndInit();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ResumeLayout(false);
        }
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private Label lblSearchRight;
        private TextBox txtSearch;
        private Button btnSearch;
        private Panel topPanel;
    }
}