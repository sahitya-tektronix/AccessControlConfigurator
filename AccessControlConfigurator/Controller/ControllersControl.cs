using AccessControlSystem.ApiClient;
using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
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


            ApplyColumnWidths();
            SetReadOnlyColumns();
            ApplyGridStyle();
            ApplyButtonStyles();

            btnSearch.Click += btnSearch_Click;
            // txtSearch.TextChanged += txtSearch_TextChanged;
            // Button style
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215); // ✅ Blue color
            btnSearch.ForeColor = Color.White;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Cursor = Cursors.Hand;

            // Border style
            btnAdd.FlatAppearance.BorderColor = Color.FromArgb(45, 62, 80);
                 btnAdd.FlatAppearance.BorderSize = 1;
            

            // Enter key search
            txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ApplySearch();
                }
            };

            dgvControllers.AllowUserToResizeColumns = false;
            dgvControllers.AutoGenerateColumns = false;
            dgvControllers.Dock = DockStyle.Fill;
            dgvControllers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvControllers.RowHeadersVisible = false;
            dgvControllers.AllowUserToAddRows = false;
            dgvControllers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Load += ControllersControl_Load;
            dgvControllers.CellContentClick += dgvControllers_CellContentClick;

            //txtSearch.TextChanged += txtSearch_TextChanged;
            //dgvControllers.CellEndEdit += dgvControllers_CellEndEdit;
            //txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;


        }
        private void ApplyButtonStyles()
        {
            StyleOutlineButton(btnAdd);
            StyleOutlineButton(btnDiscover);
            StyleOutlineButton(btnSync);
            StyleOutlineButton(btnSyncOnline);
            StyleOutlineButton(btnback);

            // 🔥 FORCE FIX (for Add button issue)
            btnAdd.ForeColor = Color.FromArgb(45, 62, 80);
            btnAdd.BackColor = Color.White;
        }
        private void StyleOutlineButton(Button btn)
        {
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(45, 62, 80);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(45, 62, 80);
            btn.FlatAppearance.BorderSize = 1;
            btn.UseVisualStyleBackColor = false;
        }
        // PAGE LOAD
        private async void ControllersControl_Load(object sender, EventArgs e)
        {
            await LoadControllersFromApi();
        }

        // LOAD CONTROLLERS
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

            //  ADD FILTER ROW FIRST
            //int filterRowIndex = dgvControllers.Rows.Add();
            //DataGridViewRow filterRow = dgvControllers.Rows[filterRowIndex];

            //filterRow.DefaultCellStyle.BackColor = Color.LightYellow;
            //filterRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
            //filterRow.DefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Italic);


            foreach (var c in controllers)
            {
                string onlineText = c.IsOnline ? "Online" : "Offline";
                string statusText = c.SyncState.ToString();
                int rowIndex = dgvControllers.Rows.Add(
                    c.Id,
                    c.Name,
                    c.MacAddress,
                    c.IpAddress,
                    onlineText,
                    statusText,
                    c.LastSyncStartedAt?.ToString("dd-MM-yyyy HH:mm"),
                    c.LastSyncCompletedAt?.ToString("dd-MM-yyyy HH:mm"),
                    c.IsEnabled
                );

                DataGridViewRow row = dgvControllers.Rows[rowIndex];

                row.Tag = c;

                if (c.IsOnline)
                    row.Cells[4].Style.ForeColor = Color.Green;
                else
                    row.Cells[4].Style.ForeColor = Color.Red;
            }
        }


        private void PopulateDiscoverControllersGrid(IEnumerable<DiscoverControllerDto> controllers)
        {
            dgvControllers.Rows.Clear();

            foreach (var c in controllers)
            {
                int rowIndex = dgvControllers.Rows.Add(
                    c.Id,
                    c.Name,
                    c.MacAddress,
                    c.IpAddress,
                    "Offline",
                    "Discovered",
                    "",
                    "",
                    false
                );

                dgvControllers.Rows[rowIndex].Tag = c;
            }
        }

        // DISCOVER
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



        // SYNC
        private async void BtnSync_Click(object sender, EventArgs e)
        {
            bool success = await _api.SyncControllersToHID();

            MessageBox.Show(success ? "Controllers synced successfully" : "Sync failed");

            if (success)
            {
                await LoadControllersFromApi(); // refresh grid
            }
        }

        // SYNC ONLINE/OFFLINE
        private async void btnSyncOnlineOffline_Click(object sender, EventArgs e)
        {
            string result = await _api.SyncControllersOnlineStatus();

            MessageBox.Show(result);

            await LoadControllersFromApi();
        }


        private async void dgvControllers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var columnName = dgvControllers.Columns[e.ColumnIndex].Name;
            var rowTag = dgvControllers.Rows[e.RowIndex].Tag;

            // ✅ EDIT BUTTON
            if (columnName == "colEdit")
            {
                ControllerDto controller = null;

                // ✅ FIX 1: Proper assignment
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

                // ✅ FIX 2: pass controller to edit page
                var editPage = new EditControllerControl(controller);

                // ✅ OPEN PAGE (same as your logic)
                MainForm.Instance.LoadPage(editPage, false);

                // ❌ DO NOT CALL LoadControllersFromApi HERE
            }
            else if (columnName == "colDelete")
            {
                if (e.RowIndex >= 0)
                {
                    var controller = dgvControllers.Rows[e.RowIndex].Tag as ControllerDto;

                    if (controller == null) return;

                    var confirm = MessageBox.Show(
                        "Are you sure you want to delete this controller?",
                        "Confirm Delete",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirm == DialogResult.Yes)
                    {
                        try
                        {
                            // ✅ CALL API
                            var result = await _api.DeleteController(controller.Id);

                            if (result == "success")
                            {
                                MessageBox.Show("Deleted successfully");

                                // ✅ Reload grid
                                await LoadControllersFromApi();
                            }
                            else
                            {
                                // ✅ ADD YOUR LOGIC HERE
                                if (result.Contains("readers are used"))
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
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
        // GRID STYLE
        private void ApplyGridStyle()
        {
            dgvControllers.BorderStyle = BorderStyle.None;
            dgvControllers.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            dgvControllers.RowHeadersVisible = false;

            dgvControllers.BackgroundColor = Color.White;

            dgvControllers.EnableHeadersVisualStyles = false;

            dgvControllers.ColumnHeadersHeight = 40;
            dgvControllers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            dgvControllers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(45, 62, 80);
            dgvControllers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvControllers.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dgvControllers.DefaultCellStyle.Font =
                new Font("Segoe UI", 10);

            dgvControllers.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(0, 120, 215);

            dgvControllers.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvControllers.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(245, 245, 245);

            dgvControllers.RowTemplate.Height = 32;

            // Disable user resizing
            dgvControllers.AllowUserToResizeColumns = false;
            dgvControllers.AllowUserToResizeRows = false;

            // Professional spacing
            dgvControllers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        // COLUMN WIDTH
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
        // READ ONLY
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

        // BACK
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

            // 🔹 1. EMPTY INPUT → SHOW ALL
            if (string.IsNullOrWhiteSpace(searchText))
            {
                PopulateControllersGrid(controllerList);
                return;
            }

            // 🔹 2. LENGTH VALIDATION (optional)
            if (searchText.Length < 1)
            {
                ShowMessage         ("Please enter at least 1 character.");
                return;
            }

            // 🔹 3. CHECK IF NUMBER (ID SEARCH)
            if (int.TryParse(searchText, out int idValue))
            {
                var idResult = controllerList
                    .Where(x => x.Id == idValue)
                    .ToList();

                // 🔹 4. NO RESULT
                if (idResult.Count == 0)
                {
                    ShowMessage("No controller found with this ID.");
                    dgvControllers.Rows.Clear();
                    return;
                }

                PopulateControllersGrid(idResult);
                return;
            }

            // 🔹 5. TEXT SEARCH
            string lower = searchText.ToLower();

            var result = controllerList.Where(x =>
                (x.Name ?? "").ToLower().Contains(lower) ||
                (x.MacAddress ?? "").ToLower().Contains(lower) ||
                (x.IpAddress ?? "").ToLower().Contains(lower) ||
                x.SyncState.ToString().ToLower().Contains(lower) ||
                (x.IsOnline ? "online" : "offline").Contains(lower) ||
                (x.IsEnabled ? "true" : "false").Contains(lower)
            ).ToList();

            // 🔹 6. NO RESULT
            if (result.Count == 0)
            {
                ShowMessage ("No matching records found.");
                dgvControllers.Rows.Clear();
                return;
            }

            PopulateControllersGrid(result);
        }
        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    ApplySearch();
        //}

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var form = new AddControllerForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                await LoadControllersFromApi(); // refresh grid
            }
        }
        private void ShowMessage(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                MessageBox.Show("Unknown message");
                return;
            }

            //  Remove ALL quotes (not just start/end)
            var clean = msg.Replace("\"", "");

            MessageBox.Show(clean);
        }
    }


        //private void dgvControllers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    // Only filter row (row 0)
        //    if (e.RowIndex != 0) return;

        //    string nameFilter = dgvControllers.Rows[0].Cells[1].Value?.ToString()?.ToLower() ?? "";
        //    string ipFilter = dgvControllers.Rows[0].Cells[3].Value?.ToString()?.ToLower() ?? "";

        //    var filtered = controllerList.Where(x =>
        //        (string.IsNullOrEmpty(nameFilter) ||
        //         (x.Name ?? "").ToLower().Contains(nameFilter))

        //        && (string.IsNullOrEmpty(ipFilter) ||
        //            (x.IpAddress ?? "").ToLower().Contains(ipFilter))
        //    ).ToList();

        //    PopulateControllersGrid(filtered);
        //}
        //private void dgvControllers_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        //{
        //    // Allow editing only filter row
        //    if (e.RowIndex != 0)
        //        e.Cancel = true;
    }

