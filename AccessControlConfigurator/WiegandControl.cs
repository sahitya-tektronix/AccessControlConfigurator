using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class WiegandControl : Form
    {
        private readonly ApiService _apiService = new ApiService();

        private List<WiegandDto> _data = new List<WiegandDto>();
        public WiegandControl()
        {
            InitializeComponent();
            dgvFormats.ReadOnly = true;
            dgvFormats.AllowUserToAddRows = false;
            dgvFormats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private async void Form_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                _data = await _apiService.GetAllAsync();
                dgvFormats.DataSource = _data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter(); // Auto search
        }

        private void ApplyFilter()
        {
            string search = txtSearch.Text.ToLower();

            var filtered = _data.Where(x =>
                (x.Name != null && x.Name.ToLower().Contains(search)) ||
                x.FormatNumber.ToString().Contains(search) ||
                x.Bits.ToString().Contains(search)
            ).ToList();

            dgvFormats.DataSource = filtered;

            if (!filtered.Any())
                MessageBox.Show("No matching data");
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadData();
        }
        //private async void btnAdd_Click(object sender, EventArgs e)
        //{
        //    var dto = new CreateWiegandFormatRequest
        //    {
        //        FormatNumber = 1,
        //        Name = "New Format",
        //        Bits = 26,
        //        FacilityCode = 0
        //    };

        //    await _api.CreateAsync(dto);
        //    await LoadData();
        //}
        //private async void btnEdit_Click(object sender, EventArgs e)
        //{
        //    if (dgvFormats.CurrentRow == null) return;

        //    var item = (WiegandFormatDto)dgvFormats.CurrentRow.DataBoundItem;

        //    var dto = new UpdateWiegandFormatRequest
        //    {
        //        Name = "Updated Name"
        //    };

        //    await _api.UpdateAsync(item.FormatNumber, dto);
        //    await LoadData();
        //}
    }
}
