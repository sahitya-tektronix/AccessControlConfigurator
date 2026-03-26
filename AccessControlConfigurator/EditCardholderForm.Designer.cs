using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class EditCardholderForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private Label lblHeader;
        private Label lblFirstName;
        private Label lblLastName;
        private Label lblMobile;
        private Label lblDepartment;
        private Label lblEmail;
        private Label lblCardNumber;
        private Label lblStartDate;
        private Label lblEndDate;

        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtMobile;
        private TextBox txtDepartment;
        private TextBox txtEmail;
        private TextBox txtCardNumber;

        private DateTimePicker dtStart;
        private DateTimePicker dtEnd;

        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblHeader = new Label();
            lblFirstName = new Label();
            lblLastName = new Label();
            lblMobile = new Label();
            lblDepartment = new Label();
            lblEmail = new Label();
            lblCardNumber = new Label();
            lblStartDate = new Label();
            lblEndDate = new Label();

            txtFirstName = new TextBox();
            txtLastName = new TextBox();
            txtMobile = new TextBox();
            txtDepartment = new TextBox();
            txtEmail = new TextBox();
            txtCardNumber = new TextBox();

            dtStart = new DateTimePicker();
            dtEnd = new DateTimePicker();
            btnSave = new Button();
            btnCancel = new Button();

            SuspendLayout();

            // Header
            lblHeader.Text = "Edit Cardholder";
            lblHeader.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblHeader.Location = new Point(20, 15);
            lblHeader.AutoSize = true;

            int labelX = 30;
            int textX = 160;

            // First Name
            lblFirstName.Text = "First Name";
            lblFirstName.Location = new Point(labelX, 60);
            txtFirstName.Location = new Point(textX, 60);
            txtFirstName.Width = 220;

            // Last Name
            lblLastName.Text = "Last Name";
            lblLastName.Location = new Point(labelX, 100);
            txtLastName.Location = new Point(textX, 100);
            txtLastName.Width = 220;

            // Mobile
            lblMobile.Text = "Mobile";
            lblMobile.Location = new Point(labelX, 140);
            txtMobile.Location = new Point(textX, 140);
            txtMobile.Width = 220;

            // Department
            lblDepartment.Text = "Department";
            lblDepartment.Location = new Point(labelX, 180);
            txtDepartment.Location = new Point(textX, 180);
            txtDepartment.Width = 220;

            // Email
            lblEmail.Text = "Email";
            lblEmail.Location = new Point(labelX, 220);
            txtEmail.Location = new Point(textX, 220);
            txtEmail.Width = 220;

            // Card Number
            lblCardNumber.Text = "Card Number";
            lblCardNumber.Location = new Point(labelX, 260);
            txtCardNumber.Location = new Point(textX, 260);
            txtCardNumber.Width = 220;

            // Start Date
            lblStartDate.Text = "Start Date";
            lblStartDate.Location = new Point(labelX, 300);
            dtStart.Location = new Point(textX, 300);
            dtStart.Width = 220;

            // End Date
            lblEndDate.Text = "End Date";
            lblEndDate.Location = new Point(labelX, 340);
            dtEnd.Location = new Point(textX, 340);
            dtEnd.Width = 220;

            // Save Button
            btnSave.Text = "Update";
            btnSave.Location = new Point(textX, 390);
            btnSave.Size = new Size(100, 32);
            btnSave.BackColor = Color.FromArgb(26, 95, 180);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += btnSave_Click;

            // Cancel Button
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(textX + 110, 390);
            btnCancel.Size = new Size(90, 32);
            btnCancel.Click += btnCancel_Click;

            // Add Controls
            Controls.Add(lblHeader);
            Controls.Add(lblFirstName); Controls.Add(txtFirstName);
            Controls.Add(lblLastName); Controls.Add(txtLastName);
            Controls.Add(lblMobile); Controls.Add(txtMobile);
            Controls.Add(lblDepartment); Controls.Add(txtDepartment);
            Controls.Add(lblEmail); Controls.Add(txtEmail);
            Controls.Add(lblCardNumber); Controls.Add(txtCardNumber);
            Controls.Add(lblStartDate); Controls.Add(dtStart);
            Controls.Add(lblEndDate); Controls.Add(dtEnd);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            // Form Settings
            Text = "Edit Cardholder";
            Size = new Size(430, 480);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.White;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;

            ResumeLayout(false);
        }
    }
}
