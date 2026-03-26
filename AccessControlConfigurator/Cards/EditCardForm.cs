using AccessControlSystem;
using AccessControlSystem.Models.Cards;
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
            {
                dtStart.Value = DateTimeOffset.FromUnixTimeSeconds(startUnix).LocalDateTime;
            }

            if (long.TryParse(card.endDateTime, out long endUnix))
            {
                dtEnd.Value = DateTimeOffset.FromUnixTimeSeconds(endUnix).LocalDateTime;
            }

            // ✅ Ensure valid default
            if (dtEnd.Value <= dtStart.Value)
            {
                dtEnd.Value = dtStart.Value.AddDays(1);
            }

            // ✅ Load dropdown
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //UPDATE BUTTON
        private async void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //  Validate Card Number
                if (!long.TryParse(txtCardNumber.Text, out long cardNumber))
                {
                    MessageBox.Show("Invalid Card Number");
                    return;
                }

                //  Validate Access Level
                if (cbAccessLevel.SelectedValue == null)
                {
                    MessageBox.Show("Please select Access Level");
                    return;
                }

                //  Validate Start Date (not past)
                if (dtStart.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Start Date cannot be in the past");
                    return;
                }

                //  Validate End Date > Start Date
                if (dtEnd.Value <= dtStart.Value)
                {
                    MessageBox.Show("End Date must be greater than Start Date");
                    return;
                }

                var card = new UpdateCardDto
                {
                    cardNumber = cardNumber,
                    accessLevelId = Convert.ToInt32(cbAccessLevel.SelectedValue),

                    //  Exact API format
                    //startDateTime = dtStart.Value.Date.ToString("yyyy-MM-ddT00:00:00Z"),
                    //endDateTime = dtEnd.Value.Date.ToString("yyyy-MM-ddT00:00:00Z"),

                    startDateTime = ((DateTimeOffset)dtStart.Value).ToUnixTimeSeconds().ToString(),
                    endDateTime = ((DateTimeOffset)dtEnd.Value).ToUnixTimeSeconds().ToString(),

                    // Safe assign
                    assignCardholder = int.TryParse(txtCardholder.Text, out var ch) ? ch : 5
                };

                var (success, error) = await _apiService.UpdateCard(cardId, card);

                if (success)
                {
                    MessageBox.Show("Card Updated Successfully");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        string.IsNullOrWhiteSpace(error) ? "Update failed." : error,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),   
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        //CANCEL BUTTON
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}