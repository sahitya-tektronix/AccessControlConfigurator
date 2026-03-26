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

                if (string.IsNullOrEmpty(search))
                {
                    dgvAccessLevels.DataSource = _levels.Select(l => new
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
                    }).ToList();

                    return;
                }

                var filtered = _levels
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

                dgvAccessLevels.DataSource = filtered;

                //  Show message if no data
                if (!filtered.Any())
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
    }
}