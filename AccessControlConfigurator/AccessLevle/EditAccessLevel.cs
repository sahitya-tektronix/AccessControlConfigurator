using AccessControlSystem;

using AccessControlSystem.Models;

using AccessControlSystem.Models.AccessLevelDto.AccessLevelDto;

using AccessControlConfigurator.Helpers;

using AccessControlSystem.Models.Acr;

using AccessControlSystem.Services;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Windows.Forms;

namespace AccessControlConfigurator

{

    public partial class EditAccessLevelForm : Form

    {

        private readonly ApiService _api = new ApiService();

        private const int NoAccessTimeZoneId = 2;

        private int accessLevelId = 0;

        private AccessLevelDto _level;

        private List<AcrDropdownDto> _acrs = new List<AcrDropdownDto>();

        private List<TimezoneDto> _zones = new List<TimezoneDto>();

        public EditAccessLevelForm(AccessLevelDto level, List<AcrDropdownDto> acrs, List<TimezoneDto> zones)

        {

            InitializeComponent();

            _level = level;

            accessLevelId = level.accessLevelId;

            txtName.Text = level.name;

            //txtDescription.Text = level.description;

            InitializeDoorGrid();

            LoadDropdowns(_level, acrs, zones);

            Resize += (s, e) => AlignLayout();

            Load += (s, e) => AlignLayout();

        }

        // Load dropdown data

        private void LoadDropdowns(AccessLevelDto level, List<AcrDropdownDto> acrs, List<TimezoneDto> zones)

        {

            try

            {

                _acrs = acrs ?? new List<AcrDropdownDto>();

                _zones = zones ?? new List<TimezoneDto>();

                PopulateDoorGrid();

            }

            catch (Exception ex)

            {

                MessageBox.Show($"Error: {ex.Message}");

            }

        }

        private void InitializeDoorGrid()

        {

            AccessControlConfigurator.Helpers.GridStyleHelper.ApplyStandardStyle(

                dgvDoorTimezones,

                fillColumns: false,

                allowColumnResize: false,

                allowColumnOrder: false);

            dgvDoorTimezones.ReadOnly = false;

            dgvDoorTimezones.AutoGenerateColumns = false;

            dgvDoorTimezones.AllowUserToAddRows = false;

            dgvDoorTimezones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvDoorTimezones.Columns.Clear();

            var colDoorId = new DataGridViewTextBoxColumn

            {

                Name = "colDoorId",

                HeaderText = "Door ID",

                ReadOnly = true,

                Width = 80,

                Visible = false

            };

            var colDoorName = new DataGridViewTextBoxColumn

            {

                Name = "colDoorName",

                HeaderText = "Door Name",

                ReadOnly = true,

                Width = 220

            };

            var colTimeZone = new DataGridViewComboBoxColumn

            {

                Name = "colTimeZone",

                HeaderText = "Time Zone",

                Width = 220,

                DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox

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

                var existing = _level?.acrs?.FirstOrDefault(a => a.acrId == acr.id);

                int timeZoneId = existing?.timeZoneId ?? defaultTimeZoneId;

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

            int inputWidth = Math.Min(300, ClientSize.Width - (padding * 2) - 40);

            lblName.Location = new Point(padding + 20, 80);

            lblName.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.LabelHeight);

            txtName.Location = new Point(padding + 20, lblName.Bottom + 5);

            txtName.Size = new Size(inputWidth, UIStyleHelper.StandardSizes.InputFieldHeight);

            lblDoors.Location = new Point(padding + 20, txtName.Bottom + verticalSpacing);

            lblDoors.Size = new Size(inputWidth + 140, UIStyleHelper.StandardSizes.LabelHeight);

            int gridTop = lblDoors.Bottom + 5;

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

            btnupdate.Location = new Point(buttonStartX, buttonY);

            btnCancel.Location = new Point(btnupdate.Right + buttonSpacing, buttonY);

        }


        // Cancel button

        private void btnCancel_Click(object sender, EventArgs e)

        {

            this.Close();

        }


        private async void btnupdate_ClickAsync(object sender, EventArgs e)

        {

            try

            {

                if (string.IsNullOrWhiteSpace(txtName.Text))

                {

                    MessageBox.Show("Access Level Name is required.");

                    return;

                }

                var dto = new AccessLevelCreateDto

                {

                    name = txtName.Text.Trim(),

                    // description = txtDescription.Text.Trim(),

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

                await _api.UpdateAccessLevel(accessLevelId, dto);

                MessageBox.Show("Access Level Updated Successfully");

                this.DialogResult = DialogResult.OK;

                this.Close();

            }

            catch (Exception ex)

            {

                MessageBox.Show(AccessLevelErrorHelper.GetMessage(ex));

            }

        }

    }

}


