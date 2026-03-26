using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class EditTimezoneForm
    {
        private Panel headerPanel;
        private Label lblHeader;

        private Label lblName;
        private Label lblNumber;
        private Label lblMode;

        public TextBox txtName;
        public TextBox txtNumber;
        public TextBox txtMode;

        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // FORM SETTINGS
            this.ClientSize = new Size(420, 260);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            // HEADER PANEL
            headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(45, 62, 80);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 45;

            lblHeader = new Label();
            lblHeader.Text = "Edit Time Zone";
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblHeader.Location = new Point(15, 12);
            lblHeader.AutoSize = true;

            headerPanel.Controls.Add(lblHeader);

            // NAME LABEL
            lblName = new Label();
            lblName.Text = "Name";
            lblName.Location = new Point(40, 70);
            lblName.AutoSize = true;

            txtName = new TextBox();
            txtName.Location = new Point(150, 65);
            txtName.Width = 200;

            // NUMBER
            lblNumber = new Label();
            lblNumber.Text = "Number";
            lblNumber.Location = new Point(40, 110);
            lblNumber.AutoSize = true;

            txtNumber = new TextBox();
            txtNumber.Location = new Point(150, 105);
            txtNumber.Width = 200;

            // MODE
            lblMode = new Label();
            lblMode.Text = "Mode";
            lblMode.Location = new Point(40, 150);
            lblMode.AutoSize = true;

            txtMode = new TextBox();
            txtMode.Location = new Point(150, 145);
            txtMode.Width = 200;

            // SAVE BUTTON
            btnSave = new Button();
            btnSave.Text = "Update";
            btnSave.Size = new Size(100, 35);
            btnSave.Location = new Point(150, 190);
            btnSave.BackColor = Color.FromArgb(40, 167, 69);
            btnSave.ForeColor = Color.White;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Click += btnSave_Click;

            // CANCEL BUTTON
            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.Location = new Point(260, 190);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Click += (s, e) => this.Close();

            // ADD CONTROLS
            Controls.Add(headerPanel);

            Controls.Add(lblName);
            Controls.Add(txtName);

            Controls.Add(lblNumber);
            Controls.Add(txtNumber);

            Controls.Add(lblMode);
            Controls.Add(txtMode);

            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            this.ResumeLayout(false);
        }
    }
}