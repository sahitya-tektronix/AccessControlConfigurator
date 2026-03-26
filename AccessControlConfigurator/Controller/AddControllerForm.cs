using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AccessControlConfigurator.Forms
{
    public partial class AddControllerForm : Form
    {
        private readonly ApiService _api = new ApiService();

        public AddControllerForm()
        {
            InitializeComponent();

            btnSave.Click += btnSave_Click;
            btnCancel.Click += (s, e) => this.Close();
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtMac.Text) ||
                    string.IsNullOrWhiteSpace(txtIp.Text))
                {
                    MessageBox.Show("Please fill all required fields");
                    return;
                }

                var dto = new AddUpdateControllerRequestDto
                {
                    Name = txtName.Text,
                    MacAddress = txtMac.Text,
                    IpAddress = txtIp.Text,

                    Port = 5000, // better from UI
                    TimeZoneId = 1,
                    LocationId = 1,

                    SubnetMask = "255.255.255.0",
                    DefaultGateway = "192.168.1.1",

                    InternalPort0IsEnabled = false,
                    InternalPort0BaudRate = 9600,
                    InternalPort0ProtocolType = 0,

                    Rs485Port1IsEnabled = true,
                    Rs485Port1BaudRate = 9600,
                    Rs485Port1ProtocolType = 0,

                    Rs485Port2IsEnabled = false,
                    Rs485Port2BaudRate = 0,
                    Rs485Port2ProtocolType = 0
                };

                var result = await _api.AddControllerAsync(dto);

                if (result.Success)
                {
                    MessageBox.Show("Controller Added Successfully");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    string error = result.Error ?? "";

                    if (error.Contains("controller_limit_exceeded"))
                        MessageBox.Show("Only one controller is allowed.");
                    else if (error.Contains("duplicate_mac_address"))
                        MessageBox.Show("MAC Address already exists.");
                    else
                        MessageBox.Show("Error: " + error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}



