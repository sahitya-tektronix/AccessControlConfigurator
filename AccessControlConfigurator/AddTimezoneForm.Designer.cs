using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class AddTimezoneForm
    {
        private Panel headerPanel;
        private Label lblHeader;

        private Label lblName;
        private Label lblNumber;
        private Label lblMode;

        private TextBox txtName;
        private TextBox txtNumber;
        private TextBox txtMode;

        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblHeader = new Label();
            lblName = new Label();
            txtName = new TextBox();
            lblNumber = new Label();
            txtNumber = new TextBox();
            lblMode = new Label();
            txtMode = new TextBox();
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
            headerPanel.Size = new Size(374, 40);
            headerPanel.TabIndex = 0;
            // 
            // lblHeader
            // 
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(10, 10);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(148, 25);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Add Time Zone";
            // 
            // lblName
            // 
            lblName.Location = new Point(40, 70);
            lblName.Name = "lblName";
            lblName.Size = new Size(100, 23);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(140, 65);
            txtName.Name = "txtName";
            txtName.Size = new Size(200, 27);
            txtName.TabIndex = 2;
            // 
            // lblNumber
            // 
            lblNumber.Location = new Point(40, 110);
            lblNumber.Name = "lblNumber";
            lblNumber.Size = new Size(100, 23);
            lblNumber.TabIndex = 3;
            lblNumber.Text = "Number";
            // 
            // txtNumber
            // 
            txtNumber.Location = new Point(140, 105);
            txtNumber.Name = "txtNumber";
            txtNumber.Size = new Size(200, 27);
            txtNumber.TabIndex = 4;
            // 
            // lblMode
            // 
            lblMode.Location = new Point(40, 150);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(100, 23);
            lblMode.TabIndex = 5;
            lblMode.Text = "Mode";
            // 
            // txtMode
            // 
            txtMode.Location = new Point(140, 145);
            txtMode.Name = "txtMode";
            txtMode.Size = new Size(200, 27);
            txtMode.TabIndex = 6;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(140, 185);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 30);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(250, 185);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 30);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            // 
            // AddTimezoneForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(374, 229);
            Controls.Add(headerPanel);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblNumber);
            Controls.Add(txtNumber);
            Controls.Add(lblMode);
            Controls.Add(txtMode);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AddTimezoneForm";
            StartPosition = FormStartPosition.CenterParent;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}