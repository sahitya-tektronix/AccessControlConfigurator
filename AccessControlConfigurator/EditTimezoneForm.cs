using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

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

            txtName.Text = timezone.name;
            txtNumber.Text = timezone.number.ToString();
            txtMode.Text = timezone.mode.ToString();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int number = 0;
                int mode = 0;

                int.TryParse(txtNumber.Text, out number);
                int.TryParse(txtMode.Text, out mode);

                _timezone.name = txtName.Text;
                _timezone.number = number;
                _timezone.mode = mode;

                await _apiService.UpdateTimezone(_timezone.id, _timezone);

                MessageBox.Show("Timezone Updated Successfully");

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}