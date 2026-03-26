using AccessControlConfigurator;
using AccessControlConfigurator.Forms;
using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccessControlSystem.Forms
{
    public partial class SioControl : UserControl
    {
        private string GetModelName(int modelNumber)
        {
            switch (modelNumber)
            {
                case 193:
                    return "X100";

                case 194:
                    return "X200";

                case 195:
                    return "X300";

                default:
                    return "None";
            }
        }
        private readonly ApiService _api = new ApiService();
        private int controllerId;
        private ControllerDto _controller;

        public SioControl(ControllerDto controller)
        {
            InitializeComponent();

            _controller = controller;
            controllerId = _controller.Id;
        
            lblTitle.Text = $"SIO Configuration (Controller ID : {controllerId})";

            ConfigureGrid(gridRS485_1);
            ConfigureGrid(gridRS485_2);

            LoadBusAddresses(gridRS485_1);
            LoadBusAddresses(gridRS485_2);

            gridRS485_1.CurrentCellDirtyStateChanged += Grid_CurrentCellDirtyStateChanged;
            gridRS485_2.CurrentCellDirtyStateChanged += Grid_CurrentCellDirtyStateChanged;

            btnBack.Click += btnBack_Click;
            btnSave.Click += btnSave_Click;

            _ = LoadSavedSios();

        }

        private void Grid_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridView grid = sender as DataGridView;

            if (grid.IsCurrentCellDirty)
            {
                grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        // ---------------- GRID CONFIGURATION ----------------

        private void ConfigureGrid(DataGridView grid)
        {
            grid.Columns.Clear();

            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colAddress",
                HeaderText = "Interface Panel Address",
                ReadOnly = true
            });

            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "colSioName",
                HeaderText = "SIO Name"
            });

            grid.Columns.Add(new DataGridViewCheckBoxColumn()
            {
                Name = "colEnable",
                HeaderText = "SIO Type",
                Width = 30
            });

            DataGridViewComboBoxColumn modelColumn = new DataGridViewComboBoxColumn()
            {
                Name = "colModel",
                HeaderText = "",
                Width = 160
            };

            modelColumn.Items.AddRange(
                "None",
                "X100",
                "X200",
                "X300"
            );

            grid.Columns.Add(modelColumn);
        }

        // ---------------- LOAD ADDRESSES ----------------

        private void LoadBusAddresses(DataGridView grid)
        {
            grid.Rows.Clear();

            for (int address = 0; address <= 31; address++)
            {
                int index = grid.Rows.Add();
                grid.Rows[index].Cells["colAddress"].Value = address;
                grid.Rows[index].Cells["colSioName"].Value = "";
                grid.Rows[index].Cells["colEnable"].Value = false;   
                grid.Rows[index].Cells["colModel"].Value = "None";
            }
        }

        // ---------------- SAVE BUTTON ----------------
        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var request = new SioBulkConfigDto
                {
                    isInternalSIOEnabled = chkOnBoard.Checked,
                    sios = new List<SioItemDto>()
                };

                AddBusConfigurations(gridRS485_1, 1, request);
                AddBusConfigurations(gridRS485_2, 2, request);

                if (HasDuplicateAddress(request.sios))
                {
                    MessageBox.Show("Duplicate interface panel addresses are not allowed.");
                    return;
                }

                bool ok = await _api.SaveSioBulkAsync(controllerId, request);

                if (!ok)
                {
                    MessageBox.Show("Server rejected the request.");
                    return;
                }

                MessageBox.Show("SIO configuration saved successfully!");

                // reload saved SIOs
                await LoadSavedSios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save failed:\n" + ex.Message);
            }
        }

        // ---------------- ADD BUS CONFIG ----------------

        private void AddBusConfigurations(DataGridView grid, int portNumber, SioBulkConfigDto request)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                //var modelCell = row.Cells["colModel"].Value;
                //var colenble = row.Cells["colEnable"].Value;

                //if (modelCell == null || modelCell.ToString() == "None")
                //    continue;
                var enableCell = row.Cells["colEnable"].Value;
                bool enabled = enableCell != null && (bool)enableCell;

                if (!enabled)
                    continue;

                var modelCell = row.Cells["colModel"].Value;

                if (modelCell == null || modelCell.ToString() == "None")
                    continue;

                int address = Convert.ToInt32(row.Cells["colAddress"].Value);

                string name = row.Cells["colSioName"].Value?.ToString();

                if (string.IsNullOrWhiteSpace(name))
                    name = $"SIO-{address}";

                int modelNumber = GetModelNumber(modelCell.ToString());

                request.sios.Add(new SioItemDto
                {
                    name = name,
                    portNumber = portNumber,
                    sioNumber = address,
                    modelNumber = modelNumber,
                    interfacePanelAddress = address
                });
            }
        }

        // ---------------- MODEL NUMBER ----------------

        private int GetModelNumber(string model)
        {
            Dictionary<string, int> modelMap = new Dictionary<string, int>()
            {
                { "X100", 193 },
                { "X200", 194 },
                { "X300", 195 }
            };

            if (modelMap.TryGetValue(model, out int number))
                return number;

            return 0;
        }


        // ---------------- DUPLICATE CHECK ----------------

        private bool HasDuplicateAddress(List<SioItemDto> list)
        {
            var bus1 = list.Where(x => x.portNumber
            == 1).Select(x => x.interfacePanelAddress);
            var bus2 = list.Where(x => x.portNumber == 2).Select(x => x.interfacePanelAddress);

            if (bus1.Count() != bus1.Distinct().Count())
                return true;

            if (bus2.Count() != bus2.Distinct().Count())
                return true;

            return false;
        }




        // ---------------- GET SAVED SIO FROM AERO PAGE ----------------

        private HashSet<int> GetSavedSioAddresses()
        {
            HashSet<int> addresses = new HashSet<int>();

            if (MainForm.Instance.CurrentPage is EditControllerControl aero)
            {
                DataGridView grid = aero.GetSioGrid();

                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.Cells["Address"].Value != null)
                    {
                        int addr = Convert.ToInt32(row.Cells["Address"].Value);
                        addresses.Add(addr);
                    }
                }
            }

            return addresses;
        }

        // ---------------- BACK BUTTON ----------------

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm.Instance.LoadPage(new EditControllerControl(_controller), false);

        }

        // ---------------- ONBOARD CHECKBOX ----------------

        private void chkOnBoard_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOnBoard.Checked)
                MessageBox.Show("On-Board controller enabled");
            else
                MessageBox.Show("On-Board controller disabled");
        }

        private async void DisableSavedRowsFromApi(DataGridView grid)
        {
            try
            {
                var savedSios = await _api.GetControllerSiosAsync(_controller.Id);

                HashSet<int> usedAddresses = new HashSet<int>();

                foreach (var sio in savedSios)
                {
                    usedAddresses.Add(sio.interfacePanelAddress);
                }

                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;

                    int address = Convert.ToInt32(row.Cells["colAddress"].Value);

                    if (usedAddresses.Contains(address))
                    {
                        DataGridViewCheckBoxCell chk =
                            row.Cells["colEnable"] as DataGridViewCheckBoxCell;

                        if (chk != null)
                        {
                            chk.ReadOnly = true;
                            chk.Value = true;
                            chk.FlatStyle = FlatStyle.Flat;
                            chk.Style.BackColor = Color.LightGray;
                        }

                        row.Cells["colEnable"].ReadOnly = true;
                    }
                }
            }
            catch
            {
                // ignore errors
            }
        }

        private async void SioControl_Load(object sender, EventArgs e)
        {
            await LoadSavedSios();
        }
        //private async Task LoadSavedSios()
        //{
        //    var saved = await _api.GetControllerSiosAsync(controllerId);

        //    foreach (var sio in saved)
        //    {
        //        DataGridView grid = sio.bus == 1 ? gridRS485_1 : gridRS485_2;

        //        foreach (DataGridViewRow row in grid.Rows)
        //        {
        //            if (row.IsNewRow) continue;

        //            int addr = Convert.ToInt32(row.Cells["colAddress"].Value);

        //            if (addr == sio.interfacePanelAddress)
        //            {
        //                row.Cells["colEnable"].Value = true;
        //                row.Cells["colEnable"].ReadOnly = true;
        //                row.Cells["colModel"].ReadOnly = true;

        //                row.Cells["colEnable"].Style.BackColor = Color.LightGray;
        //            }
        //        }
        //    }
        //}

        private async Task LoadSavedSios()
        {
            ResetGrid(gridRS485_1);
            ResetGrid(gridRS485_2);

            var saved = await _api.GetControllerSiosAsync(controllerId);

            // ✅ ADD THIS BLOCK
            bool isOnboard = false;

            foreach (var sio in saved)
            {
                // 👉 YOUR RULE:
                // SIO = 32 AND InterfacePanelAddress = 0
                if (sio.sioNumber == 32 && sio.interfacePanelAddress == 0)
                {
                    isOnboard = true;
                }

                DataGridView grid = sio.portNumber == 1 ? gridRS485_1 : gridRS485_2;

                foreach (DataGridViewRow row in grid.Rows)
                {
                    if (row.IsNewRow) continue;

                    int addr = Convert.ToInt32(row.Cells["colAddress"].Value);

                    if (addr == sio.interfacePanelAddress)
                    {
                        row.Cells["colEnable"].Value = true;
                        row.Cells["colEnable"].ReadOnly = true;

                        row.Cells["colModel"].Value = GetModelName(sio.modelNumber);
                        row.Cells["colModel"].ReadOnly = true;

                        row.Cells["colSioName"].Value = sio.name;
                        row.Cells["colSioName"].ReadOnly = true;

                        row.Cells["colEnable"].Style.BackColor = Color.LightGray;
                        row.Cells["colModel"].Style.BackColor = Color.LightGray;
                    }
                }
            }

            // ✅ SET CHECKBOX HERE
            chkOnBoard.Checked = isOnboard;
        }
        private void ResetGrid(DataGridView grid)
        {
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                // reset checkbox
                row.Cells["colEnable"].Value = false;
                row.Cells["colEnable"].ReadOnly = false;

                // reset name
                row.Cells["colSioName"].Value = "";

                // reset model
                row.Cells["colModel"].Value = "None";
                row.Cells["colModel"].ReadOnly = false;

                // reset color
                row.Cells["colEnable"].Style.BackColor = Color.White;
                row.Cells["colModel"].Style.BackColor = Color.White;
            }
        }
    }

    }
