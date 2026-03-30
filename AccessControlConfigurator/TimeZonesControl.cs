using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class TimeZonesControl : UserControl
    {
        private readonly ApiService _apiService = new ApiService();
        private TextBox txtName;
        private TextBox txtNumber;
        private TextBox txtMode;
        private List<TimezoneDto> timeZoneData;
        private System.Windows.Forms.Timer searchTimer;
        public TimeZonesControl()
        {
            InitializeComponent();

            this.Load += TimeZonesControl_Load;
            dgvTimeZones.CellFormatting += dgvTimeZones_CellFormatting;
            dgvTimeZones.ReadOnly = true;
            dgvTimeZones.EditMode = DataGridViewEditMode.EditProgrammatically;
            //StyleButton(btnAdd);
            //StyleButton(btnEdit);
            //StyleButton(btnDelete);
            //StyleButton(btnSync);
            //StyleButton(btnApply);
            //StyleButton(btnQuery);
            //StyleButton(btnRefresh);
            //StyleButton(btnback);
              // Button style
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;

            btnSearch.Click += (s, e) => ApplySearch();

            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearFilters.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbNameFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNameFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbNameFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNameFilter.SelectedIndexChanged += (s, e) => ApplySearch();
            Resize += (s, e) =>
            {
                AlignHeaderControls();
                AlignFilterControls();
            };

            AlignHeaderControls();
            AlignFilterControls();
        }
        private void FixHeaderStyle()
        {
            Helpers.GridStyleHelper.ApplyStandardStyle(dgvTimeZones);
        }

        private async void TimeZonesControl_Load(object sender, EventArgs e)
        {
            await LoadTimezones();
            FixHeaderStyle();
        }

        private async Task LoadTimezones()
        {
            try
            {
                //var data = await _apiService.GetTimezones();
                //dgvTimeZones.DataSource = data;
                timeZoneData = await _apiService.GetTimezones();
                dgvTimeZones.DataSource = null;
                dgvTimeZones.DataSource = timeZoneData;

                FormatGridHeaders();
                LoadNameFilter();
                AlignHeaderControls();
                AlignFilterControls();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadTimezones();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            AddTimezoneForm form = new AddTimezoneForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadTimezones();
            }
        }


        private async void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTimeZones.CurrentRow == null)
                {
                    MessageBox.Show("Please select a timezone to delete.");
                    return;
                }

                var timezone = (TimezoneDto)dgvTimeZones.CurrentRow.DataBoundItem;

                DialogResult result = MessageBox.Show(
                    "Are you sure you want to delete this Timezone?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    await _apiService.DeleteTimezone(timezone.id);

                    MessageBox.Show(" Deleted successfully");

                    await LoadTimezones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btnSync_Click(object sender, EventArgs e)
        {
            await _apiService.SyncTimezonesToHID();
            MessageBox.Show("Synced successfully");
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvTimeZones.CurrentRow == null)
                {
                    MessageBox.Show("Please select a Timezone to edit.");
                    return;
                }

                // Get selected row data
                var timezone = (TimezoneDto)dgvTimeZones.CurrentRow.DataBoundItem;

                // Open edit popup
                EditTimezoneForm form = new EditTimezoneForm(timezone);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    await LoadTimezones();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private async void btnApplyHoliday_Click(object sender, EventArgs e)
        //{
        //    ApplyHolidayForm form = new ApplyHolidayForm();

        //    if (form.ShowDialog() == DialogResult.OK)
        //    {
        //        await LoadTimezones();
        //    }
        //}

        //private void btnQuery_Click(object sender, EventArgs e)
        //{
        //    QueryHolidayForm form = new QueryHolidayForm();
        //    form.ShowDialog();
        //}




        private void btnback_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();


        }
        //private void StyleButton(Button btn)
        //{
        //    btn.FlatStyle = FlatStyle.Flat;
        //    btn.FlatAppearance.BorderSize = 1;

        //    btn.BackColor = Color.White;
        //    btn.ForeColor = Color.Black;

        //    btn.Height = 30;
        //    btn.Width = 90;

        //    btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        //}

        private void FormatGridHeaders()
        {
            var map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["id"] = "Timezone ID",
                ["name"] = "Name",
                ["number"] = "Number",
                ["mode"] = "Mode",
                ["acttime"] = "Start Time",
                ["deacttime"] = "End Time",
                ["intervals"] = "Intervals",
                ["idays"] = "Break Days",
                ["istart"] = "Break Start",
                ["iend"] = "Break End"
            };

            foreach (DataGridViewColumn col in dgvTimeZones.Columns)
            {
                string key = col.DataPropertyName?.ToLower() ?? col.Name.ToLower();

                if (map.ContainsKey(key))
                {
                    col.HeaderText = map[key];
                }

                // ❌ REMOVE unwanted duplicate columns
                if (string.Equals(key, "code", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "encscptimezoneex", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "timezoneid", StringComparison.OrdinalIgnoreCase))
                {
                    col.Visible = false;
                }
            }

            SetColumnDisplayOrder();
            ConfigureGridColumnWidths();
            ConfigureGridAlignment();

            dgvTimeZones.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dgvTimeZones.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvTimeZones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvTimeZones.ColumnHeadersHeight = 40;
        }

        private void SetColumnDisplayOrder()
        {
            string[] orderedColumns =
            {
                "id",
                "name",
                "number",
                "mode",
                "acttime",
                "deacttime",
                "intervals",
                "idays",
                "istart",
                "iend"
            };

            for (int i = 0; i < orderedColumns.Length; i++)
            {
                var columnName = orderedColumns[i];
                if (dgvTimeZones.Columns.Contains(columnName))
                {
                    dgvTimeZones.Columns[columnName].DisplayIndex = i;
                }
            }
        }

        private void ConfigureGridColumnWidths()
        {
            dgvTimeZones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in dgvTimeZones.Columns)
            {
                string key = col.DataPropertyName?.ToLower() ?? col.Name.ToLower();

                col.MinimumWidth = 75;
                col.FillWeight = 70;

                if (key == "name")
                {
                    col.MinimumWidth = 220;
                    col.FillWeight = 190;
                }
                else if (key == "idays")
                {
                    col.MinimumWidth = 135;
                    col.FillWeight = 110;
                }
                else if (key == "id" || key == "code" || key == "number" || key == "mode" || key == "intervals")
                {
                    col.MinimumWidth = key == "id" ? 110 : 85;
                    col.FillWeight = 65;
                }
                else if (key == "encscptimezoneex")
                {
                    col.MinimumWidth = 120;
                    col.FillWeight = 90;
                }
                else if (key == "acttime" || key == "deacttime" || key == "istart" || key == "iend")
                {
                    col.MinimumWidth = 110;
                    col.FillWeight = 100;
                }
            }
        }

        private void ConfigureGridAlignment()
        {
            foreach (DataGridViewColumn col in dgvTimeZones.Columns)
            {
                string key = col.DataPropertyName?.ToLower() ?? col.Name.ToLower();

                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.HeaderCell.Style.WrapMode = DataGridViewTriState.False;

                if (key == "name")
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
                else
                {
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }

        private void dgvTimeZones_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }

            var column = dgvTimeZones.Columns[e.ColumnIndex];
            string key = column.DataPropertyName?.ToLower() ?? column.Name.ToLower();

            if ((key == "acttime" || key == "deacttime") &&
                e.Value != null &&
                int.TryParse(e.Value.ToString(), out var seconds))
            {
                e.Value = Helpers.UIStyleHelper.FormatTimeFromSeconds(seconds);
                e.FormattingApplied = true;
            }
        }

        private void ApplySearch()
        {
            string searchText = txtSearch.Text?.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                ApplyNameFilter();
                return;
            }

            var filtered = ApplyNameFilterInternal(timeZoneData).Where(t =>
            {
                return
                    t.id.ToString().Contains(searchText) ||
                    (t.name ?? "").ToLower().Contains(searchText) ||
                    t.number.ToString().Contains(searchText) ||
                    t.mode.ToString().Contains(searchText) ||
                    t.actTime.ToString().Contains(searchText) ||
                    t.deactTime.ToString().Contains(searchText) ||
                    t.intervals.ToString().Contains(searchText) ||
                    t.iDays.ToString().Contains(searchText) ||
                    t.iStart.ToString().Contains(searchText) ||
                    t.iEnd.ToString().Contains(searchText);
            }).ToList();

            // ✅ No popup (professional UX)
            dgvTimeZones.DataSource = null;
            dgvTimeZones.DataSource = filtered.ToList();
            FormatGridHeaders();
        }
        private void LoadNameFilter()
        {
            if (timeZoneData == null || timeZoneData.Count == 0)
                return;

            var names = timeZoneData
                .Select(t => t.name ?? string.Empty)
                .Where(name => !string.IsNullOrWhiteSpace(name))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(name => name)
                .ToList();

            names.Insert(0, "All");

            cmbNameFilter.DataSource = null;
            cmbNameFilter.DataSource = names;
            cmbNameFilter.SelectedIndex = 0;
        }
        private void ApplyNameFilter()
        {
            var filtered = ApplyNameFilterInternal(timeZoneData).ToList();
            dgvTimeZones.DataSource = null;
            dgvTimeZones.DataSource = filtered;
            FormatGridHeaders();
        }
        private IEnumerable<TimezoneDto> ApplyNameFilterInternal(IEnumerable<TimezoneDto> source)
        {
            string selected = cmbNameFilter.SelectedItem?.ToString()?.Trim();
            if (string.IsNullOrWhiteSpace(selected) ||
                string.Equals(selected, "All", StringComparison.OrdinalIgnoreCase))
            {
                return source ?? Enumerable.Empty<TimezoneDto>();
            }

            return (source ?? Enumerable.Empty<TimezoneDto>())
                .Where(t => string.Equals((t.name ?? string.Empty).Trim(), selected, StringComparison.OrdinalIgnoreCase));
        }
        //private void TxtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    ApplySearch();
        //}
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            if (cmbNameFilter.Items.Count > 0)
                cmbNameFilter.SelectedIndex = 0;

            ApplySearch();
        }

        private void AlignHeaderControls()
        {
            int rightPadding = 12;
            int spacing = 8;
            int searchGroupWidth =
                btnClearFilters.Width + 6 +
                btnSearch.Width + 6 +
                txtSearch.Width + 8 +
                lblSearchRight.Width;

            int availableRight = panelHeader.ClientSize.Width - rightPadding;
            int leftEdge = lblTitle.Right + spacing;
            bool wrapSearch = (leftEdge + searchGroupWidth) > availableRight;
            int searchTop = wrapSearch ? (lblTitle.Bottom + 6) : 8;

            btnClearFilters.Location = new Point(availableRight - btnClearFilters.Width, searchTop);
            btnSearch.Location = new Point(btnClearFilters.Left - btnSearch.Width - 6, searchTop + 1);
            txtSearch.Location = new Point(btnSearch.Left - txtSearch.Width - 6, searchTop + 2);
            lblSearchRight.Location = new Point(txtSearch.Left - lblSearchRight.Width - 8, searchTop + 5);

            panelHeader.Height = wrapSearch ? (searchTop + btnSearch.Height + 8) : 45;
        }

        private void AlignFilterControls()
        {
            int spacing = 8;
            int rightPadding = 12;
            int top = 5;

            btnAdd.Location = new Point(10, top);
            btnEdit.Location = new Point(btnAdd.Right + spacing, top);
            btnDelete.Location = new Point(btnEdit.Right + spacing, top);
            btnSync.Location = new Point(btnDelete.Right + spacing, top);
            btnRefresh.Location = new Point(btnSync.Right + spacing, top);
            btnback.Location = new Point(btnRefresh.Right + spacing, top);

            int filterGroupWidth = cmbNameFilter.Width + 8 + lblNameFilter.Width;
            int availableRight = panelFilter.ClientSize.Width - rightPadding;
            bool wrapFilter = (btnback.Right + spacing + filterGroupWidth) > availableRight;
            int filterTop = wrapFilter ? (btnAdd.Bottom + 6) : 2;

            cmbNameFilter.Location = new Point(availableRight - cmbNameFilter.Width, filterTop);
            lblNameFilter.Location = new Point(cmbNameFilter.Left - lblNameFilter.Width - 8, filterTop + 3);

            panelFilter.Height = wrapFilter ? (filterTop + cmbNameFilter.Height + 6) : 32;
        }
    }
}
