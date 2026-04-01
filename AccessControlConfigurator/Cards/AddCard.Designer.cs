using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Font = System.Drawing.Font;

namespace AccessControlConfigurator
{
    partial class AddCardForm
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblCardNumber;
        private Label lblAccessLevel;
        private Label lblStartDate;
        private Label lblEndDate;
        private Label lblCardholder;

        private TextBox txtCardNumber;
        private TextBox txtAccessLevel;
        //private TextBox txtCardholder;

        private DateTimePicker dtStart;
        private DateTimePicker dtEnd;
        private Button btnClearStart;
        private Button btnClearEnd;

        private Button btnSave;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblCardNumber = new Label();
            lblAccessLevel = new Label();
            lblStartDate = new Label();
            lblEndDate = new Label();
            lblCardholder = new Label();
            txtCardNumber = new TextBox();
            txtAccessLevel = new TextBox();
            dtStart = new DateTimePicker();
            dtEnd = new DateTimePicker();
            btnClearStart = new Button();
            btnClearEnd = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(121, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add Card";
            // 
            // lblCardNumber
            // 
            lblCardNumber.AutoSize = true;
            lblCardNumber.Location = new Point(30, 80);
            lblCardNumber.Name = "lblCardNumber";
            lblCardNumber.Size = new Size(98, 20);
            lblCardNumber.TabIndex = 1;
            lblCardNumber.Text = "Card Number";
            // 
            // lblAccessLevel
            // 
            lblAccessLevel.AutoSize = true;
            lblAccessLevel.Location = new Point(30, 130);
            lblAccessLevel.Name = "lblAccessLevel";
            lblAccessLevel.Size = new Size(91, 20);
            lblAccessLevel.TabIndex = 3;
            lblAccessLevel.Text = "Access Level";
            // 
            // lblStartDate
            // 
            lblStartDate.AutoSize = true;
            lblStartDate.Location = new Point(30, 180);
            lblStartDate.Name = "lblStartDate";
            lblStartDate.Size = new Size(113, 20);
            lblStartDate.TabIndex = 5;
            lblStartDate.Text = "Start Date Time";
            // 
            // lblEndDate
            // 
            lblEndDate.AutoSize = true;
            lblEndDate.Location = new Point(30, 230);
            lblEndDate.Name = "lblEndDate";
            lblEndDate.Size = new Size(107, 20);
            lblEndDate.TabIndex = 7;
            lblEndDate.Text = "End Date Time";
            // 
            // lblCardholder
            // 
            lblCardholder.Location = new Point(0, 0);
            lblCardholder.Name = "lblCardholder";
            lblCardholder.Size = new Size(100, 23);
            lblCardholder.TabIndex = 9;
            // 
            // txtCardNumber
            // 
            txtCardNumber.Location = new Point(180, 75);
            txtCardNumber.Name = "txtCardNumber";
            txtCardNumber.Size = new Size(200, 27);
            txtCardNumber.TabIndex = 2;
            // 
            // txtAccessLevel
            // 
            txtAccessLevel.Location = new Point(180, 125);
            txtAccessLevel.Name = "txtAccessLevel";
            txtAccessLevel.Size = new Size(200, 27);
            txtAccessLevel.TabIndex = 4;
            // 
            // dtStart
            // 
            dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            dtStart.Format = DateTimePickerFormat.Custom;
            dtStart.Location = new Point(180, 175);
            dtStart.Name = "dtStart";
            dtStart.Size = new Size(190, 27);
            dtStart.TabIndex = 6;
            // 
            // dtEnd
            // 
            dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            dtEnd.Format = DateTimePickerFormat.Custom;
            dtEnd.Location = new Point(180, 225);
            dtEnd.Name = "dtEnd";
            dtEnd.Size = new Size(190, 27);
            dtEnd.TabIndex = 8;
            // 
            // btnClearStart
            // 
            btnClearStart.Location = new Point(380, 175);
            btnClearStart.Name = "btnClearStart";
            btnClearStart.Size = new Size(70, 27);
            btnClearStart.TabIndex = 7;
            btnClearStart.Text = "Clear";
            btnClearStart.UseVisualStyleBackColor = true;
            // 
            // btnClearEnd
            // 
            btnClearEnd.Location = new Point(380, 225);
            btnClearEnd.Name = "btnClearEnd";
            btnClearEnd.Size = new Size(70, 27);
            btnClearEnd.TabIndex = 9;
            btnClearEnd.Text = "Clear";
            btnClearEnd.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.SeaGreen;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(180, 330);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 35);
            btnSave.TabIndex = 11;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.Red;
            btnCancel.Location = new Point(286, 330);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 35);
            btnCancel.TabIndex = 12;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click_1;
            // 
            // AddCardForm
            // 
            ClientSize = new Size(470, 400);
            Controls.Add(btnCancel);
            Controls.Add(lblTitle);
            Controls.Add(lblCardNumber);
            Controls.Add(txtCardNumber);
            Controls.Add(lblAccessLevel);
            Controls.Add(txtAccessLevel);
            Controls.Add(lblStartDate);
            Controls.Add(dtStart);
            Controls.Add(btnClearStart);
            Controls.Add(lblEndDate);
            Controls.Add(dtEnd);
            Controls.Add(btnClearEnd);
            Controls.Add(lblCardholder);
            Controls.Add(btnSave);
            Name = "AddCardForm";
            Text = "Add Card";
            ResumeLayout(false);
            PerformLayout();
        }
        private Button btnCancel;
    }
}
