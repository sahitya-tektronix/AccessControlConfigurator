using System;

using System.Collections.Generic;
using System.Globalization;

using System.Windows.Forms;

using AccessControlConfigurator.Helpers;
using AccessControlSystem.Models;
using AccessControlSystem.Services;

namespace AccessControlConfigurator

{

    public partial class EditCardholderForm : Form

    {

        private readonly ApiService _api = new ApiService();

        private int _userId;

        public EditCardholderForm(CardholderDto cardholder)

        {

            InitializeComponent();

            if (cardholder == null)

            {

                MessageBox.Show("No data found");

                return;

            }

            // ✅ store ID

            _userId = cardholder.cardholderId;

            // ✅ bind UI

            txtFirstName.Text = cardholder.firstName;

            txtLastName.Text = cardholder.lastName;

            txtMobile.Text = cardholder.mobile;

            txtEmail.Text = cardholder.email;

            txtDepartment.Text = cardholder.department ?? "";

            txtCardNumber.Text = cardholder.cardNumber?.ToString() ?? "";

            dtStart.Format = DateTimePickerFormat.Custom;

            dtStart.CustomFormat = "dd MMM yyyy HH:mm";

            dtStart.ShowUpDown = true;

            dtEnd.Format = DateTimePickerFormat.Custom;

            dtEnd.CustomFormat = "dd MMM yyyy HH:mm";

            dtEnd.ShowUpDown = true;

            if (dtEnd.Value <= dtStart.Value)

                dtEnd.Value = dtStart.Value.AddHours(1);

            dtStart.ValueChanged += (s, e) =>

            {

                if (dtEnd.Value <= dtStart.Value)

                    dtEnd.Value = dtStart.Value.AddHours(1);

            };

        }

        private async void btnSave_Click(object sender, EventArgs e)

        {

            try

            {

                string cardNumberText = txtCardNumber.Text.Trim();

                if (!int.TryParse(cardNumberText, out int cardNumber) || cardNumber <= 0)

                {

                    MessageBox.Show("Enter a valid card number.");

                    txtCardNumber.Focus();

                    return;

                }

                var startLocal = DateTime.SpecifyKind(dtStart.Value, DateTimeKind.Local);
                var endLocal = DateTime.SpecifyKind(dtEnd.Value, DateTimeKind.Local);
                var startUtc = startLocal.ToUniversalTime();
                var endUtc = endLocal.ToUniversalTime();

                if (endUtc <= startUtc)

                {

                    MessageBox.Show("End time must be greater than start time");

                    dtEnd.Value = dtStart.Value.AddMinutes(1);

                    return;

                }

                var cardUpdate = new CardUpdateDto

                {

                    cardNumber = cardNumber,

                    startDateTime = startLocal.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture),
                    endDateTime = endLocal.ToString("yyyy-MM-ddTHH:mm:sszzz", CultureInfo.InvariantCulture),

                    accessLevelIds = new List<int> { 1 }

                };

                //var startUtc = dtStart.Value.ToUniversalTime();

                //var endUtc = dtEnd.Value.ToUniversalTime();

                //if (endUtc <= startUtc)

                //{

                //    MessageBox.Show("End Date must be greater than Start Date");

                //    return;

                //}

                var request = new UpdateCardholderRequest

                {

                    cardholder = new CardholderUpdateDto

                    {

                        firstName = txtFirstName.Text,

                        lastName = txtLastName.Text,

                        mobile = txtMobile.Text,

                        department = txtDepartment.Text,

                        email = txtEmail.Text,

                        accessLevelId = 1

                    },

                    card = cardUpdate

                };

                bool success = await _api.UpdateCardholder(_userId, request);

                if (success)

                {

                    MessageBox.Show("Cardholder updated successfully");

                    this.DialogResult = DialogResult.OK; // 🔥 important

                    this.Close();

                }

                else

                {

                    MessageBox.Show("Update failed");

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(CardholderErrorHelper.GetMessage(ex));

            }

        }

        private void btnCancel_Click(object sender, EventArgs e)

        {

            this.Close();

        }

    }

}
