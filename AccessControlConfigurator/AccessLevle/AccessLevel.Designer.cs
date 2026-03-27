using System.Windows.Forms;
using System.Drawing;

namespace AccessControlConfigurator
{
    partial class AccessLevel
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Panel panelFilter;

        private Panel panelContent;

        private Label lblTitle;

        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSync;
        private Button btnRefresh;
        private Button btnBack;
        private Label lblNameFilter;
        private ComboBox cmbNameFilter;


        private DataGridView dgvAccessLevels;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            panelHeader = new Panel();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearchRight = new Label();
            lblTitle = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSync = new Button();
            btnRefresh = new Button();
            btnBack = new Button();
            panelFilter = new Panel();
            lblNameFilter = new Label();
            cmbNameFilter = new ComboBox();
            panelContent = new Panel();
            dgvAccessLevels = new DataGridView();
            panelHeader.SuspendLayout();
            panelFilter.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccessLevels).BeginInit();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.White;
            panelHeader.Controls.Add(btnSearch);
            panelHeader.Controls.Add(txtSearch);
            panelHeader.Controls.Add(lblSearchRight);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(btnAdd);
            panelHeader.Controls.Add(btnEdit);
            panelHeader.Controls.Add(btnDelete);
            panelHeader.Controls.Add(btnSync);
            panelHeader.Controls.Add(btnRefresh);
            panelHeader.Controls.Add(btnBack);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(10, 5, 10, 5);
            panelHeader.Size = new Size(1000, 45);
            panelHeader.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(961, 10);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(36, 29);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "🔍";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(818, 11);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Size = new Size(137, 27);
            txtSearch.TabIndex = 7;
            // 
            // lblSearchRight
            // 
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(760, 14);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Size = new Size(53, 20);
            lblSearchRight.TabIndex = 8;
            lblSearchRight.Text = "Search";
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 5);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(167, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Access Levels";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(188, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 28);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(274, 8);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 28);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(360, 8);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 28);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSync
            // 
            btnSync.Location = new Point(445, 8);
            btnSync.Name = "btnSync";
            btnSync.Size = new Size(80, 28);
            btnSync.TabIndex = 4;
            btnSync.Text = "Sync";
            btnSync.Click += btnSync_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(530, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 28);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(615, 8);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(70, 28);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            // 
            // panelFilter
            // 
            panelFilter.BackColor = Color.White;
            panelFilter.Controls.Add(lblNameFilter);
            panelFilter.Controls.Add(cmbNameFilter);
            panelFilter.Dock = DockStyle.Top;
            panelFilter.Location = new Point(0, 45);
            panelFilter.Name = "panelFilter";
            panelFilter.Padding = new Padding(10, 4, 10, 4);
            panelFilter.Size = new Size(1000, 36);
            panelFilter.TabIndex = 2;
            // 
            // lblNameFilter
            // 
            lblNameFilter.AutoSize = true;
            lblNameFilter.Location = new Point(690, 9);
            lblNameFilter.Name = "lblNameFilter";
            lblNameFilter.Size = new Size(49, 20);
            lblNameFilter.TabIndex = 10;
            lblNameFilter.Text = "Name";
            // 
            // cmbNameFilter
            // 
            cmbNameFilter.Location = new Point(745, 6);
            cmbNameFilter.Name = "cmbNameFilter";
            cmbNameFilter.Size = new Size(240, 28);
            cmbNameFilter.TabIndex = 11;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(dgvAccessLevels);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 81);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(15, 10, 15, 10);
            panelContent.Size = new Size(1000, 519);
            panelContent.TabIndex = 0;
            // 
            // dgvAccessLevels
            // 
            dgvAccessLevels.AllowUserToAddRows = false;
            dgvAccessLevels.AllowUserToDeleteRows = false;
            dgvAccessLevels.AllowUserToResizeColumns = false;
            dgvAccessLevels.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 248, 248);
            dgvAccessLevels.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvAccessLevels.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccessLevels.BackgroundColor = Color.White;
            dgvAccessLevels.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvAccessLevels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvAccessLevels.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new Padding(5, 3, 5, 3);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvAccessLevels.DefaultCellStyle = dataGridViewCellStyle3;
            dgvAccessLevels.Dock = DockStyle.Fill;
            dgvAccessLevels.EnableHeadersVisualStyles = false;
            dgvAccessLevels.Location = new Point(15, 10);
            dgvAccessLevels.Name = "dgvAccessLevels";
            dgvAccessLevels.RowHeadersVisible = false;
            dgvAccessLevels.RowHeadersWidth = 51;
            dgvAccessLevels.RowTemplate.Height = 30;
            dgvAccessLevels.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccessLevels.Size = new Size(970, 499);
            dgvAccessLevels.TabIndex = 0;
            // 
            // AccessLevel
            // 
            Controls.Add(panelContent);
            Controls.Add(panelFilter);
            Controls.Add(panelHeader);
            Name = "AccessLevel";
            Size = new Size(1000, 600);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAccessLevels).EndInit();
            ResumeLayout(false);
        }
        private TextBox txtSearch;
        private Label lblSearchRight;
        private Button btnSearch;
    }
}