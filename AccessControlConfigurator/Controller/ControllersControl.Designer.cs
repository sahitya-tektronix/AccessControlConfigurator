using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    partial class ControllersControl
    {
        private Panel contentPanel;
        private Panel topPanel;
        private Label lblTitle;
        private FlowLayoutPanel buttonPanel;

        private Button btnDiscover;
        private Button btnSync;
        private Button btnSyncOnline;
        private Button btnback;

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            contentPanel = new Panel();
            dgvControllers = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colMac = new DataGridViewTextBoxColumn();
            colIp = new DataGridViewTextBoxColumn();
            colOnline = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colLastSyncStart = new DataGridViewTextBoxColumn();
            colLastSyncEnd = new DataGridViewTextBoxColumn();
            colEnable = new DataGridViewCheckBoxColumn();
            colEdit = new DataGridViewButtonColumn();
            colDelete = new DataGridViewButtonColumn();
            topPanel = new Panel();
            btnAdd = new Button();
            btnSearch = new Button();
            lblSearchRight = new Label();
            txtSearch = new TextBox();
            btnback = new Button();
            labelC = new Label();
            btnSyncOnline = new Button();
            btnSync = new Button();
            btnDiscover = new Button();
            label1 = new Label();
            contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvControllers).BeginInit();
            topPanel.SuspendLayout();
            SuspendLayout();
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.WhiteSmoke;
            contentPanel.Controls.Add(dgvControllers);
            contentPanel.Controls.Add(topPanel);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1857, 804);
            contentPanel.TabIndex = 0;
            // 
            // dgvControllers
            // 
            dgvControllers.AllowUserToResizeColumns = false;
            dgvControllers.AllowUserToResizeRows = false;
            dgvControllers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvControllers.BackgroundColor = Color.White;
            dgvControllers.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(45, 62, 80);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvControllers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvControllers.ColumnHeadersHeight = 29;
            dgvControllers.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colMac, colIp, colOnline, colStatus, colLastSyncStart, colLastSyncEnd, colEnable, colEdit, colDelete });
            dgvControllers.Dock = DockStyle.Fill;
            dgvControllers.EnableHeadersVisualStyles = false;
            dgvControllers.Location = new Point(0, 70);
            dgvControllers.Name = "dgvControllers";
            dgvControllers.RowHeadersVisible = false;
            dgvControllers.RowHeadersWidth = 51;
            dgvControllers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvControllers.Size = new Size(1857, 734);
            dgvControllers.TabIndex = 0;
            // 
            // colId
            // 
            colId.HeaderText = "ID";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            // 
            // colName
            // 
            colName.HeaderText = "Controller Name";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            // 
            // colMac
            // 
            colMac.HeaderText = "MAC Address";
            colMac.MinimumWidth = 6;
            colMac.Name = "colMac";
            // 
            // colIp
            // 
            colIp.HeaderText = "IP Address";
            colIp.MinimumWidth = 6;
            colIp.Name = "colIp";
            // 
            // colOnline
            // 
            colOnline.HeaderText = "Online";
            colOnline.MinimumWidth = 6;
            colOnline.Name = "colOnline";
            // 
            // colStatus
            // 
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            // 
            // colLastSyncStart
            // 
            colLastSyncStart.HeaderText = "Last Sync Started";
            colLastSyncStart.MinimumWidth = 6;
            colLastSyncStart.Name = "colLastSyncStart";
            // 
            // colLastSyncEnd
            // 
            colLastSyncEnd.HeaderText = "Last Sync Completed";
            colLastSyncEnd.MinimumWidth = 6;
            colLastSyncEnd.Name = "colLastSyncEnd";
            // 
            // colEnable
            // 
            colEnable.HeaderText = "Enabled";
            colEnable.MinimumWidth = 6;
            colEnable.Name = "colEnable";
            // 
            // colEdit
            // 
            colEdit.HeaderText = "";
            colEdit.MinimumWidth = 6;
            colEdit.Name = "colEdit";
            colEdit.Text = "✏";
            colEdit.UseColumnTextForButtonValue = true;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "";
            colDelete.MinimumWidth = 6;
            colDelete.Name = "colDelete";
            colDelete.Text = "🗑";
            colDelete.UseColumnTextForButtonValue = true;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.White;
            topPanel.Controls.Add(btnAdd);
            topPanel.Controls.Add(btnSearch);
            topPanel.Controls.Add(lblSearchRight);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnback);
            topPanel.Controls.Add(labelC);
            topPanel.Controls.Add(btnSyncOnline);
            topPanel.Controls.Add(btnSync);
            topPanel.Controls.Add(btnDiscover);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(10);
            topPanel.Size = new Size(1857, 70);
            topPanel.TabIndex = 1;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(163, 19);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 35);
            btnAdd.TabIndex = 9;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = SystemColors.Control;
            btnSearch.Location = new Point(1296, 25);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(36, 29);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "🔍";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            // 
            // lblSearchRight
            // 
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(985, 30);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Size = new Size(53, 20);
            lblSearchRight.TabIndex = 7;
            lblSearchRight.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(1044, 27);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Size = new Size(246, 27);
            txtSearch.TabIndex = 6;
            // 
            // btnback
            // 
            btnback.FlatStyle = FlatStyle.Flat;
            btnback.Location = new Point(665, 20);
            btnback.Margin = new Padding(5);
            btnback.Name = "btnback";
            btnback.Size = new Size(90, 32);
            btnback.TabIndex = 3;
            btnback.Text = "Back";
            btnback.Click += btnback_Click;
            // 
            // labelC
            // 
            labelC.AutoSize = true;
            labelC.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelC.Location = new Point(13, 17);
            labelC.Name = "labelC";
            labelC.Size = new Size(133, 31);
            labelC.TabIndex = 1;
            labelC.Text = "Controllers";
            // 
            // btnSyncOnline
            // 
            btnSyncOnline.FlatStyle = FlatStyle.Flat;
            btnSyncOnline.Location = new Point(485, 20);
            btnSyncOnline.Margin = new Padding(5);
            btnSyncOnline.Name = "btnSyncOnline";
            btnSyncOnline.Size = new Size(170, 32);
            btnSyncOnline.TabIndex = 2;
            btnSyncOnline.Text = "Sync Online/Offline";
            btnSyncOnline.Click += btnSyncOnlineOffline_Click;
            // 
            // btnSync
            // 
            btnSync.FlatStyle = FlatStyle.Flat;
            btnSync.Location = new Point(385, 20);
            btnSync.Margin = new Padding(5);
            btnSync.Name = "btnSync";
            btnSync.Size = new Size(90, 32);
            btnSync.TabIndex = 1;
            btnSync.Text = "Sync";
            btnSync.Click += BtnSync_Click;
            // 
            // btnDiscover
            // 
            btnDiscover.FlatStyle = FlatStyle.Flat;
            btnDiscover.Location = new Point(265, 20);
            btnDiscover.Margin = new Padding(5);
            btnDiscover.Name = "btnDiscover";
            btnDiscover.Size = new Size(110, 32);
            btnDiscover.TabIndex = 0;
            btnDiscover.Text = "Discover";
            btnDiscover.Click += BDiscover_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(542, 323);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 4;
            label1.Text = "label1";
            // 
            // ControllersControl
            // 
            Controls.Add(contentPanel);
            Name = "ControllersControl";
            Size = new Size(1857, 804);
            contentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvControllers).EndInit();
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ResumeLayout(false);
        }
        private Label lblController;
        private Label labelC;
        private Label label1;
        private TextBox txtSearch;
        private Label lblSearchRight;
        private Button btnSearch;
        private DataGridView dgvControllers;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colMac;
        private DataGridViewTextBoxColumn colIp;
        private DataGridViewTextBoxColumn colOnline;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colLastSyncStart;
        private DataGridViewTextBoxColumn colLastSyncEnd;
        private DataGridViewCheckBoxColumn colEnable;
        private DataGridViewButtonColumn colEdit;
        private DataGridViewButtonColumn colDelete;
        private Button btnAdd;
    }
}