using System.Drawing;
using System.Windows.Forms;
using AccessControlConfigurator.Helpers;

namespace AccessControlConfigurator
{
    partial class AddAccessLevelForm
    {
        private Label lblHeader;
        private Label lblName;
        private Label lblDescription;
        private Label lblAcr;
        private Label lblTimeZone;

        private TextBox txtName;
        private TextBox txtDescription;

        private ComboBox cmbAcr;
        private ComboBox cmbTimeZone;

        private Button btnSave;
        private Button btnCancel;

        private Panel headerPanel;

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblHeader = new Label();
            lblName = new Label();
            lblAcr = new Label();
            lblTimeZone = new Label();
            txtName = new TextBox();
            cmbAcr = new ComboBox();
            cmbTimeZone = new ComboBox();
            btnSave = new Button();
            btnCancel = new Button();
            headerPanel.SuspendLayout();
            SuspendLayout();
            
            // ==================== HEADER PANEL ====================
            UIStyleHelper.StyleHeaderPanel(headerPanel);
            headerPanel.Controls.Add(lblHeader);
            
            // ==================== HEADER LABEL ====================
            lblHeader.Text = "ADD ACCESS LEVEL";
            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Font = UIStyleHelper.StandardFonts.HeaderFont;
            lblHeader.ForeColor = UIStyleHelper.StandardColors.HeaderForeground;
            
            // ==================== CONTENT LABELS ====================
            UIStyleHelper.StyleLabel(lblName);
            UIStyleHelper.StyleLabel(lblAcr);
            UIStyleHelper.StyleLabel(lblTimeZone);
            
            lblName.Text = "Access Level Name";
            lblAcr.Text = "Door (ACR)";
            lblTimeZone.Text = "Time Zone";
            
            // ==================== INPUT FIELDS ====================
            UIStyleHelper.StyleTextBox(txtName);
            UIStyleHelper.StyleComboBox(cmbAcr);
            UIStyleHelper.StyleComboBox(cmbTimeZone);
            
            txtName.Name = "txtName";
            cmbAcr.Name = "cmbAcr";
            cmbTimeZone.Name = "cmbTimeZone";
            
            // ==================== BUTTONS ====================
            UIStyleHelper.StyleButton(btnSave, UIStyleHelper.ButtonStyle.Success);
            UIStyleHelper.StyleButton(btnCancel, UIStyleHelper.ButtonStyle.Danger, isSmall: true);
            
            btnSave.Text = "Save";
            btnSave.Name = "btnSave";
            btnSave.Click += btnSave_Click;
            
            btnCancel.Text = "Cancel";
            btnCancel.Name = "btnCancel";
            btnCancel.Click += btnCancel_Click;
            
            // ==================== FORM LAYOUT ====================
            ClientSize = new Size(500, 520);
            Name = "AddAccessLevelForm";
            Text = "Add Access Level";
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
            
            lblAcr.Location = new Point(padding + 20, txtName.Bottom + verticalSpacing);
            lblAcr.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.LabelHeight);
            
            cmbAcr.Location = new Point(padding + 20, lblAcr.Bottom + 5);
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
            
            btnSave.Location = new Point(buttonStartX, buttonY);
            btnCancel.Location = new Point(btnSave.Right + buttonSpacing, buttonY);
            
            // Add all controls
            Controls.Add(headerPanel);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblAcr);
            Controls.Add(cmbAcr);
            Controls.Add(lblTimeZone);
            Controls.Add(cmbTimeZone);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
            
            // Make responsive
            UIStyleHelper.MakeResponsive(this);
        }
    }
}