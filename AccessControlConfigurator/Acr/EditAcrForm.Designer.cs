using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    partial class EditAcrForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblName;
        private TextBox txtName;
        private Label lblAcrNumber;
        private NumericUpDown numAcrNumber;
        private Label lblDefaultMode;
        private NumericUpDown numDefaultMode;
        private Label lblReaderNumber;
        private NumericUpDown numReaderNumber;
        private Label lblReaderType;
       

        private Label lblReaderDirection;
        private NumericUpDown numReaderDirection;
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
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblName = new Label();
            txtName = new TextBox();
            lblAcrNumber = new Label();
            numAcrNumber = new NumericUpDown();
            lblDefaultMode = new Label();
            numDefaultMode = new NumericUpDown();
            lblReaderNumber = new Label();
            numReaderNumber = new NumericUpDown();
            lblReaderType = new Label();
            lblReaderDirection = new Label();
            lblStrikeNumber = new Label();
            numStrikeNumber = new NumericUpDown();
            lblDoorNumber = new Label();
            numDoorNumber = new NumericUpDown();
            lblRexNumber = new Label();
            numRexNumber = new NumericUpDown();
            btnSave = new Button();
            btnCancel = new Button();
            bottomPanel = new Panel();
            cbReaderType = new ComboBox();
            cbReaderDirection = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numAcrNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDefaultMode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numReaderNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numStrikeNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDoorNumber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRexNumber).BeginInit();
            bottomPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 60, 100);
            lblTitle.Location = new Point(20, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(252, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Edit ACR Configuration";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 9F);
            lblName.Location = new Point(20, 63);
            lblName.Name = "lblName";
            lblName.Size = new Size(49, 20);
            lblName.TabIndex = 1;
            lblName.Text = "Name";
            // 
            // txtName
            // 
            txtName.BorderStyle = BorderStyle.FixedSingle;
            txtName.Font = new Font("Segoe UI", 9F);
            txtName.Location = new Point(200, 60);
            txtName.Name = "txtName";
            txtName.Size = new Size(190, 27);
            txtName.TabIndex = 2;
            // 
            // lblAcrNumber
            // 
            lblAcrNumber.AutoSize = true;
            lblAcrNumber.Font = new Font("Segoe UI", 9F);
            lblAcrNumber.Location = new Point(20, 98);
            lblAcrNumber.Name = "lblAcrNumber";
            lblAcrNumber.Size = new Size(95, 20);
            lblAcrNumber.TabIndex = 3;
            lblAcrNumber.Text = "ACR Number";
            // 
            // numAcrNumber
            // 
            numAcrNumber.Enabled = false;
            numAcrNumber.Font = new Font("Segoe UI", 9F);
            numAcrNumber.Location = new Point(200, 95);
            numAcrNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numAcrNumber.Name = "numAcrNumber";
            numAcrNumber.Size = new Size(190, 27);
            numAcrNumber.TabIndex = 4;
            // 
            // lblDefaultMode
            // 
            lblDefaultMode.AutoSize = true;
            lblDefaultMode.Font = new Font("Segoe UI", 9F);
            lblDefaultMode.Location = new Point(20, 133);
            lblDefaultMode.Name = "lblDefaultMode";
            lblDefaultMode.Size = new Size(101, 20);
            lblDefaultMode.TabIndex = 5;
            lblDefaultMode.Text = "Default Mode";
            // 
            // numDefaultMode
            // 
            numDefaultMode.Font = new Font("Segoe UI", 9F);
            numDefaultMode.Location = new Point(200, 130);
            numDefaultMode.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numDefaultMode.Name = "numDefaultMode";
            numDefaultMode.Size = new Size(190, 27);
            numDefaultMode.TabIndex = 6;
            // 
            // lblReaderNumber
            // 
            lblReaderNumber.AutoSize = true;
            lblReaderNumber.Font = new Font("Segoe UI", 9F);
            lblReaderNumber.Location = new Point(20, 168);
            lblReaderNumber.Name = "lblReaderNumber";
            lblReaderNumber.Size = new Size(114, 20);
            lblReaderNumber.TabIndex = 7;
            lblReaderNumber.Text = "Reader Number";
            // 
            // numReaderNumber
            // 
            numReaderNumber.Enabled = false;
            numReaderNumber.Font = new Font("Segoe UI", 9F);
            numReaderNumber.Location = new Point(200, 165);
            numReaderNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numReaderNumber.Name = "numReaderNumber";
            numReaderNumber.Size = new Size(190, 27);
            numReaderNumber.TabIndex = 8;
            // 
            // lblReaderType
            // 
            lblReaderType.AutoSize = true;
            lblReaderType.Font = new Font("Segoe UI", 9F);
            lblReaderType.Location = new Point(20, 203);
            lblReaderType.Name = "lblReaderType";
            lblReaderType.Size = new Size(91, 20);
            lblReaderType.TabIndex = 9;
            lblReaderType.Text = "Reader Type";
            // 
            // lblReaderDirection
            // 
            lblReaderDirection.AutoSize = true;
            lblReaderDirection.Font = new Font("Segoe UI", 9F);
            lblReaderDirection.Location = new Point(20, 238);
            lblReaderDirection.Name = "lblReaderDirection";
            lblReaderDirection.Size = new Size(121, 20);
            lblReaderDirection.TabIndex = 11;
            lblReaderDirection.Text = "Reader Direction";
            // 
            // lblStrikeNumber
            // 
            lblStrikeNumber.AutoSize = true;
            lblStrikeNumber.Font = new Font("Segoe UI", 9F);
            lblStrikeNumber.Location = new Point(20, 273);
            lblStrikeNumber.Name = "lblStrikeNumber";
            lblStrikeNumber.Size = new Size(104, 20);
            lblStrikeNumber.TabIndex = 13;
            lblStrikeNumber.Text = "Strike Number";
            // 
            // numStrikeNumber
            // 
            numStrikeNumber.Font = new Font("Segoe UI", 9F);
            numStrikeNumber.Location = new Point(200, 270);
            numStrikeNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numStrikeNumber.Name = "numStrikeNumber";
            numStrikeNumber.Size = new Size(190, 27);
            numStrikeNumber.TabIndex = 14;
            // 
            // lblDoorNumber
            // 
            lblDoorNumber.AutoSize = true;
            lblDoorNumber.Font = new Font("Segoe UI", 9F);
            lblDoorNumber.Location = new Point(20, 308);
            lblDoorNumber.Name = "lblDoorNumber";
            lblDoorNumber.Size = new Size(101, 20);
            lblDoorNumber.TabIndex = 15;
            lblDoorNumber.Text = "Door Number";
            // 
            // numDoorNumber
            // 
            numDoorNumber.Enabled = false;
            numDoorNumber.Font = new Font("Segoe UI", 9F);
            numDoorNumber.Location = new Point(200, 305);
            numDoorNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numDoorNumber.Name = "numDoorNumber";
            numDoorNumber.Size = new Size(190, 27);
            numDoorNumber.TabIndex = 16;
            // 
            // lblRexNumber
            // 
            lblRexNumber.AutoSize = true;
            lblRexNumber.Font = new Font("Segoe UI", 9F);
            lblRexNumber.Location = new Point(20, 343);
            lblRexNumber.Name = "lblRexNumber";
            lblRexNumber.Size = new Size(93, 20);
            lblRexNumber.TabIndex = 17;
            lblRexNumber.Text = "REX Number";
            // 
            // numRexNumber
            // 
            numRexNumber.Font = new Font("Segoe UI", 9F);
            numRexNumber.Location = new Point(200, 340);
            numRexNumber.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            numRexNumber.Name = "numRexNumber";
            numRexNumber.Size = new Size(190, 27);
            numRexNumber.TabIndex = 18;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(26, 95, 180);
            btnSave.Cursor = Cursors.Hand;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(210, 11);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(100, 32);
            btnSave.TabIndex = 0;
            btnSave.Text = "Update";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(180, 180, 180);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 9F);
            btnCancel.Location = new Point(320, 11);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 32);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // bottomPanel
            // 
            bottomPanel.BackColor = Color.FromArgb(245, 245, 245);
            bottomPanel.Controls.Add(btnSave);
            bottomPanel.Controls.Add(btnCancel);
            bottomPanel.Dock = DockStyle.Bottom;
            bottomPanel.Location = new Point(0, 478);
            bottomPanel.Name = "bottomPanel";
            bottomPanel.Size = new Size(422, 55);
            bottomPanel.TabIndex = 19;
            // 
            // cbReaderType
            // 
            cbReaderType.FormattingEnabled = true;
            cbReaderType.Location = new Point(200, 200);
            cbReaderType.Name = "cbReaderType";
            cbReaderType.Size = new Size(190, 28);
            cbReaderType.TabIndex = 20;
            // 
            // cbReaderDirection
            // 
            cbReaderDirection.FormattingEnabled = true;
            cbReaderDirection.Location = new Point(200, 235);
            cbReaderDirection.Name = "cbReaderDirection";
            cbReaderDirection.Size = new Size(190, 28);
            cbReaderDirection.TabIndex = 21;
            // 
            // EditAcrForm
            // 
            BackColor = Color.White;
            ClientSize = new Size(422, 533);
            Controls.Add(cbReaderDirection);
            Controls.Add(cbReaderType);
            Controls.Add(lblTitle);
            Controls.Add(lblName);
            Controls.Add(txtName);
            Controls.Add(lblAcrNumber);
            Controls.Add(numAcrNumber);
            Controls.Add(lblDefaultMode);
            Controls.Add(numDefaultMode);
            Controls.Add(lblReaderNumber);
            Controls.Add(numReaderNumber);
            Controls.Add(lblReaderType);
            Controls.Add(lblReaderDirection);
            Controls.Add(lblStrikeNumber);
            Controls.Add(numStrikeNumber);
            Controls.Add(lblDoorNumber);
            Controls.Add(numDoorNumber);
            Controls.Add(lblRexNumber);
            Controls.Add(numRexNumber);
            Controls.Add(bottomPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(440, 580);
            Name = "EditAcrForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit ACR";
            ((System.ComponentModel.ISupportInitialize)numAcrNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDefaultMode).EndInit();
            ((System.ComponentModel.ISupportInitialize)numReaderNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)numStrikeNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDoorNumber).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRexNumber).EndInit();
            bottomPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        private ComboBox cbReaderType;
        private ComboBox cbReaderDirection;
    }
}