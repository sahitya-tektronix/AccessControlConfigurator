using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class ApplyHolidayForm : Form
    {
        private readonly ApiService _apiService = new ApiService();

        public ApplyHolidayForm()
        {
            InitializeComponent();
            this.Load += ApplyHolidayForm_Load;
        }

        private async void ApplyHolidayForm_Load(object sender, EventArgs e)
        {
            await LoadTimezones();
        }

        private async Task LoadTimezones()
        {
            try
            {
                var data = await _apiService.GetTimezonesDropdown();

                chkTimezones.DataSource = data;
                chkTimezones.DisplayMember = "name";
                chkTimezones.ValueMember = "id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = dtHoliday.Value;

                var selectedIds = chkTimezones.CheckedItems
                    .Cast<TimezoneDto>()
                    .Select(t => t.id)
                    .ToList();

                if (selectedIds.Count == 0)
                {
                    MessageBox.Show("Please select at least one timezone.");
                    return;
                }

                var dto = new HolidayApplyDto
                {
                    scpId = selectedIds.First(),
                    operation = "apply",
                    entries = new List<HolidayEntryDto>
                    {
                        new HolidayEntryDto
                        {
                            year = date.Year,
                            month = date.Month,
                            day = date.Day,
                            extendDays = 0,
                            typeMask = 0
                        }
                    }
                };

                await _apiService.ApplyHoliday(dto);

                MessageBox.Show("Holiday applied successfully");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}