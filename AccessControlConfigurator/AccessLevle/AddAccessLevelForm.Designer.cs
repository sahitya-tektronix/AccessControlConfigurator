using System.Drawing;

using System.Windows.Forms;

using AccessControlConfigurator.Helpers;

namespace AccessControlConfigurator

{

    partial class AddAccessLevelForm

    {

        private Label lblHeader;

        private Label lblName;

        private Label lblDoors;

        private TextBox txtName;

        private DataGridView dgvDoorTimezones;

        private Button btnSave;

        private Button btnCancel;

        private Panel headerPanel;

        private void InitializeComponent()

        {

            headerPanel = new Panel();

            lblHeader = new Label();

            lblName = new Label();

            lblDoors = new Label();

            txtName = new TextBox();

            dgvDoorTimezones = new DataGridView();

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

            UIStyleHelper.StyleLabel(lblDoors);

            lblName.Text = "Access Level Name";

            lblDoors.Text = "Doors and Time Zones";

            // ==================== INPUT FIELDS ====================

            UIStyleHelper.StyleTextBox(txtName);

            txtName.Name = "txtName";

            dgvDoorTimezones.Name = "dgvDoorTimezones";

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

            ClientSize = new Size(650, 600);

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

            int inputWidth = 360;

            int gridWidth = ClientSize.Width - (padding * 2) - 40;

            int gridHeight = 300;

            lblName.Location = new Point(padding + 20, 80);

            lblName.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.LabelHeight);

            txtName.Location = new Point(padding + 20, lblName.Bottom + 5);

            txtName.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.InputFieldHeight);

            lblDoors.Location = new Point(padding + 20, txtName.Bottom + verticalSpacing);

            lblDoors.Size = new Size(inputWidth + 140, UIStyleHelper.StandardSizes.LabelHeight);

            dgvDoorTimezones.Location = new Point(padding + 20, lblDoors.Bottom + 5);

            dgvDoorTimezones.Size = new Size(gridWidth, gridHeight);

            // Button panel

            int buttonY = dgvDoorTimezones.Bottom + verticalSpacing;

            int buttonSpacing = 10;

            int totalButtonWidth = UIStyleHelper.StandardSizes.ButtonWidth + UIStyleHelper.StandardSizes.SmallButtonWidth + buttonSpacing;

            int buttonStartX = (ClientSize.Width - totalButtonWidth) / 2;

            btnSave.Location = new Point(buttonStartX, buttonY);

            btnCancel.Location = new Point(btnSave.Right + buttonSpacing, buttonY);

            // Add all controls

            Controls.Add(headerPanel);

            Controls.Add(lblName);

            Controls.Add(txtName);

            Controls.Add(lblDoors);

            Controls.Add(dgvDoorTimezones);

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

