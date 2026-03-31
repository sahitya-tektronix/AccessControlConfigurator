using AccessControlConfigurator.Helpers;
using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class EditTimezoneForm : Form
    {
        private readonly ApiService _apiService = new ApiService();
        private readonly TimezoneDto _timezone;

        public EditTimezoneForm(TimezoneDto timezone)
        {
            InitializeComponent();
            _timezone = timezone;

            ConfigureTextEntry();

            // ? Load values (NUMERIC only)
            txtName.Text = timezone.name;
            txtNumber.Text = timezone.number.ToString();
            txtMode.Text = timezone.mode.ToString();
            txtActTime.Text = timezone.actTime.ToString();
            txtDeactTime.Text = timezone.deactTime.ToString();
            txtIntervals.Text = timezone.intervals.ToString();
            txtIDays.Text = timezone.iDays.ToString();
            txtIStart.Text = timezone.iStart.ToString();
            txtIEnd.Text = timezone.iEnd.ToString();
        }

        // =========================
        // NUMERIC INPUT RESTRICTION
        // =========================
        private void ConfigureTextEntry()
        {
            foreach (var txt in new[]
            {
                txtNumber, txtMode, txtIntervals,
                txtIDays, txtIStart, txtIEnd,
                txtActTime, txtDeactTime
            })
            {
                txt.KeyPress += NumericTextBox_KeyPress;
                txt.KeyDown += NumericTextBox_KeyDown;

                // ? Disable right-click paste
                txt.ContextMenuStrip = new ContextMenuStrip();
            }
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void NumericTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Block Ctrl+V paste
            if (e.Control && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
            }
        }

        // =========================
        // VALIDATION METHOD
        // =========================
        private bool ValidateNumeric(TextBox txt, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                MessageBox.Show($"{fieldName} is required");
                txt.Focus();
                return false;
            }

            if (!int.TryParse(txt.Text, out _))
            {
                MessageBox.Show($"{fieldName} must be a number");
                txt.Focus();
                return false;
            }

            return true;
        }

        // =========================
        // SAVE BUTTON
        // =========================
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ? Name validation
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Name is required");
                    txtName.Focus();
                    return;
                }

                // ? Numeric validation
                if (!ValidateNumeric(txtNumber, "Number") ||
                    !ValidateNumeric(txtMode, "Mode") ||
                    !ValidateNumeric(txtIntervals, "Intervals") ||
                    !ValidateNumeric(txtIDays, "Break Days") ||
                    !ValidateNumeric(txtIStart, "Break Start") ||
                    !ValidateNumeric(txtIEnd, "Break End") ||
                    !ValidateNumeric(txtActTime, "Start Time") ||
                    !ValidateNumeric(txtDeactTime, "End Time"))
                {
                    return;
                }

                // ? Convert values
                int number = int.Parse(txtNumber.Text);
                int mode = int.Parse(txtMode.Text);
                int intervals = int.Parse(txtIntervals.Text);
                int iDays = int.Parse(txtIDays.Text);
                int iStart = int.Parse(txtIStart.Text);
                int iEnd = int.Parse(txtIEnd.Text);
                int actTime = int.Parse(txtActTime.Text);
                int deactTime = int.Parse(txtDeactTime.Text);

                // ? Logical validation
                if (deactTime <= actTime)
                {
                    MessageBox.Show("End Time must be greater than Start Time");
                    txtDeactTime.Focus();
                    return;
                }

                // ? Range validation (optional)
                if (actTime < 0 || deactTime > 86400)
                {
                    MessageBox.Show("Time must be between 0 and 86400 seconds");
                    return;
                }

                // ? Unique validation
                if (!await ValidateUniqueFieldsAsync(number, txtName.Text?.Trim(), _timezone.id))
                    return;

                // ? Update DTO
                var updateDto = new TimezoneUpdateRequest
                {
                    id = _timezone.id,
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

                await _apiService.UpdateTimezone(_timezone.id, updateDto);

                MessageBox.Show("Timezone Updated Successfully");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(TimezoneErrorHelper.GetMessage(ex));
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
