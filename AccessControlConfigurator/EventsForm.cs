using AccessControlSystem.Services;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class EventsControl : UserControl
    {
        private WebSocketService _ws;
        private List<EventDto> _allData = new List<EventDto>();

        public EventsControl()
        {
            InitializeComponent();
            InitializeEventGrid();
            StartWebSocket();
            btnSearch.Click += btnSearch_Click;
            //txtSearch.TextChanged += txtSearch_TextChanged;

            // Button style
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;

            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            //dgvEvents.DataSource = _allData;


            StyleToolbarButton(btnDelete);
            StyleToolbarButton(btnrefresh);
            StyleToolbarButton(btnDelete);
            StyleToolbarButton(btnclr);
            StyleToolbarButton(btnBack);



        }

        private void InitializeEventGrid()
        {
            dgvEvents.Columns.Clear();

            dgvEvents.Columns.Add("time", "Time");
            dgvEvents.Columns.Add("eventType", "Event Type");
            dgvEvents.Columns.Add("scp", "SCP ID");
            dgvEvents.Columns.Add("door", "Door / SIO");
            dgvEvents.Columns.Add("status", "Status");
            dgvEvents.Columns.Add("commandTag", "Command Tag");
            dgvEvents.Columns.Add("description", "Description");

            // Hidden timestamp column for sorting
            dgvEvents.Columns.Add("timestamp", "Timestamp");
            dgvEvents.Columns["timestamp"].Visible = false;

            dgvEvents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private async void StartWebSocket()
        {
            _ws = new WebSocketService();

            _ws.OnError += (err) =>
            {
                MessageBox.Show("ERROR: " + err);
            };

            _ws.OnMessageReceived += (msg) =>
            {
                AddEvent(msg);
            };

            bool connected = await _ws.ConnectAsync("wss://teksmartsolutions.com/TekHIDApi/ws");

            if (connected)
                AddEvent("WebSocket Connected");
            else
                AddEvent("WebSocket Connection Failed");
        }

        public void AddEvent(string rawMessage)
        {
            DateTime now = DateTime.Now;
            string time = now.ToString("HH:mm:ss.fff");

            string eventType = "";
            string scp = "";
            string door = "";
            string status = "";
            string commandTag = "";
            string description = "";

            try
            {
                var json = JObject.Parse(rawMessage);

                // Event Type
                eventType = json["eventName"]?.ToString();

                if (eventType == "command_status_received")
                    eventType = "Command Status";

                // SCP ID
                scp = json["scpId"]?.ToString();

                // Nested data object
                var data = json["data"];

                if (data != null)
                {
                    commandTag = data["commandTag"]?.ToString();
                    status = data["statusText"]?.ToString();
                }

                // Description
                description = $"Controller: {json["controllerId"]} | Status: {status}";
            }
            catch
            {
                // fallback (non-JSON messages)
                description = rawMessage;
            }

            AddRow(time, eventType, scp, door, status, commandTag, description, now);
        }

        private void AddRow(string time, string eventType, string scp,
            string door, string status, string commandTag, string description, DateTime timestamp)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                    AddRow(time, eventType, scp, door, status, commandTag, description, timestamp)));
                return;
            }

            dgvEvents.Rows.Add(time, eventType, scp, door, status, commandTag, description, timestamp);

            // Sort latest first
            dgvEvents.Sort(dgvEvents.Columns["timestamp"], ListSortDirection.Descending);

            // Apply colors
            foreach (DataGridViewRow row in dgvEvents.Rows)
            {
                string rowStatus = row.Cells["status"].Value?.ToString();

                if (rowStatus == "Online")
                    row.DefaultCellStyle.ForeColor = Color.Green;
                else if (rowStatus == "Offline")
                    row.DefaultCellStyle.ForeColor = Color.Red;
                else if (rowStatus == "OK")
                    row.DefaultCellStyle.ForeColor = Color.DarkBlue;
            }

            // Limit rows
            if (dgvEvents.Rows.Count > 1000)
                dgvEvents.Rows.RemoveAt(dgvEvents.Rows.Count - 1);

            // Auto scroll to latest
            if (dgvEvents.Rows.Count > 0)
                dgvEvents.FirstDisplayedScrollingRowIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvEvents.Rows.Clear();
        }



        private async void btnrefresh_Click(object sender, EventArgs e)
        {
            dgvEvents.Rows.Clear();

            AddEvent("Refreshing events...");

            await Task.Delay(500);

            AddEvent("Events refreshed");
        }

        private void btnclr_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Do you want to clear all events?",
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                dgvEvents.Rows.Clear();
                AddEvent("Events cleared");
            }
        }
        private void ApplySearchFilter()
        {
            string searchText = txtSearch.Text.Trim().ToLower();

            foreach (DataGridViewRow row in dgvEvents.Rows)
            {
                if (row.IsNewRow) continue;

                bool match = false;

                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null &&
                        cell.Value.ToString().ToLower().Contains(searchText))
                    {
                        match = true;
                        break;
                    }
                }

                row.Visible = string.IsNullOrEmpty(searchText) || match;
            }
        }
        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    ApplySearchFilter();
        //}
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplySearchFilter();
        }
        private void ClearSearch()
        {
            txtSearch.Text = "";

            foreach (DataGridViewRow row in dgvEvents.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvEvents.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an event to delete");
                return;
            }

            var confirm = MessageBox.Show(
                "Are you sure you want to delete this event?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            // 🔥 REMOVE FROM DATATABLE
            var dt = dgvEvents.DataSource as DataTable;

            if (dt != null)
            {
                dt.Rows.RemoveAt(dgvEvents.SelectedRows[0].Index);
            }

            MessageBox.Show("Event deleted successfully");
        }

        private void StyleToolbarButton(Button btn)
        {
            btn.BackColor = Color.FromArgb(245, 245, 245); // light gray
            btn.ForeColor = Color.FromArgb(45, 62, 80);    // dark text

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = Color.FromArgb(200, 200, 200);
            btn.FlatAppearance.BorderSize = 1;

            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btn.Height = 30;
            btn.Width = 80;

            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            main.Show();
        }
    }
}