using System.Windows.Forms;
using System.Drawing;

namespace AccessControlConfigurator
{
    partial class AccessLevel
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Panel panelContent;

        private Label lblTitle;

        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSync;
        private Button btnRefresh;
        private Button btnBack;

        private DataGridView dgvAccessLevels;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panelHeader = new Panel();
            lblTitle = new Label();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnSync = new Button();
            btnRefresh = new Button();
            btnBack = new Button();
            panelContent = new Panel();
            dgvAccessLevels = new DataGridView();
            txtSearch = new TextBox();
            lblSearchRight = new Label();
            btnSearch = new Button();
            panelHeader.SuspendLayout();
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
            // panelContent
            // 
            panelContent.Controls.Add(dgvAccessLevels);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 45);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(15, 10, 15, 10);
            panelContent.Size = new Size(1000, 555);
            panelContent.TabIndex = 0;
            // 
            // dgvAccessLevels
            // 
            dgvAccessLevels.AllowUserToAddRows = false;
            dgvAccessLevels.AllowUserToDeleteRows = false;
            dgvAccessLevels.AllowUserToResizeColumns = false;
            dgvAccessLevels.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(248, 248, 248);
            dgvAccessLevels.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dgvAccessLevels.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccessLevels.BackgroundColor = Color.White;
            dgvAccessLevels.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvAccessLevels.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvAccessLevels.ColumnHeadersHeight = 35;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.Padding = new Padding(5, 3, 5, 3);
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvAccessLevels.DefaultCellStyle = dataGridViewCellStyle6;
            dgvAccessLevels.Dock = DockStyle.Fill;
            dgvAccessLevels.EnableHeadersVisualStyles = false;
            dgvAccessLevels.Location = new Point(15, 10);
            dgvAccessLevels.Name = "dgvAccessLevels";
            dgvAccessLevels.RowHeadersVisible = false;
            dgvAccessLevels.RowHeadersWidth = 51;
            dgvAccessLevels.RowTemplate.Height = 30;
            dgvAccessLevels.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccessLevels.Size = new Size(970, 535);
            dgvAccessLevels.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(764, 9);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Size = new Size(137, 27);
            txtSearch.TabIndex = 7;
            // 
            // lblSearchRight
            // 
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(705, 12);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Size = new Size(53, 20);
            lblSearchRight.TabIndex = 8;
            lblSearchRight.Text = "Search";
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(907, 8);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(36, 29);
            btnSearch.TabIndex = 9;
            btnSearch.Text = "🔍";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // AccessLevel
            // 
            Controls.Add(panelContent);
            Controls.Add(panelHeader);
            Name = "AccessLevel";
            Size = new Size(1000, 600);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAccessLevels).EndInit();
            ResumeLayout(false);
        }
        private TextBox txtSearch;
        private Label lblSearchRight;
        private Button btnSearch;
    }
}