using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class AddAccessLevelForm
    {
        private Label lblHeader;
        private Label lblName;
        private Label lblDescription;
        private Label lblAcr;
        private Label lblTimeZone;

        private TextBox txtName;
        private TextBox txtDescription;

        private ComboBox cmbAcr;
        private ComboBox cmbTimeZone;

        private Button btnSave;
        private Button btnCancel;

        private Panel headerPanel;

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblHeader = new Label();
            lblName = new Label();
            lblAcr = new Label();
            lblTimeZone = new Label();
            txtName = new TextBox();
            cmbAcr = new ComboBox();
            cmbTimeZone = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.FromArgb(45, 62, 80);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(380, 60);
            headerPanel.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(90, 15);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(232, 32);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "ADD ACCESS LEVEL";
            // 
            // lblName
            // 
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.Location = new Point(40, 90);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 1;
            lblName.Text = "Access Level Name";
            // 
            // lblAcr
            // 
            lblAcr.Font = new Font("Segoe UI", 10F);
            lblAcr.Location = new Point(40, 177);
            lblAcr.Name = "lblAcr";
            lblAcr.Size = new Size(100, 23);
            lblAcr.TabIndex = 5;
            lblAcr.Text = "Door (ACR)";
            // 
            // lblTimeZone
            // 
            lblTimeZone.Font = new Font("Segoe UI", 10F);
            lblTimeZone.Location = new Point(40, 285);
            lblTimeZone.Name = "lblTimeZone";
            lblTimeZone.Size = new Size(100, 23);
            lblTimeZone.TabIndex = 7;
            lblTimeZone.Text = "Time Zone";
            // 
            // txtName
            // 
            txtName.Location = new Point(40, 115);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 27);
            txtName.TabIndex = 2;
            // 
            // cmbAcr
            // 
            cmbAcr.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAcr.Location = new Point(40, 213);
            cmbAcr.Name = "cmbAcr";
            cmbAcr.Size = new Size(300, 28);
            cmbAcr.TabIndex = 6;
            // 
            // cmbTimeZone
            // 
            cmbTimeZone.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeZone.Location = new Point(40, 310);
            cmbTimeZone.Name = "cmbTimeZone";
            cmbTimeZone.Size = new Size(300, 28);
            cmbTimeZone.TabIndex = 8;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.Green;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(80, 360);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Red;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(200, 360);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // AddAccessLevelForm
            // 
            ClientSize = new Size(380, 430);
            Controls.Add(headerPanel);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblAcr);
            Controls.Add(cmbAcr);
            Controls.Add(lblTimeZone);
            Controls.Add(cmbTimeZone);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "AddAccessLevelForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Access Level";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}