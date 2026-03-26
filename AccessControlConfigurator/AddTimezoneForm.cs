using AccessControlSystem.Models;
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
using System.Xml.Linq;

namespace AccessControlConfigurator
{
    public partial class AddTimezoneForm : Form
    {
        private readonly ApiService _apiService = new ApiService();
        public AddTimezoneForm()
        {
            InitializeComponent();
        }
    
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int number = 0;
                int mode = 0;

                int.TryParse(txtNumber.Text, out number);
                int.TryParse(txtMode.Text, out mode);

                var dto = new TimezoneDto
                {
                    number = number,
                    name = txtName.Text,
                    mode = mode,
                    actTime = 0,
                    deactTime = 0,
                    intervals = 0,
                    iDays = 0,
                    iStart = 0,
                    iEnd = 0
                };

                await _apiService.CreateTimezone(dto);

                MessageBox.Show("Timezone Added Successfully");

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
    

