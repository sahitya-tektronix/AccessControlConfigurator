using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class QueryHolidayForm
    {
        private Panel headerPanel;
        private Label lblTitle;

        private Button btnQuery;
        private Button btnClose;

        private DataGridView dgvResult;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "Query Holidays";
            this.Size = new Size(700, 450);
            this.StartPosition = FormStartPosition.CenterParent;

            headerPanel = new Panel();
            headerPanel.BackColor = Color.FromArgb(52, 73, 94);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 50;

            lblTitle = new Label();
            lblTitle.Text = "Holiday Query";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.AutoSize = true;

            headerPanel.Controls.Add(lblTitle);

            btnQuery = new Button();
            btnQuery.Text = "Query";
            btnQuery.Location = new Point(20, 70);
            btnQuery.Size = new Size(80, 35);
            btnQuery.BackColor = Color.SeaGreen;
            btnQuery.ForeColor = Color.White;
            btnQuery.Click += btnQuery_Click;

            btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Location = new Point(120, 70);
            btnClose.Size = new Size(80, 35);
            btnClose.Click += btnClose_Click;

            dgvResult = new DataGridView();
            dgvResult.Location = new Point(20, 120);
            dgvResult.Size = new Size(640, 260);
            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(headerPanel);
            Controls.Add(btnQuery);
            Controls.Add(btnClose);
            Controls.Add(dgvResult);

            this.ResumeLayout(false);
        }
    }
}