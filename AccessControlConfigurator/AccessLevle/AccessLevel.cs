using AccessControlConfigurator.Forms;

using AccessControlSystem;

using AccessControlSystem.Models;

using AccessControlSystem.Models.AccessLevelDto;

using AccessControlSystem.Models.Acr;

using AccessControlSystem.Services;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using System.Windows.Forms;


namespace AccessControlConfigurator

{

    public partial class AccessLevel : UserControl

    {

        private readonly ApiService _api = new ApiService();

        private List<AccessLevelDto> _levels = new();

        private List<AcrDropdownDto> acrs = new List<AcrDropdownDto>();

        private List<TimezoneDto> zones = new List<TimezoneDto>();

        public AccessLevel()

        {

            InitializeComponent();

            _ = LoadAccessLevels();

            btnSearch.Click += btnSearch_Click;

            //txtSearch.TextChanged += txtSearch_TextChanged;

            // Button style

            btnSearch.FlatStyle = FlatStyle.Flat;

            btnSearch.BackColor = Color.FromArgb(0, 120, 215);

            btnSearch.ForeColor = Color.White;

            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbNameFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblNameFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearFilters.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbNameFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbNameFilter.SelectedIndexChanged += (s, e) => ApplyFilter();
            btnClearFilters.Click += btnClearFilters_Click;
            Resize += (s, e) =>
            {
                AlignHeaderControls();
                AlignFilterControls();
            };

            AlignHeaderControls();
            AlignFilterControls();

        }

        protected override async void OnLoad(EventArgs e)

        {

            base.OnLoad(e);

            await LoadDropdowns();

        }

        private async Task LoadDropdowns()

        {

            acrs = await _api.GetAllAcrDropdown();

            zones = await _api.GetTimezonesDropdown();

        }

        // ================= LOAD =================

        private async Task LoadAccessLevels()

        {

            try

            {

                _levels = await _api.GetAccessLevels();

                var gridData = _levels.Select(l => new

                {

                    l.accessLevelId,

                    l.name,

                    //l.description,

                    AcrCount = l.acrs != null

                        ? string.Join(", ",

                            l.acrs

                                .Where(a => a.acrId != null)

                                .Select(a => a.acrId.ToString()))

                        : "",


                    TimeZones = l.acrs != null

                        ? string.Join(", ",

                            l.acrs

                                .Where(a => a.timeZoneId != null)

                                .Select(a => a.timeZoneId.ToString()))

                        : ""

                }).ToList();

                dgvAccessLevels.DataSource = null;

                dgvAccessLevels.DataSource = gridData;

                // 🔥 SAFE COLUMN SETTING

                if (dgvAccessLevels.Columns.Contains("accessLevelId"))

                    dgvAccessLevels.Columns["accessLevelId"].HeaderText = "ID";

                if (dgvAccessLevels.Columns.Contains("name"))

                    dgvAccessLevels.Columns["name"].HeaderText = "Access Level Name";

                if (dgvAccessLevels.Columns.Contains("AcrCount"))

                    dgvAccessLevels.Columns["AcrCount"].HeaderText = "ACRs";

                if (dgvAccessLevels.Columns.Contains("TimeZones"))

                    dgvAccessLevels.Columns["TimeZones"].HeaderText = "TimeZones";

                LoadNameFilter();
                AlignHeaderControls();
                AlignFilterControls();

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }

        // ================= REFRESH =================

        private async void btnRefresh_Click(object sender, EventArgs e)

        {

            await LoadAccessLevels();

        }

        // ================= ADD =================

        private async void btnAdd_Click(object sender, EventArgs e)

        {

            var form = new AddAccessLevelForm();

            if (form.ShowDialog() == DialogResult.OK)

            {

                await LoadAccessLevels();

            }

        }

        // ================= EDIT =================

        private async void btnEdit_Click(object sender, EventArgs e)

        {

            if (dgvAccessLevels.CurrentRow == null)

            {

                MessageBox.Show("Select Access Level first");

                return;

            }

            int id = (int)dgvAccessLevels.CurrentRow.Cells["accessLevelId"].Value;

            var level = _levels.FirstOrDefault(x => x.accessLevelId == id);

            if (level == null)

            {

                MessageBox.Show("Data not found");

                return;

            }


            var form = new EditAccessLevelForm(level, acrs, zones);

            if (form.ShowDialog() == DialogResult.OK)

            {

                await LoadAccessLevels();

            }

        }

        // ================= DELETE =================

        private async void btnDelete_Click(object sender, EventArgs e)

        {

            if (dgvAccessLevels.CurrentRow == null) return;

            int id = (int)dgvAccessLevels.CurrentRow.Cells["accessLevelId"].Value;

            var level = _levels.FirstOrDefault(x => x.accessLevelId == id);

            if (level == null) return;

            var confirm = MessageBox.Show(

                $"Delete '{level.name}' ?",

                "Confirm",

                MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)

            {

                await _api.DeleteAccessLevel(level.accessLevelId);

                await LoadAccessLevels();

            }

        }

        // ================= SYNC =================

        private async void btnSync_Click(object sender, EventArgs e)

        {

            try

            {

                await _api.SyncAccessLevels();

                MessageBox.Show("Access Levels Synced Successfully");

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }

        // ================= BACK =================

        private void btnBack_Click(object sender, EventArgs e)

        {

            MainForm.Instance.LoadPage(new ControllersControl(), false);

        }

        private void ApplyFilter()

        {

            try

            {

                string search = txtSearch.Text.Trim().ToLower();

                var filtered = ApplyNameFilterInternal(_levels);

                if (!string.IsNullOrEmpty(search))

                {

                    filtered = filtered

                    .Where(l =>

                        (l.accessLevelId.ToString().Contains(search)) ||

                        (!string.IsNullOrEmpty(l.name) &&

                         l.name.ToLower().Contains(search)) ||

                        (l.acrs != null &&

                         l.acrs.Any(a =>

                            (a.acrId != null && a.acrId.ToString().Contains(search)) ||

                            (a.timeZoneId != null && a.timeZoneId.ToString().Contains(search))

                         ))

                    )

                    .ToList();

                }

                var gridData = filtered

                    .Select(l => new

                    {

                        l.accessLevelId,

                        l.name,

                        AcrCount = l.acrs != null

                            ? string.Join(", ", l.acrs.Where(a => a.acrId != null)

                                                     .Select(a => a.acrId.ToString()))

                            : "",

                        TimeZones = l.acrs != null

                             ? string.Join(", ", l.acrs.Where(a => a.timeZoneId != null)

                                                      .Select(a => a.timeZoneId.ToString()))

                             : ""

                    })

                    .ToList();

                dgvAccessLevels.DataSource = gridData;

                //  Show message if no data

                if (!gridData.Any())

                {

                    // optional (avoid spam)

                    // MessageBox.Show("No matching data");

                }

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }

        //private void txtSearch_TextChanged(object sender, EventArgs e)

        //{

        //    ApplyFilter();

        //}

        private void btnSearch_Click(object sender, EventArgs e)

        {

            ApplyFilter();

        }

        private void btnClearFilters_Click(object sender, EventArgs e)

        {

            txtSearch.Text = "";
            if (cmbNameFilter.Items.Count > 0)
                cmbNameFilter.SelectedIndex = 0;

            ApplyFilter();

        }

        private void LoadNameFilter()

        {

            if (_levels == null || _levels.Count == 0)

                return;

            var names = _levels

                .Select(l => l.name ?? string.Empty)

                .Where(name => !string.IsNullOrWhiteSpace(name))

                .Distinct(StringComparer.OrdinalIgnoreCase)

                .OrderBy(name => name)

                .ToList();

            names.Insert(0, "All");

            cmbNameFilter.DataSource = null;

            cmbNameFilter.DataSource = names;

            cmbNameFilter.SelectedIndex = 0;

        }

        private List<AccessLevelDto> ApplyNameFilterInternal(IEnumerable<AccessLevelDto> source)

        {

            string selected = cmbNameFilter.SelectedItem?.ToString()?.Trim();

            if (string.IsNullOrWhiteSpace(selected) ||

                string.Equals(selected, "All", StringComparison.OrdinalIgnoreCase))

            {

                return source?.ToList() ?? new List<AccessLevelDto>();

            }

            return (source ?? Enumerable.Empty<AccessLevelDto>())

                .Where(l => string.Equals((l.name ?? string.Empty).Trim(), selected, StringComparison.OrdinalIgnoreCase))

                .ToList();

        }

        private void AlignHeaderControls()

        {

            int rightPadding = 12;

            btnClearFilters.Location = new Point(panelHeader.ClientSize.Width - btnClearFilters.Width - rightPadding, 14);
            btnSearch.Location = new Point(btnClearFilters.Left - btnSearch.Width - 6, 14);
            txtSearch.Location = new Point(btnSearch.Left - txtSearch.Width - 8, 16);
            lblSearchRight.Location = new Point(txtSearch.Left - lblSearchRight.Width - 8, 20);

        }

        private void AlignFilterControls()

        {

            int top = 6;
            int spacing = 8;
            int rightPadding = 12;

            btnAdd.Location = new Point(10, 5);
            btnEdit.Location = new Point(btnAdd.Right + spacing, 5);
            btnDelete.Location = new Point(btnEdit.Right + spacing, 5);
            btnSync.Location = new Point(btnDelete.Right + spacing, 6);
            btnRefresh.Location = new Point(btnSync.Right + spacing, 6);
            btnBack.Location = new Point(btnRefresh.Right + spacing, 6);

            cmbNameFilter.Location = new Point(panelFilter.ClientSize.Width - cmbNameFilter.Width - rightPadding, top);
            lblNameFilter.Location = new Point(cmbNameFilter.Left - lblNameFilter.Width - 8, 9);

        }

    }

}

