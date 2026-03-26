using AccessControlSystem.Models;
using AccessControlSystem.Models.AccessLevelDto.AccessLevelDto;
using AccessControlSystem.Models.Acr;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class AddAccessLevelForm : Form
    {
        private readonly ApiService _api = new ApiService();

    public AddAccessLevelForm()
        {
            InitializeComponent();
            LoadDropdowns();
        }

        // Load both dropdowns
        private async void LoadDropdowns()
        {
            await Task.WhenAll(
                        LoadAcrs(),
                        LoadTimeZones()
                        );
        }

        // Load Doors (ACR)
        private async Task LoadAcrs()
        {
            try
            {
                var acrs = await _api.GetAllAcrDropdown();

                acrs.Insert(0, new AcrDropdownDto
                {
                    id = 0,
                    name = "None"
                });

                

                cmbAcr.DataSource = null;
                cmbAcr.DisplayMember = "name";
                cmbAcr.ValueMember = "id";  
                cmbAcr.DataSource = acrs;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load doors: " + ex.Message);
            }
        }

        // Load TimeZones
        private async Task LoadTimeZones()
        {
            try
            {
                var zones = await _api.GetTimezonesDropdown();

                zones.Insert(0, new TimezoneDto
                {
                    id = 0,
                    name = "None"
                });

                cmbTimeZone.DataSource = null;
                cmbTimeZone.DisplayMember = "name";
                cmbTimeZone.ValueMember = "Id";
                cmbTimeZone.DataSource = zones;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load time zones: " + ex.Message);
            }
        }

        // Save Access Level
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Access Level Name is required");
                    return;
                }

                if (cmbAcr.SelectedValue == null)
                {
                    MessageBox.Show("Please select a Door (ACR)");
                    return;
                }

                if (cmbTimeZone.SelectedValue == null)
                {
                    MessageBox.Show("Please select a Time Zone");
                    return;
                }

                var dto = new AccessLevelCreateDto
                {
                    name = txtName.Text.Trim(),
                    //description = txtDescription.Text.Trim(),
                    acrs = new List<AcrTimeZoneDto>
                {
                    new AcrTimeZoneDto
                    {
                        acrId = Convert.ToInt32(cmbAcr.SelectedValue),
                       // TimeZoneId = Convert.ToInt32(cmbTimeZone.SelectedValue)
                       //TimeZoneId = ((TimezoneDto)cmbTimeZone.SelectedItem).timezoneid
                       timeZoneId = (int)cmbTimeZone.SelectedValue
                    }
                }
                };

                await _api.AddAccessLevel(dto);

                MessageBox.Show("Access Level Created Successfully");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating access level: " + ex.Message);
            }
        }

        // Cancel button
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
