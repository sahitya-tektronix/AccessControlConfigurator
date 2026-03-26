using AccessControlSystem;
using AccessControlSystem.Models;
using AccessControlSystem.Models.AccessLevelDto.AccessLevelDto;
using AccessControlSystem.Models.Acr;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class EditAccessLevelForm : Form
    {
        private readonly ApiService _api = new ApiService();

        private int accessLevelId = 0;
        private AccessLevelDto _level;

        public EditAccessLevelForm(AccessLevelDto level, List<AcrDropdownDto> acrs, List<TimezoneDto> zones)
        {
            InitializeComponent();

            _level = level;
            accessLevelId = level.accessLevelId;

            txtName.Text = level.name;
            //txtDescription.Text = level.description;

            LoadDropdowns(_level,acrs,zones);
        }
   
        // Load dropdown data
        private async Task LoadDropdowns(AccessLevelDto level, List<AcrDropdownDto> acrs, List<TimezoneDto> zones)
        {
            try
            {
                 LoadAcrs(acrs);
                 LoadTimeZones(zones);

                

                if (level?.acrs != null && level.acrs.Count > 0)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        //private async void LoadDropdowns(AccessLevelDto level)
        //{
        //    try
        //    {
        //        var acrTask = LoadAcrs();
        //        var tzTask = LoadTimeZones();

        //        await Task.WhenAll(acrTask, tzTask);

        //        // Select existing values
        //        if (level?.acrs != null && level.acrs.Count > 0)
        //        {
        //            cmbAcr.SelectedValue = level.acrs[0].acrId;
        //            cmbTimeZone.SelectedValue = level.acrs[0].timeZoneId;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error: {ex.Message}");

        //    }

        //}

        // Load ACR Doors
        private void LoadAcrs(List<AcrDropdownDto> acrs)
        {
            try
            {

                if (acrs == null || acrs.Count == 0)
                {
                    MessageBox.Show("No ACR doors found.");
                    return;
                }

                cmbAcr.DisplayMember = "name";
                cmbAcr.ValueMember = "id";
                cmbAcr.DataSource = acrs;
                cmbAcr.SelectedValue = _level.acrs[0].acrId;



            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load doors: " + ex.Message);
            }
        }

        // Load TimeZones
        private void LoadTimeZones(List<TimezoneDto> zones)
        {
            try
            {
                if (zones == null || zones.Count == 0)
                {
                    MessageBox.Show("No TimeZones found.");
                    return;
                }
                var Zones=zones.FirstOrDefault(x => x.id == _level.acrs[0].timeZoneId);

                cmbTimeZone.DisplayMember = "name";
                cmbTimeZone.ValueMember = "id";
                cmbTimeZone.DataSource = zones;
                cmbTimeZone.SelectedValue = _level.acrs[0].timeZoneId;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load time zones: " + ex.Message);
            }
        }



        // Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private async void btnupdate_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Access Level Name is required.");
                    return;
                }
                var acr = new AcrTimeZoneDto
                {
                    acrId = (int)cmbAcr.SelectedValue,
                    timeZoneId = (int)cmbTimeZone.SelectedValue
                };

                var dto = new AccessLevelCreateDto
                {
                    name = txtName.Text.Trim(),
                   // description = txtDescription.Text.Trim(),
                    acrs = new List<AcrTimeZoneDto> { acr }
                };

                await _api.UpdateAccessLevel(accessLevelId, dto);

                MessageBox.Show("Access Level Updated Successfully");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating access level: " + ex.Message);
            }
        }
    }
}