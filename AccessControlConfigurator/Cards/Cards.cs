using AccessControlSystem.Models.Cards;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
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

            Load += Cards_Load;

            btnRefresh.Click += btnRefresh_Click;
            btnDelete.Click += btnDelete_Click;
            btnSync.Click += btnSync_Click;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            this.Load += (s, e) => this.FindForm().ActiveControl = null;


            dgvCards.ReadOnly = true; dgvCards.EditMode = DataGridViewEditMode.EditProgrammatically;

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

                    // ✅ GRID SETTINGS
                    dgvCards.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgvCards.ScrollBars = ScrollBars.Both;
                    dgvCards.AllowUserToResizeColumns = true;
                    dgvCards.AllowUserToOrderColumns = true;


                    // ✅ OPTIONAL: Set default width for better UI
                    foreach (DataGridViewColumn col in dgvCards.Columns)
                    {
                        col.Width = 130;
                    }

                    // ✅ Hide unwanted columns (safe check)
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

                    // ✅ Rename columns (safe check)
                    void Rename(string col, string name)
                    {
                        if (dgvCards.Columns.Contains(col))
                            dgvCards.Columns[col].HeaderText = name;
                    }

                    Rename("id", "ID");
                    Rename("cardNumber", "Card Number");
                    Rename("accessLevelId", "Access Level");
                    Rename("issueCode", "Issue Code");
                    Rename("status", "Status");
                    Rename("createdAt", "Created");
                    Rename("lastModified", "Modified");

                    // 🔥 AUTO FORMAT remaining columns (camelCase → Proper Text)
                    foreach (DataGridViewColumn col in dgvCards.Columns)
                    {
                        if (string.IsNullOrWhiteSpace(col.HeaderText) || col.HeaderText == col.Name)
                        {
                            col.HeaderText = System.Text.RegularExpressions.Regex
                                .Replace(col.Name, "([a-z])([A-Z])", "$1 $2");
                        }
                    }

                    // ✅ HEADER STYLE (makes it look clean)
                    dgvCards.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
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
        //private async void btnAdd_Click(object sender, EventArgs e)
        //{
        //    var form = new AddCardForm();

        //    if (form.ShowDialog() == DialogResult.OK)
        //    {
        //        await LoadCards();
        //    }
        //}


        private async void btnAdd_Click(object sender, EventArgs e)
        {
            this.FindForm().ActiveControl = null;
            var form = new AddCardForm();

            // 🔥 IMPORTANT: remove focus from Add button
            this.FindForm().ActiveControl = null;

            var result = form.ShowDialog();

            if (result == DialogResult.OK)
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

                if (string.IsNullOrEmpty(search))
                {
                    dgvCards.DataSource = null;
                    dgvCards.DataSource = _cards;
                    return;
                }

                var terms = search.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var filtered = _cards.Where(c =>
                {
                    // Convert entire row to searchable string
                    string row = string.Join(" ",
                        c.id,
                        c.cardNumber,
                        c.accessLevelId,
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
                }).ToList();

                dgvCards.DataSource = null;
                dgvCards.DataSource = filtered;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //private void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    ApplyCardFilter();
        //}

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyCardFilter();
        }
    }

}