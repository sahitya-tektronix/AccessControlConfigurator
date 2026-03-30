using System.Drawing;
using System.Windows.Forms;
using AccessControlConfigurator.Helpers;

namespace AccessControlConfigurator
{
    partial class AddTimezoneForm
    {
        private Panel headerPanel;
        private Label lblHeader;
        private Label lblName;
        private Label lblNumber;
        private Label lblMode;
        private Label lblActTime;
        private Label lblDeactTime;
        private Label lblIntervals;
        private Label lblIDays;
        private Label lblIStart;
        private Label lblIEnd;
        private TextBox txtName;
        private TextBox txtNumber;
        private TextBox txtMode;
        private DateTimePicker dtpActTime;
        private DateTimePicker dtpDeactTime;
        private TextBox txtIntervals;
        private TextBox txtIDays;
        private TextBox txtIStart;
        private TextBox txtIEnd;
        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblHeader = new Label();
            lblName = new Label();
            lblNumber = new Label();
            lblMode = new Label();
            lblActTime = new Label();
            lblDeactTime = new Label();
            lblIntervals = new Label();
            lblIDays = new Label();
            lblIStart = new Label();
            lblIEnd = new Label();
            txtName = new TextBox();
            txtNumber = new TextBox();
            txtMode = new TextBox();
            dtpActTime = new DateTimePicker();
            dtpDeactTime = new DateTimePicker();
            txtIntervals = new TextBox();
            txtIDays = new TextBox();
            txtIStart = new TextBox();
            txtIEnd = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            headerPanel.SuspendLayout();
            SuspendLayout();
            UIStyleHelper.StyleHeaderPanel(headerPanel);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Size = new Size(450, 60);
            UIStyleHelper.StyleLabel(lblHeader, UIStyleHelper.LabelStyle.Header);
            lblHeader.AutoSize = true;
            lblHeader.Location = new Point(18, 18);
            lblHeader.Text = "Add Time Zone";
            ConfigureLabel(lblName, "Name", 24, 78);
            ConfigureLabel(lblNumber, "Number", 24, 116);
            ConfigureLabel(lblMode, "Mode", 24, 154);
            ConfigureLabel(lblActTime, "Start Time", 24, 192);
            ConfigureLabel(lblDeactTime, "End Time", 24, 230);
            ConfigureLabel(lblIntervals, "Intervals", 24, 268);
            ConfigureLabel(lblIDays, "Break Days", 24, 306);
            ConfigureLabel(lblIStart, "Break Start", 24, 344);
            ConfigureLabel(lblIEnd, "Break End", 24, 382);
            ConfigureTextBox(txtName, 150, 74, 260);
            ConfigureTextBox(txtNumber, 150, 112, 260);
            ConfigureTextBox(txtMode, 150, 150, 260);
            ConfigureTextBox(txtIntervals, 150, 264, 260);
            ConfigureTextBox(txtIDays, 150, 302, 260);
            ConfigureTextBox(txtIStart, 150, 340, 260);
            ConfigureTextBox(txtIEnd, 150, 378, 260);
            ConfigureTimePicker(dtpActTime, 150, 188);
            ConfigureTimePicker(dtpDeactTime, 150, 226);
            UIStyleHelper.StyleButton(btnSave, UIStyleHelper.ButtonStyle.Success);
            btnSave.Location = new Point(200, 426);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 17;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            UIStyleHelper.StyleButton(btnCancel, UIStyleHelper.ButtonStyle.Default);
            btnCancel.Location = new Point(310, 426);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(450, 480);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtIEnd);
            Controls.Add(txtIStart);
            Controls.Add(txtIDays);
            Controls.Add(txtIntervals);
            Controls.Add(dtpDeactTime);
            Controls.Add(dtpActTime);
            Controls.Add(txtMode);
            Controls.Add(txtNumber);
            Controls.Add(txtName);
            Controls.Add(lblIEnd);
            Controls.Add(lblIStart);
            Controls.Add(lblIDays);
            Controls.Add(lblIntervals);
            Controls.Add(lblDeactTime);
            Controls.Add(lblActTime);
            Controls.Add(lblMode);
            Controls.Add(lblNumber);
            Controls.Add(lblName);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AddTimezoneForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Time Zone";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
        }

        private static void ConfigureLabel(Label label, string text, int x, int y)
        {
            UIStyleHelper.StyleLabel(label);
            label.Location = new Point(x, y);
            label.Size = new Size(110, 23);
            label.Text = text;
        }

        private static void ConfigureTextBox(TextBox textBox, int x, int y, int width)
        {
            UIStyleHelper.StyleTextBox(textBox);
            textBox.Location = new Point(x, y);
            textBox.Size = new Size(width, 30);
        }

        private static void ConfigureTimePicker(DateTimePicker picker, int x, int y)
        {
            picker.CustomFormat = "HH:mm:ss";
            picker.Format = DateTimePickerFormat.Custom;
            picker.ShowUpDown = true;
            picker.Font = UIStyleHelper.StandardFonts.InputFont;
            picker.CalendarMonthBackground = Color.White;
            picker.Location = new Point(x, y);
            picker.Size = new Size(260, 30);
        }
    }
}
