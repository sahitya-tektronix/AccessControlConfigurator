using AccessControlSystem.Models;
using AccessControlSystem.Models.Cards;
using AccessControlConfigurator.Helpers;
using AccessControlSystem.Services;
using System;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class AddCardForm : Form
    {
        private readonly ApiService _apiService = new ApiService();

        public AddCardForm()
        {
            InitializeComponent();

            btnCancel.Click += btnCancel_Click_1;

            this.CancelButton = btnCancel;
            this.Shown += (s, e) => this.ActiveControl = null;
            // 🔥 MAIN FIXES
            this.ActiveControl = null; // remove initial focus
            btnCancel.CausesValidation = false;

            ConfigureOptionalDate(dtStart);
            ConfigureOptionalDate(dtEnd);
            btnClearStart.Click += (s, e) => ClearOptionalDate(dtStart);
            btnClearEnd.Click += (s, e) => ClearOptionalDate(dtEnd);
        }

        private static void ConfigureOptionalDate(DateTimePicker picker)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = " ";
            picker.ShowCheckBox = false;
            picker.ValueChanged += (s, e) => UpdateOptionalDateFormat((DateTimePicker)s);
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

        // SAVE CARD
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var card = new CreateCardDto
                {
                    cardNumber = long.Parse(txtCardNumber.Text),
                    accessLevelId = int.Parse(txtAccessLevel.Text),
                    startDateTime = IsDateSelected(dtStart) ? dtStart.Value.ToUniversalTime() : (DateTime?)null,
                    endDateTime = IsDateSelected(dtEnd) ? dtEnd.Value.ToUniversalTime() : (DateTime?)null,
                    //assignCardholder = string.IsNullOrWhiteSpace(txtCardholder.Text)
                    //    ? null
                    //    : int.Parse(txtCardholder.Text)
                };

                bool hasStart = card.startDateTime.HasValue;
                bool hasEnd = card.endDateTime.HasValue;
                if (hasStart != hasEnd)
                {
                    MessageBox.Show("Both Start Date and End Date are required.",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                if (hasStart && hasEnd && card.endDateTime.Value <= card.startDateTime.Value)
                {
                    MessageBox.Show("End Date must be greater than Start Date",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var result = await _apiService.CreateCard(card);

                if (result)
                {
                    MessageBox.Show("Card Added Successfully");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    CardErrorHelper.GetMessage(ex),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }


        // CANCEL
       

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
