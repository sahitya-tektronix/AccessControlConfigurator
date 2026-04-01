using AccessControlSystem;
using AccessControlSystem.Models.Cards;
using AccessControlConfigurator.Helpers;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class EditCardForm : Form
    {
        private readonly ApiService _apiService = new ApiService();
        private int cardId;
        private List<AccessLevelDto> accessLevels = new List<AccessLevelDto>();
        public bool ClearedDates { get; private set; }

        public EditCardForm(CardDto card)
        {
            InitializeComponent();

            ConfigureOptionalDate(dtStart);
            ConfigureOptionalDate(dtEnd);
            btnClearStart.Click += (s, e) => ClearOptionalDate(dtStart);
            btnClearEnd.Click += (s, e) => ClearOptionalDate(dtEnd);

            cardId = card.id;

            txtCardholder.Text = card.assignCardholder?.ToString();
            txtCardNumber.Text = card.cardNumber.ToString();

            // ✅ Convert UNIX → Local DateTime
            if (TryParseCardDate(card.startDateTime, card.actTime, out var startDate))
            {
                dtStart.Value = startDate.LocalDateTime;
                dtStart.CustomFormat = "yyyy-MM-dd HH:mm";
            }
            else
            {
                ClearOptionalDate(dtStart);
            }

            if (TryParseCardDate(card.endDateTime, card.dactTime, out var endDate))
            {
                dtEnd.Value = endDate.LocalDateTime;
                dtEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            }
            else
            {
                ClearOptionalDate(dtEnd);
            }

            // ✅ Load Access Levels
            _ = LoadAccessLevels(card.accessLevelId ?? 0);
        }

        private static void ConfigureOptionalDate(DateTimePicker picker)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = " ";
            picker.ShowCheckBox = false;
            picker.ValueChanged += (s, e) => UpdateOptionalDateFormat((DateTimePicker)s);
            picker.DropDown += (s, e) => UpdateOptionalDateFormat((DateTimePicker)s);
            picker.KeyPress += (s, e) => e.Handled = true;
        }

        private static void UpdateOptionalDateFormat(DateTimePicker picker)
        {
            picker.CustomFormat = "yyyy-MM-dd HH:mm";
        }

        private static void ClearOptionalDate(DateTimePicker picker)
        {
            picker.CustomFormat = " ";
        }

        private static bool IsDateSelected(DateTimePicker picker)
        {
            return picker.CustomFormat != " ";
        }

        private static DateTimeOffset BuildFixedOffsetDateTime(DateTimePicker picker)
        {
            var dt = picker.Value;
            var offset = TimeSpan.FromHours(4);
            return new DateTimeOffset(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, offset);
        }

        private static bool TryParseCardDate(string value, int unixFallback, out DateTimeOffset parsed)
        {
            parsed = default;
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (long.TryParse(value, out var unix))
                {
                    parsed = DateTimeOffset.FromUnixTimeSeconds(unix);
                    return true;
                }

                if (DateTimeOffset.TryParse(value, out parsed))
                    return true;
            }

            if (unixFallback > 0)
            {
                parsed = DateTimeOffset.FromUnixTimeSeconds(unixFallback);
                return true;
            }

            return false;
        }

        // ✅ Load Access Levels
        private async Task LoadAccessLevels(int selectedId)
        {
            try
            {
                accessLevels = await _apiService.GetAccessLevels();

                cbAccessLevel.DataSource = accessLevels;
                cbAccessLevel.DisplayMember = "name";
                cbAccessLevel.ValueMember = "accessLevelId";

                cbAccessLevel.SelectedValue = selectedId;
            }
            catch
            {
                MessageBox.Show("Failed to load access levels", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ UPDATE BUTTON
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Card Number Validation
                if (!long.TryParse(txtCardNumber.Text, out long cardNumber))
                {
                    MessageBox.Show("Please enter a valid Card Number", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Access Level Validation
                if (cbAccessLevel.SelectedValue == null)
                {
                    MessageBox.Show("Please select Access Level", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool hasStart = IsDateSelected(dtStart);
                bool hasEnd = IsDateSelected(dtEnd);
                if (hasStart != hasEnd)
                {
                    MessageBox.Show("Both Start Date and End Date are required.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (hasStart && hasEnd)
                {
                    var startOffset = BuildFixedOffsetDateTime(dtStart);
                    var endOffset = BuildFixedOffsetDateTime(dtEnd);
                    if (endOffset <= startOffset)
                    {
                        MessageBox.Show("End Date must be greater than Start Date", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // ✅ Prepare API request
                var card = new UpdateCardDto
                {
                    cardNumber = cardNumber,
                    accessLevelId = Convert.ToInt32(cbAccessLevel.SelectedValue),

                    // ✅ Match Add Card: ISO-8601 with timezone when set; "0" when cleared
                    startDateTime = IsDateSelected(dtStart)
                        ? BuildFixedOffsetDateTime(dtStart).ToString("yyyy-MM-ddTHH:mm:sszzz", System.Globalization.CultureInfo.InvariantCulture)
                        : null,
                    endDateTime = IsDateSelected(dtEnd)
                        ? BuildFixedOffsetDateTime(dtEnd).ToString("yyyy-MM-ddTHH:mm:sszzz", System.Globalization.CultureInfo.InvariantCulture)
                        : null,

                    // ✅ NULL if empty
                    assignCardholder = string.IsNullOrWhiteSpace(txtCardholder.Text)
                        ? (int?)null
                        : int.Parse(txtCardholder.Text)
                };

                var (success, error) = await _apiService.UpdateCard(cardId, card);

                if (success)
                {
                    ClearedDates = !hasStart && !hasEnd;
                    MessageBox.Show("Card updated successfully", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(CardErrorHelper.GetMessage(error), "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Unexpected error occurred. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ✅ CANCEL BUTTON
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
