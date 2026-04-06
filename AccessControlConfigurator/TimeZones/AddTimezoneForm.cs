using AccessControlConfigurator.Helpers;
using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class AddTimezoneForm : Form
    {
        private readonly ApiService _apiService = new ApiService();

        public AddTimezoneForm()
        {
            InitializeComponent();

            // ? REMOVE time format defaults
            txtActTime.Text = "";
            txtDeactTime.Text = "";

            ConfigureTextEntry();
        }

        // =========================
        // SAVE BUTTON
        // =========================
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ? Validate numeric fields
                if (!TryGetInt(txtNumber.Text, "Number", out var number) ||
                    !TryGetInt(txtMode.Text, "Mode", out var mode) ||
                    !TryGetInt(txtIntervals.Text, "Intervals", out var intervals) ||
                    !TryGetInt(txtIDays.Text, "iDays", out var iDays) ||
                    !TryGetInt(txtIStart.Text, "iStart", out var iStart) ||
                    !TryGetInt(txtIEnd.Text, "iEnd", out var iEnd) ||

                    // ? Start / End Time as NUMBER
                    !TryGetInt(txtActTime.Text, "Start Time", out var actTime) ||
                    !TryGetInt(txtDeactTime.Text, "End Time", out var deactTime))
                {
                    return;
                }

                // ? Name validation
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Name is required");
                    txtName.Focus();
                    return;
                }

                // ? Unique validation
                if (!await ValidateUniqueFieldsAsync(number, txtName.Text?.Trim(), null))
                    return;

                // ? Logical validation
                if (deactTime <= actTime)
                {
                    MessageBox.Show(
                        "End Time must be greater than Start Time.",
                        "Invalid Time Range",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // ? Create DTO
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

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(TimezoneErrorHelper.GetMessage(ex));
            }
        }

        // =========================
        // CANCEL
        // =========================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        // =========================
        // COMMON INT VALIDATION
        // =========================
        private static bool TryGetInt(string raw, string label, out int value)
        {
            value = 0;

            if (!int.TryParse(raw?.Trim(), out value))
            {
                MessageBox.Show($"{label} must be a number");
                return false;
            }

            return true;
        }

        // =========================
        // INPUT RESTRICTIONS
        // =========================
        private void ConfigureTextEntry()
        {
            // ? All numeric fields
            foreach (var textBox in new[]
            {
                txtNumber, txtMode, txtIntervals, txtIDays, txtIStart, txtIEnd,
                txtActTime, txtDeactTime
            })
            {
                textBox.KeyPress += NumericTextBox_KeyPress;
            }
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // ? Allow only numbers + backspace
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        // =========================
        // UNIQUE VALIDATION
        // =========================
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
                existing.Any(t =>
                    string.Equals((t.name ?? "").Trim(), name, StringComparison.OrdinalIgnoreCase) &&
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

