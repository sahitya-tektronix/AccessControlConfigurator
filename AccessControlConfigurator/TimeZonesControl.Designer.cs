using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class TimeZonesControl
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
        private Button btnApply;
        private Button btnQuery;

        private Button btnRefresh;
        private Button btnback;
        private Label lblNameFilter;
        private ComboBox cmbNameFilter;

        private DataGridView dgvTimeZones;

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
            btnback = new Button();
            panelFilter = new Panel();
            lblNameFilter = new Label();
            cmbNameFilter = new ComboBox();
            panelContent = new Panel();
            dgvTimeZones = new DataGridView();
            panelHeader.SuspendLayout();
            panelFilter.SuspendLayout();
            panelContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTimeZones).BeginInit();
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
            panelHeader.Controls.Add(btnback);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(10, 5, 10, 5);
            panelHeader.Size = new Size(1000, 45);
            panelHeader.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(951, 7);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(36, 29);
            btnSearch.TabIndex = 11;
            btnSearch.Text = "🔍";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(741, 8);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Size = new Size(204, 27);
            txtSearch.TabIndex = 10;
            // 
            // lblSearchRight
            // 
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(691, 12);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Size = new Size(53, 20);
            lblSearchRight.TabIndex = 9;
            lblSearchRight.Text = "Search";
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Left;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 5);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(159, 35);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Time Zones";
            lblTitle.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(175, 8);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(80, 28);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(261, 9);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 28);
            btnEdit.TabIndex = 2;
            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(347, 9);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 28);
            btnDelete.TabIndex = 3;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnSync
            // 
            btnSync.Location = new Point(433, 9);
            btnSync.Name = "btnSync";
            btnSync.Size = new Size(90, 28);
            btnSync.TabIndex = 4;
            btnSync.Text = "Sync HID";
            btnSync.Click += btnSync_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(529, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 28);
            btnRefresh.TabIndex = 7;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnback
            // 
            btnback.Location = new Point(615, 8);
            btnback.Name = "btnback";
            btnback.Size = new Size(70, 28);
            btnback.TabIndex = 8;
            btnback.Text = "Back";
            btnback.Click += btnback_Click;
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
            lblNameFilter.Location = new Point(691, 5);
            lblNameFilter.Name = "lblNameFilter";
            lblNameFilter.Size = new Size(49, 20);
            lblNameFilter.TabIndex = 12;
            lblNameFilter.Text = "Name";
            // 
            // cmbNameFilter
            // 
            cmbNameFilter.Location = new Point(747, 2);
            cmbNameFilter.Name = "cmbNameFilter";
            cmbNameFilter.Size = new Size(240, 28);
            cmbNameFilter.TabIndex = 13;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(dgvTimeZones);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(0, 81);
            panelContent.Name = "panelContent";
            panelContent.Padding = new Padding(15, 10, 15, 10);
            panelContent.Size = new Size(1000, 519);
            panelContent.TabIndex = 0;
            // 
            // dgvTimeZones
            // 
            dgvTimeZones.AllowUserToAddRows = false;
            dgvTimeZones.AllowUserToDeleteRows = false;
            dgvTimeZones.AllowUserToResizeColumns = false;
            dgvTimeZones.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(248, 248, 248);
            dgvTimeZones.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvTimeZones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTimeZones.BackgroundColor = Color.White;
            dgvTimeZones.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvTimeZones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvTimeZones.ColumnHeadersHeight = 35;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle3.Padding = new Padding(5, 3, 5, 3);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvTimeZones.DefaultCellStyle = dataGridViewCellStyle3;
            dgvTimeZones.Dock = DockStyle.Fill;
            dgvTimeZones.EnableHeadersVisualStyles = false;
            dgvTimeZones.Location = new Point(15, 10);
            dgvTimeZones.Name = "dgvTimeZones";
            dgvTimeZones.RowHeadersVisible = false;
            dgvTimeZones.RowHeadersWidth = 51;
            dgvTimeZones.RowTemplate.Height = 30;
            dgvTimeZones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTimeZones.Size = new Size(970, 499);
            dgvTimeZones.TabIndex = 0;
            // 
            // TimeZonesControl
            // 
            Controls.Add(panelContent);
            Controls.Add(panelFilter);
            Controls.Add(panelHeader);
            Name = "TimeZonesControl";
            Size = new Size(1000, 600);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelFilter.ResumeLayout(false);
            panelFilter.PerformLayout();
            panelContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTimeZones).EndInit();
            ResumeLayout(false);
        }
        private Label lblSearchRight;
        private TextBox txtSearch;
        private Button btnSearch;
    }
}