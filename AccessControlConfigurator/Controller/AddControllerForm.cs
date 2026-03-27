using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

            // ✅ Optional: auto uppercase while typing
            txtMac.TextChanged += (s, e) =>
            {
                int pos = txtMac.SelectionStart;
                txtMac.Text = txtMac.Text.ToUpper();
                txtMac.SelectionStart = pos;
            };
        }

        // ✅ MAC Validation Method
        private bool IsValidMac(string mac)
        {
            return Regex.IsMatch(
                mac,
                @"^([0-9A-F]{2}:){5}([0-9A-F]{2})$"
            );
        }

        // ✅ IP Validation (Optional but recommended)
        private bool IsValidIp(string ip)
        {
            return System.Net.IPAddress.TryParse(ip, out _);
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Required Fields
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtMac.Text) ||
                    string.IsNullOrWhiteSpace(txtIp.Text))
                {
                    MessageBox.Show(
                        "Please fill all required fields",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // ✅ MAC Validation
                if (!IsValidMac(txtMac.Text))
                {
                    MessageBox.Show(
                        "Invalid MAC Address format.\nUse format: AA:BB:CC:DD:EE:FF",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // ✅ IP Validation
                if (!IsValidIp(txtIp.Text))
                {
                    MessageBox.Show(
                        "Invalid IP Address format",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                var dto = new AddUpdateControllerRequestDto
                {
                    Name = txtName.Text.Trim(),
                    MacAddress = txtMac.Text.Trim(),
                    IpAddress = txtIp.Text.Trim(),

                    Port = 5000,
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
                    MessageBox.Show(
                        "Controller added successfully",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(
                        GetUserFriendlyError(result.Error),
                        "Warning",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(
                    "Unexpected error occurred. Please try again.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ✅ Clean Error Messages
        private string GetUserFriendlyError(string error)
        {
            if (string.IsNullOrWhiteSpace(error))
                return "Something went wrong. Please try again.";

            if (error.Contains("controller_limit_exceeded"))
                return "Only one controller is allowed.";

            if (error.Contains("duplicate_mac_address"))
                return "MAC Address already exists.";

            return "Failed to add controller. Please try again.";
        }
    }
}