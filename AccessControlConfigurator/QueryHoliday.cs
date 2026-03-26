using AccessControlSystem.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class QueryHolidayForm : Form
    {
        private readonly ApiService _apiService = new ApiService();

        public QueryHolidayForm()
        {
            InitializeComponent();
        }

        private async void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                int scpId = 1;

                var result = await _apiService.QueryHoliday(scpId);

                dgvResult.DataSource = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}