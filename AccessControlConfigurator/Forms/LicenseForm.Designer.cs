namespace AccessControlConfigurator
{
    partial class LicenseForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblTitle      = new System.Windows.Forms.Label();
            lblLicenseKey = new System.Windows.Forms.Label();
            txtLicenseKey = new System.Windows.Forms.TextBox();
            btnSave       = new System.Windows.Forms.Button();
            btnClear      = new System.Windows.Forms.Button();
            btnCancel     = new System.Windows.Forms.Button();
            SuspendLayout();
            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            lblTitle.Location = new System.Drawing.Point(24, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.TabIndex = 0;
            lblTitle.Text = "License";
            // lblLicenseKey
            lblLicenseKey.AutoSize = true;
            lblLicenseKey.Location = new System.Drawing.Point(24, 75);
            lblLicenseKey.Name = "lblLicenseKey";
            lblLicenseKey.TabIndex = 1;
            lblLicenseKey.Text = "License Key";
            // txtLicenseKey
            txtLicenseKey.Location = new System.Drawing.Point(24, 100);
            txtLicenseKey.Name = "txtLicenseKey";
            txtLicenseKey.Size = new System.Drawing.Size(520, 27);
            txtLicenseKey.TabIndex = 2;
            // btnSave
            btnSave.Location = new System.Drawing.Point(24, 155);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(110, 38);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // btnClear
            btnClear.Location = new System.Drawing.Point(144, 155);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(110, 38);
            btnClear.TabIndex = 4;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // btnCancel
            btnCancel.Location = new System.Drawing.Point(434, 155);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(110, 38);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // LicenseForm
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(570, 220);
            Controls.Add(btnCancel);
            Controls.Add(btnClear);
            Controls.Add(btnSave);
            Controls.Add(txtLicenseKey);
            Controls.Add(lblLicenseKey);
            Controls.Add(lblTitle);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "LicenseForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "License";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblLicenseKey;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
    }
}
