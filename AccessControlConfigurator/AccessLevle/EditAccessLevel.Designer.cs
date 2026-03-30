using System.Drawing;
using System.Windows.Forms;
using AccessControlConfigurator.Helpers;

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
            
            // ==================== HEADER PANEL ====================
            UIStyleHelper.StyleHeaderPanel(headerPanel);
            headerPanel.Controls.Add(lblHeader);
            
            // ==================== HEADER LABEL ====================
            lblHeader.Text = "EDIT ACCESS LEVEL";
            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Font = UIStyleHelper.StandardFonts.HeaderFont;
            lblHeader.ForeColor = UIStyleHelper.StandardColors.HeaderForeground;
            
            // ==================== CONTENT LABELS ====================
            UIStyleHelper.StyleLabel(lblName);
            UIStyleHelper.StyleLabel(lblDoor);
            UIStyleHelper.StyleLabel(lblTimeZone);
            
            lblName.Text = "Access Level";
            lblDoor.Text = "Door (ACR)";
            lblTimeZone.Text = "Time Zone";
            
            // ==================== INPUT FIELDS ====================
            UIStyleHelper.StyleTextBox(txtName);
            UIStyleHelper.StyleComboBox(cmbAcr);
            UIStyleHelper.StyleComboBox(cmbTimeZone);
            
            txtName.Name = "txtName";
            cmbAcr.Name = "cmbAcr";
            cmbTimeZone.Name = "cmbTimeZone";
            
            // ==================== BUTTONS ====================
            UIStyleHelper.StyleButton(btnupdate, UIStyleHelper.ButtonStyle.Success);
            UIStyleHelper.StyleButton(btnCancel, UIStyleHelper.ButtonStyle.Danger, isSmall: true);
            
            btnupdate.Text = "Update";
            btnupdate.Name = "btnupdate";
            btnupdate.Click += btnupdate_ClickAsync;
            
            btnCancel.Text = "Cancel";
            btnCancel.Name = "btnCancel";
            btnCancel.Click += btnCancel_Click;
            
            // ==================== FORM LAYOUT ====================
            ClientSize = new Size(500, 520);
            Name = "EditAccessLevelForm";
            Text = "Edit Access Level";
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.Sizable;
            MinimumSize = new Size(400, 400);
            
            // Position controls
            int padding = UIStyleHelper.StandardSizes.Padding;
            int verticalSpacing = UIStyleHelper.StandardSizes.VerticalSpacing;
            int inputWidth = 300;
            
            lblName.Location = new Point(padding + 20, 80);
            lblName.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.LabelHeight);
            
            txtName.Location = new Point(padding + 20, lblName.Bottom + 5);
            txtName.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.InputFieldHeight);
            
            lblDoor.Location = new Point(padding + 20, txtName.Bottom + verticalSpacing);
            lblDoor.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.LabelHeight);
            
            cmbAcr.Location = new Point(padding + 20, lblDoor.Bottom + 5);
            cmbAcr.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.InputFieldHeight);
            
            lblTimeZone.Location = new Point(padding + 20, cmbAcr.Bottom + verticalSpacing);
            lblTimeZone.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.LabelHeight);
            
            cmbTimeZone.Location = new Point(padding + 20, lblTimeZone.Bottom + 5);
            cmbTimeZone.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.InputFieldHeight);
            
            // Button panel
            int buttonY = cmbTimeZone.Bottom + verticalSpacing;
            int buttonSpacing = 10;
            int totalButtonWidth = UIStyleHelper.StandardSizes.ButtonWidth + UIStyleHelper.StandardSizes.SmallButtonWidth + buttonSpacing;
            int buttonStartX = (ClientSize.Width - totalButtonWidth) / 2;
            
            btnupdate.Location = new Point(buttonStartX, buttonY);
            btnCancel.Location = new Point(btnupdate.Right + buttonSpacing, buttonY);
            
            // Add all controls
            Controls.Add(headerPanel);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblDoor);
            Controls.Add(cmbAcr);
            Controls.Add(lblTimeZone);
            Controls.Add(cmbTimeZone);
            Controls.Add(btnupdate);
            Controls.Add(btnCancel);
            
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}