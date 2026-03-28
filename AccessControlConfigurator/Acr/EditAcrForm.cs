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

            AcrData = acr ?? new AcrDto();
           
            numDoorNumber.ReadOnly = false;
            numDoorNumber.Enabled = true;

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
            cbReaderType.Items.Add("Card");
            cbReaderType.Items.Add("Pin");
            cbReaderType.Items.Add("Card + Pin");
            
            // Reader Direction
            cbReaderDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cbReaderDirection.Items.Clear();
            cbReaderDirection.Items.Add("Entry");
            cbReaderDirection.Items.Add("Exit");
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
            cbReaderType.SelectedIndex =
                (AcrData.readerType >= 0 && AcrData.readerType < cbReaderType.Items.Count)
                ? AcrData.readerType : 0;

            cbReaderDirection.SelectedIndex =
                (AcrData.readerDirection >= 0 && AcrData.readerDirection < cbReaderDirection.Items.Count)
                ? AcrData.readerDirection : 0;

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
            AcrData.acrNumber = (int)numAcrNumber.Value;
            AcrData.defaultMode = (int)numDefaultMode.Value;
            AcrData.readerNumber = (int)numReaderNumber.Value;

            // ✅ GET FROM DROPDOWN
            AcrData.readerType = cbReaderType.SelectedIndex;
            AcrData.readerDirection = cbReaderDirection.SelectedIndex;

            AcrData.strikeNumber = (int)numStrikeNumber.Value;
            AcrData.doorNumber = (int)numDoorNumber.Value;  // ✅ editable
            AcrData.rex0Number = (int)numRexNumber.Value;

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
