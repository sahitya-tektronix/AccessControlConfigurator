using AccessControlConfigurator.Helpers;

namespace AccessControlConfigurator
{
    partial class AddCardholderForm
    {
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblMobile;

        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtMobile;

        private System.Windows.Forms.CheckBox chkActive;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;

        private void InitializeComponent()
        {
            headerPanel = new System.Windows.Forms.Panel();
            lblHeader = new System.Windows.Forms.Label();
            lblFirstName = new Label();
            lblLastName = new Label();
            lblUserName = new Label();
            lblEmail = new Label();
            lblMobile = new Label();
            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtUserName = new TextBox();
            txtEmail = new TextBox();
            txtMobile = new TextBox();
            chkActive = new CheckBox();
            btnSave = new Button();
            btnCancel = new Button();
            headerPanel.SuspendLayout();
            SuspendLayout();
            //
            // headerPanel + lblHeader
            //
            UIStyleHelper.StyleHeaderPanel(headerPanel);
            headerPanel.Controls.Add(lblHeader);

            lblHeader.Text = "ADD CARDHOLDER";
            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Font = UIStyleHelper.StandardFonts.HeaderFont;
            lblHeader.ForeColor = UIStyleHelper.StandardColors.HeaderForeground;
            //
            // lblFirstName
            //
            lblFirstName.Location = new Point(30, 80);
            lblFirstName.Name = "lblFirstName";
            lblFirstName.Size = new Size(100, 23);
            lblFirstName.TabIndex = 0;
            lblFirstName.Text = "First Name";
            // 
            // lblLastName
            // 
            lblLastName.Location = new Point(30, 120);
            lblLastName.Name = "lblLastName";
            lblLastName.Size = new Size(100, 23);
            lblLastName.TabIndex = 2;
            lblLastName.Text = "Last Name";
            //
            // lblUserName
            //
            lblUserName.Location = new Point(30, 160);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(100, 23);
            lblUserName.TabIndex = 4;
            lblUserName.Text = "User Name";
            //
            // lblEmail
            //
            lblEmail.Location = new Point(30, 200);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 6;
            lblEmail.Text = "Email";
            //
            // lblMobile
            //
            lblMobile.Location = new Point(30, 240);
            lblMobile.Name = "lblMobile";
            lblMobile.Size = new Size(100, 23);
            lblMobile.TabIndex = 8;
            lblMobile.Text = "Mobile";
            //
            // txtFirstName
            //
            txtFirstName.Location = new Point(150, 80);
            txtFirstName.Name = "txtFirstName";
            txtFirstName.Size = new Size(200, 27);
            txtFirstName.TabIndex = 1;
            //
            // txtLastName
            //
            txtLastName.Location = new Point(150, 120);
            txtLastName.Name = "txtLastName";
            txtLastName.Size = new Size(200, 27);
            txtLastName.TabIndex = 3;
            //
            // txtUserName
            //
            txtUserName.Location = new Point(150, 160);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(200, 27);
            txtUserName.TabIndex = 5;
            //
            // txtEmail
            //
            txtEmail.Location = new Point(150, 200);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(200, 27);
            txtEmail.TabIndex = 7;
            //
            // txtMobile
            //
            txtMobile.Location = new Point(150, 240);
            txtMobile.Name = "txtMobile";
            txtMobile.Size = new Size(200, 27);
            txtMobile.TabIndex = 9;
            //
            // chkActive
            //
            chkActive.Location = new Point(150, 280);
            chkActive.Name = "chkActive";
            chkActive.Size = new Size(104, 24);
            chkActive.TabIndex = 10;
            chkActive.Text = "Active";
            //
            // btnSave
            //
            btnSave.Location = new Point(150, 320);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 31);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            //
            // btnCancel
            //
            btnCancel.Location = new Point(250, 320);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 31);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            //
            // AddCardholderForm
            //
            ClientSize = new Size(402, 370);
            Controls.Add(headerPanel);
            Controls.Add(lblFirstName);
            Controls.Add(txtFirstName);
            Controls.Add(lblLastName);
            Controls.Add(txtLastName);
            Controls.Add(lblUserName);
            Controls.Add(txtUserName);
            Controls.Add(lblEmail);
            Controls.Add(txtEmail);
            Controls.Add(lblMobile);
            Controls.Add(txtMobile);
            Controls.Add(chkActive);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "AddCardholderForm";
            Text = "Add Cardholder";
            headerPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
    }