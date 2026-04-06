using AccessControlConfigurator.Helpers;
using AccessControlSystem.Forms;
using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    public partial class EditControllerControl : UserControl
    {
        private ControllerDto _controller;
        private BindingList<SioModel> _sioList;
        private readonly ApiService _api = new ApiService();
        private int _controllerId;

        // ===== Constructor when coming from Controllers page =====
        public EditControllerControl(ControllerDto controller)
        {
            InitializeComponent();

            _controller = controller;
            _controllerId = controller.Id;
            _sioList = new BindingList<SioModel>();

            if (_controller == null)
            {
                MessageBox.Show("Controller data missing!");
                return;
            }

            SetupGrid();
            LoadController();

            if (!_controller.IsEnabled)
            {
                ConfigureDiscoveredControllerView();
            }

            dgvSIO.DataSource = _sioList;

            this.Load += async (s, e) => await RefreshSiosAsync();
            btnAddSIO.Click += btnAddSIO_Click;
            btnBack.Click += btnBack_Click;
        }

        // ===== Constructor when coming BACK from Onboard page =====
        public EditControllerControl(ControllerDto controller, BindingList<SioModel> sioList)
        {
            InitializeComponent();

            _controller = controller;
            _sioList = sioList ?? new BindingList<SioModel>();

            SetupGrid();
            LoadController();
            ConfigureDiscoveredControllerView();

            dgvSIO.DataSource = _sioList;

            this.Load += async (s, e) => await RefreshSiosAsync();
            btnAddSIO.Click += btnAddSIO_Click;
            btnBack.Click += btnBack_Click;
        }

        // ===== Load TimeZones into ComboBox =====
        private async Task LoadTimeZonesAsync()
        {
            try
            {
                var timezones = await _api.GetTimeZonesAsync();
                cmbTimeZone.DataSource = timezones;
                cmbTimeZone.DisplayMember = "Name";
                cmbTimeZone.ValueMember = "Id";

                // Pre-select the controller's current timezone
                if (_controller != null && _controller.timeZoneId > 0)
                {
                    cmbTimeZone.SelectedValue = _controller.TimeZoneId;
                }
            }
            catch
            {
                // Silently ignore if timezone load fails
            }
        }

        // ===== Load SIOs =====
        internal async Task RefreshSiosAsync()
        {
            try
            {
                // Skip for discovered controllers
                if (!_controller.IsEnabled)
                {
                    dgvSIO.DataSource = null;
                    panelAdd.Visible = false;
                    panelGrid.Visible = false;
                    return;
                }

                var sios = await _api.GetSiosAsync(_controller.Id);
                _sioList = new BindingList<SioModel>(sios);
                dgvSIO.DataSource = _sioList;

                foreach (DataGridViewColumn col in dgvSIO.Columns)
                {
                    if (col is DataGridViewButtonColumn)
                    {
                        col.ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(SioErrorHelper.GetMessage(ex, "Failed to load SIOs."));
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm.Instance.LoadPage(new ControllersControl(), false);
        }

        // ===== Setup Grid =====
        private void SetupGrid()
        {
            AccessControlConfigurator.Helpers.GridStyleHelper.ApplyStandardStyle(dgvSIO);

            dgvSIO.AutoGenerateColumns = false;
            dgvSIO.RowHeadersVisible = false;
            dgvSIO.AllowUserToAddRows = false;
            dgvSIO.AllowUserToDeleteRows = false;
            dgvSIO.AllowUserToResizeRows = false;
            dgvSIO.AllowUserToResizeColumns = false;
            dgvSIO.AllowUserToOrderColumns = false;
            dgvSIO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSIO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSIO.ReadOnly = true;
            dgvSIO.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvSIO.MultiSelect = false;
            dgvSIO.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            colHardwareId.HeaderText = "ID";
            colHardwareId.DataPropertyName = "Id";
            colSioNumber.DataPropertyName = "SioNumber";
            colType.DataPropertyName = "InterfaceType";
            colPort.DataPropertyName = "PortNumber";
            colAddress.DataPropertyName = "InterfacePanelAddress";
            colStatus.DataPropertyName = "ComStatus";
            colOnline.DataPropertyName = "IsOnline";

            dgvSIO.CellContentClick += dgvSIO_CellContentClick;
            dgvSIO.CellFormatting += dgvSIO_CellFormatting;

            foreach (DataGridViewColumn col in dgvSIO.Columns)
            {
                col.Resizable = DataGridViewTriState.False;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        // ===== Load Controller Info =====
        private async void LoadController()
        {
            txtName.Text = _controller.Name ?? "";
            txtIP.Text = _controller.IpAddress ?? "";
            txtMac.Text = _controller.MacAddress ?? "";
            txtId.Text = _controller.ScpId.ToString();

            txtMac.ReadOnly = true;
            txtId.ReadOnly = true;
            txtIP.ReadOnly = false;
            txtName.ReadOnly = false;

            // Load timezone dropdown and pre-select current value
            await LoadTimeZonesAsync();
        }

        // ===== Online Status Color =====
        private void dgvSIO_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSIO.Columns[e.ColumnIndex].Name == "colOnline")
            {
                if (e.Value != null)
                {
                    bool isOnline = false;

                    if (e.Value is bool b)
                    {
                        isOnline = b;
                    }
                    else if (e.Value.ToString().ToLower() == "online")
                    {
                        isOnline = true;
                    }

                    if (isOnline)
                    {
                        e.Value = "Online";
                        e.CellStyle.ForeColor = Color.Green;
                    }
                    else
                    {
                        e.Value = "Offline";
                        e.CellStyle.ForeColor = Color.Red;
                    }

                    e.FormattingApplied = true;
                }
            }
        }

        // ===== Add New SIO =====
        private void btnAddSIO_Click(object sender, EventArgs e)
        {
            SioControl sioPage = new SioControl(_controller, this);
            sioPage.Dock = DockStyle.Fill;

            Control parent = this.Parent;
            parent.Controls.Clear();
            parent.Controls.Add(sioPage);
        }

        // ===== Grid Buttons =====
        private async void dgvSIO_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvSIO.Columns[e.ColumnIndex].Name == "colDelete")
            {
                var selectedSio = _sioList[e.RowIndex];

                var confirm = MessageBox.Show(
                    "Delete this SIO?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm == DialogResult.Yes)
                {
                    try
                    {
                        await _api.DeleteSioAsync(_controller.Id, selectedSio.Id);
                        _sioList.RemoveAt(e.RowIndex);
                        MessageBox.Show("SIO deleted successfully.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(SioErrorHelper.GetMessage(ex));
                    }
                }
            }

            if (dgvSIO.Columns[e.ColumnIndex].Name == "colEdit")
            {
                var selectedSio = _sioList[e.RowIndex];
                MainForm.Instance.LoadPage(
                    new OnboardConfigControl(_controller, _sioList, selectedSio),
                    false);
            }
        }

        public DataGridView GetSioGrid()
        {
            return dgvSIO;
        }

        // ===== Save Controller =====
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateForm())
                    return;

                // Get selected TimeZone Id
                int selectedTimeZoneId = cmbTimeZone.SelectedValue != null
                    ? (int)cmbTimeZone.SelectedValue
                    : 1;

                int controllerId = _controllerId;

                var dto = new AddUpdateControllerRequestDto
                {
                    Id = controllerId,
                    Name = txtName.Text,
                    MacAddress = txtMac.Text,
                    IpAddress = txtIP.Text,
                    Port = 5000,
                    TimeZoneId = selectedTimeZoneId,
                    LocationId = 1,
                    SubnetMask = "255.255.255.0",
                    DefaultGateway = "192.168.1.1",
                    InternalPort0IsEnabled = true,
                    InternalPort0BaudRate = 0,
                    InternalPort0ProtocolType = 0,
                    Rs485Port1IsEnabled = true,
                    Rs485Port1BaudRate = 38400,
                    Rs485Port1ProtocolType = 0,
                    Rs485Port2IsEnabled = true,
                    Rs485Port2BaudRate = 38400,
                    Rs485Port2ProtocolType = 0
                };

                if (!_controller.IsEnabled) // NEW (DISCOVERED)
                {
                    var addResult = await _api.AddControllerAsync(dto);

                    if (!addResult.Success)
                    {
                        MessageBox.Show(
                            ControllerErrorHelper.GetMessage(addResult.Error),
                            "Add failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }
                else // EXISTING — UPDATE
                {
                    var updateResult = await _api.UpdateControllerAsync(controllerId, dto);

                    if (!updateResult.Success)
                    {
                        MessageBox.Show(
                            ControllerErrorHelper.GetMessage(updateResult.Error),
                            "Update failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                        return;
                    }
                }

                MessageBox.Show("Saved successfully");
                _controller.IsEnabled = true;
                ConfigureDiscoveredControllerView();
                await RefreshSiosAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ControllerErrorHelper.GetMessage(ex, "Save failed."));
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Controller Name is required");
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMac.Text))
            {
                MessageBox.Show("MAC Address is required");
                txtMac.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtMac.Text, @"^([A-Za-z0-9]{2}:){5}([A-Za-z0-9]{2})$"))
            {
                MessageBox.Show("Invalid MAC Address format (AA:BB:CC:DD:EE:FF)");
                txtMac.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtIP.Text))
            {
                MessageBox.Show("IP Address is required");
                txtIP.Focus();
                return false;
            }

            if (!System.Net.IPAddress.TryParse(txtIP.Text, out _))
            {
                MessageBox.Show("Invalid IP Address");
                txtIP.Focus();
                return false;
            }

            return true;
        }

        private void ConfigureDiscoveredControllerView()
        {
            bool isActiveController = _controller?.IsEnabled == true;
            panelAdd.Visible = isActiveController;
            panelGrid.Visible = isActiveController;
            btnAddSIO.Enabled = isActiveController;
        }
    }
}