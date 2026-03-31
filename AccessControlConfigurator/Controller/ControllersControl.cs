using AccessControlSystem.ApiClient;
using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator.Forms
{
    public partial class ControllersControl : UserControl
    {
        private readonly ApiService _api = new ApiService();
        private List<ControllerDto> controllerList = new List<ControllerDto>();

        public ControllersControl()
        {
            InitializeComponent();
            FixHeaderStyle();
            ApplyColumnWidths();
            SetReadOnlyColumns();
            ApplyButtonStyles();

            btnSearch.Click += btnSearch_Click;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Cursor = Cursors.Hand;

            btnAdd.FlatAppearance.BorderColor = Color.FromArgb(45, 62, 80);
            btnAdd.FlatAppearance.BorderSize = 1;

            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                    ApplySearch();
            };

            dgvControllers.AllowUserToResizeColumns = false;
            dgvControllers.AutoGenerateColumns = false;
            dgvControllers.Dock = DockStyle.Fill;
            dgvControllers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvControllers.RowHeadersVisible = false;
            dgvControllers.AllowUserToAddRows = false;
            dgvControllers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            Load += ControllersControl_Load;
            Resize += (s, e) => AlignSearchControls();
            topPanel.SizeChanged += (s, e) => AlignSearchControls();
            dgvControllers.CellContentClick += dgvControllers_CellContentClick;

            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearfillter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            txtSearch.Width = 200;
            btnSearch.Width = 40;
            btnClearfillter.Width = 70;

            AlignSearchControls();
        }

        private void AlignSearchControls()
        {
            int rightPadding = 20;
            int spacing = 5;

            int leftGroupRight = new Control[] { btnDiscover, btnSync, btnSyncOnline, btnAdd, btnback, labelC }
                .Select(c => c.Right)
                .DefaultIfEmpty(0)
                .Max();

            int availableRight = topPanel.ClientSize.Width - rightPadding;
            int fixedGroupWidth =
                btnClearfillter.Width + spacing +
                btnSearch.Width + spacing +
                lblSearchRight.Width + spacing;

            int maxSearchWidth = availableRight - leftGroupRight - fixedGroupWidth;
            int minSearchWidth = 100;
            int maxAllowedWidth = 260;

            bool wrapSearch = maxSearchWidth < minSearchWidth;
            int searchTop = wrapSearch ? (btnAdd.Bottom + 6) : txtSearch.Top;

            int searchWidth = Math.Max(minSearchWidth, Math.Min(maxSearchWidth, maxAllowedWidth));
            txtSearch.Width = searchWidth;

            btnClearfillter.Left = availableRight - btnClearfillter.Width;
            btnSearch.Left = btnClearfillter.Left - btnSearch.Width - spacing;
            txtSearch.Left = btnSearch.Left - txtSearch.Width - spacing;
            lblSearchRight.Left = txtSearch.Left - lblSearchRight.Width - spacing;

            if (!wrapSearch)
            {
                int minSearchLeft = leftGroupRight + spacing;
                if (lblSearchRight.Left < minSearchLeft)
                {
                    wrapSearch = true;
                    searchTop = btnAdd.Bottom + 6;

                    btnClearfillter.Left = availableRight - btnClearfillter.Width;
                    btnSearch.Left = btnClearfillter.Left - btnSearch.Width - spacing;
                    txtSearch.Left = btnSearch.Left - txtSearch.Width - spacing;
                    lblSearchRight.Left = txtSearch.Left - lblSearchRight.Width - spacing;
                }
            }

            btnSearch.Top = searchTop;
            btnClearfillter.Top = searchTop;
            txtSearch.Top = searchTop + 2;
            lblSearchRight.Top = searchTop + (txtSearch.Height / 2) - (lblSearchRight.Height / 2) + 2;

            if (topPanel != null)
            {
                int minTopHeight = 70;
                int wrappedHeight = searchTop + btnSearch.Height + 10;
                topPanel.Height = wrapSearch ? Math.Max(minTopHeight, wrappedHeight) : minTopHeight;
            }
        }

        private void FixHeaderStyle()
        {
            Helpers.GridStyleHelper.ApplyStandardStyle(dgvControllers);
        }

        private void ApplyButtonStyles()
        {
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnAdd, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnDiscover, 110);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnSync, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnSyncOnline, 170);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnback, 90);
        }

        private async void ControllersControl_Load(object sender, EventArgs e)
        {
            await LoadControllersFromApi();
        }

        private async Task LoadControllersFromApi()
        {
            try
            {
                var controllers = await _api.GetControllersAsync();
                controllerList = controllers.ToList();
                PopulateControllersGrid(controllers);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private void PopulateControllersGrid(IEnumerable<ControllerDto> controllers)
        {
            dgvControllers.SuspendLayout();
            dgvControllers.Rows.Clear();
            dgvControllers.Refresh();

            foreach (var c in controllers)
            {
                string statusText = GetControllerStatusText(c);
                int rowIndex = dgvControllers.Rows.Add(
                    c.Id,
                    c.Name,
                    c.MacAddress,
                    c.IpAddress,
                    statusText,
                    statusText,
                    c.LastSyncStartedAt?.ToString("dd-MM-yyyy HH:mm"),
                    c.LastSyncCompletedAt?.ToString("dd-MM-yyyy HH:mm"),
                    c.IsEnabled
                );

                DataGridViewRow row = dgvControllers.Rows[rowIndex];
                row.Tag = c;

                if (string.Equals(statusText, "Offline", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(statusText, "Sync Failed", StringComparison.OrdinalIgnoreCase))
                {
                    row.Cells[4].Style.ForeColor = Color.Red;
                }
                else if (string.Equals(statusText, "Sync Completed", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(statusText, "Online", StringComparison.OrdinalIgnoreCase))
                {
                    row.Cells[4].Style.ForeColor = Color.Green;
                }
                else
                {
                    row.Cells[4].Style.ForeColor = Color.DarkOrange;
                }
            }
        }

        private void PopulateDiscoverControllersGrid(IEnumerable<DiscoverControllerDto> controllers)
        {
            dgvControllers.Rows.Clear();

            foreach (var c in controllers)
            {
                string statusText = ToControllerSyncStateLabel(c.Status);
                int rowIndex = dgvControllers.Rows.Add(
                    c.Id,
                    c.Name,
                    c.MacAddress,
                    c.IpAddress,
                    statusText,
                    statusText,
                    "",
                    "",
                    false
                );

                dgvControllers.Rows[rowIndex].Tag = c;
            }
        }

        private async void BDiscover_Click(object sender, EventArgs e)
        {
            try
            {
                var controllers = await _api.DiscoverControllers();

                if (controllers == null || controllers.Count == 0)
                {
                    ShowMessage("No controllers discovered.");
                    return;
                }

                PopulateDiscoverControllersGrid(controllers);
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }

        private async void BtnSync_Click(object sender, EventArgs e)
        {
            bool success = await _api.SyncControllersToHID();
            MessageBox.Show(success ? "Controllers synced successfully" : "Sync failed");

            if (success)
                await LoadControllersFromApi();
        }

        private async void btnSyncOnlineOffline_Click(object sender, EventArgs e)
        {
            string result = await _api.SyncControllersOnlineStatus();
            MessageBox.Show(result);
            await LoadControllersFromApi();
        }

        private async void dgvControllers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var columnName = dgvControllers.Columns[e.ColumnIndex].Name;
            var rowTag = dgvControllers.Rows[e.RowIndex].Tag;

            if (columnName == "colEdit")
            {
                ControllerDto controller = null;

                if (rowTag is ControllerDto c)
                {
                    controller = c;
                }
                else if (rowTag is DiscoverControllerDto discoverController)
                {
                    controller = new ControllerDto
                    {
                        Id = discoverController.Id,
                        Name = discoverController.Name,
                        IpAddress = discoverController.IpAddress,
                        MacAddress = discoverController.MacAddress,
                        IsEnabled = discoverController.IsEnabled,
                        Status = discoverController.Status
                    };
                }
                else
                {
                    ShowMessage("Controller row binding failed");
                    return;
                }

                var editPage = new EditControllerControl(controller);
                MainForm.Instance.LoadPage(editPage, false);
            }
            else if (columnName == "colDelete")
            {
                var controller = dgvControllers.Rows[e.RowIndex].Tag as ControllerDto;
                if (controller == null)
                    return;

                var confirm = MessageBox.Show(
                    "Are you sure you want to delete this controller?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.Yes)
                    return;

                try
                {
                    var result = await _api.DeleteController(controller.Id);

                    if (result == "success")
                    {
                        MessageBox.Show("Deleted successfully");
                        await LoadControllersFromApi();
                    }
                    else if (result.Contains("readers are used"))
                    {
                        MessageBox.Show(
                            "This controller is used in Access Levels.\nPlease remove it before deleting.",
                            "Delete Not Allowed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show(result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ApplyColumnWidths()
        {
            colId.FillWeight = 8;
            colName.FillWeight = 28;
            colMac.FillWeight = 18;
            colIp.FillWeight = 15;
            colOnline.FillWeight = 10;
            colStatus.FillWeight = 12;
            colLastSyncStart.FillWeight = 20;
            colLastSyncEnd.FillWeight = 20;
            colEnable.FillWeight = 10;
            colEdit.FillWeight = 6;
            colDelete.FillWeight = 6;
        }

        private void SetReadOnlyColumns()
        {
            colId.ReadOnly = true;
            colName.ReadOnly = true;
            colMac.ReadOnly = true;
            colIp.ReadOnly = true;
            colOnline.ReadOnly = true;
            colStatus.ReadOnly = true;
            colLastSyncStart.ReadOnly = true;
            colLastSyncEnd.ReadOnly = true;
            colEnable.ReadOnly = true;
        }

        private void btnback_Click(object sender, EventArgs e)
        {
            MainForm.Instance.LoadPage(new ControllersControl(), false);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearch();
        }

        private void ApplySearch()
        {
            string searchText = txtSearch.Text?.Trim();

            if (string.IsNullOrWhiteSpace(searchText))
            {
                PopulateControllersGrid(controllerList);
                return;
            }

            if (int.TryParse(searchText, out int idValue))
            {
                var idResult = controllerList.Where(x => x.Id == idValue).ToList();
                if (idResult.Count == 0)
                {
                    ShowMessage("No controller found with this ID.");
                    dgvControllers.Rows.Clear();
                    return;
                }

                PopulateControllersGrid(idResult);
                return;
            }

            string lower = searchText.ToLowerInvariant();
            var result = controllerList.Where(x =>
                (x.Name ?? string.Empty).ToLowerInvariant().Contains(lower) ||
                (x.MacAddress ?? string.Empty).ToLowerInvariant().Contains(lower) ||
                (x.IpAddress ?? string.Empty).ToLowerInvariant().Contains(lower) ||
                GetControllerStatusText(x).ToLowerInvariant().Contains(lower) ||
                (x.IsEnabled ? "true" : "false").Contains(lower)
            ).ToList();

            if (result.Count == 0)
            {
                ShowMessage("No matching records found.");
                dgvControllers.Rows.Clear();
                return;
            }

            PopulateControllersGrid(result);
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddControllerForm();
            if (form.ShowDialog() == DialogResult.OK)
                await LoadControllersFromApi();
        }

        private void ShowMessage(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                MessageBox.Show("Unknown message");
                return;
            }

            MessageBox.Show(msg.Replace("\"", ""));
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            ApplySearch();
        }

        private static string GetControllerStatusText(ControllerDto controller)
        {
            if (controller == null)
                return "Unknown";

            if (controller.IsOnline)
                return "Online";

            return "Offline";
        }

        private static string ToControllerSyncStateLabel(int state)
        {
            return state switch
            {
                1 => "Sync Required",
                2 => "Syncing",
                3 => "Sync Completed",
                4 => "Sync Failed",
                5 => "Offline",
                _ => "Unknown"
            };
        }

        private static string NormalizeStatusLabel(string value)
        {
            var cleaned = value.Replace("_", " ").Trim();
            return cleaned.ToLowerInvariant() switch
            {
                "syncrequired" => "Sync Required",
                "sync required" => "Sync Required",
                "syncing" => "Syncing",
                "synccompleted" => "Sync Completed",
                "sync completed" => "Sync Completed",
                "syncfailed" => "Sync Failed",
                "sync failed" => "Sync Failed",
                "offline" => "Offline",
                "online" => "Online",
                _ => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cleaned.ToLowerInvariant())
            };
        }
    }
}
