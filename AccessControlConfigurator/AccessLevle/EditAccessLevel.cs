using AccessControlSystem;
using AccessControlSystem.Models;
using AccessControlSystem.Models.AccessLevelDto.AccessLevelDto;
using AccessControlSystem.Models.Acr;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class EditAccessLevelForm : Form
    {
        private readonly ApiService _api = new ApiService();

        private readonly int _accessLevelId;
        private readonly AccessLevelDto _level;

        private List<AcrDropdownDto> _availableAcrs = new List<AcrDropdownDto>();
        private List<TimezoneDto> _availableTimeZones = new List<TimezoneDto>();

        private const string NoAccessText = "No Access";

        public EditAccessLevelForm(AccessLevelDto level, List<AcrDropdownDto> acrs, List<TimezoneDto> zones)
        {
            InitializeComponent();

            _level = level ?? throw new ArgumentNullException(nameof(level));
            _accessLevelId = level.accessLevelId;

            _availableAcrs = acrs ?? new List<AcrDropdownDto>();
            _availableTimeZones = zones ?? new List<TimezoneDto>();

            Load += EditAccessLevelForm_Load;

            btnupdate.Click += btnupdate_ClickAsync;
            btnCancel.Click += btnCancel_Click;
            btnApplyToSelected.Click += btnApplyToSelected_Click;
            btnClearAll.Click += btnClearAll_Click;
            btnSearch.Click += btnSearch_Click;
            txtSearch.TextChanged += txtSearch_TextChanged;
            dgvMappings.CurrentCellDirtyStateChanged += dgvMappings_CurrentCellDirtyStateChanged;
            dgvMappings.CellValueChanged += dgvMappings_CellValueChanged;
        }

        private void EditAccessLevelForm_Load(object sender, EventArgs e)
        {
            try
            {
                SetLoadingState(true);

                txtName.Text = _level.name ?? string.Empty;

                LoadBulkTimeZoneDropdown();
                BuildGrid();
                LoadExistingMappings();
                UpdateSummary();
            }
            finally
            {
                SetLoadingState(false);
            }
        }

        private void SetLoadingState(bool isLoading)
        {
            UseWaitCursor = isLoading;
            btnupdate.Enabled = !isLoading;
            btnApplyToSelected.Enabled = !isLoading;
            btnClearAll.Enabled = !isLoading;
            btnSearch.Enabled = !isLoading;
        }

        private void LoadBulkTimeZoneDropdown()
        {
            var bulkSource = new List<TimeZoneOption>
            {
                new TimeZoneOption { id = 0, name = NoAccessText }
            };

            bulkSource.AddRange(_availableTimeZones.Select(z => new TimeZoneOption
            {
                id = z.id,
                name = z.name
            }));

            cmbBulkTimeZone.DataSource = null;
            cmbBulkTimeZone.DisplayMember = "name";
            cmbBulkTimeZone.ValueMember = "id";
            cmbBulkTimeZone.DataSource = bulkSource;
            cmbBulkTimeZone.SelectedIndex = 0;
        }

        private void BuildGrid()
        {
            dgvMappings.Rows.Clear();
            dgvMappings.Columns.Clear();

            dgvMappings.AutoGenerateColumns = false;
            dgvMappings.AllowUserToAddRows = false;
            dgvMappings.AllowUserToDeleteRows = false;
            dgvMappings.AllowUserToResizeRows = false;
            dgvMappings.MultiSelect = true;
            dgvMappings.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMappings.RowHeadersVisible = false;
            dgvMappings.BackgroundColor = Color.White;
            dgvMappings.BorderStyle = BorderStyle.None;
            dgvMappings.EnableHeadersVisualStyles = false;
            dgvMappings.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(22, 125, 211);
            dgvMappings.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMappings.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvMappings.ColumnHeadersHeight = 38;
            dgvMappings.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvMappings.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            dgvMappings.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 252);
            dgvMappings.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvMappings.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);
            dgvMappings.RowTemplate.Height = 34;
            dgvMappings.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            var colDoorName = new DataGridViewTextBoxColumn
            {
                Name = "colDoorName",
                HeaderText = "Door Name",
                FillWeight = 45,
                ReadOnly = true
            };

            var colDoorId = new DataGridViewTextBoxColumn
            {
                Name = "colDoorId",
                HeaderText = "Door ID",
                Visible = false,
                ReadOnly = true
            };

            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "colStatus",
                HeaderText = "Status",
                FillWeight = 18,
                ReadOnly = true
            };

            var colTimeZone = new DataGridViewComboBoxColumn
            {
                Name = "colTimeZone",
                HeaderText = "Time Zone",
                FillWeight = 37,
                DisplayMember = "name",
                ValueMember = "id",
                FlatStyle = FlatStyle.Flat,
                DataSource = GetTimeZoneOptions()
            };

            dgvMappings.Columns.Add(colDoorName);
            dgvMappings.Columns.Add(colDoorId);
            dgvMappings.Columns.Add(colStatus);
            dgvMappings.Columns.Add(colTimeZone);

            foreach (var acr in _availableAcrs.OrderBy(x => x.name))
            {
                int rowIndex = dgvMappings.Rows.Add(
                    acr.name ?? $"Door {acr.id}",
                    acr.id,
                    NoAccessText,
                    0
                );

                dgvMappings.Rows[rowIndex].Tag = acr;
            }
        }

        private List<TimeZoneOption> GetTimeZoneOptions()
        {
            var options = new List<TimeZoneOption>
            {
                new TimeZoneOption { id = 0, name = NoAccessText }
            };

            options.AddRange(_availableTimeZones.Select(z => new TimeZoneOption
            {
                id = z.id,
                name = z.name
            }));

            return options;
        }

        private void LoadExistingMappings()
        {
            if (_level.acrs == null || _level.acrs.Count == 0)
                return;

            foreach (DataGridViewRow row in dgvMappings.Rows)
            {
                int acrId = 0;
                if (row.Cells["colDoorId"].Value != null)
                    int.TryParse(row.Cells["colDoorId"].Value.ToString(), out acrId);

                var existing = _level.acrs.FirstOrDefault(x => x.acrId == acrId);
                if (existing != null)
                {
                    int tzId = existing.timeZoneId ?? 0;
                    row.Cells["colTimeZone"].Value = tzId;
                    UpdateRowStatus(row);
                }
            }
        }

        private void dgvMappings_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvMappings.IsCurrentCellDirty)
                dgvMappings.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void dgvMappings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvMappings.Columns[e.ColumnIndex].Name == "colTimeZone")
            {
                UpdateRowStatus(dgvMappings.Rows[e.RowIndex]);
                UpdateSummary();
            }
        }

        private void UpdateRowStatus(DataGridViewRow row)
        {
            int timeZoneId = 0;

            if (row.Cells["colTimeZone"].Value != null)
                int.TryParse(row.Cells["colTimeZone"].Value.ToString(), out timeZoneId);

            row.Cells["colStatus"].Value = timeZoneId > 0 ? "Assigned" : NoAccessText;
        }

        private void UpdateSummary()
        {
            int total = dgvMappings.Rows.Count;
            int assigned = 0;

            foreach (DataGridViewRow row in dgvMappings.Rows)
            {
                int timeZoneId = 0;
                if (row.Cells["colTimeZone"].Value != null)
                    int.TryParse(row.Cells["colTimeZone"].Value.ToString(), out timeZoneId);

                if (timeZoneId > 0)
                    assigned++;
            }

            lblTotalDoorsValue.Text = total.ToString();
            lblAssignedValue.Text = assigned.ToString();
            lblUnassignedValue.Text = (total - assigned).ToString();
        }

        private void btnApplyToSelected_Click(object sender, EventArgs e)
        {
            if (cmbBulkTimeZone.SelectedValue == null)
            {
                MessageBox.Show("Please select a time zone to apply.");
                return;
            }

            if (dgvMappings.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select one or more doors.");
                return;
            }

            int selectedTimeZoneId = Convert.ToInt32(cmbBulkTimeZone.SelectedValue);

            foreach (DataGridViewRow row in dgvMappings.SelectedRows)
            {
                row.Cells["colTimeZone"].Value = selectedTimeZoneId;
                UpdateRowStatus(row);
            }

            UpdateSummary();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvMappings.Rows)
            {
                row.Cells["colTimeZone"].Value = 0;
                UpdateRowStatus(row);
            }

            UpdateSummary();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyDoorFilter();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyDoorFilter();
        }

        private void ApplyDoorFilter()
        {
            string keyword = txtSearch.Text.Trim().ToLowerInvariant();

            foreach (DataGridViewRow row in dgvMappings.Rows)
            {
                string doorName = row.Cells["colDoorName"].Value?.ToString()?.ToLowerInvariant() ?? string.Empty;
                row.Visible = string.IsNullOrWhiteSpace(keyword) || doorName.Contains(keyword);
            }
        }

        private List<AcrTimeZoneDto> CollectMappings()
        {
            var mappings = new List<AcrTimeZoneDto>();

            foreach (DataGridViewRow row in dgvMappings.Rows)
            {
                int acrId = 0;
                int timeZoneId = 0;

                if (row.Cells["colDoorId"].Value != null)
                    int.TryParse(row.Cells["colDoorId"].Value.ToString(), out acrId);

                if (row.Cells["colTimeZone"].Value != null)
                    int.TryParse(row.Cells["colTimeZone"].Value.ToString(), out timeZoneId);

                if (acrId > 0 && timeZoneId > 0)
                {
                    mappings.Add(new AcrTimeZoneDto
                    {
                        acrId = acrId,
                        timeZoneId = timeZoneId
                    });
                }
            }

            return mappings;
        }

        private async void btnupdate_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                string accessLevelName = txtName.Text.Trim();

                if (string.IsNullOrWhiteSpace(accessLevelName))
                {
                    MessageBox.Show("Access Level Name is required.");
                    txtName.Focus();
                    return;
                }

                var mappings = CollectMappings();

                if (mappings.Count == 0)
                {
                    MessageBox.Show("Please assign at least one door to a time zone.");
                    return;
                }

                btnupdate.Enabled = false;

                var dto = new AccessLevelCreateDto
                {
                    name = accessLevelName,
                    acrs = mappings
                };

                await _api.UpdateAccessLevel(_accessLevelId, dto);

                MessageBox.Show("Access Level Updated Successfully.");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating access level: " + ex.Message);
            }
            finally
            {
                btnupdate.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private class TimeZoneOption
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }
}
