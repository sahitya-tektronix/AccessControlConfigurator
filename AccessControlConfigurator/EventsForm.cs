using AccessControlSystem.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class EventsControl : UserControl
    {
        private WebSocketService _ws;
        private List<EventRow> _allData = new List<EventRow>();
        private readonly System.Windows.Forms.Timer _refreshTimer;
        private bool _isWebSocketConnecting;
        private bool _isWebSocketConnected;
        private bool _isDisposed;

        public EventsControl()
        {
            InitializeComponent();
            InitializeEventGrid();
            btnSearch.Click += btnSearch_Click;
            Load += EventsControl_Load;
            VisibleChanged += EventsControl_VisibleChanged;

            // Button style
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.BackColor = Color.FromArgb(0, 120, 215);
            btnSearch.ForeColor = Color.White;

            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbEventTypeFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbScpFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbEventTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbScpFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEventTypeFilter.SelectedIndexChanged += (s, e) => ApplyFilters();
            cmbScpFilter.SelectedIndexChanged += (s, e) => ApplyFilters();
            btnClearFilters.Click += btnClearFilters_Click;
            //dgvEvents.DataSource = _allData;


            StyleToolbarButton(btnDelete);
            StyleToolbarButton(btnrefresh);
            StyleToolbarButton(btnDelete);
            StyleToolbarButton(btnclr);
            StyleToolbarButton(btnBack);
            btnBack.Click += btnBack_Click_1;
            btnrefresh.Click += btnrefresh_Click;
            btnDelete.Click += btnDelete_Click;
            btnclr.Click += btnclr_Click;

            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 250;
            _refreshTimer.Tick += RefreshTimer_Tick;

        }

        private void InitializeEventGrid()
        {
            dgvEvents.Columns.Clear();

            dgvEvents.Columns.Add("time", "Time");
            dgvEvents.Columns.Add("eventType", "Event Category");
            dgvEvents.Columns.Add("scp", "SCP ID");
            dgvEvents.Columns.Add("cardNumber", "Card Number");
            dgvEvents.Columns.Add("cardStatus", "Card Status");
            dgvEvents.Columns.Add("door", "Door / SIO");
            dgvEvents.Columns.Add("status", "Status");
            dgvEvents.Columns.Add("commandTag", "Command Tag");
            dgvEvents.Columns.Add("description", "Description");

            // Hidden timestamp column for sorting
            dgvEvents.Columns.Add("timestamp", "Timestamp");
            dgvEvents.Columns["timestamp"].Visible = false;

            Helpers.GridStyleHelper.ApplyStandardStyle(dgvEvents);
            dgvEvents.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }

        private void EventsControl_Load(object sender, EventArgs e)
        {
            _ = EnsureWebSocketStartedAsync();
        }

        private void EventsControl_VisibleChanged(object sender, EventArgs e)
        {
            if (_isDisposed)
                return;

            if (Visible)
            {
                _ = EnsureWebSocketStartedAsync();
            }
            else
            {
                _ = StopWebSocketAsync();
            }
        }

        private async System.Threading.Tasks.Task EnsureWebSocketStartedAsync()
        {
            if (_isDisposed || _isWebSocketConnecting || _isWebSocketConnected)
                return;

            _isWebSocketConnecting = true;

            try
            {
                _ws = new WebSocketService();
                _ws.OnError += HandleWebSocketError;
                _ws.OnMessageReceived += HandleWebSocketMessage;

                bool connected = await _ws.ConnectAsync("wss://teksmartsolutions.com/TekHIDApi/ws");

                _isWebSocketConnected = connected;
                AddEvent(connected ? "WebSocket Connected" : "WebSocket Connection Failed");
            }
            finally
            {
                _isWebSocketConnecting = false;
            }
        }

        private async System.Threading.Tasks.Task StopWebSocketAsync()
        {
            if (_ws == null)
                return;

            var socket = _ws;
            _ws = null;
            _isWebSocketConnected = false;
            _isWebSocketConnecting = false;

            socket.OnError -= HandleWebSocketError;
            socket.OnMessageReceived -= HandleWebSocketMessage;

            await socket.DisconnectAsync();
        }

        private void HandleWebSocketMessage(string message)
        {
            AddEvent(message);
        }

        private void HandleWebSocketError(string error)
        {
            AddEvent("WebSocket Error: " + error);
        }

        public void AddEvent(string rawMessage)
        {
            if (_isDisposed)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(AddEvent), rawMessage);
                return;
            }

            DateTime now = DateTime.Now;
            DateTime eventTime = now;
            string time = now.ToString("HH:mm:ss.fff");

            string eventType = "";
            string scp = "";
            string cardNumber = "";
            string cardStatus = "";
            string door = "";
            string status = "";
            string commandTag = "";
            string description = "";

            try
            {
                var json = JObject.Parse(rawMessage);

                // Event Category
                eventType = GetString(json, "eventName", "EventName");

                if (eventType == "command_status_received")
                    eventType = "Command Status";

                // SCP ID
                scp = GetString(json, "scpId", "ScpId");

                var timestampRaw = GetString(json, "timestamp", "Timestamp");
                if (DateTime.TryParse(timestampRaw, out var parsedTimestamp))
                    eventTime = parsedTimestamp;

                // Nested data object
                var data = json["data"] as JObject ?? json["Data"] as JObject;

                if (data != null)
                {
                    commandTag = GetString(data, "commandTag", "CommandTag");
                    status = GetString(data, "statusText", "StatusText", "statusDescription", "StatusDescription");

                    cardNumber = GetString(data, "cardNumber", "CardNumber");

                    var deviceName = GetString(data, "deviceName", "DeviceName");
                    var deviceId = GetString(data, "deviceId", "DeviceId");
                    var acrNumber = GetString(data, "acrNumber", "AcrNumber");
                    var sioNumber = GetString(data, "sioNumber", "SioNumber");
                    var mpNumber = GetString(data, "mpNumber", "MpNumber");
                    var cpNumber = GetString(data, "cpNumber", "CpNumber");
                    var sourceType = GetString(data, "sourceType", "SourceType");
                    var sourceNumber = GetString(data, "sourceNumber", "SourceNumber");

                    if (!string.IsNullOrWhiteSpace(deviceName))
                        door = deviceName;
                    else if (!string.IsNullOrWhiteSpace(deviceId))
                        door = $"Device {deviceId}";
                    else if (!string.IsNullOrWhiteSpace(acrNumber))
                        door = $"ACR {acrNumber}";
                    else if (!string.IsNullOrWhiteSpace(sioNumber))
                        door = $"SIO {sioNumber}";
                    else if (!string.IsNullOrWhiteSpace(mpNumber))
                        door = $"MP {mpNumber}";
                    else if (!string.IsNullOrWhiteSpace(cpNumber))
                        door = $"CP {cpNumber}";
                    else if (!string.IsNullOrWhiteSpace(sourceType) || !string.IsNullOrWhiteSpace(sourceNumber))
                        door = $"Source {sourceType}:{sourceNumber}";

                    cardStatus = GetString(data, "cardStatus", "CardStatus");
                    if (string.IsNullOrWhiteSpace(status))
                    {
                        var isOnlineRaw = GetString(data, "isOnline", "IsOnline");
                        if (bool.TryParse(isOnlineRaw, out var isOnline))
                            status = isOnline ? "Online" : "Offline";

                        var isOnRaw = GetString(data, "isOn", "IsOn");
                        if (string.IsNullOrWhiteSpace(status) && bool.TryParse(isOnRaw, out var isOn))
                            status = isOn ? "On" : "Off";

                        var valueRaw = GetString(data, "value", "Value");
                        if (string.IsNullOrWhiteSpace(status) && !string.IsNullOrWhiteSpace(valueRaw))
                            status = valueRaw;
                    }

                    var eventDateTimeRaw = GetString(data, "eventDateTime", "EventDateTime");
                    if (DateTime.TryParse(eventDateTimeRaw, out var parsedEventTime))
                        eventTime = parsedEventTime;

                    if (string.IsNullOrWhiteSpace(description))
                    {
                        var modelDesc = GetString(data, "modelDescription", "ModelDescription");
                        var tranDesc = GetString(data, "tranCodeDescription", "TranCodeDescription");
                        var commandName = GetString(data, "commandName", "CommandName");
                        var commandDescription = GetString(data, "commandDescription", "CommandDescription");

                        description = string.Join(" | ", new[]
                        {
                            modelDesc,
                            tranDesc,
                            commandName,
                            commandDescription,
                            status
                        }.Where(v => !string.IsNullOrWhiteSpace(v)));
                    }
                }

                // Description
                if (string.IsNullOrWhiteSpace(description))
                {
                    var controllerId = GetString(json, "controllerId", "ControllerId");
                    description = $"Controller: {controllerId} | Status: {status}";
                }
            }
            catch
            {
                // fallback (non-JSON messages)
                description = rawMessage;
            }

            time = eventTime.ToString("HH:mm:ss.fff");
            var row = new EventRow
            {
                Time = time,
                EventType = eventType,
                Scp = scp,
                CardNumber = cardNumber,
                CardStatus = cardStatus,
                Door = door,
                Status = status,
                CommandTag = commandTag,
                Description = description,
                Timestamp = eventTime
            };

            _allData.Add(row);

            if (_allData.Count > 1000)
                _allData.RemoveAt(0);

            QueueRefresh();
        }

        private void AddRow(string time, string eventType, string scp, string cardNumber, string cardStatus,
            string door, string status, string commandTag, string description, DateTime timestamp)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                    AddRow(time, eventType, scp, cardNumber, cardStatus, door, status, commandTag, description, timestamp)));
                return;
            }

            dgvEvents.Rows.Add(time, eventType, scp, cardNumber, cardStatus, door, status, commandTag, description, timestamp);

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
        private void BindGrid(IEnumerable<EventRow> data)
        {
            dgvEvents.Rows.Clear();

            foreach (var row in data)
            {
                dgvEvents.Rows.Add(
                    row.Time,
                    row.EventType,
                    row.Scp,
                    row.CardNumber,
                    row.CardStatus,
                    row.Door,
                    row.Status,
                    row.CommandTag,
                    row.Description,
                    row.Timestamp);
            }

            dgvEvents.Sort(dgvEvents.Columns["timestamp"], ListSortDirection.Descending);

            foreach (DataGridViewRow gridRow in dgvEvents.Rows)
            {
                string rowStatus = gridRow.Cells["status"].Value?.ToString();

                if (rowStatus == "Online")
                    gridRow.DefaultCellStyle.ForeColor = Color.Green;
                else if (rowStatus == "Offline")
                    gridRow.DefaultCellStyle.ForeColor = Color.Red;
                else if (rowStatus == "OK")
                    gridRow.DefaultCellStyle.ForeColor = Color.DarkBlue;
            }

            if (dgvEvents.Rows.Count > 0)
                dgvEvents.FirstDisplayedScrollingRowIndex = 0;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            dgvEvents.Rows.Clear();
        }

        private async void btnrefresh_Click(object sender, EventArgs e)
        {
            await StopWebSocketAsync();
            _allData.Clear();
            dgvEvents.Rows.Clear();
            LoadFilters();
            await EnsureWebSocketStartedAsync();
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
                _allData.Clear();
                AddEvent("Events cleared");
            }
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            _refreshTimer.Stop();
            LoadFilters();
            ApplyFilters();
        }

        private void QueueRefresh()
        {
            if (_isDisposed)
                return;

            _refreshTimer.Stop();
            _refreshTimer.Start();
        }

        private void ApplyFilters()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(ApplyFilters));
                return;
            }

            IEnumerable<EventRow> filtered = _allData;

            string eventType = cmbEventTypeFilter.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(eventType) &&
                !string.Equals(eventType, "All", StringComparison.OrdinalIgnoreCase))
            {
                filtered = filtered.Where(r =>
                    string.Equals(r.EventType ?? string.Empty, eventType, StringComparison.OrdinalIgnoreCase));
            }

            string scp = cmbScpFilter.SelectedItem?.ToString();
            if (!string.IsNullOrWhiteSpace(scp) &&
                !string.Equals(scp, "All", StringComparison.OrdinalIgnoreCase))
            {
                filtered = filtered.Where(r =>
                    string.Equals(r.Scp ?? string.Empty, scp, StringComparison.OrdinalIgnoreCase));
            }

            string searchText = txtSearch.Text.Trim().ToLower();
            if (!string.IsNullOrEmpty(searchText))
            {
                filtered = filtered.Where(r =>
                    string.Join(" ",
                        r.Time,
                        r.EventType,
                        r.Scp,
                        r.CardNumber,
                        r.CardStatus,
                        r.Door,
                        r.Status,
                        r.CommandTag,
                        r.Description
                    ).ToLower().Contains(searchText));
            }

            BindGrid(filtered);
        }
        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    ApplySearchFilter();
        //}
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }
        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (cmbEventTypeFilter.Items.Count > 0)
                cmbEventTypeFilter.SelectedIndex = 0;
            if (cmbScpFilter.Items.Count > 0)
                cmbScpFilter.SelectedIndex = 0;

            ApplyFilters();
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

            var row = dgvEvents.SelectedRows[0];
            string time = row.Cells["time"].Value?.ToString();
            string eventType = row.Cells["eventType"].Value?.ToString();
            string scp = row.Cells["scp"].Value?.ToString();
            string cardNumber = row.Cells["cardNumber"].Value?.ToString();
            string cardStatus = row.Cells["cardStatus"].Value?.ToString();
            string status = row.Cells["status"].Value?.ToString();
            string commandTag = row.Cells["commandTag"].Value?.ToString();
            string description = row.Cells["description"].Value?.ToString();

            var match = _allData.FirstOrDefault(r =>
                r.Time == time &&
                r.EventType == eventType &&
                r.Scp == scp &&
                r.CardNumber == cardNumber &&
                r.CardStatus == cardStatus &&
                r.Status == status &&
                r.CommandTag == commandTag &&
                r.Description == description);

            if (match != null)
                _allData.Remove(match);

            ApplyFilters();

            MessageBox.Show("Event deleted successfully");
        }

        private void StyleToolbarButton(Button btn)
        {
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btn, 80);
        }

        private void btnBack_Click_1(object sender, EventArgs e)
        {
            _ = StopWebSocketAsync();
            MainForm.Instance?.LoadControllersPage();
        }
        private void LoadFilters()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(LoadFilters));
                return;
            }

            var eventTypes = _allData
                .Select(r => r.EventType ?? string.Empty)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(v => v)
                .ToList();

            var scpIds = _allData
                .Select(r => r.Scp ?? string.Empty)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(v => v)
                .ToList();

            eventTypes.Insert(0, "All");
            scpIds.Insert(0, "All");

            string selectedEventType = cmbEventTypeFilter.SelectedItem?.ToString();
            string selectedScp = cmbScpFilter.SelectedItem?.ToString();

            cmbEventTypeFilter.DataSource = null;
            cmbEventTypeFilter.DataSource = eventTypes;

            cmbScpFilter.DataSource = null;
            cmbScpFilter.DataSource = scpIds;

            if (!string.IsNullOrWhiteSpace(selectedEventType) &&
                eventTypes.Contains(selectedEventType))
            {
                cmbEventTypeFilter.SelectedItem = selectedEventType;
            }
            else
            {
                cmbEventTypeFilter.SelectedIndex = 0;
            }

            if (!string.IsNullOrWhiteSpace(selectedScp) &&
                scpIds.Contains(selectedScp))
            {
                cmbScpFilter.SelectedItem = selectedScp;
            }
            else
            {
                cmbScpFilter.SelectedIndex = 0;
            }
        }
        private class EventRow
        {
            public string Time { get; set; }
            public string EventType { get; set; }
            public string Scp { get; set; }
            public string CardNumber { get; set; }
            public string CardStatus { get; set; }
            public string Door { get; set; }
            public string Status { get; set; }
            public string CommandTag { get; set; }
            public string Description { get; set; }
            public DateTime Timestamp { get; set; }
        }

        private static string GetString(JObject obj, params string[] keys)
        {
            foreach (var key in keys)
            {
                var token = obj.SelectToken(key);
                if (token != null && token.Type != JTokenType.Null)
                    return token.ToString();
            }

            return string.Empty;
        }

        protected override async void Dispose(bool disposing)
        {
            if (disposing)
            {
                _isDisposed = true;
                _refreshTimer?.Stop();
                if (_refreshTimer != null)
                    _refreshTimer.Tick -= RefreshTimer_Tick;

                await StopWebSocketAsync();
            }

            base.Dispose(disposing);
        }
    }
}
