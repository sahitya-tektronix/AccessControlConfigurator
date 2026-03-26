using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    partial class AddAcrForm
    {
        private TextBox txtName;

        private NumericUpDown numAcrNumber;
        private NumericUpDown numReaderNumber;
        private NumericUpDown numStrikeNumber;
        private NumericUpDown numDoorNumber;
        private NumericUpDown numRexNumber;

        private ComboBox cmbDefaultMode;
        private ComboBox cmbReaderType;
        private ComboBox cmbReaderDirection;

        private Button btnSave;
        private Button btnCancel;

        private void InitializeComponent()
        {
            txtName = new TextBox();

            numAcrNumber = new NumericUpDown();
            numReaderNumber = new NumericUpDown();
            numStrikeNumber = new NumericUpDown();
            numDoorNumber = new NumericUpDown();
            numRexNumber = new NumericUpDown();

            cmbDefaultMode = new ComboBox();
            cmbReaderType = new ComboBox();
            cmbReaderDirection = new ComboBox();

            btnSave = new Button();
            btnCancel = new Button();

            SuspendLayout();

            this.Text = "Add ACR";
            this.Width = 420;
            this.Height = 420;
            this.StartPosition = FormStartPosition.CenterParent;

            int labelX = 20;
            int controlX = 160;
            int y = 20;

            // Name
            Controls.Add(new Label() { Text = "Name", Left = labelX, Top = y + 5, Width = 120 });
            txtName.Left = controlX;
            txtName.Top = y;
            txtName.Width = 200;
            Controls.Add(txtName);
            y += 35;

            // ACR Number
            Controls.Add(new Label() { Text = "ACR Number", Left = labelX, Top = y + 5 });
            numAcrNumber.Left = controlX;
            numAcrNumber.Top = y;
            numAcrNumber.Width = 200;
            Controls.Add(numAcrNumber);
            y += 35;

            // Default Mode
            Controls.Add(new Label() { Text = "Default Mode", Left = labelX, Top = y + 5 });
            cmbDefaultMode.Left = controlX;
            cmbDefaultMode.Top = y;
            cmbDefaultMode.Width = 200;
            Controls.Add(cmbDefaultMode);
            y += 35;

            // Reader Number
            Controls.Add(new Label() { Text = "Reader Number", Left = labelX, Top = y + 5 });
            numReaderNumber.Left = controlX;
            numReaderNumber.Top = y;
            numReaderNumber.Width = 200;
            Controls.Add(numReaderNumber);
            y += 35;

            // Reader Type
            Controls.Add(new Label() { Text = "Reader Type", Left = labelX, Top = y + 5 });
            cmbReaderType.Left = controlX;
            cmbReaderType.Top = y;
            cmbReaderType.Width = 200;
            Controls.Add(cmbReaderType);
            y += 35;

            // Reader Direction
            Controls.Add(new Label() { Text = "Reader Direction", Left = labelX, Top = y + 5 });
            cmbReaderDirection.Left = controlX;
            cmbReaderDirection.Top = y;
            cmbReaderDirection.Width = 200;
            Controls.Add(cmbReaderDirection);
            y += 35;

            // Strike Number
            Controls.Add(new Label() { Text = "Strike Number", Left = labelX, Top = y + 5 });
            numStrikeNumber.Left = controlX;
            numStrikeNumber.Top = y;
            numStrikeNumber.Width = 200;
            Controls.Add(numStrikeNumber);
            y += 35;

            // Door Number
            Controls.Add(new Label() { Text = "Door Number", Left = labelX, Top = y + 5 });
            numDoorNumber.Left = controlX;
            numDoorNumber.Top = y;
            numDoorNumber.Width = 200;
            Controls.Add(numDoorNumber);
            y += 35;

            // REX Number
            Controls.Add(new Label() { Text = "REX Number", Left = labelX, Top = y + 5 });
            numRexNumber.Left = controlX;
            numRexNumber.Top = y;
            numRexNumber.Width = 200;
            Controls.Add(numRexNumber);
            y += 40;

            // Save Button
            btnSave.Text = "Save";
            btnSave.Left = 160;
            btnSave.Top = y;
            btnSave.Width = 80;
            btnSave.Click += btnSave_Click;
            Controls.Add(btnSave);

            // Cancel Button
            btnCancel.Text = "Cancel";
            btnCancel.Left = 250;
            btnCancel.Top = y;
            btnCancel.Width = 80;
            btnCancel.Click += btnCancel_Click;
            Controls.Add(btnCancel);

            ResumeLayout(false);
        }
    }
}