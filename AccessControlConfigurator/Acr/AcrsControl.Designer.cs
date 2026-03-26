using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace AccessControlConfigurator
{
    partial class AcrsControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblacr;

        private Button btnBack;
        private Button btnRefresh;
        //private Button btnAdd;
        private Button btnEdit;
        private ComboBox cmbControllerId;
        private ComboBox cmbSioNumber;
        private ComboBox cmbReader;

        private Label lblController;
        private Label lblSio;
        private Label lblReader;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();

            // ===== TITLE =====
            lblacr = new Label();
            lblacr.Dock = DockStyle.Top;
            lblacr.Height = 40;
            lblacr.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblacr.Text = "ACRs (Doors)";
            lblacr.TextAlign = ContentAlignment.MiddleLeft;
            lblacr.Padding = new Padding(10, 0, 0, 0);
            lblacr.BackColor = Color.FromArgb(245, 245, 245);

            // ===== TOP PANEL =====
            Panel topPanel = new Panel();
            topPanel.Dock = DockStyle.Top;
            topPanel.Height = 45;
            topPanel.BackColor = Color.White;

            // ===== BUTTONS =====
            btnEdit = new Button();
            btnEdit.Text = "Edit";
            btnEdit.Size = new Size(80, 28);
            btnEdit.Location = new Point(10, 8);
            btnEdit.Click += btnEdit_Click;

            btnRefresh = new Button();
            btnRefresh.Text = "Refresh";
            btnRefresh.Size = new Size(80, 28);
            btnRefresh.Location = new Point(100, 8);
            btnRefresh.Click += btnRefresh_Click;

            btnBack = new Button();
            btnBack.Text = "Back";
            btnBack.Size = new Size(80, 28);
            btnBack.Location = new Point(190, 8);
            btnBack.Click += btnBack_Click;

            // ===== FILTERS =====
            lblController = new Label();
            lblController.Text = "Controller";
            lblController.Location = new Point(300, 12);
            lblController.AutoSize = true;

            cmbControllerId = new ComboBox();
            cmbControllerId.Location = new Point(380, 8);
            cmbControllerId.Size = new Size(70, 28);
            cmbControllerId.DropDownStyle = ComboBoxStyle.DropDownList;

            lblSio = new Label();
            lblSio.Text = "Sio";
            lblSio.Location = new Point(460, 12);
            lblSio.AutoSize = true;

            cmbSioNumber = new ComboBox();
            cmbSioNumber.Location = new Point(490, 8);
            cmbSioNumber.Size = new Size(60, 28);
            cmbSioNumber.DropDownStyle = ComboBoxStyle.DropDownList;

            lblReader = new Label();
            lblReader.Text = "Reader";
            lblReader.Location = new Point(560, 12);
            lblReader.AutoSize = true;

            cmbReader = new ComboBox();
            cmbReader.Location = new Point(620, 8);
            cmbReader.Size = new Size(90, 28);
            cmbReader.DropDownStyle = ComboBoxStyle.DropDownList;

            // ===== SEARCH =====
            lblSearchRight = new Label();
            lblSearchRight.Text = "Search";
            lblSearchRight.Location = new Point(720, 12);
            lblSearchRight.AutoSize = true;

            txtSearch = new TextBox();
            txtSearch.Location = new Point(780, 8);
            txtSearch.Size = new Size(160, 27);

            btnSearch = new Button();
            btnSearch.Text = "🔍";
            btnSearch.Size = new Size(36, 27);
            btnSearch.Location = new Point(950, 8);
            btnSearch.Click += btnSearch_Click;

            // ===== ADD CONTROLS TO PANEL =====
            topPanel.Controls.Add(btnEdit);
            topPanel.Controls.Add(btnRefresh);
            topPanel.Controls.Add(btnBack);

            topPanel.Controls.Add(lblController);
            topPanel.Controls.Add(cmbControllerId);

            topPanel.Controls.Add(lblSio);
            topPanel.Controls.Add(cmbSioNumber);

            topPanel.Controls.Add(lblReader);
            topPanel.Controls.Add(cmbReader);

            topPanel.Controls.Add(lblSearchRight);
            topPanel.Controls.Add(txtSearch);
            topPanel.Controls.Add(btnSearch);

            // ===== GRID =====
            dgvAcrs = new DataGridView();
            dgvAcrs.Dock = DockStyle.Fill;
            dgvAcrs.BackgroundColor = Color.White;
            dgvAcrs.BorderStyle = BorderStyle.None;
            dgvAcrs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAcrs.AllowUserToAddRows = false;
            dgvAcrs.RowHeadersVisible = false;

            dataGridViewCellStyle2.BackColor = Color.FromArgb(245, 245, 245);
            dgvAcrs.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle2;

            dataGridViewCellStyle3.BackColor = Color.FromArgb(240, 240, 240);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvAcrs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvAcrs.ColumnHeadersHeight = 35;

            dgvAcrs.Columns.Add("ColId", "ID");
            dgvAcrs.Columns.Add("colName", "ACR Name");
            dgvAcrs.Columns.Add("ColControllerId", "Controller Id");
            dgvAcrs.Columns.Add("ColSioNumber", "Sio Number");
            dgvAcrs.Columns.Add("colReader", "Reader Name");
            dgvAcrs.Columns.Add("colAcrNumber", "ACR Number");
            dgvAcrs.Columns.Add("colOnline", "Online");

            dgvAcrs.Dock = DockStyle.Fill;

            dgvAcrs.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvAcrs.AllowUserToResizeColumns = false;
            dgvAcrs.AllowUserToResizeRows = false;

            dgvAcrs.ColumnHeadersHeightSizeMode =
                DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            foreach (DataGridViewColumn col in dgvAcrs.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
            }




            // Column ratios
            dgvAcrs.Columns["ColId"].FillWeight = 50;
            dgvAcrs.Columns["colName"].FillWeight = 150;
            dgvAcrs.Columns["ColControllerId"].FillWeight = 120;
            dgvAcrs.Columns["ColSioNumber"].FillWeight = 120;
            dgvAcrs.Columns["colReader"].FillWeight = 120;
            dgvAcrs.Columns["colAcrNumber"].FillWeight = 120;
            dgvAcrs.Columns["colOnline"].FillWeight = 100;

            dgvAcrs.Columns["ColId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAcrs.Columns["ColControllerId"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAcrs.Columns["ColSioNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAcrs.Columns["colAcrNumber"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAcrs.Columns["colOnline"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgvAcrs.EnableHeadersVisualStyles = false;
            dgvAcrs.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvAcrs.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // ===== FINAL ADD ORDER (VERY IMPORTANT) =====
            Controls.Add(dgvAcrs);   // bottom
            Controls.Add(topPanel);  // toolbar
            Controls.Add(lblacr);    // title

            Size = new Size(1000, 600);
        }
        private Label lblSearchRight;
        private TextBox txtSearch;
        private Button btnSearch;
        private DataGridViewTextBoxColumn colOnline;
        private DataGridViewTextBoxColumn colAcrNumber;
        private DataGridViewTextBoxColumn colReader;
        private DataGridViewTextBoxColumn ColSioNumber;
        private DataGridViewTextBoxColumn ColControllerId;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn ColId;
        private DataGridView dgvAcrs;
    }
}