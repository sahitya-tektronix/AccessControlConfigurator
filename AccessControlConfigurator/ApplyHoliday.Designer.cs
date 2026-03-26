using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class ApplyHolidayForm
    {
        private Panel headerPanel;
        private Label lblHeader;

        private Label lblTimezone;
        private CheckedListBox chkTimezones;

        private Label lblDate;
        private DateTimePicker dtHoliday;

        private Button btnApply;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "Apply Holiday";
            this.Size = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(52, 73, 94);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 50;

            lblHeader = new Label();
            lblHeader.Text = "Apply Holiday";
            lblHeader.ForeColor = Color.White;
            lblHeader.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblHeader.Location = new Point(15, 15);
            lblHeader.AutoSize = true;

            headerPanel.Controls.Add(lblHeader);

            lblTimezone = new Label();
            lblTimezone.Text = "Timezone";
            lblTimezone.Location = new Point(30, 70);
            lblTimezone.AutoSize = true;

            chkTimezones = new CheckedListBox();
            chkTimezones.Location = new Point(120, 70);
            chkTimezones.Size = new Size(200, 100);
            chkTimezones.CheckOnClick = true;

            lblDate = new Label();
            lblDate.Text = "Holiday Date";
            lblDate.Location = new Point(30, 190);
            lblDate.AutoSize = true;

            dtHoliday = new DateTimePicker();
            dtHoliday.Location = new Point(120, 185);
            dtHoliday.Format = DateTimePickerFormat.Short;

            btnApply = new Button();
            btnApply.Text = "Apply";
            btnApply.BackColor = Color.SeaGreen;
            btnApply.ForeColor = Color.White;
            btnApply.Location = new Point(120, 230);
            btnApply.Size = new Size(80, 35);
            btnApply.Click += btnApply_Click;

            btnCancel = new Button();
            btnCancel.Text = "Cancel";
            btnCancel.Location = new Point(220, 230);
            btnCancel.Size = new Size(80, 35);
            btnCancel.Click += btnCancel_Click;

            Controls.Add(headerPanel);
            Controls.Add(lblTimezone);
            Controls.Add(chkTimezones);
            Controls.Add(lblDate);
            Controls.Add(dtHoliday);
            Controls.Add(btnApply);
            Controls.Add(btnCancel);

            this.ResumeLayout(false);
        }
    }
}