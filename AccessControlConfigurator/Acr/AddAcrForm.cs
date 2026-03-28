using AccessControlSystem.Models.Acr;
using System;
using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    public partial class AddAcrForm : Form
    {
        public AcrDto AcrData { get; private set; }

        public AddAcrForm()
        {
            InitializeComponent();

            AcrData = new AcrDto();

            LoadDropdowns();
        }

        private void LoadDropdowns()
        {
            cmbDefaultMode.Items.AddRange(new object[]
            {
                "Locked",
                "Unlocked"
            });

            cmbReaderType.Items.AddRange(new object[]
            {
                "Card",
                "Pin",
                "Card + Pin"
            });

            cmbReaderDirection.Items.AddRange(new object[]
            {
                "Entry",
                "Exit"
            });

            cmbDefaultMode.SelectedIndex = 0;
            cmbReaderType.SelectedIndex = 0;
            cmbReaderDirection.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name is required");
                return;
            }

            AcrData.name = txtName.Text;

            AcrData.acrNumber = (int)numAcrNumber.Value;

            AcrData.defaultMode = cmbDefaultMode.SelectedIndex;

            AcrData.readerNumber = (int)numReaderNumber.Value;

            AcrData.readerType = cmbReaderType.SelectedIndex;

            AcrData.readerDirection = cmbReaderDirection.SelectedIndex;

            AcrData.strikeNumber = (int)numStrikeNumber.Value;

            AcrData.doorNumber = (int)numDoorNumber.Value;

            AcrData.rex0Number = (int)numRexNumber.Value;
            AcrData.rexNumber = AcrData.rex0Number;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
