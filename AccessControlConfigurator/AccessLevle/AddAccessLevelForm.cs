using AccessControlSystem.Models;

using AccessControlSystem.Models.AccessLevelDto.AccessLevelDto;

using AccessControlConfigurator.Helpers;

using AccessControlSystem.Models.Acr;

using AccessControlSystem.Services;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using System.Windows.Forms;

namespace AccessControlConfigurator

{

    public partial class AddAccessLevelForm : Form

    {

        private readonly ApiService _api = new ApiService();

        private const int NoAccessTimeZoneId = 2;

        private List<AcrDropdownDto> _acrs = new List<AcrDropdownDto>();

        private List<TimezoneDto> _zones = new List<TimezoneDto>();

        public AddAccessLevelForm()

        {

            InitializeComponent();

            InitializeDoorGrid();

            LoadDropdowns();

            Resize += (s, e) => AlignLayout();

            Load += (s, e) => AlignLayout();

        }

        // Load both dropdowns

        private async void LoadDropdowns()

        {

            await Task.WhenAll(

                LoadAcrs(),

                LoadTimeZones()

            );

            PopulateDoorGrid();

        }

        // Load Doors (ACR)

        private async Task LoadAcrs()

        {

            try

            {

                _acrs = await _api.GetAllAcrDropdown();

            }

            catch (Exception ex)

            {

                MessageBox.Show("Failed to load doors: " + ex.Message);

            }

        }

        // Load TimeZones

        private async Task LoadTimeZones()

        {

            try

            {

                _zones = await _api.GetTimezonesDropdown();

            }

            catch (Exception ex)

            {

                MessageBox.Show("Failed to load time zones: " + ex.Message);

            }

        }

        private void InitializeDoorGrid()

        {

            Helpers.GridStyleHelper.ApplyStandardStyle(

                dgvDoorTimezones,

                fillColumns: false,

                allowColumnResize: false,

                allowColumnOrder: false);

            dgvDoorTimezones.ReadOnly = false;

            dgvDoorTimezones.AutoGenerateColumns = false;

            dgvDoorTimezones.AllowUserToAddRows = false;

            dgvDoorTimezones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDoorTimezones.Columns.Clear();

            // ✅ Enable column fill so no blank space appears on the right

            dgvDoorTimezones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var colDoorId = new DataGridViewTextBoxColumn

            {

                Name = "colDoorId",

                HeaderText = "Door ID",

                ReadOnly = true,

                Visible = false

            };

            var colDoorName = new DataGridViewTextBoxColumn

            {

                Name = "colDoorName",

                HeaderText = "Door Name",

                ReadOnly = true,

                FillWeight = 50   // ✅ 50% of grid width

            };

            var colTimeZone = new DataGridViewComboBoxColumn

            {

                Name = "colTimeZone",

                HeaderText = "Time Zone",

                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,

                FillWeight = 50   // ✅ 50% of grid width

            };

            dgvDoorTimezones.Columns.AddRange(colDoorId, colDoorName, colTimeZone);

        }

        private void PopulateDoorGrid()

        {

            if (_acrs == null || _acrs.Count == 0)

                return;

            EnsureNoAccessTimeZone();

            var tzColumn = dgvDoorTimezones.Columns["colTimeZone"] as DataGridViewComboBoxColumn;

            if (tzColumn != null)

            {

                tzColumn.DataSource = _zones;

                tzColumn.DisplayMember = "name";

                tzColumn.ValueMember = "id";

            }

            dgvDoorTimezones.Rows.Clear();

            int defaultTimeZoneId = GetDefaultTimeZoneId();

            foreach (var acr in _acrs)

            {

                int timeZoneId = defaultTimeZoneId;

                if (_zones != null && !_zones.Any(z => z.id == timeZoneId))

                    timeZoneId = defaultTimeZoneId;

                int rowIndex = dgvDoorTimezones.Rows.Add(

                    acr.id,

                    acr.name ?? string.Empty,

                    timeZoneId);

                dgvDoorTimezones.Rows[rowIndex].Tag = acr;

            }

        }

        private int GetDefaultTimeZoneId()

        {

            if (_zones != null && _zones.Any(z => z.id == NoAccessTimeZoneId))

                return NoAccessTimeZoneId;

            return _zones != null && _zones.Count > 0

                ? _zones[0].id

                : NoAccessTimeZoneId;

        }

        private void EnsureNoAccessTimeZone()

        {

            if (_zones == null)

                _zones = new List<TimezoneDto>();

            if (_zones.Any(z => z.id == NoAccessTimeZoneId))

                return;

            _zones.Add(new TimezoneDto

            {

                id = NoAccessTimeZoneId,

                name = "No Access"

            });

        }

        private void AlignLayout()

        {

            int padding = UIStyleHelper.StandardSizes.Padding;

            int verticalSpacing = UIStyleHelper.StandardSizes.VerticalSpacing;

            int inputWidth = Math.Min(360, ClientSize.Width - (padding * 2) - 40);

            lblName.Location = new Point(padding + 20, 80);

            lblName.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.LabelHeight);

            txtName.Location = new Point(padding + 20, lblName.Bottom + 5);

            txtName.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.InputFieldHeight);

            lblDoors.Location = new Point(padding + 20, txtName.Bottom + verticalSpacing);

            // ✅ Label width now also stretches full grid width

            lblDoors.Size = new Size(ClientSize.Width - (padding * 2) - 40, UIStyleHelper.StandardSizes.LabelHeight);

            int gridTop = lblDoors.Bottom + 5;

            // ✅ Grid stretches full form width minus padding

            int gridWidth = ClientSize.Width - (padding * 2) - 40;

            int bottomPadding = verticalSpacing + UIStyleHelper.StandardSizes.ButtonHeight + 20;

            int gridHeight = Math.Max(180, ClientSize.Height - gridTop - bottomPadding);

            dgvDoorTimezones.Location = new Point(padding + 20, gridTop);

            dgvDoorTimezones.Size = new Size(gridWidth, gridHeight);

            int buttonY = dgvDoorTimezones.Bottom + verticalSpacing;

            int buttonSpacing = 10;

            int totalButtonWidth = UIStyleHelper.StandardSizes.ButtonWidth +

                UIStyleHelper.StandardSizes.SmallButtonWidth + buttonSpacing;

            int buttonStartX = (ClientSize.Width - totalButtonWidth) / 2;

            btnSave.Location = new Point(buttonStartX, buttonY);

            btnCancel.Location = new Point(btnSave.Right + buttonSpacing, buttonY);

        }

        // Save Access Level

        private async void btnSave_Click(object sender, EventArgs e)

        {

            try

            {

                if (string.IsNullOrWhiteSpace(txtName.Text))

                {

                    MessageBox.Show("Access Level Name is required");

                    return;

                }

                if (_acrs == null || _acrs.Count == 0)

                {

                    MessageBox.Show("No doors available to save.");

                    return;

                }

                var dto = new AccessLevelCreateDto

                {

                    name = txtName.Text.Trim(),

                    //description = txtDescription.Text.Trim(),

                    acrs = new List<AcrTimeZoneDto>()

                };

                int defaultTimeZoneId = GetDefaultTimeZoneId();

                foreach (DataGridViewRow row in dgvDoorTimezones.Rows)

                {

                    if (row.IsNewRow)

                        continue;

                    int doorId = Convert.ToInt32(row.Cells["colDoorId"].Value);

                    string doorName = row.Cells["colDoorName"].Value?.ToString() ?? string.Empty;

                    int timeZoneId = row.Cells["colTimeZone"].Value != null

                        ? Convert.ToInt32(row.Cells["colTimeZone"].Value)

                        : defaultTimeZoneId;

                    dto.acrs.Add(new AcrTimeZoneDto

                    {

                        acrId = doorId,

                        acrName = doorName,

                        timeZoneId = timeZoneId

                    });

                }

                await _api.AddAccessLevel(dto);

                MessageBox.Show("Access Level Created Successfully");

                this.DialogResult = DialogResult.OK;

                this.Close();

            }

            catch (Exception ex)

            {

                MessageBox.Show("Error creating access level: " + ex.Message);

            }

        }

        // Cancel button

        private void btnCancel_Click(object sender, EventArgs e)

        {

            this.Close();

        }

    }

}

