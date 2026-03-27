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

            //txtSearch.KeyDown += (s, e) =>
            //{
            //    if (e.KeyCode == Keys.Enter)
            //        ApplySearch();
            //};

            // ✅ Timer setup
            //searchTimer = new System.Windows.Forms.Timer();
            //searchTimer.Interval = 300;

            //searchTimer.Tick += (s, e) =>
            //{
            //    searchTimer.Stop();
            //    ApplySearch();
            //};

            // ✅ Real-time search
            //txtSearch.TextChanged += (s, e) =>
            //{
            //    searchTimer.Stop();
            //    searchTimer.Start();
            //};
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbNameFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblNameFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbNameFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbNameFilter.SelectedIndexChanged += (s, e) => ApplySearch();
        }

        private async void TimeZonesControl_Load(object sender, EventArgs e)
        {
            await LoadTimezones();
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
                ["code"] = "Code",
                ["name"] = "Name",
                ["encScpTimezoneEx"] = "Encoded SCP TZ",
                ["number"] = "Number",
                ["mode"] = "Mode",
                ["actTime"] = "Activation Time",
                ["deactTime"] = "Deactivation Time",
                ["intervals"] = "Intervals",
                ["iDays"] = "Interval Days",
                ["iStart"] = "Interval Start",
                ["iEnd"] = "Interval End",
                ["timeZoneId"] = "ID"
            };

            foreach (DataGridViewColumn col in dgvTimeZones.Columns)
            {
                var key = col.DataPropertyName ?? col.Name;
                if (map.TryGetValue(key, out var header))
                    col.HeaderText = header;
            }

            dgvTimeZones.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);
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
                    t.code.ToString().Contains(searchText) ||
                    (t.name ?? "").ToLower().Contains(searchText) ||
                    t.encScpTimezoneEx.ToString().Contains(searchText) ||
                    t.number.ToString().Contains(searchText) ||
                    t.mode.ToString().Contains(searchText) ||
                    t.actTime.ToString().Contains(searchText) ||
                    t.deactTime.ToString().Contains(searchText) ||
                    t.intervals.ToString().Contains(searchText) ||
                    t.iDays.ToString().Contains(searchText) ||
                    t.iStart.ToString().Contains(searchText) ||
                    t.iEnd.ToString().Contains(searchText) ||
                    t.timeZoneId.ToString().Contains(searchText);
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
    }
}