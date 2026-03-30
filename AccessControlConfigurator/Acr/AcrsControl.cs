using AccessControlConfigurator.Forms;
using AccessControlSystem.Models;
using AccessControlSystem.Models.Acr;
using AccessControlSystem.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace AccessControlConfigurator
{
    public partial class AcrsControl : UserControl
    {
        private ApiService _api = new ApiService();
        private List<AcrDto> _allData = new List<AcrDto>();


        private ControllerDto _controller;
        private BindingList<SioModel> _sioList;
        private SioModel _selectedSio;

        private List<AcrDto> acrData;

        // ================= CONSTRUCTOR =================
        public AcrsControl(
            ControllerDto controller,
            BindingList<SioModel> sioList,
            SioModel selectedSio)
        {
            InitializeComponent();

            _controller = controller;
            _sioList = sioList;
            _selectedSio = selectedSio;

            LoadFilters(); // ✅ MUST

            cmbControllerId.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbSioNumber.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbReader.SelectedIndexChanged += (s, e) => ApplyFilters();
            btnClearFilters.Click += btnClearFilters_Click;

            dgvAcrs.CellBeginEdit += (s, e) => e.Cancel = true;
            // Button style
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;
            btnSearch.Click += btnSearch_Click;
            //txtSearch.TextChanged += txtSearch_TextChanged;

            ConfigureGrid();


            // ⌨️ Enter key search
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    ApplySearch();
            };
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearFilters.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right; ;
            int rightMargin = 20;

            // Clear button (last)
            btnClearFilters.Location = new Point(this.Width - btnClearFilters.Width - rightMargin, 10);

            // Search button (before clear)
            btnSearch.Location = new Point(btnClearFilters.Left - btnSearch.Width - 5, 10);

            // Textbox (before search)
            txtSearch.Location = new Point(btnSearch.Left - txtSearch.Width - 5, 10);

            // Label (before textbox)
            lblSearchRight.Location = new Point(txtSearch.Left - lblSearchRight.Width - 5, 14);
            dgvAcrs.MultiSelect = false;
            dgvAcrs.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 120, 215);
            dgvAcrs.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvAcrs.CellClick += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dgvAcrs.Rows[e.RowIndex].Selected = true;
            };
        }

        private void ConfigureGrid()
        {
            Helpers.GridStyleHelper.ApplyStandardStyle(dgvAcrs);

            dgvAcrs.AutoGenerateColumns = false;
            dgvAcrs.AllowUserToOrderColumns = false;
            dgvAcrs.AllowUserToResizeColumns = false;
            dgvAcrs.AllowUserToResizeRows = false;
            dgvAcrs.MultiSelect = false;
            dgvAcrs.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (DataGridViewColumn col in dgvAcrs.Columns)
            {
                col.ReadOnly = true;
                col.Resizable = DataGridViewTriState.False;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        // ================= LOAD CONTROL =================
        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadAcrs();
        }

        // ================= LOAD DATA =================
        private async Task LoadAcrs()
        {
            try
            {
                var data = await _api.GetAcrSearchAsync("");

                if (data == null || data.Count == 0)
                {
                    dgvAcrs.Rows.Clear();
                    return;
                }

                _allData = data;
                acrData = data;

                PopulateGrid(_allData);

                LoadFilters(); // ✅ fill dropdowns

                // ✅ default selection
                cmbControllerId.SelectedIndex = 0;
                cmbSioNumber.SelectedIndex = 0;
                cmbReader.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                var msg = ex.Message ?? string.Empty;
                if (msg.Contains("acr_number_in_use", StringComparison.OrdinalIgnoreCase) ||
                    msg.Contains("already in use", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "ACR number already in use for this Controller and SIO.\nPlease choose a different ACR number.",
                        "Duplicate ACR Number",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ================= SEARCH =================
        private void ApplySearch()
        {
            string searchText = txtSearch.Text?.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                PopulateGrid(acrData);
                return;
            }

            var filtered = acrData.Where(a =>
            {
                // Normalize all fields
                string id = a.id.ToString();
                string name = (a.name ?? "").ToLower();
                string controller = a.controllerID.ToString();
                string sio = a.sioNumber.ToString();
                string acr = a.acrNumber.ToString();
                string reader = $"reader {a.readerNumber}".ToLower();
                string readerNum = a.readerNumber.ToString();
                string online = a.isOnline ? "online" : "offline";

                // Match ANY field
                return id.Contains(searchText) ||
                       name.Contains(searchText) ||
                       controller.Contains(searchText) ||
                       sio.Contains(searchText) ||
                       acr.Contains(searchText) ||
                       reader.Contains(searchText) ||
                       readerNum.Contains(searchText) ||
                       online.Contains(searchText);
            }).ToList();

            PopulateGrid(filtered);
        }

        // ================= GRID =================
        private void PopulateGrid(List<AcrDto> data)
        {
            dgvAcrs.Rows.Clear();

            foreach (var a in data)
            {
                string onlineText = a.isOnline ? "Online" : "Offline";

                int rowIndex = dgvAcrs.Rows.Add(
                    a.id,
                    a.name,
                    a.controllerID,
                    a.sioNumber,
                    $"Reader {a.readerNumber}",
                    a.acrNumber,
                    onlineText
                );

                dgvAcrs.Rows[rowIndex].Cells[6].Style.ForeColor =
                    a.isOnline ? Color.Green : Color.Red;
            }
        }

        // ================= BUTTON EVENTS =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearch();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (cmbControllerId.Items.Count > 0)
                cmbControllerId.SelectedIndex = 0;
            if (cmbSioNumber.Items.Count > 0)
                cmbSioNumber.SelectedIndex = 0;
            if (cmbReader.Items.Count > 0)
                cmbReader.SelectedIndex = 0;

            PopulateGrid(_allData);
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            await LoadAcrs();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm.Instance.LoadPage(new ControllersControl(), false);
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Check selection
                if (dgvAcrs.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Select ACR first");
                    return;
                }

                // ✅ Get ID from first column (SAFE & CORRECT)
                int acrId = Convert.ToInt32(
                    dgvAcrs.SelectedRows[0].Cells[0].Value);

                // ✅ Find selected data
                var li = acrData.FirstOrDefault(x => x.id == acrId);

                if (li == null)
                {
                    MessageBox.Show("Data not found");
                    return;
                }

                // ✅ Open edit form
                var form = new EditAcrForm(li);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    var updatePayload = new AcrDto
                    {
                        name = form.AcrData.name,
                        defaultAcrName = string.IsNullOrWhiteSpace(form.AcrData.defaultAcrName)
                            ? form.AcrData.name
                            : form.AcrData.defaultAcrName,
                        acrNumber = li.acrNumber,
                        defaultMode = form.AcrData.defaultMode,
                        readerNumber = li.readerNumber,
                        readerType = 2201,
                        readerDirection = form.AcrData.readerDirection,
                        controllerID = li.controllerID,
                        sioNumber = li.sioNumber,
                        strikeNumber = form.AcrData.strikeNumber,
                        doorNumber = li.doorNumber,
                        rex0Number = form.AcrData.rex0Number,
                        rexNumber = form.AcrData.rexNumber
                    };

                    // ✅ Update API
                    await _api.UpdateAcrAsync(
                        li.controllerID,
                        li.sioNumber,
                        li.id,
                        updatePayload);

                    // ✅ Refresh grid
                    await LoadAcrs();

                    MessageBox.Show("ACR updated successfully!");
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message ?? string.Empty;
                if (message.Contains("409") ||
                    message.Contains("Conflict", StringComparison.OrdinalIgnoreCase) ||
                    message.Contains("already in use", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "ACR update failed because the server reported a conflict. The current ACR number or reader mapping is being treated as a duplicate by the API.",
                        "ACR Update Conflict",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Error: " + ex.Message);
            }
        
        }
        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    ApplySearch();
        //}

        private void dgvAcrs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void LoadFilters()
        {
            if (_allData == null || _allData.Count == 0)
                return;

            // Controller
            var controllers = _allData
                .Select(x => x.controllerID.ToString())
                .Distinct()
                .ToList();

            controllers.Insert(0, "All");
            cmbControllerId.DataSource = controllers;

            // SIO
            var sioList = _allData
                .Select(x => x.sioNumber.ToString())
                .Distinct()
                .ToList();

            sioList.Insert(0, "All");
            cmbSioNumber.DataSource = sioList;

            // Reader
            var readers = _allData
                .Select(x => $"Reader {x.readerNumber}")
                .Distinct()
                .ToList();

            readers.Insert(0, "All");
            cmbReader.DataSource = readers;
        }
        private void ApplyFilters()
        {
            var filtered = _allData.AsEnumerable();

            string controller = cmbControllerId.SelectedItem?.ToString();
            string sio = cmbSioNumber.SelectedItem?.ToString();
            string reader = cmbReader.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(controller) && controller != "All")
                filtered = filtered.Where(x => x.controllerID.ToString() == controller);

            if (!string.IsNullOrEmpty(sio) && sio != "All")
                filtered = filtered.Where(x => x.sioNumber.ToString() == sio);

            if (!string.IsNullOrEmpty(reader) && reader != "All")
                filtered = filtered.Where(x => $"Reader {x.readerNumber}" == reader);

            PopulateGrid(filtered.ToList());
        }

        private void cmbControllerId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
