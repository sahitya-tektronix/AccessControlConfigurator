using System;
using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    public partial class MappingControl : UserControl
    {
        public MappingControl()
        {
            InitializeComponent();
            LoadControllers();
            InitializeGrid();
        }

        private void LoadControllers()
        {
            cmbController.Items.Add("07326826");
            cmbController.Items.Add("22222222");
            cmbController.Items.Add("11111111");

            cmbReader.Items.Add("Reader 1");
            cmbReader.Items.Add("Reader 2");
            cmbReader.Items.Add("Reader 3");
        }

        private void InitializeGrid()
        {
            dgvMapping.Columns.Add("Controller", "Controller");
            dgvMapping.Columns.Add("Reader", "Reader");
            dgvMapping.Columns.Add("Door", "Door Name");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbController.Text == "" || cmbReader.Text == "" || txtDoorName.Text == "")
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            dgvMapping.Rows.Add(
                cmbController.Text,
                cmbReader.Text,
                txtDoorName.Text
            );

            txtDoorName.Clear();
        }
    }
}
