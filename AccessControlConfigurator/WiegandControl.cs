using AccessControlSystem.Models;
using AccessControlConfigurator.Helpers;
using AccessControlSystem.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class WiegandControl : UserControl
    {
        private readonly ApiService _apiService = new ApiService();

        private List<WiegandDto> _data = new List<WiegandDto>();
        public WiegandControl()
        {
            InitializeComponent();
            ApplyButtonStyles();
            dgvFormats.ReadOnly = true;
            dgvFormats.AllowUserToAddRows = false;
            dgvFormats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormats.AutoGenerateColumns = false;
            InitializeGrid();
            Load += Form_Load;
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnClearFilters.Click += btnClearFilters_Click;
            txtSearch.TextChanged += (s, e) => ApplyFilter();
            txtFormatNumberFilter.TextChanged += (s, e) => ApplyFilter();
            txtBitsFilter.TextChanged += (s, e) => ApplyFilter();
            txtFacilityCodeFilter.TextChanged += (s, e) => ApplyFilter();
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            lblSearchRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            btnClearFilters.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            Resize += (s, e) => AdjustHeaderLayout();
            AdjustHeaderLayout();
        }

        private void AdjustHeaderLayout()
        {
            int spacing = 14;
            int rightPadding = 12;

            AlignActionButtons(spacing);

            int actionsRight = btnRefresh.Right + spacing;
            int searchWidth = searchPanel.Width;
            int available = topPanel.ClientSize.Width - rightPadding;
            bool wrapSearch = actionsRight + searchWidth > available;

            if (wrapSearch)
            {
                searchPanel.Location = new Point(
                    topPanel.ClientSize.Width - searchPanel.Width - rightPadding,
                    btnAdd.Bottom + 6);
                topPanel.Height = btnAdd.Bottom + searchPanel.Height + 8;
            }
            else
            {
                searchPanel.Location = new Point(
                    topPanel.ClientSize.Width - searchPanel.Width - rightPadding,
                    0);
                topPanel.Height = 50;
            }
        }
        private void InitializeGrid()
        {
            dgvFormats.Columns.Clear();

            dgvFormats.Columns.Add(BuildTextColumn("FormatNumber", "Format", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("Name", "Name", 180, DataGridViewContentAlignment.MiddleLeft));
            dgvFormats.Columns.Add(BuildTextColumn("Bits", "Bits", 50, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("FacilityCode", "Facility Code", 80, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("Flags", "Flags", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("PeLen", "PE Len", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("PeLoc", "PE Loc", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("PoLen", "PO Len", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("PoLoc", "PO Loc", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("FcLen", "FC Len", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("FcLoc", "FC Loc", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("ChLen", "CH Len", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("ChLoc", "CH Loc", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("IcLen", "IC Len", 60, DataGridViewContentAlignment.MiddleCenter));
            dgvFormats.Columns.Add(BuildTextColumn("IcLoc", "IC Loc", 60, DataGridViewContentAlignment.MiddleCenter));

            Helpers.GridStyleHelper.ApplyStandardStyle(dgvFormats);
            dgvFormats.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvFormats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }
        private async void Form_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            try
            {
                _data = await _apiService.GetAllAsync();
                dgvFormats.DataSource = _data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(WiegandErrorHelper.GetMessage(ex));
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtFormatNumberFilter.Clear();
            txtBitsFilter.Clear();
            txtFacilityCodeFilter.Clear();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            string search = txtSearch.Text.ToLower();
            if (!TryParseFilter(txtFormatNumberFilter.Text, "Format Number", out short? formatNumber))
                return;
            if (!TryParseFilter(txtBitsFilter.Text, "Bits", out short? bits))
                return;
            if (!TryParseFilter(txtFacilityCodeFilter.Text, "Facility Code", out short? facilityCode))
                return;

            var filtered = _data.Where(x =>
                (string.IsNullOrWhiteSpace(search) ||
                    (x.Name != null && x.Name.ToLower().Contains(search))) &&
                (!formatNumber.HasValue || x.FormatNumber == formatNumber.Value) &&
                (!bits.HasValue || x.Bits == bits.Value) &&
                (!facilityCode.HasValue || x.FacilityCode == facilityCode.Value)
            ).ToList();

            dgvFormats.DataSource = filtered;
        }
        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            txtFormatNumberFilter.Clear();
            txtBitsFilter.Clear();
            txtFacilityCodeFilter.Clear();
            await LoadData();
        }
        private async void btnAdd_Click(object sender, EventArgs e)
        {
            var dto = new CreateWiegandFormatRequest();

            if (!TryEditWiegand(dto, isEdit: false, existingFormats: _data, out var createDto))
                return;

            try
            {
                await _apiService.CreateWiegandFormatAsync(createDto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(WiegandErrorHelper.GetMessage(ex));
                return;
            }

            await LoadData();
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvFormats.CurrentRow == null)
                return;

            var item = dgvFormats.CurrentRow.DataBoundItem as WiegandDto;
            if (item == null)
                return;

            var updateDto = new UpdateWiegandFormatRequest
            {
                Name = item.Name,
                Bits = item.Bits,
                FacilityCode = item.FacilityCode,
                Flags = item.Flags,
                PeLen = item.PeLen,
                PeLoc = item.PeLoc,
                PoLen = item.PoLen,
                PoLoc = item.PoLoc,
                FcLen = item.FcLen,
                FcLoc = item.FcLoc,
                ChLen = item.ChLen,
                ChLoc = item.ChLoc,
                IcLen = item.IcLen,
                IcLoc = item.IcLoc
            };

            if (!TryEditWiegand(updateDto, item.FormatNumber, isEdit: true, existingFormats: _data, out var edited))
                return;

            try
            {
                await _apiService.UpdateWiegandFormatByFormatNumberAsync(item.FormatNumber, edited);
            }
            catch (Exception ex)
            {
                MessageBox.Show(WiegandErrorHelper.GetMessage(ex));
                return;
            }

            await LoadData();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvFormats.CurrentRow == null)
                return;

            var item = dgvFormats.CurrentRow.DataBoundItem as WiegandDto;
            if (item == null)
                return;

            var confirm = MessageBox.Show(
                $"Delete Wiegand format {item.FormatNumber}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes)
                return;

            try
            {
                await _apiService.DeleteByFormatNumberAsync(item.FormatNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show(WiegandErrorHelper.GetMessage(ex));
                return;
            }

            await LoadData();
        }

        private bool TryEditWiegand(CreateWiegandFormatRequest dto, bool isEdit, List<WiegandDto> existingFormats, out CreateWiegandFormatRequest result)
        {
            result = dto;
            using var dialog = BuildWiegandDialog(
                "Wiegand Format",
                dto.FormatNumber,
                dto.Name,
                dto.Bits,
                dto.FacilityCode,
                dto.Flags,
                dto.PeLen,
                dto.PeLoc,
                dto.PoLen,
                dto.PoLoc,
                dto.FcLen,
                dto.FcLoc,
                dto.ChLen,
                dto.ChLoc,
                dto.IcLen,
                dto.IcLoc,
                isEdit,
                out var controls);

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return false;

            if (!TryParseWiegandForm(controls, existingFormats, isEdit, out var parsed))
                return false;

            result = parsed;

            return true;
        }

        private bool TryEditWiegand(UpdateWiegandFormatRequest dto, short formatNumber, bool isEdit, List<WiegandDto> existingFormats, out UpdateWiegandFormatRequest result)
        {
            result = dto;
            using var dialog = BuildWiegandDialog(
                "Wiegand Format",
                formatNumber,
                dto.Name,
                dto.Bits ?? 0,
                dto.FacilityCode ?? 0,
                dto.Flags,
                dto.PeLen,
                dto.PeLoc,
                dto.PoLen,
                dto.PoLoc,
                dto.FcLen,
                dto.FcLoc,
                dto.ChLen,
                dto.ChLoc,
                dto.IcLen,
                dto.IcLoc,
                isEdit,
                out var controls);

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return false;

            if (!TryParseWiegandForm(controls, existingFormats, isEdit, out var parsed))
                return false;

            result = new UpdateWiegandFormatRequest
            {
                FormatNumber = parsed.FormatNumber,
                Name = parsed.Name,
                Bits = parsed.Bits,
                FacilityCode = parsed.FacilityCode,
                Flags = parsed.Flags,
                PeLen = parsed.PeLen,
                PeLoc = parsed.PeLoc,
                PoLen = parsed.PoLen,
                PoLoc = parsed.PoLoc,
                FcLen = parsed.FcLen,
                FcLoc = parsed.FcLoc,
                ChLen = parsed.ChLen,
                ChLoc = parsed.ChLoc,
                IcLen = parsed.IcLen,
                IcLoc = parsed.IcLoc
            };

            return true;
        }

        private static Form BuildWiegandDialog(
            string title,
            short formatNumber,
            string name,
            short bits,
            short facilityCode,
            short? flags,
            short? peLen,
            short? peLoc,
            short? poLen,
            short? poLoc,
            short? fcLen,
            short? fcLoc,
            short? chLen,
            short? chLoc,
            short? icLen,
            short? icLoc,
            bool isEdit,
            out WiegandDialogControls controls)
        {
            var dialog = new Form
            {
                Text = title,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                Width = 520,
                Height = 520
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 16,
                Padding = new Padding(10),
                AutoSize = true
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

            controls = new WiegandDialogControls
            {
                FormatNumber = BuildNumberTextBox(formatNumber, isEdit, "0-7"),
                Name = BuildTextBox(name, "Wiegand-26"),
                Bits = BuildNumberTextBox(bits, false, "e.g., 26"),
                FacilityCode = BuildNumberTextBox(facilityCode, false, "0 or -1"),
                Flags = BuildNumberTextBox(flags, false, "0"),
                PeLen = BuildNumberTextBox(peLen, false, "13"),
                PeLoc = BuildNumberTextBox(peLoc, false, "0"),
                PoLen = BuildNumberTextBox(poLen, false, "13"),
                PoLoc = BuildNumberTextBox(poLoc, false, "13"),
                FcLen = BuildNumberTextBox(fcLen, false, "8"),
                FcLoc = BuildNumberTextBox(fcLoc, false, "1"),
                ChLen = BuildNumberTextBox(chLen, false, "16"),
                ChLoc = BuildNumberTextBox(chLoc, false, "9"),
                IcLen = BuildNumberTextBox(icLen, false, "0"),
                IcLoc = BuildNumberTextBox(icLoc, false, "0")
            };

            controls.FormatNumber.KeyPress += DigitsOnly_KeyPress;

            AddRow(layout, "Format Number", controls.FormatNumber);
            AddRow(layout, "Name", controls.Name);
            AddRow(layout, "Bits", controls.Bits);
            AddRow(layout, "Facility Code", controls.FacilityCode);
            AddRow(layout, "Flags", controls.Flags);
            AddRow(layout, "PE Length", controls.PeLen);
            AddRow(layout, "PE Location", controls.PeLoc);
            AddRow(layout, "PO Length", controls.PoLen);
            AddRow(layout, "PO Location", controls.PoLoc);
            AddRow(layout, "FC Length", controls.FcLen);
            AddRow(layout, "FC Location", controls.FcLoc);
            AddRow(layout, "CH Length", controls.ChLen);
            AddRow(layout, "CH Location", controls.ChLoc);
            AddRow(layout, "IC Length", controls.IcLen);
            AddRow(layout, "IC Location", controls.IcLoc);

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                FlowDirection = FlowDirection.RightToLeft,
                Height = 40
            };
            var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Width = 80 };
            var cancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Width = 80 };
            buttons.Controls.Add(ok);
            buttons.Controls.Add(cancel);

            dialog.Controls.Add(layout);
            dialog.Controls.Add(buttons);
            dialog.AcceptButton = ok;
            dialog.CancelButton = cancel;

            return dialog;
        }

        private static TextBox BuildNumberTextBox(short? value, bool readOnly, string placeholder)
        {
            return new TextBox
            {
                Text = value.HasValue ? value.Value.ToString() : string.Empty,
                Dock = DockStyle.Fill,
                ReadOnly = readOnly,
                PlaceholderText = placeholder ?? string.Empty
            };
        }

        private static TextBox BuildTextBox(string value, string placeholder)
        {
            return new TextBox
            {
                Text = value ?? string.Empty,
                Dock = DockStyle.Fill,
                PlaceholderText = placeholder ?? string.Empty
            };
        }

        private static void AddRow(TableLayoutPanel layout, string labelText, Control control)
        {
            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            layout.Controls.Add(label);
            layout.Controls.Add(control);
        }

        private static DataGridViewTextBoxColumn BuildTextColumn(
            string property,
            string header,
            int fillWeight,
            DataGridViewContentAlignment alignment)
        {
            return new DataGridViewTextBoxColumn
            {
                DataPropertyName = property,
                HeaderText = header,
                FillWeight = fillWeight,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = alignment
                }
            };
        }

        private class WiegandDialogControls
        {
            public TextBox FormatNumber { get; set; }
            public TextBox Name { get; set; }
            public TextBox Bits { get; set; }
            public TextBox FacilityCode { get; set; }
            public TextBox Flags { get; set; }
            public TextBox PeLen { get; set; }
            public TextBox PeLoc { get; set; }
            public TextBox PoLen { get; set; }
            public TextBox PoLoc { get; set; }
            public TextBox FcLen { get; set; }
            public TextBox FcLoc { get; set; }
            public TextBox ChLen { get; set; }
            public TextBox ChLoc { get; set; }
            public TextBox IcLen { get; set; }
            public TextBox IcLoc { get; set; }
        }

        private static bool TryParseFilter(string raw, string label, out short? value)
        {
            value = null;
            if (string.IsNullOrWhiteSpace(raw))
                return true;

            if (!short.TryParse(raw.Trim(), out var parsed))
            {
                MessageBox.Show($"{label} must contain numbers only.");
                return false;
            }

            value = parsed;
            return true;
        }

        private static bool TryParseWiegandForm(
            WiegandDialogControls controls,
            List<WiegandDto> existingFormats,
            bool isEdit,
            out CreateWiegandFormatRequest result)
        {
            result = new CreateWiegandFormatRequest();

            if (string.IsNullOrWhiteSpace(controls.Name.Text))
            {
                MessageBox.Show("Name is required.");
                return false;
            }

            if (!TryParseRequiredShort(controls.FormatNumber.Text, "Format Number", out var formatNumber))
                return false;
            if (!TryParseRequiredShort(controls.Bits.Text, "Bits", out var bits))
                return false;
            if (!TryParseRequiredShort(controls.FacilityCode.Text, "Facility Code", out var facilityCode))
                return false;
            if (!TryParseOptionalShort(controls.Flags.Text, "Flags", out var flags))
                return false;
            if (!TryParseOptionalShort(controls.PeLen.Text, "PE Length", out var peLen))
                return false;
            if (!TryParseOptionalShort(controls.PeLoc.Text, "PE Location", out var peLoc))
                return false;
            if (!TryParseOptionalShort(controls.PoLen.Text, "PO Length", out var poLen))
                return false;
            if (!TryParseOptionalShort(controls.PoLoc.Text, "PO Location", out var poLoc))
                return false;
            if (!TryParseOptionalShort(controls.FcLen.Text, "FC Length", out var fcLen))
                return false;
            if (!TryParseOptionalShort(controls.FcLoc.Text, "FC Location", out var fcLoc))
                return false;
            if (!TryParseOptionalShort(controls.ChLen.Text, "CH Length", out var chLen))
                return false;
            if (!TryParseOptionalShort(controls.ChLoc.Text, "CH Location", out var chLoc))
                return false;
            if (!TryParseOptionalShort(controls.IcLen.Text, "IC Length", out var icLen))
                return false;
            if (!TryParseOptionalShort(controls.IcLoc.Text, "IC Location", out var icLoc))
                return false;

            if (formatNumber < 0 || formatNumber > 7)
            {
                MessageBox.Show("Format Number must be a number between 0 and 7.");
                return false;
            }

            // Check for uniqueness only when adding (not editing)
            if (!isEdit && existingFormats?.Any(x => x.FormatNumber == formatNumber) == true)
            {
                MessageBox.Show("Format Number already exists. Please enter a unique number between 0 and 7.");
                return false;
            }

            // Check max 8 formats total
            if (!isEdit && existingFormats?.Count >= 8)
            {
                MessageBox.Show("Maximum of 8 card formats can be added.");
                return false;
            }

            if (bits <= 0)
            {
                MessageBox.Show("Bits must be greater than zero.");
                return false;
            }
            if (facilityCode < -1)
            {
                MessageBox.Show("Facility Code must be -1 or a non-negative value.");
                return false;
            }

            result = new CreateWiegandFormatRequest
            {
                FormatNumber = formatNumber,
                Name = controls.Name.Text.Trim(),
                Bits = bits,
                FacilityCode = facilityCode,
                Flags = flags,
                PeLen = peLen,
                PeLoc = peLoc,
                PoLen = poLen,
                PoLoc = poLoc,
                FcLen = fcLen,
                FcLoc = fcLoc,
                ChLen = chLen,
                ChLoc = chLoc,
                IcLen = icLen,
                IcLoc = icLoc
            };

            return true;
        }

        private static bool TryParseRequiredShort(string raw, string label, out short value)
        {
            value = 0;
            if (!short.TryParse(raw?.Trim(), out value))
            {
                MessageBox.Show($"{label} must contain numbers only.");
                return false;
            }

            return true;
        }

        private static bool TryParseOptionalShort(string raw, string label, out short? value)
        {
            value = null;
            if (string.IsNullOrWhiteSpace(raw))
                return true;

            if (!short.TryParse(raw.Trim(), out var parsed))
            {
                MessageBox.Show($"{label} must contain numbers only.");
                return false;
            }

            value = parsed;
            return true;
        }

        private void ApplyButtonStyles()
        {
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnAdd, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnEdit, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnDelete, 90);
            Helpers.UIStyleHelper.StyleOutlineToolbarButton(btnRefresh, 90);
        }

        private static void DigitsOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        private void AlignActionButtons(int spacing)
        {
            int top = 10;
            btnAdd.Location = new Point(220, top);
            btnEdit.Location = new Point(btnAdd.Right + spacing, top);
            btnDelete.Location = new Point(btnEdit.Right + spacing, top);
            btnRefresh.Location = new Point(btnDelete.Right + spacing, top);
        }
    }
}
