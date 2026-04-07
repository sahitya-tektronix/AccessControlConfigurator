namespace AccessControlConfigurator
{
    partial class ConnectionSettingsForm
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
            lblBaseUrl   = new System.Windows.Forms.Label();
            cmbProtocol  = new System.Windows.Forms.ComboBox();
            txtBaseUrl   = new System.Windows.Forms.TextBox();
            btnSave      = new System.Windows.Forms.Button();
            btnReset     = new System.Windows.Forms.Button();
            btnCancel    = new System.Windows.Forms.Button();
            SuspendLayout();
            // lblBaseUrl
            lblBaseUrl.AutoSize = true;
            lblBaseUrl.Location = new System.Drawing.Point(24, 18);
            lblBaseUrl.Name = "lblBaseUrl";
            lblBaseUrl.TabIndex = 0;
            lblBaseUrl.Text = "Base URL";
            // cmbProtocol
            cmbProtocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbProtocol.Items.AddRange(new object[] { "http / ws", "https / wss" });
            cmbProtocol.Location = new System.Drawing.Point(24, 46);
            cmbProtocol.Name = "cmbProtocol";
            cmbProtocol.Size = new System.Drawing.Size(112, 28);
            cmbProtocol.TabIndex = 1;
            cmbProtocol.SelectedIndexChanged += cmbProtocol_SelectedIndexChanged;
            // txtBaseUrl
            txtBaseUrl.Location = new System.Drawing.Point(144, 46);
            txtBaseUrl.Name = "txtBaseUrl";
            txtBaseUrl.Size = new System.Drawing.Size(340, 27);
            txtBaseUrl.TabIndex = 2;
            txtBaseUrl.TextChanged += txtBaseUrl_TextChanged;
            // btnSave
            btnSave.Location = new System.Drawing.Point(24, 96);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(110, 38);
            btnSave.TabIndex = 3;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // btnReset
            btnReset.Location = new System.Drawing.Point(144, 96);
            btnReset.Name = "btnReset";
            btnReset.Size = new System.Drawing.Size(120, 38);
            btnReset.TabIndex = 4;
            btnReset.Text = "Reset Default";
            btnReset.UseVisualStyleBackColor = true;
            btnReset.Click += btnReset_Click;
            // btnCancel
            btnCancel.Location = new System.Drawing.Point(374, 96);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(110, 38);
            btnCancel.TabIndex = 5;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // ConnectionSettingsForm
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(514, 158);
            Controls.Add(btnCancel);
            Controls.Add(btnReset);
            Controls.Add(btnSave);
            Controls.Add(txtBaseUrl);
            Controls.Add(cmbProtocol);
            Controls.Add(lblBaseUrl);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ConnectionSettingsForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Connection Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.Label lblBaseUrl;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.TextBox txtBaseUrl;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnCancel;
    }
}
