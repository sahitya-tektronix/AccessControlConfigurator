using AccessControlConfigurator.Forms;
using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator.Controls
{
    public partial class CardholdersControl : UserControl
    {
        private readonly ApiService _api = new ApiService();
        private List<CardholderDto> _allCardholders = new List<CardholderDto>();
        public CardholdersControl()
        {
            InitializeComponent();

            // ✅ FULL PAGE

            // ✅ MARGIN (THIS REPLACES PANEL)
            this.Padding = new Padding(15); // 🔥 space from all sides

            // ✅ GRID SETTINGS
            dgvCardholders.Dock = DockStyle.Fill;
            dgvCardholders.BorderStyle = BorderStyle.None;
            dgvCardholders.RowHeadersVisible = false;
            dgvCardholders.AllowUserToAddRows = false;
            dgvCardholders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ✅ HEADER STYLE
            dgvCardholders.EnableHeadersVisualStyles = false;
            dgvCardholders.ColumnHeadersHeight = 40;
            dgvCardholders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            dgvCardholders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCardholders.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            _ = LoadCardholders();
        
        LoadCardholders();
        

        // 🔥 FORCE REFRESH
        dgvCardholders.Refresh();
            _ = LoadCardholders();

           
//dgvCardholders.Dock = DockStyle.Fill;
         //   this.Controls.Add(dgvCardholders);

            btnSearch.Click += btnSearch_Click;
            //txtSearch.TextChanged += txtSearch_TextChanged;

            // Button style
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;

            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dgvCardholders.RowHeadersVisible = false;
            dgvCardholders.AllowUserToAddRows = false;

            dgvCardholders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async Task LoadCardholders()
        {
            try
            {
                dgvCardholders.Rows.Clear();

                var list = await _api.GetCardholders();
                _allCardholders = list; //  store full data
                BindGrid(_allCardholders); // 🔥 bind

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGrid(List<CardholderDto> data)
        {
            dgvCardholders.Rows.Clear();

            foreach (var c in data)
            {
                dgvCardholders.Rows.Add(
                    c.cardholderId,
                    c.firstName,
                    c.lastName,
                    c.userName,
                    c.email,
                    c.mobile,
                    c.isActive
                );
            }
        }

        private int GetSelectedId()
        {
            if (dgvCardholders.SelectedRows.Count == 0)
                return 0;

            return Convert.ToInt32(
                dgvCardholders.SelectedRows[0].Cells[0].Value);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadCardholders();
        }


        private async void btnAdd_Click(object sender, EventArgs e)
        {
            AddCardholderForm frm = new AddCardholderForm();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                await LoadCardholders(); // refresh grid after save
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm.Instance.LoadPage(new ControllersControl(), false);
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvCardholders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to delete");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this cardholder?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                int id = Convert.ToInt32(
                    dgvCardholders.SelectedRows[0].Cells[0].Value);

                bool isDeleted = await _api.DeleteCardholder(id);

                if (isDeleted)
                {
                    MessageBox.Show("Deleted successfully");

                    await LoadCardholders();
                    dgvCardholders.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //private void btnEdit_Click(object sender, EventArgs e)
        //{
        //    int id = GetSelectedId();

        //    if (id == 0)
        //        return;

        //    EditCardholderForm frm = new EditCardholderForm(id);
        //    frm.ShowDialog();

        //    _ = LoadCardholders();

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCardholders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row");
                return;
            }

            // ✅ NOW THIS WILL RUN
            var row = dgvCardholders.SelectedRows[0];

            var cardholder = new CardholderDto
            {
                cardholderId = Convert.ToInt32(row.Cells[0].Value),
                firstName = row.Cells[1].Value?.ToString(),
                lastName = row.Cells[2].Value?.ToString(),
                mobile = row.Cells[5].Value?.ToString(),
                email = row.Cells[4].Value?.ToString(),
                department = ""
            }; ;

            var form = new EditCardholderForm(cardholder);

            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCardholders(); // 🔥 refresh grid
            }
        }
        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    ApplySearch();
        //}
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearch();
        }
        private void ApplySearch()
        {
            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                BindGrid(_allCardholders);
                return;
            }

            var filtered = _allCardholders.Where(c =>
                c.cardholderId.ToString().Contains(keyword) ||
                (c.firstName != null && c.firstName.ToLower().Contains(keyword)) ||
                (c.lastName != null && c.lastName.ToLower().Contains(keyword)) ||
                (c.userName != null && c.userName.ToLower().Contains(keyword)) ||
                (c.email != null && c.email.ToLower().Contains(keyword)) ||
                (c.mobile != null && c.mobile.ToLower().Contains(keyword)) ||
                c.isActive.ToString().ToLower().Contains(keyword)
            ).ToList();

            BindGrid(filtered);

            // 🔥 OPTIONAL: No data message
            if (filtered.Count == 0)
            {
                //MessageBox.Show("No matching data");
            }
        }
            private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }
    }
    
}