using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    public partial class AddControllerForm : Form
    {
        private Label lblTitle;
        private Label lblName, lblMac, lblIp;
        private TextBox txtName, txtMac, txtIp;
        private Button btnSave, btnCancel;

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblName = new Label();
            txtName = new TextBox();
            lblMac = new Label();
            txtMac = new TextBox();
            lblIp = new Label();
            txtIp = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(110, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(184, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add Controller";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(30, 70);
            lblName.Name = "lblName";
            lblName.Size = new Size(119, 20);
            lblName.TabIndex = 1;
            lblName.Text = "Controller Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(150, 65);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 2;
            // 
            // lblMac
            // 
            lblMac.AutoSize = true;
            lblMac.Location = new Point(30, 110);
            lblMac.Name = "lblMac";
            lblMac.Size = new Size(98, 20);
            lblMac.TabIndex = 3;
            lblMac.Text = "MAC Address";
            // 
            // txtMac
            // 
            txtMac.Location = new Point(150, 105);
            txtMac.Name = "txtMac";
            txtMac.Size = new Size(200, 27);
            txtMac.TabIndex = 4;
            // 
            // lblIp
            // 
            lblIp.AutoSize = true;
            lblIp.Location = new Point(30, 150);
            lblIp.Name = "lblIp";
            lblIp.Size = new Size(78, 20);
            lblIp.TabIndex = 5;
            lblIp.Text = "IP Address";
            // 
            // txtIp
            // 
            txtIp.Location = new Point(150, 145);
            txtIp.Name = "txtIp";
            txtIp.Size = new Size(200, 27);
            txtIp.TabIndex = 6;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(110, 200);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 39);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(220, 53, 69);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(230, 200);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 39);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            // 
            // AddControllerForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(382, 318);
            Controls.Add(lblTitle);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblMac);
            Controls.Add(txtMac);
            Controls.Add(lblIp);
            Controls.Add(txtIp);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "AddControllerForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Controller";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}