using AccessControlSystem.Models.Cards;

using AccessControlSystem.Services;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using System.Windows.Forms;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AccessControlConfigurator

{

    public partial class Cards : UserControl

    {

        private readonly ApiService _apiService = new ApiService();

        private List<CardDto> _cards = new List<CardDto>();

        public Cards()

        {

            InitializeComponent();
            ApplyButtonStyles();

            Load += Cards_Load;
            dgvCards.Enabled = true;

            // Click handlers are wired in the designer.

            dgvCards.ReadOnly = true; dgvCards.EditMode = DataGridViewEditMode.EditProgrammatically;

            // btnSearch click handler is wired in the designer.

            //txtSearch.TextChanged += txtSearch_TextChanged;

            // Button style

            btnSearch.FlatStyle = FlatStyle.Flat;

            btnSearch.BackColor = Color.FromArgb(0, 120, 215);

            btnSearch.ForeColor = Color.White;

            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClearFilters.Anchor = AnchorStyles.Top | AnchorStyles.Right;


            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblCardNumberFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            txtCardNumberFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblActTimeFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbActTimeFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblDeactTimeFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbDeactTimeFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbActTimeFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbDeactTimeFilter.DropDownStyle = ComboBoxStyle.DropDownList;

            txtCardNumberFilter.TextChanged += (s, e) => ApplyCardFilter();

            cmbActTimeFilter.SelectedIndexChanged += (s, e) => ApplyCardFilter();

            cmbDeactTimeFilter.SelectedIndexChanged += (s, e) => ApplyCardFilter();

            Resize += (s, e) =>
            {
                AlignHeaderControls();
                AlignFilterControls();
            };

            AlignHeaderControls();
            AlignFilterControls();

        }

        private async void Cards_Load(object sender, EventArgs e)

        {

            await LoadCards();

        }

        // LOAD CARDS FROM API

        //private async Task LoadCards()

        //{

        //    try

        //    {

        //        var cards = await _apiService.GetCards();

        //        dgvCards.DataSource = cards;

        //        // Hide unwanted columns

        //        dgvCards.Columns["enCcAdbCard"].Visible = false;

        //        dgvCards.Columns["scpId"].Visible = false;

        //        dgvCards.Columns["flags"].Visible = false;

        //        dgvCards.Columns["pin_MAX_PIN_EXTD"].Visible = false;

        //        dgvCards.Columns["alvl_MAX_ALVL_EXTD"].Visible = false;

        //        dgvCards.Columns["apb_Loc"].Visible = false;

        //        dgvCards.Columns["use_Count"].Visible = false;

        //        dgvCards.Columns["alvls"].Visible = false;

        //        dgvCards.Columns["raw_Command"].Visible = false;

        //        // Rename columns

        //        dgvCards.Columns["id"].HeaderText = "ID";

        //        dgvCards.Columns["cardNumber"].HeaderText = "Card Number";

        //        dgvCards.Columns["accessLevelId"].HeaderText = "Access Level";

        //        dgvCards.Columns["issueCode"].HeaderText = "Issue Code";

        //        dgvCards.Columns["status"].HeaderText = "Status";

        //        dgvCards.Columns["createdAt"].HeaderText = "Created";

        //        dgvCards.Columns["lastModified"].HeaderText = "Modified";

        //    }

        //    catch (Exception ex)

        //    {

        //        MessageBox.Show(ex.Message);

        //    }

        //}

        private async Task LoadCards()

        {

            try

            {

                // 🔥 STEP 1: get data

                var data = await _apiService.GetCards();

                // 🔥 STEP 2: filter deleted

                _cards = data

                    .Where(c => c.isDeleted == 0)

                    .ToList();

                // 🔥 STEP 3: bind AFTER filtering

                dgvCards.DataSource = null;

                dgvCards.DataSource = _cards;

                ApplyGridSettings();

                LoadFilters();
                AlignHeaderControls();
                AlignFilterControls();

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }


        // REFRESH

        private async void btnRefresh_Click(object sender, EventArgs e)

        {

            await LoadCards();

        }

        // DELETE CARD

        private async void btnDelete_Click(object sender, EventArgs e)

        {

            if (dgvCards.CurrentRow == null)

            {

                MessageBox.Show("Select a card first");

                return;

            }

            var card = (CardDto)dgvCards.CurrentRow.DataBoundItem;

            var confirm = MessageBox.Show(

                $"Delete Card {card.cardNumber} ?",

                "Confirm Delete",

                MessageBoxButtons.YesNo,

                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try

            {

                await _apiService.DeleteCard(card.cardNumber);

                MessageBox.Show("Card deleted successfully",

                    "Success",

                    MessageBoxButtons.OK,

                    MessageBoxIcon.Information);

                //  reload fresh data

                await LoadCards();

                //  VERY IMPORTANT (reset search)

                txtSearch.Text = "";

                //  force UI refresh

                dgvCards.DataSource = null;

                dgvCards.DataSource = _cards;

                dgvCards.Refresh();

                dgvCards.ClearSelection();

            }

            catch (Exception ex)

            {

                MessageBox.Show("Delete failed: " + ex.Message);

            }

        }

        // =============================

        // ADD CARD

        // =============================

        private async void btnAdd_Click(object sender, EventArgs e)

        {

            var form = new AddCardForm();

            if (form.ShowDialog() == DialogResult.OK)

            {

                await LoadCards();

            }

        }

        // =============================

        // EDIT CARD

        // =============================

        private async void btnEdit_Click(object sender, EventArgs e)

        {

            if (dgvCards.CurrentRow == null)

            {

                MessageBox.Show("Select a card first");

                return;

            }

            var card = (CardDto)dgvCards.CurrentRow.DataBoundItem;

            var form = new EditCardForm(card);

            if (form.ShowDialog() == DialogResult.OK)

            {

                await LoadCards();

            }

        }

        // =============================

        // SYNC CARDS

        // =============================

        private async void btnSync_Click(object sender, EventArgs e)

        {

            try

            {

                Cursor = Cursors.WaitCursor;

                var result = await _apiService.SyncCardsToHID();

                if (result)

                {

                    MessageBox.Show("Cards synced to HID successfully.",

                        "Sync Completed",

                        MessageBoxButtons.OK,

                        MessageBoxIcon.Information);

                }

                await LoadCards(); // refresh grid

            }

            catch (Exception ex)

            {

                MessageBox.Show("Sync failed\n" + ex.Message);

            }

            finally

            {

                Cursor = Cursors.Default;

            }

        }

        private void btnBack_Click(object sender, EventArgs e)

        {

            MainForm main = new MainForm();

            main.Show();

            //  MainForm.Instance.LoadPage(new Cards(), true);

        }

        private void ApplyCardFilter()

        {

            try

            {

                string search = txtSearch.Text.Trim().ToLower();

                var filtered = _cards.AsEnumerable();

                string cardNumber = txtCardNumberFilter.Text.Trim();

                if (!string.IsNullOrWhiteSpace(cardNumber))

                {

                    filtered = filtered.Where(c => c.cardNumber.ToString().Contains(cardNumber));

                }

                string actTime = cmbActTimeFilter.SelectedItem?.ToString();

                if (!string.IsNullOrWhiteSpace(actTime) &&

                    !string.Equals(actTime, "All", StringComparison.OrdinalIgnoreCase))

                {

                    filtered = filtered.Where(c => c.actTime.ToString() == actTime);

                }

                string deactTime = cmbDeactTimeFilter.SelectedItem?.ToString();

                if (!string.IsNullOrWhiteSpace(deactTime) &&

                    !string.Equals(deactTime, "All", StringComparison.OrdinalIgnoreCase))

                {

                    filtered = filtered.Where(c => c.dactTime.ToString() == deactTime);

                }

                if (!string.IsNullOrEmpty(search))

                {

                    var terms = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    filtered = filtered.Where(c =>

                    {

                        // Convert entire row to searchable string

                        string row = string.Join(" ",

                            c.id,

                            c.cardNumber,

                            c.accessLevelId,
                            c.accessLevelName,

                            c.issueCode,

                            c.status,

                            c.actTime,

                            c.dactTime,

                            c.vacDate,

                            c.tmpDate,

                            c.createdAt,

                            c.lastModified

                        ).ToLower();

                        // 🔥 ALL terms must match (powerful search)

                        return terms.All(t => row.Contains(t));

                    });

                }

                var result = filtered.ToList();

                dgvCards.DataSource = null;

                dgvCards.DataSource = result;

                ApplyGridSettings();

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }

        private void LoadFilters()

        {

            if (_cards == null || _cards.Count == 0)

                return;

            var actTimes = _cards

                .Select(c => c.actTime.ToString())

                .Distinct()

                .OrderBy(n => n)

                .ToList();

            var deactTimes = _cards

                .Select(c => c.dactTime.ToString())

                .Distinct()

                .OrderBy(n => n)

                .ToList();

            actTimes.Insert(0, "All");

            deactTimes.Insert(0, "All");

            cmbActTimeFilter.DataSource = null;

            cmbActTimeFilter.DataSource = actTimes;

            cmbDeactTimeFilter.DataSource = null;

            cmbDeactTimeFilter.DataSource = deactTimes;

            cmbActTimeFilter.SelectedIndex = 0;

            cmbDeactTimeFilter.SelectedIndex = 0;

        }

        private void ApplyGridSettings()

        {

            Helpers.GridStyleHelper.ApplyStandardStyle(
                dgvCards,
                fillColumns: false,
                allowColumnResize: false,
                allowColumnOrder: false);

            dgvCards.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgvCards.ScrollBars = ScrollBars.Both;

            dgvCards.AllowUserToResizeColumns = false;

            dgvCards.AllowUserToOrderColumns = false;

            foreach (DataGridViewColumn col in dgvCards.Columns)

            {

                col.Width = 130;
                col.Resizable = DataGridViewTriState.False;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            }

            EnsureLocalTimeColumns();

            void Hide(string col)

            {

                if (dgvCards.Columns.Contains(col))

                    dgvCards.Columns[col].Visible = false;

            }

            Hide("enCcAdbCard");

            Hide("scpId");

            Hide("flags");

            Hide("pin_MAX_PIN_EXTD");

            Hide("alvl_MAX_ALVL_EXTD");

            Hide("apb_Loc");

            Hide("use_Count");

            Hide("alvls");

            Hide("raw_Command");
            Hide("tmpDays");
            Hide("tmpDate");
            Hide("alvl_Prec_MAX_ACR_PER_SCP");
            Hide("acrNumbers");
            Hide("acrScpIds");
            Hide("vacDate");
            Hide("vacDays");
            Hide("issueCode");
            Hide("isDeleted");
            Hide("startDateTime");
            Hide("endDateTime");
            Hide("status");
            Hide("user_Level_MAX_ULVL");
            Hide("lastModified");

            var headerMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)

            {

                ["id"] = "ID",

                ["cardNumber"] = "Card Number",

                ["accessLevelId"] = "Access Level ID",
                ["accessLevelName"] = "Access Level Name",

                ["issueCode"] = "Issue Code",

                ["status"] = "Status",

                ["actTime"] = "Activation Time",

                ["dactTime"] = "Deactivation Time",

                ["vacDate"] = "Vacation Date",

                ["vacDays"] = "Vacation Days",

                ["tmpDate"] = "Temporary Date",

                ["tmpDays"] = "Temporary Days",

                ["createdAt"] = "Created",

                ["lastModified"] = "Last Modified",

                ["updatedAt"] = "Updated",

                ["startDateTime"] = "Start Date/Time",

                ["endDateTime"] = "End Date/Time",

                ["assignCardholder"] = "Cardholder ID",

                ["user_Level_MAX_ULVL"] = "User Level",

                ["alvl_Prec_MAX_ACR_PER_SCP"] = "Access Level Prec",

                ["acrNumbers"] = "ACR Numbers",

                ["acrScpIds"] = "ACR SCP IDs"

            };

            foreach (DataGridViewColumn col in dgvCards.Columns)

            {

                var key = col.DataPropertyName ?? col.Name;

                if (headerMap.TryGetValue(key, out var header))

                    col.HeaderText = header;

                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.DefaultCellStyle.Alignment =
                    string.Equals(key, "cardNumber", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "accessLevelName", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "actTimeLocal", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "dactTimeLocal", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "createdAt", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(key, "lastModified", StringComparison.OrdinalIgnoreCase)
                        ? DataGridViewContentAlignment.MiddleLeft
                        : DataGridViewContentAlignment.MiddleCenter;

            }

            dgvCards.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            ApplyColumnWidths();

        }

        private void EnsureLocalTimeColumns()

        {

            if (!dgvCards.Columns.Contains("actTimeLocal"))

            {

                dgvCards.Columns.Add(new DataGridViewTextBoxColumn

                {

                    Name = "actTimeLocal",

                    HeaderText = "Activation Time (Local)",

                    ReadOnly = true

                });

            }

            if (!dgvCards.Columns.Contains("dactTimeLocal"))

            {

                dgvCards.Columns.Add(new DataGridViewTextBoxColumn

                {

                    Name = "dactTimeLocal",

                    HeaderText = "Deactivation Time (Local)",

                    ReadOnly = true

                });

            }

            if (dgvCards.Columns.Contains("actTime") && dgvCards.Columns.Contains("actTimeLocal"))

            {

                dgvCards.Columns["actTimeLocal"].DisplayIndex =

                    dgvCards.Columns["actTime"].DisplayIndex + 1;

            }

            if (dgvCards.Columns.Contains("dactTime") && dgvCards.Columns.Contains("dactTimeLocal"))

            {

                dgvCards.Columns["dactTimeLocal"].DisplayIndex =

                    dgvCards.Columns["dactTime"].DisplayIndex + 1;

            }

            foreach (DataGridViewRow row in dgvCards.Rows)

            {

                if (row.DataBoundItem is CardDto card)

                {

                    row.Cells["actTimeLocal"].Value =

                        DateTimeOffset.FromUnixTimeSeconds(card.actTime)

                            .LocalDateTime

                            .ToString("yyyy-MM-dd HH:mm:ss");

                    row.Cells["dactTimeLocal"].Value =

                        DateTimeOffset.FromUnixTimeSeconds(card.dactTime)

                            .LocalDateTime

                            .ToString("yyyy-MM-dd HH:mm:ss");

                }

            }

        }

        private void ApplyColumnWidths()

        {

            SetWidth("id", 80);
            SetWidth("cardNumber", 160);
            SetWidth("accessLevelId", 150);
            SetWidth("accessLevelName", 180);
            SetWidth("actTime", 160);
            SetWidth("dactTime", 165);
            SetWidth("actTimeLocal", 185);
            SetWidth("dactTimeLocal", 195);
            SetWidth("user_Level_MAX_ULVL", 120);
            SetWidth("status", 90);
            SetWidth("createdAt", 155);
            SetWidth("lastModified", 165);

        }

        private void SetWidth(string columnName, int width)

        {

            if (dgvCards.Columns.Contains(columnName))

            {

                dgvCards.Columns[columnName].Width = width;

            }

        }

        private void AlignHeaderControls()

        {

            int top = 12;
            int left = 158;
            int spacing = 10;
            int rightPadding = 12;

            btnAdd.Location = new Point(left, top);
            btnEdit.Location = new Point(btnAdd.Right + spacing, top);
            btnDelete.Location = new Point(btnEdit.Right + spacing, top);
            btnSync.Location = new Point(btnDelete.Right + spacing, top);
            btnRefresh.Location = new Point(btnSync.Right + spacing, top);
            btnBack.Location = new Point(btnRefresh.Right + spacing, top);

            int searchGroupWidth =
                btnClearFilters.Width + 8 +
                btnSearch.Width + 8 +
                txtSearch.Width + 8 +
                lblSearchRight.Width;

            int availableRight = headerPanel.ClientSize.Width - rightPadding;
            bool wrapSearch = (btnBack.Right + spacing + searchGroupWidth) > availableRight;
            int searchTop = wrapSearch ? (top + btnAdd.Height + 8) : top;

            btnClearFilters.Location = new Point(availableRight - btnClearFilters.Width, searchTop);
            btnSearch.Location = new Point(btnClearFilters.Left - btnSearch.Width - 8, searchTop);
            txtSearch.Location = new Point(btnSearch.Left - txtSearch.Width - 8, searchTop + 1);
            lblSearchRight.Location = new Point(txtSearch.Left - lblSearchRight.Width - 8, searchTop + 5);

            if (mainLayout != null && mainLayout.RowStyles.Count > 0)
            {
                mainLayout.RowStyles[0].Height = wrapSearch ? 96F : 60F;
            }

        }

        private void AlignFilterControls()

        {

            int top = 4;
            int rightPadding = 12;
            int spacing = 10;

            cmbDeactTimeFilter.Location = new Point(filterPanel.ClientSize.Width - cmbDeactTimeFilter.Width - rightPadding, top);
            lblDeactTimeFilter.Location = new Point(cmbDeactTimeFilter.Left - lblDeactTimeFilter.Width - 8, 8);

            cmbActTimeFilter.Location = new Point(lblDeactTimeFilter.Left - cmbActTimeFilter.Width - spacing, top);
            lblActTimeFilter.Location = new Point(cmbActTimeFilter.Left - lblActTimeFilter.Width - 8, 8);

            txtCardNumberFilter.Location = new Point(lblActTimeFilter.Left - txtCardNumberFilter.Width - spacing, top);
            lblCardNumberFilter.Location = new Point(txtCardNumberFilter.Left - lblCardNumberFilter.Width - 8, 8);

        }

        //private void txtSearch_TextChanged(object sender, EventArgs e)

        //{

        //    ApplyCardFilter();

        //}

        private void btnSearch_Click(object sender, EventArgs e)

        {

            ApplyCardFilter();

        }

        private void btnClearFilters_Click(object sender, EventArgs e)

        {

            txtSearch.Text = "";
            txtCardNumberFilter.Text = "";
            if (cmbActTimeFilter.Items.Count > 0)
                cmbActTimeFilter.SelectedIndex = 0;
            if (cmbDeactTimeFilter.Items.Count > 0)
                cmbDeactTimeFilter.SelectedIndex = 0;

            ApplyCardFilter();

        }

        private void ApplyButtonStyles()
        {
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnAdd, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnEdit, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnDelete, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnSync, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnRefresh, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnBack, 90);
        }

    }

}

