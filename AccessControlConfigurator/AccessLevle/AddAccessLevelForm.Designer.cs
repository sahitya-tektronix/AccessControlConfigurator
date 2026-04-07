using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class AddAccessLevelForm
    {
        private Label lblHeader;
        private Label lblName;
        private TextBox txtName;

        private Panel headerPanel;
        private Panel bodyPanel;
        private Panel summaryPanel;
        private Panel footerPanel;

        private Label lblSearch;
        private TextBox txtSearch;
        private Button btnSearch;

        private Label lblBulkTimeZone;
        private ComboBox cmbBulkTimeZone;
        private Button btnApplyToSelected;
        private Button btnClearAll;

        private Label lblDoorsHeader;
        private DataGridView dgvMappings;

        private Label lblTotalDoorsTitle;
        private Label lblTotalDoorsValue;
        private Label lblAssignedTitle;
        private Label lblAssignedValue;
        private Label lblUnassignedTitle;
        private Label lblUnassignedValue;

        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblHeader = new Label();

            bodyPanel = new Panel();
            lblName = new Label();
            txtName = new TextBox();

            summaryPanel = new Panel();
            lblTotalDoorsTitle = new Label();
            lblTotalDoorsValue = new Label();
            lblAssignedTitle = new Label();
            lblAssignedValue = new Label();
            lblUnassignedTitle = new Label();
            lblUnassignedValue = new Label();

            lblSearch = new Label();
            txtSearch = new TextBox();
            btnSearch = new Button();

            lblBulkTimeZone = new Label();
            cmbBulkTimeZone = new ComboBox();
            btnApplyToSelected = new Button();
            btnClearAll = new Button();

            lblDoorsHeader = new Label();
            dgvMappings = new DataGridView();

            footerPanel = new Panel();
            btnSave = new Button();
            btnCancel = new Button();

            headerPanel.SuspendLayout();
            bodyPanel.SuspendLayout();
            summaryPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMappings).BeginInit();
            footerPanel.SuspendLayout();
            SuspendLayout();

            // headerPanel
            headerPanel.BackColor = Color.FromArgb(45, 62, 80);
            headerPanel.Controls.Add(lblHeader);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(980, 74);
            headerPanel.TabIndex = 0;

            // lblHeader
            lblHeader.AutoSize = true;
            lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.Location = new Point(352, 18);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(283, 37);
            lblHeader.TabIndex = 0;
            lblHeader.Text = "ADD ACCESS LEVEL";

            // bodyPanel
            bodyPanel.BackColor = Color.FromArgb(245, 247, 250);
            bodyPanel.Controls.Add(lblName);
            bodyPanel.Controls.Add(txtName);
            bodyPanel.Controls.Add(summaryPanel);
            bodyPanel.Controls.Add(lblSearch);
            bodyPanel.Controls.Add(txtSearch);
            bodyPanel.Controls.Add(btnSearch);
            bodyPanel.Controls.Add(lblBulkTimeZone);
            bodyPanel.Controls.Add(cmbBulkTimeZone);
            bodyPanel.Controls.Add(btnApplyToSelected);
            bodyPanel.Controls.Add(btnClearAll);
            bodyPanel.Controls.Add(lblDoorsHeader);
            bodyPanel.Controls.Add(dgvMappings);
            bodyPanel.Controls.Add(footerPanel);
            bodyPanel.Dock = DockStyle.Fill;
            bodyPanel.Location = new Point(0, 74);
            bodyPanel.Name = "bodyPanel";
            bodyPanel.Padding = new Padding(24, 18, 24, 18);
            bodyPanel.Size = new Size(980, 646);
            bodyPanel.TabIndex = 1;

            // lblName
            lblName.AutoSize = true;
            lblName.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            lblName.Location = new Point(38, 24);
            lblName.Name = "lblName";
            lblName.Size = new Size(161, 25);
            lblName.TabIndex = 0;
            lblName.Text = "Access Level Name";

            // txtName
            txtName.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            txtName.Location = new Point(38, 54);
            txtName.Name = "txtName";
            txtName.Size = new Size(430, 32);
            txtName.TabIndex = 1;

            // summaryPanel
            summaryPanel.BackColor = Color.White;
            summaryPanel.BorderStyle = BorderStyle.FixedSingle;
            summaryPanel.Controls.Add(lblTotalDoorsTitle);
            summaryPanel.Controls.Add(lblTotalDoorsValue);
            summaryPanel.Controls.Add(lblAssignedTitle);
            summaryPanel.Controls.Add(lblAssignedValue);
            summaryPanel.Controls.Add(lblUnassignedTitle);
            summaryPanel.Controls.Add(lblUnassignedValue);
            summaryPanel.Location = new Point(510, 24);
            summaryPanel.Name = "summaryPanel";
            summaryPanel.Size = new Size(430, 62);
            summaryPanel.TabIndex = 2;

            // lblTotalDoorsTitle
            lblTotalDoorsTitle.AutoSize = true;
            lblTotalDoorsTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblTotalDoorsTitle.Location = new Point(18, 10);
            lblTotalDoorsTitle.Name = "lblTotalDoorsTitle";
            lblTotalDoorsTitle.Size = new Size(81, 20);
            lblTotalDoorsTitle.TabIndex = 0;
            lblTotalDoorsTitle.Text = "Total Doors";

            // lblTotalDoorsValue
            lblTotalDoorsValue.AutoSize = true;
            lblTotalDoorsValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTotalDoorsValue.ForeColor = Color.FromArgb(22, 125, 211);
            lblTotalDoorsValue.Location = new Point(43, 30);
            lblTotalDoorsValue.Name = "lblTotalDoorsValue";
            lblTotalDoorsValue.Size = new Size(24, 28);
            lblTotalDoorsValue.TabIndex = 1;
            lblTotalDoorsValue.Text = "0";

            // lblAssignedTitle
            lblAssignedTitle.AutoSize = true;
            lblAssignedTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblAssignedTitle.Location = new Point(171, 10);
            lblAssignedTitle.Name = "lblAssignedTitle";
            lblAssignedTitle.Size = new Size(68, 20);
            lblAssignedTitle.TabIndex = 2;
            lblAssignedTitle.Text = "Assigned";

            // lblAssignedValue
            lblAssignedValue.AutoSize = true;
            lblAssignedValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAssignedValue.ForeColor = Color.Green;
            lblAssignedValue.Location = new Point(192, 30);
            lblAssignedValue.Name = "lblAssignedValue";
            lblAssignedValue.Size = new Size(24, 28);
            lblAssignedValue.TabIndex = 3;
            lblAssignedValue.Text = "0";

            // lblUnassignedTitle
            lblUnassignedTitle.AutoSize = true;
            lblUnassignedTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblUnassignedTitle.Location = new Point(308, 10);
            lblUnassignedTitle.Name = "lblUnassignedTitle";
            lblUnassignedTitle.Size = new Size(88, 20);
            lblUnassignedTitle.TabIndex = 4;
            lblUnassignedTitle.Text = "Unassigned";

            // lblUnassignedValue
            lblUnassignedValue.AutoSize = true;
            lblUnassignedValue.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblUnassignedValue.ForeColor = Color.DarkOrange;
            lblUnassignedValue.Location = new Point(340, 30);
            lblUnassignedValue.Name = "lblUnassignedValue";
            lblUnassignedValue.Size = new Size(24, 28);
            lblUnassignedValue.TabIndex = 5;
            lblUnassignedValue.Text = "0";

            // lblSearch
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F);
            lblSearch.Location = new Point(38, 108);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(96, 23);
            lblSearch.TabIndex = 3;
            lblSearch.Text = "Search Door";

            // txtSearch
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(38, 136);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Type door name";
            txtSearch.Size = new Size(270, 30);
            txtSearch.TabIndex = 4;

            // btnSearch
            btnSearch.Size = new Size(100, 36);
            btnSearch.Location = new Point(320, 136);
            btnSearch.BackColor = Color.FromArgb(22, 125, 211);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.Name = "btnSearch";
            btnSearch.TabIndex = 5;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = false;

            // lblBulkTimeZone
            lblBulkTimeZone.AutoSize = true;
            lblBulkTimeZone.Font = new Font("Segoe UI", 10F);
            lblBulkTimeZone.Location = new Point(510, 108);
            lblBulkTimeZone.Name = "lblBulkTimeZone";
            lblBulkTimeZone.Size = new Size(139, 23);
            lblBulkTimeZone.TabIndex = 6;
            lblBulkTimeZone.Text = "Bulk Time Zone";

            // cmbBulkTimeZone
            cmbBulkTimeZone.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBulkTimeZone.Font = new Font("Segoe UI", 10F);
            cmbBulkTimeZone.Location = new Point(510, 136);
            cmbBulkTimeZone.Name = "cmbBulkTimeZone";
            cmbBulkTimeZone.Size = new Size(220, 31);
            cmbBulkTimeZone.TabIndex = 7;

            // btnApplyToSelected
            btnApplyToSelected.Size = new Size(130, 36);
            btnApplyToSelected.Location = new Point(740, 136);
            btnApplyToSelected.BackColor = Color.FromArgb(22, 125, 211);
            btnApplyToSelected.ForeColor = Color.White;
            btnApplyToSelected.FlatStyle = FlatStyle.Flat;
            btnApplyToSelected.FlatAppearance.BorderSize = 0;
            btnApplyToSelected.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnApplyToSelected.Name = "btnApplyToSelected";
            btnApplyToSelected.TabIndex = 8;
            btnApplyToSelected.Text = "Apply Selected";
            btnApplyToSelected.UseVisualStyleBackColor = false;

            // btnClearAll
            btnClearAll.Size = new Size(96, 36);
            btnClearAll.Location = new Point(878, 136);
            btnClearAll.BackColor = Color.FromArgb(108, 117, 125);
            btnClearAll.ForeColor = Color.White;
            btnClearAll.FlatStyle = FlatStyle.Flat;
            btnClearAll.FlatAppearance.BorderSize = 0;
            btnClearAll.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.TabIndex = 9;
            btnClearAll.Text = "Clear All";
            btnClearAll.UseVisualStyleBackColor = false;

            // lblDoorsHeader
            lblDoorsHeader.AutoSize = true;
            lblDoorsHeader.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDoorsHeader.Location = new Point(38, 188);
            lblDoorsHeader.Name = "lblDoorsHeader";
            lblDoorsHeader.Size = new Size(180, 25);
            lblDoorsHeader.TabIndex = 10;
            lblDoorsHeader.Text = "Doors and Time Zones";

            // dgvMappings
            dgvMappings.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvMappings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMappings.Location = new Point(38, 222);
            dgvMappings.Name = "dgvMappings";
            dgvMappings.RowHeadersWidth = 51;
            dgvMappings.Size = new Size(902, 336);
            dgvMappings.TabIndex = 11;

            // footerPanel
            footerPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            footerPanel.BackColor = Color.Transparent;
            footerPanel.Controls.Add(btnSave);
            footerPanel.Controls.Add(btnCancel);
            footerPanel.Location = new Point(38, 573);
            footerPanel.Name = "footerPanel";
            footerPanel.Size = new Size(902, 46);
            footerPanel.TabIndex = 12;

            // btnSave
            btnSave.BackColor = Color.Green;
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(318, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 38);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;

            // btnCancel
            btnCancel.BackColor = Color.Red;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(462, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 38);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;

            // AddAccessLevelForm
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(980, 720);
            Controls.Add(bodyPanel);
            Controls.Add(headerPanel);
            MinimumSize = new Size(1000, 760);
            Name = "AddAccessLevelForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Access Level";

            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            bodyPanel.ResumeLayout(false);
            bodyPanel.PerformLayout();
            summaryPanel.ResumeLayout(false);
            summaryPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvMappings).EndInit();
            footerPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
