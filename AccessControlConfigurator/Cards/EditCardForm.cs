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

        public EditCardForm(CardDto card)
        {
            InitializeComponent();

            // ✅ DateTime Picker Settings
            dtStart.Format = DateTimePickerFormat.Custom;
            dtStart.CustomFormat = "dd MMM yyyy HH:mm";
            dtStart.ShowUpDown = true;

            dtEnd.Format = DateTimePickerFormat.Custom;
            dtEnd.CustomFormat = "dd MMM yyyy HH:mm";
            dtEnd.ShowUpDown = true;

            cardId = card.id;

            txtCardholder.Text = card.assignCardholder?.ToString();
            txtCardNumber.Text = card.cardNumber.ToString();

            // ✅ Convert UNIX → Local DateTime
            if (long.TryParse(card.startDateTime, out long startUnix))
                dtStart.Value = DateTimeOffset.FromUnixTimeSeconds(startUnix).LocalDateTime;

            if (long.TryParse(card.endDateTime, out long endUnix))
                dtEnd.Value = DateTimeOffset.FromUnixTimeSeconds(endUnix).LocalDateTime;

            // ✅ Ensure valid default
            if (dtEnd.Value <= dtStart.Value)
                dtEnd.Value = dtStart.Value.AddHours(1);

            // ✅ Auto-fix End Date when Start changes
            dtStart.ValueChanged += (s, e) =>
            {
                if (dtEnd.Value <= dtStart.Value)
                    dtEnd.Value = dtStart.Value.AddHours(1);
            };

            // ✅ Load Access Levels
            _ = LoadAccessLevels(card.accessLevelId ?? 0);
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
                if (dtStart.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Start Date cannot be in the past",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // ✅ End Date Validation
                if (dtEnd.Value <= dtStart.Value)
                {
                    MessageBox.Show("End Date must be greater than Start Date", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Prepare API request
                var card = new UpdateCardDto
                {
                    cardNumber = cardNumber,
                    accessLevelId = Convert.ToInt32(cbAccessLevel.SelectedValue),

                    // ✅ Correct Date Format
                    startDateTime = dtStart.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"),
                    endDateTime = dtEnd.Value.ToString("yyyy-MM-ddTHH:mm:sszzz"),

                    // ✅ NULL if empty
                    assignCardholder = string.IsNullOrWhiteSpace(txtCardholder.Text)
                        ? (int?)null
                        : int.Parse(txtCardholder.Text)
                };

                var (success, error) = await _apiService.UpdateCard(cardId, card);

                if (success)
                {
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
