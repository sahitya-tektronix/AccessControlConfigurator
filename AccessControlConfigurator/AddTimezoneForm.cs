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
            txtActTime.Text = "00:00:00";
            txtDeactTime.Text = "00:00:00";
            ConfigureTextEntry();
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

                if (!await ValidateUniqueFieldsAsync(number, txtName.Text?.Trim(), null))
                    return;

                if (!TryGetTimeSeconds(txtActTime.Text, "Start Time", out var actTime) ||
                    !TryGetTimeSeconds(txtDeactTime.Text, "End Time", out var deactTime))
                {
                    return;
                }

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

        private void ConfigureTextEntry()
        {
            foreach (var textBox in new[] { txtNumber, txtMode, txtIntervals, txtIDays, txtIStart, txtIEnd })
                textBox.KeyPress += NumericTextBox_KeyPress;

            txtActTime.KeyPress += TimeTextBox_KeyPress;
            txtDeactTime.KeyPress += TimeTextBox_KeyPress;
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void TimeTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ':')
                e.Handled = true;
        }

        private static bool TryGetTimeSeconds(string raw, string label, out int value)
        {
            value = 0;
            if (!UIStyleHelper.TryParseTimeToSeconds(raw?.Trim(), out value))
            {
                MessageBox.Show($"{label} must be in HH:MM:SS format.");
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUniqueFieldsAsync(int number, string name, int? currentId)
        {
            var existing = await _apiService.GetTimezones();

            if (existing.Any(t => t.number == number && (!currentId.HasValue || t.id != currentId.Value)))
            {
                MessageBox.Show("Timezone number must be unique.");
                txtNumber.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(name) &&
                existing.Any(t => string.Equals((t.name ?? string.Empty).Trim(), name, StringComparison.OrdinalIgnoreCase) &&
                                  (!currentId.HasValue || t.id != currentId.Value)))
            {
                MessageBox.Show("Timezone name must be unique.");
                txtName.Focus();
                return false;
            }

            return true;
        }
    }
}
