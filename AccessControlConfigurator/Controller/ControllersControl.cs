using AccessControlConfigurator.Helpers;
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

            txtSearch.TextChanged += (s, e) => ApplySearch();
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
            dgvControllers.CellPainting += dgvControllers_CellPainting;

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
            if (topPanel == null || topPanel.IsDisposed)
                return;

            int rightPadding = 20;
            int leftPadding = 15;
            int spacing = 6;
            int rowSpacing = 8;

            int minSearchWidth = 90;
            int maxSearchWidth = 220;

            int leftGroupRight = new Control[] { btnAdd, btnDiscover, btnSync, btnSyncOnline, btnback }
                .Where(c => c != null && !c.IsDisposed && c.Visible)
                .Select(c => c.Right)
                .DefaultIfEmpty(leftPadding)
                .Max();

            int nonSearchWidth =
                lblSearchRight.Width + spacing +
                btnSearch.Width + spacing +
                btnClearfillter.Width;

            int availableWidthForSearch =
                topPanel.ClientSize.Width - rightPadding - leftGroupRight - spacing - nonSearchWidth - spacing;

            bool moveToNextRow = availableWidthForSearch < minSearchWidth;

            int searchRowTop;
            if (moveToNextRow)
            {
                searchRowTop = btnAdd.Bottom + rowSpacing;
                availableWidthForSearch =
                    topPanel.ClientSize.Width - leftPadding - rightPadding - nonSearchWidth - spacing;
            }
            else
            {
                searchRowTop = btnAdd.Top;
            }

            int searchWidth = Math.Max(minSearchWidth, Math.Min(availableWidthForSearch, maxSearchWidth));

            btnClearfillter.Left = topPanel.ClientSize.Width - rightPadding - btnClearfillter.Width;
            btnSearch.Left = btnClearfillter.Left - spacing - btnSearch.Width;
            txtSearch.Width = searchWidth;
            txtSearch.Left = btnSearch.Left - spacing - txtSearch.Width;
            lblSearchRight.Left = txtSearch.Left - spacing - lblSearchRight.Width;

            if (!moveToNextRow && lblSearchRight.Left <= leftGroupRight + spacing)
            {
                moveToNextRow = true;
                searchRowTop = btnAdd.Bottom + rowSpacing;

                availableWidthForSearch =
                    topPanel.ClientSize.Width - leftPadding - rightPadding - nonSearchWidth - spacing;

                searchWidth = Math.Max(minSearchWidth, Math.Min(availableWidthForSearch, maxSearchWidth));

                btnClearfillter.Left = topPanel.ClientSize.Width - rightPadding - btnClearfillter.Width;
                btnSearch.Left = btnClearfillter.Left - spacing - btnSearch.Width;
                txtSearch.Width = searchWidth;
                txtSearch.Left = btnSearch.Left - spacing - txtSearch.Width;
                lblSearchRight.Left = txtSearch.Left - spacing - lblSearchRight.Width;
            }

            if (lblSearchRight.Left < leftPadding)
            {
                int shiftRight = leftPadding - lblSearchRight.Left;
                lblSearchRight.Left += shiftRight;
                txtSearch.Left += shiftRight;
                btnSearch.Left += shiftRight;
                btnClearfillter.Left += shiftRight;
            }

            btnSearch.Top = searchRowTop;
            btnClearfillter.Top = searchRowTop;
            txtSearch.Top = searchRowTop + ((btnSearch.Height - txtSearch.Height) / 2);
            lblSearchRight.Top = searchRowTop + ((btnSearch.Height - lblSearchRight.Height) / 2);

            int normalHeight = Math.Max(btnAdd.Bottom + 10, 70);
            int wrappedHeight = btnSearch.Bottom + 10;
            topPanel.Height = moveToNextRow ? wrappedHeight : normalHeight;

            lblSearchRight.BringToFront();
            txtSearch.BringToFront();
            btnSearch.BringToFront();
            btnClearfillter.BringToFront();
        }

        private void FixHeaderStyle()
        {
            Helpers.GridStyleHelper.ApplyStandardStyle(dgvControllers);
        }

        private void ApplyButtonStyles()
        {
            Helpers.UIStyleHelper.StylePrimaryToolbarButton(btnAdd, 100);
            btnAdd.Text = "+ Add";

            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnDiscover, 120);
            btnDiscover.Text = "\u2295 Discover";

            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnSync, 90);
            btnSync.Text = "\u21BA Sync";

            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnSyncOnline, 160);
            btnSyncOnline.Text = "\u21C5 Sync Online/Offline";

            Helpers.UIStyleHelper.StyleNeutralToolbarButton(btnback, 90);
            btnback.Text = "\u2190 Back";
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

                if (controllers == null || !controllers.Any())
                {
                    PopulateControllersGrid(new List<ControllerDto>());
                    return;
                }

                controllerList = controllers.ToList();
                PopulateControllersGrid(controllerList);
            }
            catch (Exception ex)
            {
                ShowMessage(ControllerErrorHelper.GetMessage(ex));
            }
        }

        private void PopulateControllersGrid(IEnumerable<ControllerDto> controllers)
        {
            if (dgvControllers.InvokeRequired)
            {
                dgvControllers.Invoke(() => PopulateControllersGrid(controllers));
                return;
            }

            dgvControllers.SuspendLayout();
            dgvControllers.Rows.Clear();

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

            dgvControllers.ResumeLayout();
        }

        private void PopulateDiscoverControllersGrid(IEnumerable<DiscoverControllerDto> controllers)
        {
            if (dgvControllers.InvokeRequired)
            {
                dgvControllers.Invoke(() => PopulateDiscoverControllersGrid(controllers));
                return;
            }

            dgvControllers.SuspendLayout();
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

            dgvControllers.ResumeLayout();
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
                ShowMessage(ControllerErrorHelper.GetMessage(ex));
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

        private void dgvControllers_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            var colName = dgvControllers.Columns[e.ColumnIndex].Name;
            if (colName != "colEdit" && colName != "colReset" && colName != "colDelete")
                return;

            string icon;
            Color iconColor;
            switch (colName)
            {
                case "colEdit":
                    icon = "\u270E";  // ✎ pencil
                    iconColor = Color.FromArgb(0, 120, 215);
                    break;
                case "colReset":
                    icon = "\u27F2";  // ⟲ rotate
                    iconColor = Color.FromArgb(196, 43, 28);
                    break;
                default: // colDelete
                    icon = "\u2715";  // ✕ cross
                    iconColor = Color.FromArgb(196, 43, 28);
                    break;
            }

            // Paint row selection background
            e.Paint(e.ClipBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.SelectionBackground);

            // Button rect with consistent padding
            var btnRect = Rectangle.Inflate(e.CellBounds, -4, -4);
            using var bgBrush = new SolidBrush(Color.White);
            using var borderPen = new Pen(iconColor);
            e.Graphics.FillRectangle(bgBrush, btnRect);
            e.Graphics.DrawRectangle(borderPen, btnRect);

            // Icon — fixed size for all three
            using var font = new Font("Segoe UI", 11F, FontStyle.Bold);
            using var textBrush = new SolidBrush(iconColor);
            var fmt = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            e.Graphics.DrawString(icon, font, textBrush, e.CellBounds, fmt);

            e.Handled = true;
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
            else if (columnName == "colReset")
            {
                ControllerDto controller = null;

                if (dgvControllers.Rows[e.RowIndex].Tag is ControllerDto c)
                {
                    controller = c;
                }
                else if (dgvControllers.Rows[e.RowIndex].Tag is DiscoverControllerDto d)
                {
                    controller = new ControllerDto
                    {
                        Id = d.Id,
                        Name = d.Name,
                        IpAddress = d.IpAddress,
                        MacAddress = d.MacAddress,
                        IsEnabled = d.IsEnabled,
                        Status = d.Status
                    };
                }

                if (controller == null)
                    return;

                // Step 1: prompt for MAC address
                string enteredMac = ShowInputDialog(
                    "Enter MAC Address",
                    $"Enter the MAC address for controller '{controller.Name}' to confirm reset:");

                if (enteredMac == null)   // user cancelled
                    return;

                // Step 2: validate against row's MAC
                if (!string.Equals(enteredMac.Trim(), controller.MacAddress?.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show(
                        "MAC address does not match. Reset cancelled.",
                        "Validation Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Step 3: confirm reset
                var confirm = MessageBox.Show(
                    "MAC address correct. Do you want to reset?",
                    "Confirm Reset",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning);

                if (confirm != DialogResult.OK)
                    return;

                try
                {
                    var result = await _api.ResetController(controller.Id);

                    if (result == "success")
                    {
                        MessageBox.Show("Controller reset successfully.");
                        await LoadControllersFromApi();
                    }
                    else
                    {
                        MessageBox.Show(ControllerErrorHelper.GetMessage(result));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ControllerErrorHelper.GetMessage(ex));
                }

                return;
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
                        MessageBox.Show(ControllerErrorHelper.GetMessage(result));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ControllerErrorHelper.GetMessage(ex));
                }
            }
        }

        // Returns the entered text, or null if the user cancelled.
        private static string ShowInputDialog(string title, string prompt)
        {
            using var form = new Form
            {
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                Width = 420,
                Height = 175,
                BackColor = Color.White
            };

            var lbl = new Label
            {
                Text = prompt,
                Left = 16,
                Top = 16,
                Width = 376,
                Height = 40,
                Font = new Font("Segoe UI", 9.5F)
            };

            var txt = new TextBox
            {
                Left = 16,
                Top = 62,
                Width = 376,
                Font = new Font("Segoe UI", 10F)
            };

            var btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Left = 216,
                Top = 98,
                Width = 85,
                Height = 30,
                BackColor = Color.FromArgb(0, 120, 215),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnOk.FlatAppearance.BorderSize = 0;

            var btnCancel = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                Left = 310,
                Top = 98,
                Width = 82,
                Height = 30,
                FlatStyle = FlatStyle.Flat
            };

            form.Controls.AddRange(new Control[] { lbl, txt, btnOk, btnCancel });
            form.AcceptButton = btnOk;
            form.CancelButton = btnCancel;

            return form.ShowDialog() == DialogResult.OK ? txt.Text : null;
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
            colReset.FillWeight = 6;
            colDelete.FillWeight = 6;

            // Reset button — red text so it stands out as a destructive action
            colReset.DefaultCellStyle.ForeColor = Color.FromArgb(196, 43, 28);
            colReset.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
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

            MessageBox.Show(ControllerErrorHelper.GetMessage(msg).Replace("\"", ""));
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