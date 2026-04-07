using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace AccessControlConfigurator
{
    partial class AcrsControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblacr;

        private Button btnBack;
        private Button btnRefresh;
        //private Button btnAdd;
        private Button btnEdit;
        private Button btnClearFilters;
        private ComboBox cmbControllerId;
        private ComboBox cmbSioNumber;
        private ComboBox cmbReader;
        private ComboBox cmbControllerName;
        private ComboBox cmbSioName;
        private ComboBox cmbAcrName;

        private Label lblController;
        private Label lblSio;
        private Label lblReader;
        private Label lblControllerName;
        private Label lblSioName;
        private Label lblAcrName;

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
            lblacr = new Label();
            topPanel = new Panel();
            btnEdit = new Button();
            btnRefresh = new Button();
            btnBack = new Button();
            btnClearFilters = new Button();
            lblController = new Label();
            cmbControllerId = new ComboBox();
            lblSio = new Label();
            cmbSioNumber = new ComboBox();
            lblReader = new Label();
            cmbReader = new ComboBox();
            lblAcrName = new Label();
            cmbAcrName = new ComboBox();
            lblControllerName = new Label();
            cmbControllerName = new ComboBox();
            lblSioName = new Label();
            cmbSioName = new ComboBox();
            lblSearchRight = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();
            dgvAcrs = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAcrs).BeginInit();
            SuspendLayout();
            // 
            // lblacr
            // 
            lblacr.BackColor = Color.FromArgb(245, 245, 245);
            lblacr.Dock = DockStyle.Top;
            lblacr.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblacr.Location = new Point(0, 0);
            lblacr.Name = "lblacr";
            lblacr.Padding = new Padding(10, 0, 0, 0);
            lblacr.Size = new Size(1000, 40);
            lblacr.TabIndex = 5;
            lblacr.Text = "ACRs (Doors)";
            lblacr.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.White;
            topPanel.Controls.Add(btnEdit);
            topPanel.Controls.Add(btnRefresh);
            topPanel.Controls.Add(btnBack);
            topPanel.Controls.Add(lblController);
            topPanel.Controls.Add(cmbControllerId);
            topPanel.Controls.Add(lblSio);
            topPanel.Controls.Add(cmbSioNumber);
            topPanel.Controls.Add(lblReader);
            topPanel.Controls.Add(cmbReader);
            topPanel.Controls.Add(lblAcrName);
            topPanel.Controls.Add(cmbAcrName);
            topPanel.Controls.Add(lblSioName);
            topPanel.Controls.Add(cmbSioName);
            topPanel.Controls.Add(lblControllerName);
            topPanel.Controls.Add(cmbControllerName);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 40);
            topPanel.Name = "topPanel";
            topPanel.Size = new Size(1000, 45);
            topPanel.TabIndex = 1;
            // 
            // btnEdit
            // 
            btnEdit.Location = new Point(10, 8);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(80, 28);
            btnEdit.TabIndex = 0;
            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnRefresh
            // 
            btnRefresh.Location = new Point(100, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(80, 28);
            btnRefresh.TabIndex = 1;
            btnRefresh.Text = "Refresh";
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(190, 8);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(80, 28);
            btnBack.TabIndex = 2;
            btnBack.Text = "Back";
            btnBack.Visible = false;
            btnBack.Click += btnBack_Click;
            // 
            // btnClearFilters
            // 
            btnClearFilters.Location = new Point(911, 3);
            btnClearFilters.Name = "btnClearFilters";
            btnClearFilters.Size = new Size(80, 28);
            btnClearFilters.TabIndex = 9;
            btnClearFilters.Text = "Clear";
            btnClearFilters.Click += btnClearFilters_Click;
            // 
            // lblController
            // 
            lblController.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblController.AutoSize = true;
            lblController.Location = new Point(520, 12);
            lblController.Name = "lblController";
            lblController.Size = new Size(75, 20);
            lblController.TabIndex = 3;
            lblController.Text = "Controller";
            // 
            // cmbControllerId
            // 
            cmbControllerId.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbControllerId.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbControllerId.Location = new Point(600, 8);
            cmbControllerId.Name = "cmbControllerId";
            cmbControllerId.Size = new Size(80, 28);
            cmbControllerId.TabIndex = 4;
            // 
            // lblSio
            // 
            lblSio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSio.AutoSize = true;
            lblSio.Location = new Point(700, 12);
            lblSio.Name = "lblSio";
            lblSio.Size = new Size(30, 20);
            lblSio.TabIndex = 5;
            lblSio.Text = "Sio";
            // 
            // cmbSioNumber
            // 
            cmbSioNumber.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbSioNumber.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSioNumber.Location = new Point(740, 8);
            cmbSioNumber.Name = "cmbSioNumber";
            cmbSioNumber.Size = new Size(70, 28);
            cmbSioNumber.TabIndex = 6;
            // 
            // lblReader
            // 
            lblReader.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblReader.AutoSize = true;
            lblReader.Location = new Point(830, 12);
            lblReader.Name = "lblReader";
            lblReader.Size = new Size(56, 20);
            lblReader.TabIndex = 7;
            lblReader.Text = "Reader";
            // 
            // cmbReader
            // 
            cmbReader.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbReader.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbReader.Location = new Point(890, 8);
            cmbReader.Name = "cmbReader";
            cmbReader.Size = new Size(100, 28);
            cmbReader.TabIndex = 8;
            // 
            // lblAcrName
            // 
            lblAcrName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAcrName.AutoSize = true;
            lblAcrName.Location = new Point(240, 40);
            lblAcrName.Name = "lblAcrName";
            lblAcrName.Size = new Size(74, 20);
            lblAcrName.TabIndex = 9;
            lblAcrName.Text = "ACR Name";
            // 
            // cmbAcrName
            // 
            cmbAcrName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbAcrName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAcrName.Location = new Point(320, 36);
            cmbAcrName.Name = "cmbAcrName";
            cmbAcrName.Size = new Size(140, 28);
            cmbAcrName.TabIndex = 10;
            // 
            // lblSioName
            // 
            lblSioName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSioName.AutoSize = true;
            lblSioName.Location = new Point(470, 40);
            lblSioName.Name = "lblSioName";
            lblSioName.Size = new Size(70, 20);
            lblSioName.TabIndex = 11;
            lblSioName.Text = "Sio Name";
            // 
            // cmbSioName
            // 
            cmbSioName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbSioName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSioName.Location = new Point(550, 36);
            cmbSioName.Name = "cmbSioName";
            cmbSioName.Size = new Size(120, 28);
            cmbSioName.TabIndex = 12;
            // 
            // lblControllerName
            // 
            lblControllerName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblControllerName.AutoSize = true;
            lblControllerName.Location = new Point(680, 40);
            lblControllerName.Name = "lblControllerName";
            lblControllerName.Size = new Size(117, 20);
            lblControllerName.TabIndex = 13;
            lblControllerName.Text = "Controller Name";
            // 
            // cmbControllerName
            // 
            cmbControllerName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbControllerName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbControllerName.Location = new Point(800, 36);
            cmbControllerName.Name = "cmbControllerName";
            cmbControllerName.Size = new Size(190, 28);
            cmbControllerName.TabIndex = 14;
            // 
            // lblSearchRight
            // 
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(600, 8);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Size = new Size(53, 20);
            lblSearchRight.TabIndex = 2;
            lblSearchRight.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtSearch.Location = new Point(659, 3);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(200, 27);
            txtSearch.TabIndex = 3;
            // 
            // btnSearch
            // 
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Location = new Point(865, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(40, 27);
            btnSearch.TabIndex = 4;
            btnSearch.Text = "🔍";
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvAcrs
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(245, 245, 245);
            dgvAcrs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvAcrs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAcrs.BackgroundColor = Color.White;
            dgvAcrs.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvAcrs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvAcrs.ColumnHeadersHeight = 35;
            dgvAcrs.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn8, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn9, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6, dataGridViewTextBoxColumn7 });
            dgvAcrs.Dock = DockStyle.Fill;
            dgvAcrs.Location = new Point(0, 85);
            dgvAcrs.Name = "dgvAcrs";
            dgvAcrs.RowHeadersVisible = false;
            dgvAcrs.RowHeadersWidth = 51;
            dgvAcrs.Size = new Size(1000, 515);
            dgvAcrs.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "ID";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "ACR Name";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Controller Id";
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn8
            // 
            dataGridViewTextBoxColumn8.HeaderText = "Controller Name";
            dataGridViewTextBoxColumn8.MinimumWidth = 6;
            dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Sio Number";
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn9
            // 
            dataGridViewTextBoxColumn9.HeaderText = "Sio Name";
            dataGridViewTextBoxColumn9.MinimumWidth = 6;
            dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Reader Name";
            dataGridViewTextBoxColumn5.MinimumWidth = 6;
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "ACR Number";
            dataGridViewTextBoxColumn6.MinimumWidth = 6;
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            dataGridViewTextBoxColumn7.HeaderText = "Online";
            dataGridViewTextBoxColumn7.MinimumWidth = 6;
            dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // AcrsControl
            // 
            Controls.Add(dgvAcrs);
            Controls.Add(topPanel);
            Controls.Add(lblSearchRight);
            Controls.Add(btnClearFilters);
            Controls.Add(txtSearch);
            Controls.Add(btnSearch);
            Controls.Add(lblacr);
            Name = "AcrsControl";
            Size = new Size(1000, 600);
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAcrs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblSearchRight;
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridViewTextBoxColumn colOnline;
        private DataGridViewTextBoxColumn colAcrNumber;
        private DataGridViewTextBoxColumn colReader;
        private DataGridViewTextBoxColumn ColSioNumber;
        private DataGridViewTextBoxColumn ColControllerId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn ColId;
        private DataGridView dgvAcrs;
        private Panel topPanel;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    }
}
