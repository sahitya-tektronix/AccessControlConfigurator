using System.Drawing;
using System.Windows.Forms;
using AccessControlConfigurator.Helpers;

namespace AccessControlConfigurator.Forms
{
    partial class EditAcrForm
    {
        private System.ComponentModel.IContainer components = null;

        private Panel headerPanel;
        private Label lblHeader;
        private Label lblName;
        private TextBox txtName;
        private Label lblAcrNumber;
        private NumericUpDown numAcrNumber;
        private Label lblDefaultMode;
        private NumericUpDown numDefaultMode;
        private Label lblReaderNumber;
        private NumericUpDown numReaderNumber;
        private Label lblReaderType;
        private ComboBox cbReaderType;
        private Label lblReaderDirection;
        private ComboBox cbReaderDirection;
        private Label lblStrikeNumber;
        private NumericUpDown numStrikeNumber;
        private Label lblDoorNumber;
        private NumericUpDown numDoorNumber;
        private Label lblRexNumber;
        private NumericUpDown numRexNumber;
        private Button btnSave;
        private Button btnCancel;
        private Panel bottomPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblHeader = new Label();
            lblName = new Label();
            txtName = new TextBox();
            lblAcrNumber = new Label();
            numAcrNumber = new NumericUpDown();
            lblDefaultMode = new Label();
            numDefaultMode = new NumericUpDown();
            lblReaderNumber = new Label();
            numReaderNumber = new NumericUpDown();
            lblReaderType = new Label();
            cbReaderType = new ComboBox();
            lblReaderDirection = new Label();
            cbReaderDirection = new ComboBox();
            lblStrikeNumber = new Label();
            numStrikeNumber = new NumericUpDown();
            lblDoorNumber = new Label();
            numDoorNumber = new NumericUpDown();
            lblRexNumber = new Label();
            numRexNumber = new NumericUpDown();
            btnSave = new Button();
            btnCancel = new Button();
            bottomPanel = new Panel();
            ((System.ComponentModel.ISupportInitialize)numAcrNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDefaultMode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numReaderNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStrikeNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDoorNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRexNumber).BeginInit();
            headerPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            //
            // headerPanel + lblHeader
            //
            Helpers.UIStyleHelper.StyleHeaderPanel(headerPanel);
            headerPanel.Controls.Add(lblHeader);

            lblHeader.Text = "EDIT ACR SETTINGS";
            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Font = Helpers.UIStyleHelper.StandardFonts.HeaderFont;
            lblHeader.ForeColor = Helpers.UIStyleHelper.StandardColors.HeaderForeground;
            //
            // lblName
            // 
            lblName.Location = new Point(24, 70);
            lblName.Name = "lblName";
            lblName.Size = new Size(120, 23);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.Location = new Point(170, 66);
            txtName.Name = "txtName";
            txtName.Size = new Size(240, 27);
            txtName.TabIndex = 2;
            // 
            // lblAcrNumber
            // 
            lblAcrNumber.Location = new Point(24, 108);
            lblAcrNumber.Name = "lblAcrNumber";
            lblAcrNumber.Size = new Size(120, 23);
            lblAcrNumber.TabIndex = 3;
            lblAcrNumber.Text = "ACR Number";
            // 
            // numAcrNumber
            // 
            numAcrNumber.Enabled = false;
            numAcrNumber.Location = new Point(170, 104);
            numAcrNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numAcrNumber.Name = "numAcrNumber";
            numAcrNumber.Size = new Size(240, 27);
            numAcrNumber.TabIndex = 4;
            // 
            // lblDefaultMode
            // 
            lblDefaultMode.Location = new Point(24, 146);
            lblDefaultMode.Name = "lblDefaultMode";
            lblDefaultMode.Size = new Size(120, 23);
            lblDefaultMode.TabIndex = 5;
            lblDefaultMode.Text = "Default Mode";
            // 
            // numDefaultMode
            // 
            numDefaultMode.Location = new Point(170, 142);
            numDefaultMode.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numDefaultMode.Name = "numDefaultMode";
            numDefaultMode.Size = new Size(240, 27);
            numDefaultMode.TabIndex = 6;
            // 
            // lblReaderNumber
            // 
            lblReaderNumber.Location = new Point(24, 184);
            lblReaderNumber.Name = "lblReaderNumber";
            lblReaderNumber.Size = new Size(120, 23);
            lblReaderNumber.TabIndex = 7;
            lblReaderNumber.Text = "Reader Number";
            // 
            // numReaderNumber
            // 
            numReaderNumber.Enabled = false;
            numReaderNumber.Location = new Point(170, 180);
            numReaderNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numReaderNumber.Name = "numReaderNumber";
            numReaderNumber.Size = new Size(240, 27);
            numReaderNumber.TabIndex = 8;
            // 
            // lblReaderType
            // 
            lblReaderType.Location = new Point(24, 222);
            lblReaderType.Name = "lblReaderType";
            lblReaderType.Size = new Size(120, 23);
            lblReaderType.TabIndex = 9;
            lblReaderType.Text = "Reader Type";
            // 
            // cbReaderType
            // 
            cbReaderType.Location = new Point(170, 218);
            cbReaderType.Name = "cbReaderType";
            cbReaderType.Size = new Size(240, 28);
            cbReaderType.TabIndex = 10;
            // 
            // lblReaderDirection
            // 
            lblReaderDirection.Location = new Point(24, 260);
            lblReaderDirection.Name = "lblReaderDirection";
            lblReaderDirection.Size = new Size(120, 23);
            lblReaderDirection.TabIndex = 11;
            lblReaderDirection.Text = "Reader Direction";
            // 
            // cbReaderDirection
            // 
            cbReaderDirection.Location = new Point(170, 256);
            cbReaderDirection.Name = "cbReaderDirection";
            cbReaderDirection.Size = new Size(240, 28);
            cbReaderDirection.TabIndex = 12;
            // 
            // lblStrikeNumber
            // 
            lblStrikeNumber.Location = new Point(24, 298);
            lblStrikeNumber.Name = "lblStrikeNumber";
            lblStrikeNumber.Size = new Size(120, 23);
            lblStrikeNumber.TabIndex = 13;
            lblStrikeNumber.Text = "Strike Number";
            // 
            // numStrikeNumber
            // 
            numStrikeNumber.Enabled = false;
            numStrikeNumber.Location = new Point(170, 294);
            numStrikeNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numStrikeNumber.Name = "numStrikeNumber";
            numStrikeNumber.Size = new Size(240, 27);
            numStrikeNumber.TabIndex = 14;
            // 
            // lblDoorNumber
            // 
            lblDoorNumber.Location = new Point(24, 336);
            lblDoorNumber.Name = "lblDoorNumber";
            lblDoorNumber.Size = new Size(120, 23);
            lblDoorNumber.TabIndex = 15;
            lblDoorNumber.Text = "Door Number";
            // 
            // numDoorNumber
            // 
            numDoorNumber.Enabled = false;
            numDoorNumber.Location = new Point(170, 332);
            numDoorNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numDoorNumber.Name = "numDoorNumber";
            numDoorNumber.Size = new Size(240, 27);
            numDoorNumber.TabIndex = 16;
            // 
            // lblRexNumber
            // 
            lblRexNumber.Location = new Point(24, 374);
            lblRexNumber.Name = "lblRexNumber";
            lblRexNumber.Size = new Size(120, 23);
            lblRexNumber.TabIndex = 17;
            lblRexNumber.Text = "REX Number";
            // 
            // numRexNumber
            // 
            numRexNumber.Enabled = false;
            numRexNumber.Location = new Point(170, 370);
            numRexNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numRexNumber.Name = "numRexNumber";
            numRexNumber.Size = new Size(240, 27);
            numRexNumber.TabIndex = 18;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(185, 10);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 35);
            btnSave.TabIndex = 0;
            btnSave.Text = "Update";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(300, 10);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(100, 35);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = Color.WhiteSmoke;
            bottomPanel.Controls.Add(btnSave);
            bottomPanel.Controls.Add(btnCancel);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 431);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(438, 55);
            bottomPanel.TabIndex = 19;
            // 
            // EditAcrForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(438, 486);
            Controls.Add(bottomPanel);
            Controls.Add(numRexNumber);
            Controls.Add(lblRexNumber);
            Controls.Add(numDoorNumber);
            Controls.Add(lblDoorNumber);
            Controls.Add(numStrikeNumber);
            Controls.Add(lblStrikeNumber);
            Controls.Add(cbReaderDirection);
            Controls.Add(lblReaderDirection);
            Controls.Add(cbReaderType);
            Controls.Add(lblReaderType);
            Controls.Add(numReaderNumber);
            Controls.Add(lblReaderNumber);
            Controls.Add(numDefaultMode);
            Controls.Add(lblDefaultMode);
            Controls.Add(numAcrNumber);
            Controls.Add(lblAcrNumber);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditAcrForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit ACR";
            ((System.ComponentModel.ISupportInitialize)numAcrNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDefaultMode).EndInit();
            ((System.ComponentModel.ISupportInitialize)numReaderNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStrikeNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDoorNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRexNumber).EndInit();
            headerPanel.ResumeLayout(false);
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
