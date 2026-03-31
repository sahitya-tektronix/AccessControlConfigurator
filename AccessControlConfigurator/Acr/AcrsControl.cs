using AccessControlConfigurator.Forms;
using AccessControlSystem.Models;
using AccessControlSystem.Models.Acr;
using AccessControlSystem.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
            ApplyButtonStyles();

            cmbControllerId.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbSioNumber.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbReader.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbAcrName.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbControllerName.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbSioName.SelectedIndexChanged += (s, e) => ApplyFilters();
            btnClearFilters.Click += btnClearFilters_Click;

            dgvAcrs.CellBeginEdit += (s, e) => e.Cancel = true;
            // Button style
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;
            btnSearch.Click += btnSearch_Click;
            //txtSearch.TextChanged += txtSearch_TextChanged;

            ConfigureGrid();
            Resize += (s, e) => AlignToolbar();
            topPanel.SizeChanged += (s, e) => AlignToolbar();


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
            AlignToolbar();
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
                if (cmbAcrName.Items.Count > 0)
                    cmbAcrName.SelectedIndex = 0;
                if (cmbControllerName.Items.Count > 0)
                    cmbControllerName.SelectedIndex = 0;
                if (cmbSioName.Items.Count > 0)
                    cmbSioName.SelectedIndex = 0;
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
                string controllerName = (a.controllerName ?? "").ToLower();
                string sio = a.sioNumber.ToString();
                string sioName = (a.sioName ?? "").ToLower();
                string acr = a.acrNumber.ToString();
                string reader = $"reader {a.readerNumber}".ToLower();
                string readerNum = a.readerNumber.ToString();
                string online = ResolveIsOnline(a) ? "online" : "offline";

                // Match ANY field
                return id.Contains(searchText) ||
                       name.Contains(searchText) ||
                       controller.Contains(searchText) ||
                       controllerName.Contains(searchText) ||
                       sio.Contains(searchText) ||
                       sioName.Contains(searchText) ||
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
                bool isOnline = ResolveIsOnline(a);
                string onlineText = isOnline ? "Online" : "Offline";

                int rowIndex = dgvAcrs.Rows.Add(
                    a.id,
                    a.name,
                    a.controllerID,
                    a.controllerName ?? string.Empty,
                    a.sioNumber,
                    a.sioName ?? string.Empty,
                    $"Reader {a.readerNumber}",
                    a.acrNumber,
                    onlineText
                );

                dgvAcrs.Rows[rowIndex].Cells[8].Style.ForeColor =
                    isOnline ? Color.Green : Color.Red;
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
            if (cmbAcrName.Items.Count > 0)
                cmbAcrName.SelectedIndex = 0;
            if (cmbControllerName.Items.Count > 0)
                cmbControllerName.SelectedIndex = 0;
            if (cmbSioName.Items.Count > 0)
                cmbSioName.SelectedIndex = 0;

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
                        acrNumber = form.AcrData.acrNumber,
                        defaultMode = form.AcrData.defaultMode,
                        readerNumber = li.readerNumber,
                        readerType = 2201,
                        readerDirection = form.AcrData.readerDirection,
                        controllerID = li.controllerID,
                        sioNumber = li.sioNumber,
                        strikeNumber = form.AcrData.strikeNumber,
                        doorNumber = form.AcrData.doorNumber,
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

            // ACR Name
            var acrNames = _allData
                .Select(x => x.name ?? string.Empty)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();

            acrNames.Insert(0, "All");
            cmbAcrName.DataSource = acrNames;

            // Controller Name
            var controllerNames = _allData
                .Select(x => x.controllerName ?? string.Empty)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();

            controllerNames.Insert(0, "All");
            cmbControllerName.DataSource = controllerNames;

            // Sio Name
            var sioNames = _allData
                .Select(x => x.sioName ?? string.Empty)
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(x => x)
                .ToList();

            sioNames.Insert(0, "All");
            cmbSioName.DataSource = sioNames;
        }
        private void ApplyFilters()
        {
            var filtered = _allData.AsEnumerable();

            string controller = cmbControllerId.SelectedItem?.ToString();
            string sio = cmbSioNumber.SelectedItem?.ToString();
            string reader = cmbReader.SelectedItem?.ToString();
            string acrName = cmbAcrName.SelectedItem?.ToString();
            string controllerName = cmbControllerName.SelectedItem?.ToString();
            string sioName = cmbSioName.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(controller) && controller != "All")
                filtered = filtered.Where(x => x.controllerID.ToString() == controller);

            if (!string.IsNullOrEmpty(sio) && sio != "All")
                filtered = filtered.Where(x => x.sioNumber.ToString() == sio);

            if (!string.IsNullOrEmpty(reader) && reader != "All")
                filtered = filtered.Where(x => $"Reader {x.readerNumber}" == reader);

            if (!string.IsNullOrEmpty(acrName) && acrName != "All")
                filtered = filtered.Where(x => string.Equals(x.name ?? string.Empty, acrName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(controllerName) && controllerName != "All")
                filtered = filtered.Where(x => string.Equals(x.controllerName ?? string.Empty, controllerName, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(sioName) && sioName != "All")
                filtered = filtered.Where(x => string.Equals(x.sioName ?? string.Empty, sioName, StringComparison.OrdinalIgnoreCase));

            PopulateGrid(filtered.ToList());
        }

        private void cmbControllerId_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ApplyButtonStyles()
        {
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnEdit, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnRefresh, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnBack, 90);
        }

        private void AlignToolbar()
        {
            int top = 8;
            int spacing = 14;
            int rightPadding = 10;

            btnEdit.Location = new Point(10, top);
            btnRefresh.Location = new Point(btnEdit.Right + spacing, top);
            btnBack.Location = new Point(btnRefresh.Right + spacing, top);

            int filterTop = top;
            cmbReader.Location = new Point(topPanel.ClientSize.Width - cmbReader.Width - rightPadding, filterTop);
            lblReader.Location = new Point(cmbReader.Left - lblReader.Width - 8, filterTop + 4);
            cmbSioNumber.Location = new Point(lblReader.Left - cmbSioNumber.Width - 16, filterTop);
            lblSio.Location = new Point(cmbSioNumber.Left - lblSio.Width - 8, filterTop + 4);
            cmbControllerId.Location = new Point(lblSio.Left - cmbControllerId.Width - 16, filterTop);
            lblController.Location = new Point(cmbControllerId.Left - lblController.Width - 8, filterTop + 4);

            int filterTop2 = filterTop + cmbReader.Height + 6;
            cmbControllerName.Location = new Point(topPanel.ClientSize.Width - cmbControllerName.Width - rightPadding, filterTop2);
            lblControllerName.Location = new Point(cmbControllerName.Left - lblControllerName.Width - 8, filterTop2 + 4);
            cmbSioName.Location = new Point(lblControllerName.Left - cmbSioName.Width - 16, filterTop2);
            lblSioName.Location = new Point(cmbSioName.Left - lblSioName.Width - 8, filterTop2 + 4);
            cmbAcrName.Location = new Point(lblSioName.Left - cmbAcrName.Width - 16, filterTop2);
            lblAcrName.Location = new Point(cmbAcrName.Left - lblAcrName.Width - 8, filterTop2 + 4);

            bool wrapFilters = lblController.Left <= btnBack.Right + spacing;
            if (wrapFilters)
            {
                filterTop = btnEdit.Bottom + 10;
                cmbReader.Location = new Point(topPanel.ClientSize.Width - cmbReader.Width - rightPadding, filterTop);
                lblReader.Location = new Point(cmbReader.Left - lblReader.Width - 8, filterTop + 4);
                cmbSioNumber.Location = new Point(lblReader.Left - cmbSioNumber.Width - 16, filterTop);
                lblSio.Location = new Point(cmbSioNumber.Left - lblSio.Width - 8, filterTop + 4);
                cmbControllerId.Location = new Point(lblSio.Left - cmbControllerId.Width - 16, filterTop);
                lblController.Location = new Point(cmbControllerId.Left - lblController.Width - 8, filterTop + 4);

                filterTop2 = filterTop + cmbReader.Height + 6;
                cmbControllerName.Location = new Point(topPanel.ClientSize.Width - cmbControllerName.Width - rightPadding, filterTop2);
                lblControllerName.Location = new Point(cmbControllerName.Left - lblControllerName.Width - 8, filterTop2 + 4);
                cmbSioName.Location = new Point(lblControllerName.Left - cmbSioName.Width - 16, filterTop2);
                lblSioName.Location = new Point(cmbSioName.Left - lblSioName.Width - 8, filterTop2 + 4);
                cmbAcrName.Location = new Point(lblSioName.Left - cmbAcrName.Width - 16, filterTop2);
                lblAcrName.Location = new Point(cmbAcrName.Left - lblAcrName.Width - 8, filterTop2 + 4);
            }

            topPanel.Height = Math.Max(50, cmbControllerName.Bottom + 10);

            int searchTop = topPanel.Bottom + 8;
            btnClearFilters.Location = new Point(Width - btnClearFilters.Width - 10, searchTop);
            btnSearch.Location = new Point(btnClearFilters.Left - btnSearch.Width - 8, searchTop);
            txtSearch.Location = new Point(btnSearch.Left - txtSearch.Width - 8, searchTop + 1);
            lblSearchRight.Location = new Point(txtSearch.Left - lblSearchRight.Width - 8, searchTop + 4);

            dgvAcrs.Location = new Point(0, btnSearch.Bottom + 8);
            dgvAcrs.Size = new Size(Width, Height - dgvAcrs.Top);
        }

        private static bool ResolveIsOnline(AcrDto acr)
        {
            if (acr == null)
                return false;

            var type = acr.GetType();
            foreach (var propertyName in new[] { "isOnline", "IsOnline", "online", "Online" })
            {
                var property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                if (property == null)
                    continue;

                var rawValue = property.GetValue(acr);
                if (rawValue is bool boolValue)
                    return boolValue;

                if (rawValue != null && bool.TryParse(rawValue.ToString(), out var parsed))
                    return parsed;
            }

            foreach (var propertyName in new[] { "status", "Status", "statusText", "StatusText" })
            {
                var property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                var text = property?.GetValue(acr)?.ToString();
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                if (text.Equals("online", StringComparison.OrdinalIgnoreCase))
                    return true;

                if (text.Equals("offline", StringComparison.OrdinalIgnoreCase))
                    return false;
            }

            return false;
        }
    }
}
