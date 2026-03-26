namespace AccessControlConfigurator.Forms
{
    partial class EditControllerControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblHeader;


        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.GroupBox grpController;

        private System.Windows.Forms.TextBox txtMac;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;

        private System.Windows.Forms.Label lblMac;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblName;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnAddSIO;

        private System.Windows.Forms.Panel panelAdd;
        private System.Windows.Forms.Panel panelGrid;

        private System.Windows.Forms.DataGridView dgvSIO;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            btnBack = new Button();
            lblHeader = new Label();
            panelBody = new Panel();
            panelGrid = new Panel();
            dgvSIO = new DataGridView();
            panelAdd = new Panel();
            btnAddSIO = new Button();
            grpController = new GroupBox();
            lblMac = new Label();
            txtMac = new TextBox();
            lblIP = new Label();
            txtIP = new TextBox();
            lblId = new Label();
            txtId = new TextBox();
            lblName = new Label();
            txtName = new TextBox();
            btnSave = new Button();
            colHardwareId = new DataGridViewTextBoxColumn();
            colSioNumber = new DataGridViewTextBoxColumn();
            colsioname = new DataGridViewTextBoxColumn();
            colType = new DataGridViewTextBoxColumn();
            colPort = new DataGridViewTextBoxColumn();
            colAddress = new DataGridViewTextBoxColumn();
            colStatus = new DataGridViewTextBoxColumn();
            colOnline = new DataGridViewTextBoxColumn();
            colEdit = new DataGridViewButtonColumn();
            colDelete = new DataGridViewButtonColumn();
            panelHeader.SuspendLayout();
            panelBody.SuspendLayout();
            panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSIO).BeginInit();
            panelAdd.SuspendLayout();
            grpController.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.WhiteSmoke;
            panelHeader.Controls.Add(btnBack);
            panelHeader.Controls.Add(lblHeader);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 60);
            panelHeader.TabIndex = 1;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.DarkOliveGreen;
            btnBack.Location = new Point(1091, 15);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 31);
            btnBack.TabIndex = 1;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeader.Location = new Point(15, 15);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(265, 37);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Aero Configuration";
            // 
            // panelBody
            // 
            panelBody.BackColor = Color.White;
            panelBody.Controls.Add(panelGrid);
            panelBody.Controls.Add(panelAdd);
            panelBody.Controls.Add(grpController);
            panelBody.Dock = DockStyle.Fill;
            panelBody.Location = new Point(0, 60);
            panelBody.Name = "panelBody";
            panelBody.Size = new Size(1200, 740);
            panelBody.TabIndex = 0;
            // 
            // panelGrid
            // 
            panelGrid.Controls.Add(dgvSIO);
            panelGrid.Dock = DockStyle.Fill;
            panelGrid.Location = new Point(0, 230);
            panelGrid.Name = "panelGrid";
            panelGrid.Padding = new Padding(15);
            panelGrid.Size = new Size(1200, 510);
            panelGrid.TabIndex = 0;
            // 
            // dgvSIO
            // 
            dgvSIO.AllowUserToAddRows = false;
            dgvSIO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSIO.ColumnHeadersHeight = 29;
            dgvSIO.Columns.AddRange(new DataGridViewColumn[] { colHardwareId, colSioNumber, colsioname, colType, colPort, colAddress, colStatus, colOnline, colEdit, colDelete });
            dgvSIO.Dock = DockStyle.Fill;
            dgvSIO.Location = new Point(15, 15);
            dgvSIO.Name = "dgvSIO";
            dgvSIO.RowHeadersVisible = false;
            dgvSIO.RowHeadersWidth = 51;
            dgvSIO.Size = new Size(1170, 480);
            dgvSIO.TabIndex = 0;
            // 
            // panelAdd
            // 
            panelAdd.Controls.Add(btnAddSIO);
            panelAdd.Dock = DockStyle.Top;
            panelAdd.Location = new Point(0, 180);
            panelAdd.Name = "panelAdd";
            panelAdd.Padding = new Padding(15, 8, 0, 8);
            panelAdd.Size = new Size(1200, 50);
            panelAdd.TabIndex = 1;
            // 
            // btnAddSIO
            // 
            btnAddSIO.Dock = DockStyle.Left;
            btnAddSIO.Location = new Point(15, 8);
            btnAddSIO.Name = "btnAddSIO";
            btnAddSIO.Size = new Size(150, 34);
            btnAddSIO.TabIndex = 0;
            btnAddSIO.Text = "Add New SIO";
            // 
            // grpController
            // 
            grpController.Controls.Add(lblMac);
            grpController.Controls.Add(txtMac);
            grpController.Controls.Add(lblIP);
            grpController.Controls.Add(txtIP);
            grpController.Controls.Add(lblId);
            grpController.Controls.Add(txtId);
            grpController.Controls.Add(lblName);
            grpController.Controls.Add(txtName);
            grpController.Controls.Add(btnSave);
            grpController.Dock = DockStyle.Top;
            grpController.Location = new Point(0, 0);
            grpController.Name = "grpController";
            grpController.Padding = new Padding(15);
            grpController.Size = new Size(1200, 180);
            grpController.TabIndex = 2;
            grpController.TabStop = false;
            grpController.Text = "Controller";
            // 
            // lblMac
            // 
            lblMac.Location = new Point(20, 35);
            lblMac.Name = "lblMac";
            lblMac.Size = new Size(100, 23);
            lblMac.TabIndex = 0;
            lblMac.Text = "MAC Address";
            // 
            // txtMac
            // 
            txtMac.Location = new Point(150, 32);
            txtMac.Name = "txtMac";
            txtMac.Size = new Size(220, 27);
            txtMac.TabIndex = 1;
            // 
            // lblIP
            // 
            lblIP.Location = new Point(420, 35);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(100, 23);
            lblIP.TabIndex = 2;
            lblIP.Text = "IP Address";
            // 
            // txtIP
            // 
            txtIP.Location = new Point(520, 32);
            txtIP.Name = "txtIP";
            txtIP.Size = new Size(220, 27);
            txtIP.TabIndex = 3;
            // 
            // lblId
            // 
            lblId.Location = new Point(20, 72);
            lblId.Name = "lblId";
            lblId.Size = new Size(100, 23);
            lblId.TabIndex = 6;
            lblId.Text = "SCPID";
            // 
            // txtId
            // 
            txtId.Location = new Point(150, 72);
            txtId.Name = "txtId";
            txtId.Size = new Size(220, 27);
            txtId.TabIndex = 7;
            // 
            // lblName
            // 
            lblName.Location = new Point(20, 115);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 8;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(150, 112);
            txtName.Name = "txtName";
            txtName.Size = new Size(590, 27);
            txtName.TabIndex = 9;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(150, 140);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 30);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // colHardwareId
            // 
            colHardwareId.DataPropertyName = "Id";
            colHardwareId.HeaderText = "Hardware ID";
            colHardwareId.MinimumWidth = 6;
            colHardwareId.Name = "colHardwareId";
            // 
            // colSioNumber
            // 
            colSioNumber.DataPropertyName = "SioNumber";
            colSioNumber.HeaderText = "SIO Number";
            colSioNumber.MinimumWidth = 6;
            colSioNumber.Name = "colSioNumber";
            // 
            // colsioname
            // 
            colsioname.DataPropertyName = "Name";
            colsioname.HeaderText = "Sio Name";
            colsioname.MinimumWidth = 6;
            colsioname.Name = "colsioname";
            // 
            // colType
            // 
            colType.DataPropertyName = "Type";
            colType.HeaderText = "Type";
            colType.MinimumWidth = 6;
            colType.Name = "colType";
            // 
            // colPort
            // 
            colPort.DataPropertyName = "Port";
            colPort.HeaderText = "Port";
            colPort.MinimumWidth = 6;
            colPort.Name = "colPort";
            // 
            // colAddress
            // 
            colAddress.DataPropertyName = "Address";
            colAddress.HeaderText = "Address";
            colAddress.MinimumWidth = 6;
            colAddress.Name = "colAddress";
            // 
            // colStatus
            // 
            colStatus.DataPropertyName = "Status";
            colStatus.HeaderText = "Status";
            colStatus.MinimumWidth = 6;
            colStatus.Name = "colStatus";
            // 
            // colOnline
            // 
            colOnline.HeaderText = "Online";
            colOnline.MinimumWidth = 6;
            colOnline.Name = "colOnline";
            // 
            // colEdit
            // 
            colEdit.HeaderText = "";
            colEdit.MinimumWidth = 6;
            colEdit.Name = "colEdit";
            colEdit.Text = "Edit";
            colEdit.UseColumnTextForButtonValue = true;
            // 
            // colDelete
            // 
            colDelete.HeaderText = "";
            colDelete.MinimumWidth = 6;
            colDelete.Name = "colDelete";
            colDelete.Text = "Delete";
            colDelete.UseColumnTextForButtonValue = true;
            // 
            // EditControllerControl
            // 
            BackColor = Color.White;
            Controls.Add(panelBody);
            Controls.Add(panelHeader);
            Name = "EditControllerControl";
            Size = new Size(1200, 800);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelBody.ResumeLayout(false);
            panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSIO).EndInit();
            panelAdd.ResumeLayout(false);
            grpController.ResumeLayout(false);
            grpController.PerformLayout();
            ResumeLayout(false);

        }
        private System.Windows.Forms.Button btnBack;
        private DataGridViewTextBoxColumn colHardwareId;
        private DataGridViewTextBoxColumn colSioNumber;
        private DataGridViewTextBoxColumn colsioname;
        private DataGridViewTextBoxColumn colType;
        private DataGridViewTextBoxColumn colPort;
        private DataGridViewTextBoxColumn colAddress;
        private DataGridViewTextBoxColumn colStatus;
        private DataGridViewTextBoxColumn colOnline;
        private DataGridViewButtonColumn colEdit;
        private DataGridViewButtonColumn colDelete;
    }
}