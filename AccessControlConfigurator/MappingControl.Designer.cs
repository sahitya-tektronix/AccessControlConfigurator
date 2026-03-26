namespace AccessControlConfigurator.Forms
{
    partial class MappingControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.ComboBox cmbController;
        private System.Windows.Forms.ComboBox cmbReader;
        private System.Windows.Forms.TextBox txtDoorName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvMapping;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.cmbController = new System.Windows.Forms.ComboBox();
            this.cmbReader = new System.Windows.Forms.ComboBox();
            this.txtDoorName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvMapping = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlForm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1154, 55);
            this.pnlTop.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(205, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Door Mapping";
            // 
            // pnlForm
            // 
            this.pnlForm.Controls.Add(this.cmbController);
            this.pnlForm.Controls.Add(this.cmbReader);
            this.pnlForm.Controls.Add(this.txtDoorName);
            this.pnlForm.Controls.Add(this.btnAdd);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlForm.Location = new System.Drawing.Point(0, 55);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(10);
            this.pnlForm.Size = new System.Drawing.Size(1154, 80);
            this.pnlForm.TabIndex = 1;
            // 
            // cmbController
            // 
            this.cmbController.Location = new System.Drawing.Point(20, 20);
            this.cmbController.Name = "cmbController";
            this.cmbController.Size = new System.Drawing.Size(180, 24);
            this.cmbController.TabIndex = 0;
            // 
            // cmbReader
            // 
            this.cmbReader.Location = new System.Drawing.Point(220, 20);
            this.cmbReader.Name = "cmbReader";
            this.cmbReader.Size = new System.Drawing.Size(150, 24);
            this.cmbReader.TabIndex = 1;
            // 
            // txtDoorName
            // 
            this.txtDoorName.Location = new System.Drawing.Point(390, 20);
            this.txtDoorName.Name = "txtDoorName";
            this.txtDoorName.Size = new System.Drawing.Size(200, 22);
            this.txtDoorName.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(610, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add Mapping";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvMapping
            // 
            this.dgvMapping.AllowUserToAddRows = false;
            this.dgvMapping.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMapping.BackgroundColor = System.Drawing.Color.White;
            this.dgvMapping.ColumnHeadersHeight = 29;
            this.dgvMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMapping.Location = new System.Drawing.Point(0, 135);
            this.dgvMapping.Name = "dgvMapping";
            this.dgvMapping.RowHeadersVisible = false;
            this.dgvMapping.RowHeadersWidth = 51;
            this.dgvMapping.Size = new System.Drawing.Size(1154, 597);
            this.dgvMapping.TabIndex = 0;
            // 
            // MappingControl
            // 
            this.Controls.Add(this.dgvMapping);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlTop);
            this.Name = "MappingControl";
            this.Size = new System.Drawing.Size(1154, 732);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
