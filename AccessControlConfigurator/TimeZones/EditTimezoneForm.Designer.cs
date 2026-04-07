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
        private Label lblStart;
        private Label lblEnd;
        private Label lblDays;
        private Label lblScheduleHint;

        private TextBox txtName;
        private NumericUpDown numNumber;
        private ComboBox cmbMode;
        private DateTimePicker dtStart;
        private DateTimePicker dtEnd;

        private CheckBox chkMonday;
        private CheckBox chkTuesday;
        private CheckBox chkWednesday;
        private CheckBox chkThursday;
        private CheckBox chkFriday;
        private CheckBox chkSaturday;
        private CheckBox chkSunday;
        private CheckBox chkPublicHoliday;

        private Panel pnlSchedule;

        private Button btnClearAll;
        private Button btnExample;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblHeader = new Label();
            lblName = new Label();
            lblNumber = new Label();
            lblMode = new Label();
            lblStart = new Label();
            lblEnd = new Label();
            lblDays = new Label();
            lblScheduleHint = new Label();
            txtName = new TextBox();
            numNumber = new NumericUpDown();
            cmbMode = new ComboBox();
            dtStart = new DateTimePicker();
            dtEnd = new DateTimePicker();
            chkMonday = new CheckBox();
            chkTuesday = new CheckBox();
            chkWednesday = new CheckBox();
            chkThursday = new CheckBox();
            chkFriday = new CheckBox();
            chkSaturday = new CheckBox();
            chkSunday = new CheckBox();
            chkPublicHoliday = new CheckBox();
            pnlSchedule = new Panel();
            btnClearAll = new Button();
            btnExample = new Button();
            btnSave = new Button();
            btnCancel = new Button();

            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(numNumber)).BeginInit();
            SuspendLayout();

            // headerPanel
            headerPanel.BackColor = Color.FromArgb(45, 62, 80);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(940, 56);
            headerPanel.TabIndex = 0;

            // lblHeader
            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Name = "lblHeader";
            lblHeader.TabIndex = 0;
            lblHeader.Text = "Edit Time Zone";
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;

            // lblName
            lblName.Location = new Point(26, 78);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 23);
            lblName.TabIndex = 1;
            lblName.Text = "Name";

            // txtName
            txtName.Location = new Point(150, 75);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 27);
            txtName.TabIndex = 2;

            // btnExample
            btnExample.BackColor = Color.FromArgb(22, 125, 211);
            btnExample.FlatAppearance.BorderSize = 0;
            btnExample.FlatStyle = FlatStyle.Flat;
            btnExample.ForeColor = Color.White;
            btnExample.Location = new Point(570, 72);
            btnExample.Name = "btnExample";
            btnExample.Size = new Size(125, 32);
            btnExample.TabIndex = 3;
            btnExample.Text = "Mon-Fri 9-17";
            btnExample.UseVisualStyleBackColor = false;

            // btnClearAll
            btnClearAll.BackColor = Color.FromArgb(108, 117, 125);
            btnClearAll.FlatAppearance.BorderSize = 0;
            btnClearAll.FlatStyle = FlatStyle.Flat;
            btnClearAll.ForeColor = Color.White;
            btnClearAll.Location = new Point(710, 72);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(120, 32);
            btnClearAll.TabIndex = 4;
            btnClearAll.Text = "Clear All";
            btnClearAll.UseVisualStyleBackColor = false;

            // lblMode
            lblMode.Location = new Point(26, 118);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(120, 23);
            lblMode.TabIndex = 5;
            lblMode.Text = "Mode";

            // cmbMode
            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMode.Location = new Point(150, 115);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(180, 28);
            cmbMode.TabIndex = 6;

            // lblStart
            lblStart.Location = new Point(370, 118);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(80, 23);
            lblStart.TabIndex = 7;
            lblStart.Text = "Start Time";

            // dtStart
            dtStart.Location = new Point(450, 115);
            dtStart.Name = "dtStart";
            dtStart.Size = new Size(140, 27);
            dtStart.TabIndex = 8;

            // lblEnd
            lblEnd.Location = new Point(620, 118);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(70, 23);
            lblEnd.TabIndex = 9;
            lblEnd.Text = "End Time";

            // dtEnd
            dtEnd.Location = new Point(700, 115);
            dtEnd.Name = "dtEnd";
            dtEnd.Size = new Size(140, 27);
            dtEnd.TabIndex = 10;

            // lblNumber
            lblNumber.Location = new Point(570, 155);
            lblNumber.Name = "lblNumber";
            lblNumber.Size = new Size(130, 23);
            lblNumber.TabIndex = 11;
            lblNumber.Text = "Timezone Number";

            // numNumber
            numNumber.Location = new Point(710, 152);
            numNumber.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            numNumber.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numNumber.Name = "numNumber";
            numNumber.Size = new Size(120, 27);
            numNumber.TabIndex = 12;
            numNumber.Value = new decimal(new int[] { 1, 0, 0, 0 });

            // lblDays
            lblDays.Location = new Point(26, 195);
            lblDays.Name = "lblDays";
            lblDays.Size = new Size(130, 23);
            lblDays.TabIndex = 13;
            lblDays.Text = "Working Schedule";

            // lblScheduleHint
            lblScheduleHint.AutoSize = true;
            lblScheduleHint.ForeColor = Color.FromArgb(100, 110, 120);
            lblScheduleHint.Location = new Point(150, 195);
            lblScheduleHint.Name = "lblScheduleHint";
            lblScheduleHint.TabIndex = 14;
            lblScheduleHint.Text = "Loaded existing schedule. Click any day row to modify.";

            // pnlSchedule
            pnlSchedule.BackColor = Color.White;
            pnlSchedule.BorderStyle = BorderStyle.FixedSingle;
            pnlSchedule.Location = new Point(26, 225);
            pnlSchedule.Name = "pnlSchedule";
            pnlSchedule.Size = new Size(880, 310);
            pnlSchedule.TabIndex = 15;

            // hidden legacy checkboxes (used internally for day state)
            chkMonday.Location    = new Point(30, 555);   chkMonday.Name    = "chkMonday";    chkMonday.Size    = new Size(20,20); chkMonday.TabIndex    = 16;
            chkTuesday.Location   = new Point(55, 555);   chkTuesday.Name   = "chkTuesday";   chkTuesday.Size   = new Size(20,20); chkTuesday.TabIndex   = 17;
            chkWednesday.Location = new Point(80, 555);   chkWednesday.Name = "chkWednesday"; chkWednesday.Size = new Size(20,20); chkWednesday.TabIndex = 18;
            chkThursday.Location  = new Point(105, 555);  chkThursday.Name  = "chkThursday";  chkThursday.Size  = new Size(20,20); chkThursday.TabIndex  = 19;
            chkFriday.Location    = new Point(130, 555);  chkFriday.Name    = "chkFriday";    chkFriday.Size    = new Size(20,20); chkFriday.TabIndex    = 20;
            chkSaturday.Location  = new Point(155, 555);  chkSaturday.Name  = "chkSaturday";  chkSaturday.Size  = new Size(20,20); chkSaturday.TabIndex  = 21;
            chkSunday.Location    = new Point(180, 555);  chkSunday.Name    = "chkSunday";    chkSunday.Size    = new Size(20,20); chkSunday.TabIndex    = 22;
            chkPublicHoliday.Location = new Point(205, 555); chkPublicHoliday.Name = "chkPublicHoliday"; chkPublicHoliday.Size = new Size(20,20); chkPublicHoliday.TabIndex = 23;

            // btnSave (Update)
            btnSave.BackColor = Color.SeaGreen;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(610, 555);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(130, 38);
            btnSave.TabIndex = 24;
            btnSave.Text = "Update";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            // btnCancel
            btnCancel.Location = new Point(758, 555);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(130, 38);
            btnCancel.TabIndex = 25;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;

            // EditTimezoneForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(940, 610);
            Controls.Add(headerPanel);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(btnExample);
            Controls.Add(btnClearAll);
            Controls.Add(lblMode);
            Controls.Add(cmbMode);
            Controls.Add(lblStart);
            Controls.Add(dtStart);
            Controls.Add(lblEnd);
            Controls.Add(dtEnd);
            Controls.Add(lblNumber);
            Controls.Add(numNumber);
            Controls.Add(lblDays);
            Controls.Add(lblScheduleHint);
            Controls.Add(pnlSchedule);
            Controls.Add(chkMonday);
            Controls.Add(chkTuesday);
            Controls.Add(chkWednesday);
            Controls.Add(chkThursday);
            Controls.Add(chkFriday);
            Controls.Add(chkSaturday);
            Controls.Add(chkSunday);
            Controls.Add(chkPublicHoliday);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "EditTimezoneForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Time Zone";

            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(numNumber)).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
