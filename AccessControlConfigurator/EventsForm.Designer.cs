namespace AccessControlConfigurator
{
    partial class EventsControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvEvents;

        private void InitializeComponent()
        {
            topPanel = new Panel();
            btnBack = new Button();
            btnSearch = new Button();
            txtSearch = new TextBox();
            lblSearchRight = new Label();
            btnclr = new Button();
            btnrefresh = new Button();
            btnDelete = new Button();
            lblTitle = new Label();
            dgvEvents = new DataGridView();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).BeginInit();
            SuspendLayout();
            // 
            // topPanel
            // 
            topPanel.BackColor = Color.WhiteSmoke;
            topPanel.Controls.Add(btnBack);
            topPanel.Controls.Add(btnSearch);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(lblSearchRight);
            topPanel.Controls.Add(btnclr);
            topPanel.Controls.Add(btnrefresh);
            topPanel.Controls.Add(btnDelete);
            topPanel.Controls.Add(lblTitle);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(10);
            topPanel.Size = new Size(1200, 45);
            topPanel.TabIndex = 1;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(577, 5);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 13;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click_1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(1151, 8);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(36, 29);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "🔍";
            btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(974, 10);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search here";
            txtSearch.Size = new Size(171, 27);
            txtSearch.TabIndex = 11;
            // 
            // lblSearchRight
            // 
            lblSearchRight.AutoSize = true;
            lblSearchRight.Location = new Point(915, 14);
            lblSearchRight.Name = "lblSearchRight";
            lblSearchRight.Size = new Size(53, 20);
            lblSearchRight.TabIndex = 10;
            lblSearchRight.Text = "Search";
            // 
            // btnclr
            // 
            btnclr.Location = new Point(240, 6);
            btnclr.Name = "btnclr";
            btnclr.Size = new Size(131, 29);
            btnclr.TabIndex = 4;
            btnclr.Text = "Clear Events";
            btnclr.UseVisualStyleBackColor = true;
            btnclr.Click += btnclr_Click;
            // 
            // btnrefresh
            // 
            btnrefresh.Location = new Point(377, 6);
            btnrefresh.Name = "btnrefresh";
            btnrefresh.Size = new Size(94, 29);
            btnrefresh.TabIndex = 3;
            btnrefresh.Text = "Refresh";
            btnrefresh.UseVisualStyleBackColor = true;
            btnrefresh.Click += btnrefresh_Click;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(477, 6);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "Delete";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(13, 10);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(205, 25);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Live Controller Events";
            // 
            // dgvEvents
            // 
            dgvEvents.AllowUserToAddRows = false;
            dgvEvents.AllowUserToResizeColumns = false;
            dgvEvents.AllowUserToResizeRows = false;
            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEvents.BackgroundColor = Color.White;
            dgvEvents.BorderStyle = BorderStyle.None;
            dgvEvents.ColumnHeadersHeight = 29;
            dgvEvents.Dock = DockStyle.Fill;
            dgvEvents.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvEvents.Location = new Point(0, 45);
            dgvEvents.Name = "dgvEvents";
            dgvEvents.ReadOnly = true;
            dgvEvents.RowHeadersVisible = false;
            dgvEvents.RowHeadersWidth = 51;
            dgvEvents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEvents.Size = new Size(1200, 655);
            dgvEvents.TabIndex = 0;
            // 
            // EventsControl
            // 
            BackColor = Color.White;
            Controls.Add(dgvEvents);
            Controls.Add(topPanel);
            Name = "EventsControl";
            Size = new Size(1200, 700);
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEvents).EndInit();
            ResumeLayout(false);
        }
        private Button btnDelete;
        private Button btnrefresh;
        private Button btnclr;
        private Label lblSearchRight;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnBack;
    }
}