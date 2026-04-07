using AccessControlConfigurator.Helpers;

namespace AccessControlConfigurator
{
    partial class EditCardForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.Label lblAccessLevel;
        private System.Windows.Forms.Label lblCardholder;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;

        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.TextBox txtCardholder;

        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.Button btnClearStart;
        private System.Windows.Forms.Button btnClearEnd;

        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnCancel;

        private void InitializeComponent()
        {
            headerPanel = new System.Windows.Forms.Panel();
            lblHeader = new System.Windows.Forms.Label();
            lblCardNumber = new Label();
            lblAccessLevel = new Label();
            lblCardholder = new Label();
            lblStart = new Label();
            lblEnd = new Label();
            txtCardNumber = new TextBox();
            txtCardholder = new TextBox();
            dtStart = new DateTimePicker();
            dtEnd = new DateTimePicker();
            btnClearStart = new Button();
            btnClearEnd = new Button();
            btnUpdate = new Button();
            btnCancel = new Button();
            cbAccessLevel = new ComboBox();
            headerPanel.SuspendLayout();
            SuspendLayout();
            //
            // headerPanel + lblHeader
            //
            UIStyleHelper.StyleHeaderPanel(headerPanel);
            headerPanel.Controls.Add(lblHeader);

            lblHeader.Text = "EDIT CARD";
            lblHeader.AutoSize = false;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleCenter;
            lblHeader.Font = UIStyleHelper.StandardFonts.HeaderFont;
            lblHeader.ForeColor = UIStyleHelper.StandardColors.HeaderForeground;
            //
            // lblCardNumber
            // 
            lblCardNumber.Location = new Point(30, 60);
            lblCardNumber.Name = "lblCardNumber";
            lblCardNumber.Size = new Size(100, 23);
            lblCardNumber.TabIndex = 1;
            lblCardNumber.Text = "Card Number";
            // 
            // lblAccessLevel
            // 
            lblAccessLevel.Location = new Point(30, 100);
            lblAccessLevel.Name = "lblAccessLevel";
            lblAccessLevel.Size = new Size(100, 23);
            lblAccessLevel.TabIndex = 3;
            lblAccessLevel.Text = "Access Level";
            // 
            // lblCardholder
            // 
            lblCardholder.Location = new Point(0, 0);
            lblCardholder.Name = "lblCardholder";
            lblCardholder.Size = new Size(100, 23);
            lblCardholder.TabIndex = 5;
            // 
            // lblStart
            // 
            lblStart.Location = new Point(30, 148);
            lblStart.Name = "lblStart";
            lblStart.Size = new Size(100, 23);
            lblStart.TabIndex = 7;
            lblStart.Text = "Start Date";
            // 
            // lblEnd
            // 
            lblEnd.Location = new Point(30, 198);
            lblEnd.Name = "lblEnd";
            lblEnd.Size = new Size(100, 23);
            lblEnd.TabIndex = 9;
            lblEnd.Text = "End Date";
            // 
            // txtCardNumber
            // 
            txtCardNumber.Location = new Point(150, 55);
            txtCardNumber.Name = "txtCardNumber";
            txtCardNumber.Size = new Size(200, 27);
            txtCardNumber.TabIndex = 2;
            // 
            // txtCardholder
            // 
            txtCardholder.Location = new Point(0, 0);
            txtCardholder.Name = "txtCardholder";
            txtCardholder.Size = new Size(100, 27);
            txtCardholder.TabIndex = 6;
            // 
            // dtStart
            // 
            dtStart.Location = new Point(150, 148);
            dtStart.Name = "dtStart";
            dtStart.Size = new Size(140, 27);
            dtStart.TabIndex = 8;
            // 
            // dtEnd
            // 
            dtEnd.Location = new Point(150, 198);
            dtEnd.Name = "dtEnd";
            dtEnd.Size = new Size(140, 27);
            dtEnd.TabIndex = 10;
            // 
            // btnClearStart
            // 
            btnClearStart.Location = new Point(300, 148);
            btnClearStart.Name = "btnClearStart";
            btnClearStart.Size = new Size(70, 27);
            btnClearStart.TabIndex = 9;
            btnClearStart.Text = "Clear";
            btnClearStart.UseVisualStyleBackColor = true;
            // 
            // btnClearEnd
            // 
            btnClearEnd.Location = new Point(300, 198);
            btnClearEnd.Name = "btnClearEnd";
            btnClearEnd.Size = new Size(70, 27);
            btnClearEnd.TabIndex = 11;
            btnClearEnd.Text = "Clear";
            btnClearEnd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.SeaGreen;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(150, 260);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(90, 35);
            btnUpdate.TabIndex = 11;
            btnUpdate.Text = "Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Firebrick;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(260, 260);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 35);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // cbAccessLevel
            // 
            cbAccessLevel.FormattingEnabled = true;
            cbAccessLevel.Location = new Point(151, 104);
            cbAccessLevel.Name = "cbAccessLevel";
            cbAccessLevel.Size = new Size(199, 28);
            cbAccessLevel.TabIndex = 13;
            // 
            // EditCardForm
            // 
            ClientSize = new Size(400, 330);
            Controls.Add(cbAccessLevel);
            Controls.Add(headerPanel);
            Controls.Add(lblCardNumber);
            Controls.Add(txtCardNumber);
            Controls.Add(lblAccessLevel);
            Controls.Add(lblCardholder);
            Controls.Add(txtCardholder);
            Controls.Add(lblStart);
            Controls.Add(dtStart);
            Controls.Add(btnClearStart);
            Controls.Add(lblEnd);
            Controls.Add(dtEnd);
            Controls.Add(btnClearEnd);
            Controls.Add(btnUpdate);
            Controls.Add(btnCancel);
            Name = "EditCardForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Card";
            headerPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        private ComboBox cbAccessLevel;
    }
}
