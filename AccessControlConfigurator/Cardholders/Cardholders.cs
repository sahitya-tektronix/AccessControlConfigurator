using AccessControlConfigurator.Forms;
using AccessControlConfigurator.Helpers;
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
            _ = LoadCardholders();

            btnSearch.Click += btnSearch_Click;

            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;

            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearFilters.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            txtNameFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtEmailFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNameFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEmailFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            txtNameFilter.PlaceholderText = "Search name";
            txtEmailFilter.PlaceholderText = "Search email";

            GridStyleHelper.ApplyStandardStyle(dgvCardholders);
            dgvCardholders.ColumnHeadersVisible = true;

            txtNameFilter.TextChanged += (s, e) => ApplyFilters();
            txtEmailFilter.TextChanged += (s, e) => ApplyFilters();
            btnClearFilters.Click += btnClearFilters_Click;

            Resize += (s, e) => AlignLayout();
            AlignLayout();
        }

        private async Task LoadCardholders()
        {
            try
            {
                dgvCardholders.Rows.Clear();

                var list = await _api.GetCardholders();
                _allCardholders = list;
                BindGrid(_allCardholders);
                AlignLayout();
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

        private static string BuildName(CardholderDto cardholder)
        {
            return $"{cardholder.firstName} {cardholder.lastName}".Trim();
        }

        private int GetSelectedId()
        {
            if (dgvCardholders.SelectedRows.Count == 0)
                return 0;

            return Convert.ToInt32(dgvCardholders.SelectedRows[0].Cells[0].Value);
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
                await LoadCardholders();
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
                int id = Convert.ToInt32(dgvCardholders.SelectedRows[0].Cells[0].Value);

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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvCardholders.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row");
                return;
            }

            var row = dgvCardholders.SelectedRows[0];

            var cardholder = new CardholderDto
            {
                cardholderId = Convert.ToInt32(row.Cells[0].Value),
                firstName = row.Cells[1].Value?.ToString(),
                lastName = row.Cells[2].Value?.ToString(),
                mobile = row.Cells[5].Value?.ToString(),
                email = row.Cells[4].Value?.ToString(),
                department = ""
            };

            var form = new EditCardholderForm(cardholder);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _ = LoadCardholders();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            txtNameFilter.Text = "";
            txtEmailFilter.Text = "";
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            var filtered = _allCardholders.AsEnumerable();

            string nameFilter = txtNameFilter.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(nameFilter))
            {
                filtered = filtered.Where(c => BuildName(c).ToLower().Contains(nameFilter));
            }

            string emailFilter = txtEmailFilter.Text.Trim().ToLower();
            if (!string.IsNullOrWhiteSpace(emailFilter))
            {
                filtered = filtered.Where(c => (c.email ?? string.Empty).ToLower().Contains(emailFilter));
            }

            string keyword = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(keyword))
            {
                BindGrid(filtered.ToList());
                return;
            }

            var searched = filtered.Where(c =>
                c.cardholderId.ToString().Contains(keyword) ||
                (c.firstName != null && c.firstName.ToLower().Contains(keyword)) ||
                (c.lastName != null && c.lastName.ToLower().Contains(keyword)) ||
                (c.userName != null && c.userName.ToLower().Contains(keyword)) ||
                (c.email != null && c.email.ToLower().Contains(keyword)) ||
                (c.mobile != null && c.mobile.ToLower().Contains(keyword)) ||
                c.isActive.ToString().ToLower().Contains(keyword)
            ).ToList();

            BindGrid(searched);
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
        }

        private void filterPanel_Paint(object sender, PaintEventArgs e)
        {
        }

        private void AlignLayout()
        {
            int top = 2;
            int left = 164;
            int spacing = 10;
            int rightPadding = 12;

            btnAdd.Location = new Point(left, top);
            btnEdit.Location = new Point(btnAdd.Right + spacing, top);
            btnDelete.Location = new Point(btnEdit.Right + spacing, top);
            btnRefresh.Location = new Point(btnDelete.Right + spacing, top);
            btnBack.Location = new Point(btnRefresh.Right + spacing, top);

            int searchGroupWidth =
                btnClearFilters.Width + 6 +
                btnSearch.Width + 6 +
                txtSearch.Width + 8 +
                lblSearchRight.Width;

            int availableRight = topPanel.ClientSize.Width - rightPadding;
            bool wrapSearch = (btnBack.Right + spacing + searchGroupWidth) > availableRight;
            int searchTop = wrapSearch ? (top + btnAdd.Height + 6) : top;

            btnClearFilters.Location = new Point(availableRight - btnClearFilters.Width, searchTop + 3);
            btnSearch.Location = new Point(btnClearFilters.Left - btnSearch.Width - 6, searchTop + 1);
            txtSearch.Location = new Point(btnSearch.Left - txtSearch.Width - 6, searchTop + 3);
            lblSearchRight.Location = new Point(txtSearch.Left - lblSearchRight.Width - 8, searchTop + 7);

            if (topPanel != null)
                topPanel.Height = wrapSearch ? 72 : 36;

            txtEmailFilter.Location = new Point(filterPanel.ClientSize.Width - txtEmailFilter.Width - 12, 4);
            lblEmailFilter.Location = new Point(txtEmailFilter.Left - lblEmailFilter.Width - 8, 8);
            txtNameFilter.Location = new Point(lblEmailFilter.Left - txtNameFilter.Width - 14, 4);
            lblNameFilter.Location = new Point(txtNameFilter.Left - lblNameFilter.Width - 8, 8);
        }
    }
}
