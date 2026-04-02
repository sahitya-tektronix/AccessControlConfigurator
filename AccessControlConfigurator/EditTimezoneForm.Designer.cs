using System.Drawing;
using System.Windows.Forms;
using AccessControlConfigurator.Helpers;

namespace AccessControlConfigurator
{
    partial class EditTimezoneForm
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
        public TextBox txtName;
        public TextBox txtNumber;
        public TextBox txtMode;
        public TextBox txtActTime;
        public TextBox txtDeactTime;
        public TextBox txtIntervals;
        public TextBox txtIDays;
        public TextBox txtIStart;
        public TextBox txtIEnd;
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
            txtActTime = new TextBox();
            txtDeactTime = new TextBox();
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
            headerPanel.Size = new Size(470, 60);
            UIStyleHelper.StyleLabel(lblHeader, UIStyleHelper.LabelStyle.Header);
            lblHeader.AutoSize = true;
            lblHeader.Location = new Point(18, 18);
            lblHeader.Text = "Edit Time Zone";
            ConfigureLabel(lblName, "Name", 24, 78);
            ConfigureLabel(lblNumber, "Number", 24, 116);
            ConfigureLabel(lblMode, "Mode", 24, 154);
            ConfigureLabel(lblActTime, "Start Time", 24, 192);
            ConfigureLabel(lblDeactTime, "End Time", 24, 230);
            ConfigureLabel(lblIntervals, "Intervals", 24, 268);
            ConfigureLabel(lblIDays, "iDays", 24, 306);
            ConfigureLabel(lblIStart, "iStart", 24, 344);
            ConfigureLabel(lblIEnd, "iEnd", 24, 382);
            ConfigureTextBox(txtName, 150, 74, 280);
            ConfigureTextBox(txtNumber, 150, 112, 280);
            ConfigureTextBox(txtMode, 150, 150, 280);
            ConfigureTimeTextBox(txtActTime, 150, 188);
            ConfigureTimeTextBox(txtDeactTime, 150, 226);
            ConfigureTextBox(txtIntervals, 150, 264, 280);
            ConfigureTextBox(txtIDays, 150, 302, 280);
            ConfigureTextBox(txtIStart, 150, 340, 280);
            ConfigureTextBox(txtIEnd, 150, 378, 280);
            UIStyleHelper.StyleButton(btnSave, UIStyleHelper.ButtonStyle.Primary);
            btnSave.Location = new Point(210, 426);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 17;
            btnSave.Text = "Update";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            UIStyleHelper.StyleButton(btnCancel, UIStyleHelper.ButtonStyle.Default);
            btnCancel.Location = new Point(330, 426);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 18;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += (s, e) => Close();
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(470, 480);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtIEnd);
            Controls.Add(txtIStart);
            Controls.Add(txtIDays);
            Controls.Add(txtIntervals);
            Controls.Add(txtDeactTime);
            Controls.Add(txtActTime);
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
            Name = "EditTimezoneForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Time Zone";
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

        private static void ConfigureTimeTextBox(TextBox textBox, int x, int y)
        {
            ConfigureTextBox(textBox, x, y, 280);
            
            textBox.MaxLength = 8;
        }
    }
}
