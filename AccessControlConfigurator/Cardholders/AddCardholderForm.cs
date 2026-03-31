using AccessControlSystem.Models;
using AccessControlConfigurator.Helpers;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class AddCardholderForm : Form
    {
        private readonly ApiService _api = new ApiService();

        public AddCardholderForm()
        {
            InitializeComponent();
            this.CancelButton = btnCancel;
            btnCancel.CausesValidation = false;
            btnCancel.TabStop = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var dto = new CardholderDto
                {
                    firstName = txtFirstName.Text.Trim(),
                    lastName = txtLastName.Text.Trim(),
                    userName = txtUserName.Text.Trim(),
                    email = txtEmail.Text.Trim(),
                    mobile = txtMobile.Text.Trim(),
                    isActive = chkActive.Checked
                };

                bool success = await _api.CreateCardholder(dto);

                if (success)
                {
                    MessageBox.Show("Cardholder saved successfully");

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(CardholderErrorHelper.GetMessage(ex));
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.ActiveControl = null;                 // 🔥 remove focus issue
            this.DialogResult = DialogResult.Cancel;   // return to parent
            this.Close();                              // close immediately
        }
    }
    }
