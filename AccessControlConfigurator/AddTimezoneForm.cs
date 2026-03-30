using AccessControlSystem.Models;
using AccessControlSystem.Services;
using AccessControlConfigurator.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AccessControlConfigurator
{
    public partial class AddTimezoneForm : Form
    {
        private readonly ApiService _apiService = new ApiService();
        public AddTimezoneForm()
        {
            InitializeComponent();
            dtpActTime.Value = DateTime.Today;
            dtpDeactTime.Value = DateTime.Today;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!TryGetInt(txtNumber.Text, "Number", out var number) ||
                    !TryGetInt(txtMode.Text, "Mode", out var mode) ||
                    !TryGetInt(txtIntervals.Text, "Intervals", out var intervals) ||
                    !TryGetInt(txtIDays.Text, "Break Days", out var iDays) ||
                    !TryGetInt(txtIStart.Text, "Break Start", out var iStart) ||
                    !TryGetInt(txtIEnd.Text, "Break End", out var iEnd))
                {
                    return;
                }

                int actTime = GetSecondsFromPicker(dtpActTime);
                int deactTime = GetSecondsFromPicker(dtpDeactTime);

                if (deactTime <= actTime)
                {
                    MessageBox.Show(
                        "Please select a correct end time. End time must be greater than start time.",
                        "Invalid Time Range",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var dto = new TimezoneCreateRequest
                {
                    number = number,
                    name = txtName.Text?.Trim(),
                    mode = mode,
                    actTime = actTime,
                    deactTime = deactTime,
                    intervals = intervals,
                    iDays = iDays,
                    iStart = iStart,
                    iEnd = iEnd
                };

                await _apiService.CreateTimezone(dto);

                MessageBox.Show("Timezone Added Successfully");

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

        private static bool TryGetInt(string raw, string label, out int value)
        {
            value = 0;
            if (!int.TryParse(raw?.Trim(), out value))
            {
                MessageBox.Show($"Invalid {label}");
                return false;
            }

            return true;
        }

        private static int GetSecondsFromPicker(DateTimePicker picker)
        {
            var time = picker.Value.TimeOfDay;
            return (time.Hours * 3600) + (time.Minutes * 60) + time.Seconds;
        }
    }
}
