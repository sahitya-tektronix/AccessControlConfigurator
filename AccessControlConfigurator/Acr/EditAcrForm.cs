using AccessControlSystem.Models.Acr;
using System;
using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    public partial class EditAcrForm : Form
    {
        public AcrDto AcrData { get; private set; }

        public EditAcrForm(AcrDto acr)
        {
            InitializeComponent();

            AcrData = acr == null
                ? new AcrDto()
                : new AcrDto
                {
                    id = acr.id,
                    controllerID = acr.controllerID,
                    sioNumber = acr.sioNumber,
                    name = acr.name,
                    acrNumber = acr.acrNumber,
                    defaultAcrName = acr.defaultAcrName,
                    defaultMode = acr.defaultMode,
                    readerNumber = acr.readerNumber,
                    readerType = acr.readerType,
                    readerDirection = acr.readerDirection,
                    strikeNumber = acr.strikeNumber,
                    doorNumber = acr.doorNumber,
                    rexNumber = acr.rexNumber,
                    acrId = acr.acrId,
                    rex0Number = acr.rex0Number
                };

            InitializeDropdowns();
            LoadValues();
        }

        // ✅ Dropdown initialization
        private void InitializeDropdowns()
        {
            cbReaderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbReaderDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            // Reader Type
            cbReaderType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbReaderType.Items.Clear();
            cbReaderType.Items.Add("Signo Reader");
            cbReaderType.SelectedIndex = 0;
            
            // Reader Direction
            cbReaderDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cbReaderDirection.Items.Clear();
            cbReaderDirection.Items.Add("In");
            cbReaderDirection.Items.Add("Out");
            cbReaderDirection.Items.Add("In/Out");
        }

        // ════════════════════════════════════════════════════════
        // Load values
        // ════════════════════════════════════════════════════════
        private void LoadValues()
        {
            txtName.Text = AcrData.name ?? "";

            SafeSet(numAcrNumber, AcrData.acrNumber);
            SafeSet(numDefaultMode, AcrData.defaultMode);
            SafeSet(numReaderNumber, AcrData.readerNumber);

            // ✅ SET DROPDOWNS
            cbReaderType.SelectedIndex = AcrData.readerType == 2201 ? 0 : 0;

            cbReaderDirection.SelectedIndex = AcrData.readerDirection switch
            {
                1 => 0,
                2 => 1,
                3 => 2,
                _ => 0
            };

            SafeSet(numStrikeNumber, AcrData.strikeNumber);
            SafeSet(numDoorNumber, AcrData.doorNumber);   // ✅ editable
            SafeSet(numRexNumber, AcrData.rex0Number);
        }

        // Clamps value between Min and Max
        private void SafeSet(NumericUpDown num, decimal value)
        {
            if (value < num.Minimum) value = num.Minimum;
            if (value > num.Maximum) value = num.Maximum;
            num.Value = value;
        }

        // ════════════════════════════════════════════════════════
        // Save
        // ════════════════════════════════════════════════════════
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            AcrData.name = txtName.Text.Trim();
            AcrData.defaultAcrName = AcrData.name;
            AcrData.acrNumber = (int)numAcrNumber.Value;
            AcrData.defaultMode = (int)numDefaultMode.Value;
            AcrData.readerNumber = (int)numReaderNumber.Value;

            // ✅ GET FROM DROPDOWN
            AcrData.readerType = 2201;
            AcrData.readerDirection = cbReaderDirection.SelectedIndex switch
            {
                0 => 1,
                1 => 2,
                2 => 3,
                _ => 1
            };

            AcrData.strikeNumber = (int)numStrikeNumber.Value;
            AcrData.doorNumber = (int)numDoorNumber.Value;
            AcrData.rex0Number = (int)numRexNumber.Value;
            AcrData.rexNumber = AcrData.rex0Number;

            DialogResult = DialogResult.OK;
            Close();
        }

        // ════════════════════════════════════════════════════════
        // Cancel
        // ════════════════════════════════════════════════════════
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
