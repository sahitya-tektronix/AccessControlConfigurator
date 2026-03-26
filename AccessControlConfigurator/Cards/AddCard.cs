using AccessControlSystem.Models;
using AccessControlSystem.Models.Cards;
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

            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click_1;

            this.CancelButton = btnCancel;
            this.Shown += (s, e) => this.ActiveControl = null;
            // 🔥 MAIN FIXES
            this.ActiveControl = null; // remove initial focus
            btnCancel.CausesValidation = false;
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
                    startDateTime = dtStart.Value.ToUniversalTime(),
                    endDateTime = dtEnd.Value.ToUniversalTime(),
                    //assignCardholder = string.IsNullOrWhiteSpace(txtCardholder.Text)
                    //    ? null
                    //    : int.Parse(txtCardholder.Text)
                };

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

                string msg = ex.Message;

                // 🔥 Handle duplicate card properly
                if (msg.Contains("duplicate_card_number") ||
                    msg.Contains("already exists"))
                {
                    MessageBox.Show("Card number already exists!",
                        "Duplicate Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show("Something went wrong",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }


        // CANCEL
       

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}