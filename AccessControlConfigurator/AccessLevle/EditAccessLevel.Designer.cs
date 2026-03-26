using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class EditAccessLevelForm
    {
        private Panel headerPanel;
        private Label lblHeader;
        private Label lblName;
        private Label lblDoor;
        private Label lblTimeZone;

        private TextBox txtName;
        //private TextBox txtDescription;

        private ComboBox cmbAcr;
        private ComboBox cmbTimeZone;

        private Button btnupdate;
        private Button btnCancel;

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblHeader = new Label();
            lblName = new Label();
            lblDoor = new Label();
            lblTimeZone = new Label();
            txtName = new TextBox();
            cmbAcr = new ComboBox();
            cmbTimeZone = new ComboBox();
            btnupdate = new Button();
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
            lblHeader.Location = new Point(85, 15);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(232, 32);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "EDIT ACCESS LEVEL";
            // 
            // lblName
            // 
            lblName.Font = new Font("Segoe UI", 10F);
            lblName.Location = new Point(40, 90);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 1;
            lblName.Text = "Access Level";
            // 
            // lblDoor
            // 
            lblDoor.Font = new Font("Segoe UI", 10F);
            lblDoor.Location = new Point(40, 163);
            lblDoor.Name = "lblDoor";
            lblDoor.Size = new Size(100, 23);
            lblDoor.TabIndex = 5;
            lblDoor.Text = "Door (ACR)";
            // 
            // lblTimeZone
            // 
            lblTimeZone.Font = new Font("Segoe UI", 10F);
            lblTimeZone.Location = new Point(40, 259);
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
            cmbAcr.Location = new Point(40, 209);
            cmbAcr.Name = "cmbAcr";
            cmbAcr.Size = new Size(300, 28);
            cmbAcr.TabIndex = 6;
            // 
            // cmbTimeZone
            // 
            cmbTimeZone.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeZone.Location = new Point(40, 297);
            cmbTimeZone.Name = "cmbTimeZone";
            cmbTimeZone.Size = new Size(300, 28);
            cmbTimeZone.TabIndex = 8;
            // 
            // btnupdate
            // 
            btnupdate.BackColor = Color.Green;
            btnupdate.ForeColor = Color.White;
            btnupdate.Location = new Point(80, 360);
            btnupdate.Name = "btnupdate";
            btnupdate.Size = new Size(100, 35);
            btnupdate.TabIndex = 9;
            btnupdate.Text = "Update";
            btnupdate.UseVisualStyleBackColor = false;
            btnupdate.Click += btnupdate_ClickAsync;
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
            // EditAccessLevelForm
            // 
            ClientSize = new Size(380, 430);
            Controls.Add(headerPanel);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblDoor);
            Controls.Add(cmbAcr);
            Controls.Add(lblTimeZone);
            Controls.Add(cmbTimeZone);
            Controls.Add(btnupdate);
            Controls.Add(btnCancel);
            Name = "EditAccessLevelForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Access Level";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}