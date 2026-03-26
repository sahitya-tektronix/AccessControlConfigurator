using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

            // ❗ card number not available → leave empty
            txtCardNumber.Text = "";
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int cardNumber = 0;
                int.TryParse(txtCardNumber.Text, out cardNumber);

                //var startUtc = dtStart.Value.ToUniversalTime();
                //var endUtc = dtEnd.Value.ToUniversalTime();

                //if (endUtc <= startUtc)
                //{
                //    MessageBox.Show("End Date must be greater than Start Date");
                //    return;
                //}
                var startUtc = dtStart.Value.ToUniversalTime();
                var endUtc = dtEnd.Value.ToUniversalTime();

                if ((endUtc - startUtc).TotalMinutes < 1)
                {
                    MessageBox.Show("End time must be greater than start time");
                    dtEnd.Value = dtStart.Value.AddMinutes(1); // helpful default
                    return;
                }

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
                    card = new CardUpdateDto
                    {
                        cardNumber = cardNumber,
                        startDateTime = dtStart.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                        endDateTime = dtEnd.Value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ssZ"),
                        accessLevelIds = new List<int> { 1 }
                    }
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
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}