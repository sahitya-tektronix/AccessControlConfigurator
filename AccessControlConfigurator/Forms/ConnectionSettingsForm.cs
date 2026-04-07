using System;
using System.Drawing;
using System.Windows.Forms;
using AccessControlSystem.ApiClient;

namespace AccessControlConfigurator
{
    public partial class ConnectionSettingsForm : Form
    {
        private readonly AppSettingsService _settingsService = new AppSettingsService();
        private bool _isUpdatingUi = false;
        private readonly string _placeholder = "example: teksmartsolutions.com/TekHIDAPI/";
        private bool _isPlaceholderActive = false;

        public ConnectionSettingsForm()
        {
            InitializeComponent();
            Load += ConnectionSettingsForm_Load;
            txtBaseUrl.Enter += txtBaseUrl_Enter;
            txtBaseUrl.Leave += txtBaseUrl_Leave;
        }

        private void ConnectionSettingsForm_Load(object sender, EventArgs e)
        {
            _isUpdatingUi = true;
            cmbProtocol.SelectedIndex = _settingsService.ApiScheme == "http" ? 0 : 1;

            if (string.IsNullOrWhiteSpace(_settingsService.HostPath))
                ShowPlaceholder();
            else
            {
                txtBaseUrl.Text = _settingsService.HostPath;
                txtBaseUrl.ForeColor = Color.Black;
                _isPlaceholderActive = false;
            }
            _isUpdatingUi = false;
        }

        private void cmbProtocol_SelectedIndexChanged(object sender, EventArgs e) { }

        private void txtBaseUrl_TextChanged(object sender, EventArgs e)
        {
            if (_isUpdatingUi || _isPlaceholderActive) return;
            AutoDetectProtocolFromText();
        }

        private void txtBaseUrl_Enter(object sender, EventArgs e) => RemovePlaceholder();

        private void txtBaseUrl_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBaseUrl.Text))
                ShowPlaceholder();
        }

        private void ShowPlaceholder()
        {
            _isUpdatingUi = true;
            _isPlaceholderActive = true;
            txtBaseUrl.Text = _placeholder;
            txtBaseUrl.ForeColor = Color.Gray;
            _isUpdatingUi = false;
        }

        private void RemovePlaceholder()
        {
            if (!_isPlaceholderActive) return;
            _isUpdatingUi = true;
            txtBaseUrl.Text = "";
            txtBaseUrl.ForeColor = Color.Black;
            _isPlaceholderActive = false;
            _isUpdatingUi = false;
        }

        private void AutoDetectProtocolFromText()
        {
            string text = txtBaseUrl.Text.Trim();
            foreach (var (prefix, idx) in new[] { ("http://", 0), ("https://", 1), ("ws://", 0), ("wss://", 1) })
            {
                if (text.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    _isUpdatingUi = true;
                    cmbProtocol.SelectedIndex = idx;
                    txtBaseUrl.Text = text.Substring(prefix.Length);
                    txtBaseUrl.SelectionStart = txtBaseUrl.Text.Length;
                    _isUpdatingUi = false;
                    return;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string scheme = cmbProtocol.SelectedIndex == 0 ? "http" : "https";
            string hostPath = _isPlaceholderActive ? "" : txtBaseUrl.Text.Trim();

            if (string.IsNullOrWhiteSpace(hostPath))
            {
                MessageBox.Show("Please enter the base URL.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBaseUrl.Focus();
                return;
            }

            foreach (var prefix in new[] { "https://", "http://", "wss://", "ws://" })
                hostPath = hostPath.Replace(prefix, "", StringComparison.OrdinalIgnoreCase);

            if (!Uri.TryCreate($"{scheme}://{hostPath}", UriKind.Absolute, out _))
            {
                MessageBox.Show("Invalid URL. Please enter a valid server address.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBaseUrl.Focus();
                return;
            }

            _settingsService.Save(scheme, hostPath);
            MessageBox.Show("Connection settings saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _settingsService.Reset();
            _isUpdatingUi = true;
            cmbProtocol.SelectedIndex = 1;
            _isUpdatingUi = false;
            ShowPlaceholder();
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}
