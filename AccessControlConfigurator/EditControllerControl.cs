using AccessControlSystem.Forms;
using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Json;
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
                MessageBox.Show("This is a discovered controller. Configure and save to activate.");
            }

            dgvSIO.DataSource = _sioList;

            this.Load += async (s, e) => await LoadSiosAsync();

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

            dgvSIO.DataSource = _sioList;

            this.Load += async (s, e) => await LoadSiosAsync();

            btnAddSIO.Click += btnAddSIO_Click;
            btnBack.Click += btnBack_Click;
        }

        // ===== Load SIOs =====
        private async Task LoadSiosAsync()
        {
            try
            {
                // ✅ SKIP FOR DISCOVERED CONTROLLERS
                if (!_controller.IsEnabled)
                {
                    dgvSIO.DataSource = null;

                    // Optional UX
                    MessageBox.Show("SIOs are available only after activating the controller.");

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
                MessageBox.Show("Failed to load SIOs: " + ex.Message);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm.Instance.LoadPage(new ControllersControl(), false);
        }

        // ===== Setup Grid =====
        private void SetupGrid()
        {
            dgvSIO.AutoGenerateColumns = false;
            dgvSIO.RowHeadersVisible = false;
            dgvSIO.AllowUserToAddRows = false;
            dgvSIO.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSIO.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            dgvSIO.ReadOnly = true;
            dgvSIO.AllowUserToDeleteRows = false;
            dgvSIO.EditMode = DataGridViewEditMode.EditProgrammatically;

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


            colHardwareId.HeaderText = "ID";
            colHardwareId.DataPropertyName = "Id";

            colSioNumber.DataPropertyName = "SioNumber";
            colType.DataPropertyName = "InterfaceType";
            colPort.DataPropertyName = "PortNumber";
            colAddress.DataPropertyName = "InterfacePanelAddress";
            colStatus.DataPropertyName = "ComStatus";

            // Online column
            colOnline.DataPropertyName = "IsOnline";

            dgvSIO.CellContentClick += dgvSIO_CellContentClick;
            dgvSIO.CellFormatting += dgvSIO_CellFormatting;
        }

        // ===== Load Controller Info =====
        private void LoadController()
        {
            txtName.Text = _controller.Name ?? "";
            txtIP.Text = _controller.IpAddress ?? "";
            txtMac.Text = _controller.MacAddress ?? "";
            txtId.Text = _controller.ScpId.ToString();
            //txtId.Text = _controller.ScpId?.ToString() ?? "";

            txtMac.ReadOnly = true;
            //txtType.ReadOnly = false;
            txtId.ReadOnly = true;
            txtIP.ReadOnly = false;   // ✅ editable
            txtName.ReadOnly = false;

            //txtType.Text = "";
        }

        // ===== Online Status Color =====
        private void dgvSIO_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSIO.Columns[e.ColumnIndex].Name == "colOnline")
            {
                if (e.Value != null)
                {
                    // ✅ Handle both bool and string safely
                    bool isOnline = false;

                    if (e.Value is bool)
                    {
                        isOnline = (bool)e.Value;
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
            SioControl sioPage = new SioControl(_controller);
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
                        MessageBox.Show("Delete failed: " + ex.Message);
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

                int controllerId = _controllerId;

                var dto = new AddUpdateControllerRequestDto
                {
                    Id = controllerId,
                    Name = txtName.Text,
                    MacAddress = txtMac.Text,
                    IpAddress = txtIP.Text,
                    Port = 5000,
                    TimeZoneId = 1,
                    LocationId = 1,
                    SubnetMask = "255.255.255.0",
                    DefaultGateway = "192.168.1.1",
                    InternalPort0IsEnabled = false,
                    InternalPort0BaudRate = 9600,
                    InternalPort0ProtocolType = 0,
                    Rs485Port1IsEnabled = true,
                    Rs485Port1BaudRate = 9600,
                    Rs485Port1ProtocolType = 0,
                    Rs485Port2IsEnabled = false,
                    Rs485Port2BaudRate = 0,
                    Rs485Port2ProtocolType = 0
                };

                if (controllerId == 0) // NEW (DISCOVERED)
                {
                    var addResult = await _api.AddControllerAsync(dto);

                    if (!addResult.Success)
                    {
                        MessageBox.Show("Add failed:\n" + addResult.Error);
                        return;
                    }
                }
                else // EXISTING → UPDATE
                {
                    var updateResult = await _api.UpdateControllerAsync(controllerId, dto);

                    if (!updateResult.Success)
                    {
                        MessageBox.Show("Update failed:\n" + updateResult.Error);
                        return;
                    }
                }

                MessageBox.Show("Saved successfully");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed:\n" + ex.Message);

            }
        }
        private bool ValidateForm()
        {
            // Name
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Controller Name is required");
                txtName.Focus();
                return false;
            }

            // MAC Address
            if (string.IsNullOrWhiteSpace(txtMac.Text))
            {
                MessageBox.Show("MAC Address is required");
                txtMac.Focus();
                return false;
            }

            // MAC format
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtMac.Text,
                @"^([0-9A-Fa-f]{2}:){5}([0-9A-Fa-f]{2})$"))
            {
                MessageBox.Show("Invalid MAC Address format (AA:BB:CC:DD:EE:FF)");
                txtMac.Focus();
                return false;
            }

            // IP Address
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
    }
}