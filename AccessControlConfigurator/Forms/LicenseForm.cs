using System.Drawing;
using System.Windows.Forms;
using AccessControlSystem.ApiClient;

namespace AccessControlConfigurator
{
    public partial class LicenseForm : Form
    {
        private readonly string _placeholder = "Enter license key";
        private bool _isPlaceholderActive = false;

        public LicenseForm()
        {
            InitializeComponent();
            Load += LicenseForm_Load;
            txtLicenseKey.Enter += txtLicenseKey_Enter;
            txtLicenseKey.Leave += txtLicenseKey_Leave;
        }

        private void LicenseForm_Load(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AppConfig.LicenseKey))
                ShowPlaceholder();
            else
            {
                txtLicenseKey.Text = AppConfig.LicenseKey;
                txtLicenseKey.ForeColor = Color.Black;
                _isPlaceholderActive = false;
            }
        }

        private void txtLicenseKey_Enter(object sender, System.EventArgs e) => RemovePlaceholder();

        private void txtLicenseKey_Leave(object sender, System.EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLicenseKey.Text))
                ShowPlaceholder();
        }

        private void ShowPlaceholder()
        {
            _isPlaceholderActive = true;
            txtLicenseKey.Text = _placeholder;
            txtLicenseKey.ForeColor = Color.Gray;
        }

        private void RemovePlaceholder()
        {
            if (!_isPlaceholderActive) return;
            txtLicenseKey.Text = "";
            txtLicenseKey.ForeColor = Color.Black;
            _isPlaceholderActive = false;
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string licenseKey = _isPlaceholderActive ? "" : txtLicenseKey.Text.Trim();
            if (string.IsNullOrWhiteSpace(licenseKey))
            {
                MessageBox.Show("Please enter a license key.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLicenseKey.Focus();
                return;
            }

            AppConfig.SaveLicenseKey(licenseKey);
            MessageBox.Show("License key saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btnClear_Click(object sender, System.EventArgs e)
        {
            AppConfig.ResetLicenseKey();
            ShowPlaceholder();
        }

        private void btnCancel_Click(object sender, System.EventArgs e) => Close();
    }
}
