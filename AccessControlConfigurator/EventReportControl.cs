using AccessControlSystem.Models;
using AccessControlSystem.Services;
using ClosedXML.Excel;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class EventReportControl : UserControl
    {
        private readonly ApiService _apiService = new ApiService();
        private List<EventReportItem> _allRows = new List<EventReportItem>();
        private int _pageNumber = 1;
        private int _pageSize = 500;
        private int _totalPages = 1;
        private int _totalCount = 0;
        private bool _filterByCreatedDate = false;
        private DateTime? _startDate;
        private DateTime? _endDate;
        private List<string> _cardNumbers = new List<string>();
        private string _timeDisplayMode = "UTC";
        private ComboBox cmbTimeDisplay;
        private Label lblTimeDisplay;

        public EventReportControl()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            InitializeComponent();
            InitializeTimeDisplayDropdown();
            InitializeGrid();
            InitializeColumnsChooser();
            InitializePagination();

            btnReload.Click += async (s, e) => await LoadEventsAsync();
            btnExportExcel.Click += (s, e) => ExportExcel();
            btnExportPdf.Click += (s, e) => ExportPdf();
            btnApplyFilters.Click += async (s, e) => await ApplyDateFiltersAsync();
            btnSearchCardNumbers.Click += async (s, e) => await ApplyCardNumberFilterAsync();
            btnClearFilters.Click += async (s, e) => await ClearFiltersAsync();

            Load += async (s, e) => await LoadEventsAsync();
            Resize += (s, e) => AlignHeaderControls();
            Load += (s, e) => AlignHeaderControls();
        }

        private void InitializeGrid()
        {
            dgvEvents.Columns.Clear();

            dgvEvents.Columns.Add("EventDateTime", "Event Time");
            dgvEvents.Columns.Add("CardNumber", "Card Number");
            dgvEvents.Columns.Add("CardHolder", "Card Holder");
            dgvEvents.Columns.Add("ControllerName", "Controller Name");
            dgvEvents.Columns.Add("AcrName", "ACR Name");
            dgvEvents.Columns.Add("EventDescription", "Event Description");
            dgvEvents.Columns.Add("EventDetails", "Event Details");
            dgvEvents.Columns.Add("ScpId", "SCP ID");
            dgvEvents.Columns.Add("AcrNumber", "ACR Number");
            dgvEvents.Columns.Add("CreatedAt", "Created At");

            Helpers.GridStyleHelper.ApplyStandardStyle(dgvEvents);
            dgvEvents.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvEvents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }

        private void InitializeColumnsChooser()
        {
            chkColEventTime.CheckedChanged += (_, __) => ApplyColumnVisibility();
            chkColCardNumber.CheckedChanged += (_, __) => ApplyColumnVisibility();
            chkColControllerName.CheckedChanged += (_, __) => ApplyColumnVisibility();
            chkColScpId.CheckedChanged += (_, __) => ApplyColumnVisibility();
            chkColEventDescription.CheckedChanged += (_, __) => ApplyColumnVisibility();
            chkColCreatedAt.CheckedChanged += (_, __) => ApplyColumnVisibility();

            ApplyColumnVisibility();
        }

        private void InitializeTimeDisplayDropdown()
        {
            lblTimeDisplay = new Label();
            lblTimeDisplay.Text = "Time Display";
            lblTimeDisplay.AutoSize = true;

            cmbTimeDisplay = new ComboBox();
            cmbTimeDisplay.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeDisplay.Items.AddRange(new object[] { "UTC", "Local" });
            cmbTimeDisplay.SelectedIndex = 0;
            cmbTimeDisplay.Width = 100;
            cmbTimeDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            panelHeader.Controls.Add(lblTimeDisplay);
            panelHeader.Controls.Add(cmbTimeDisplay);

            cmbTimeDisplay.SelectedIndexChanged += (s, e) =>
            {
                _timeDisplayMode = cmbTimeDisplay.SelectedItem?.ToString() ?? "UTC";
                BindGrid(_allRows);
            };
        }

        private async System.Threading.Tasks.Task LoadEventsAsync()
        {
            try
            {
                var request = BuildFilterRequest();

                var response = await _apiService.GetEventReportAsync(request);
                _allRows = response.data ?? new List<EventReportItem>();

                BindGrid(_allRows);
                UpdatePagination(response.pagination);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private EventReportFilterRequest BuildFilterRequest()
        {
            return new EventReportFilterRequest
            {
                pageNumber = _pageNumber,
                pageSize = _pageSize,
                isFilterByCreatedDate = _filterByCreatedDate,
                startDate = _startDate?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                endDate = _endDate?.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                cardNumbers = _cardNumbers
            };
        }

        private void BindGrid(IEnumerable<EventReportItem> data)
        {
            dgvEvents.Rows.Clear();

            foreach (var row in data)
            {
                dgvEvents.Rows.Add(
                    FormatDateTime(row.eventDateTime),
                    row.cardNumber,
                    row.cardHolder,
                    row.controllerName,
                    row.acrName,
                    row.eventDescription,
                    row.eventDetails,
                    row.scpId,
                    row.acrNumber,
                    FormatDateTime(row.createdAt));
            }

            ApplyColumnVisibility();
        }

        private void ApplyColumnVisibility()
        {
            SetColumnVisible("EventDateTime", chkColEventTime.Checked);
            SetColumnVisible("CardNumber", chkColCardNumber.Checked);
            SetColumnVisible("ControllerName", chkColControllerName.Checked);
            SetColumnVisible("ScpId", chkColScpId.Checked);
            SetColumnVisible("EventDescription", chkColEventDescription.Checked);
            SetColumnVisible("CreatedAt", chkColCreatedAt.Checked);
        }

        private void SetColumnVisible(string columnName, bool visible)
        {
            if (dgvEvents.Columns.Contains(columnName))
                dgvEvents.Columns[columnName].Visible = visible;
        }

        private List<DataGridViewColumn> GetVisibleColumns()
        {
            return dgvEvents.Columns
                .Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .ToList();
        }

        private List<EventReportItem> GetVisibleRows()
        {
            var rows = new List<EventReportItem>();

            foreach (DataGridViewRow row in dgvEvents.Rows)
            {
                if (row.IsNewRow)
                    continue;

                rows.Add(new EventReportItem
                {
                    eventDateTime = row.Cells["EventDateTime"].Value?.ToString(),
                    cardNumber = row.Cells["CardNumber"].Value?.ToString(),
                    cardHolder = row.Cells["CardHolder"].Value?.ToString(),
                    controllerName = row.Cells["ControllerName"].Value?.ToString(),
                    acrName = row.Cells["AcrName"].Value?.ToString(),
                    eventDescription = row.Cells["EventDescription"].Value?.ToString(),
                    eventDetails = row.Cells["EventDetails"].Value?.ToString(),
                    scpId = TryGetInt(row.Cells["ScpId"].Value?.ToString()),
                    acrNumber = TryGetInt(row.Cells["AcrNumber"].Value?.ToString()),
                    createdAt = row.Cells["CreatedAt"].Value?.ToString()
                });
            }

            return rows;
        }

        private void ExportExcel()
        {
            var columns = GetVisibleColumns();
            var rows = GetVisibleRows();

            if (rows.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = "event-report.xlsx"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            using var workbook = new XLWorkbook();
            var sheet = workbook.Worksheets.Add("Event Report");

            for (int i = 0; i < columns.Count; i++)
            {
                sheet.Cell(1, i + 1).Value = columns[i].HeaderText;
            }

            for (int r = 0; r < rows.Count; r++)
            {
                var row = rows[r];
                for (int c = 0; c < columns.Count; c++)
                {
                    var header = columns[c].HeaderText;
                    sheet.Cell(r + 2, c + 1).Value = GetValueByHeader(row, header);
                }
            }

            sheet.Columns().AdjustToContents();
            workbook.SaveAs(dialog.FileName);
            MessageBox.Show("Excel exported successfully.");
        }

        private void ExportPdf()
        {
            var columns = GetVisibleColumns();
            var rows = GetVisibleRows();

            if (rows.Count == 0)
            {
                MessageBox.Show("No data to export.");
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Filter = "PDF Document (*.pdf)|*.pdf",
                FileName = "event-report.pdf"
            };

            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            var filePath = dialog.FileName;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Size(PageSizes.A4.Landscape());
                    page.Content().Column(col =>
                    {
                        col.Item().Text("Event Report").FontSize(16).SemiBold();
                        col.Item().Text(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columnsDef =>
                            {
                                foreach (var _ in columns)
                                    columnsDef.RelativeColumn();
                            });

                            table.Header(header =>
                            {
                                foreach (var column in columns)
                                {
                                    header.Cell()
                                        .Background(Colors.Grey.Lighten3)
                                        .Padding(4)
                                        .Text(column.HeaderText)
                                        .SemiBold();
                                }
                            });

                            foreach (var row in rows)
                            {
                                foreach (var column in columns)
                                {
                                    var value = GetValueByHeader(row, column.HeaderText);
                                    table.Cell().Padding(4).Text(value);
                                }
                            }
                        });
                    });
                });
            }).GeneratePdf(filePath);

            MessageBox.Show("PDF exported successfully.");
        }

        private string GetValueByHeader(EventReportItem row, string header)
        {
            return header switch
            {
                "Event Time" => FormatDateTime(row.eventDateTime),
                "Card Number" => row.cardNumber ?? string.Empty,
                "Card Holder" => row.cardHolder ?? string.Empty,
                "Controller Name" => row.controllerName ?? string.Empty,
                "ACR Name" => row.acrName ?? string.Empty,
                "Event Description" => row.eventDescription ?? string.Empty,
                "Event Details" => row.eventDetails ?? string.Empty,
                "SCP ID" => row.scpId.ToString(),
                "ACR Number" => row.acrNumber.ToString(),
                "Created At" => FormatDateTime(row.createdAt),
                _ => string.Empty
            };
        }

        private static int TryGetInt(string value)
        {
            return int.TryParse(value, out var parsed) ? parsed : 0;
        }

        private string FormatDateTime(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            try
            {
                var normalized = value.Trim();

                bool hasTimezone =
                    normalized.EndsWith("Z", StringComparison.OrdinalIgnoreCase) ||
                    HasExplicitOffset(normalized);

                if (!hasTimezone)
                {
                    normalized = normalized.Replace(' ', 'T') + "Z";
                }

                if (!DateTimeOffset.TryParse(normalized, out var parsed))
                    return value;

                if (string.Equals(_timeDisplayMode, "Local", StringComparison.OrdinalIgnoreCase))
                    return parsed.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

                return parsed.UtcDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
                return value;
            }
        }

        private static bool HasExplicitOffset(string value)
        {
            int tIndex = value.IndexOf('T');
            if (tIndex < 0)
                tIndex = value.IndexOf(' ');

            if (tIndex < 0 || tIndex >= value.Length - 1)
                return false;

            string timePart = value.Substring(tIndex + 1);
            return timePart.Contains("+") || timePart.LastIndexOf('-') > 1;
        }

        private void InitializePagination()
        {
            cmbPageSize.Items.Clear();
            cmbPageSize.Items.AddRange(new object[] { 50, 100, 200, 500 });
            cmbPageSize.SelectedItem = _pageSize;

            cmbPageSize.SelectedIndexChanged += async (s, e) =>
            {
                if (cmbPageSize.SelectedItem is int size)
                {
                    _pageSize = size;
                    _pageNumber = 1;
                    await LoadEventsAsync();
                }
            };

            btnPrevPage.Click += async (s, e) =>
            {
                if (_pageNumber <= 1)
                    return;

                _pageNumber--;
                await LoadEventsAsync();
            };

            btnNextPage.Click += async (s, e) =>
            {
                if (_pageNumber >= _totalPages)
                    return;

                _pageNumber++;
                await LoadEventsAsync();
            };

            UpdatePagination(null);
        }

        private void UpdatePagination(EventReportPagination pagination)
        {
            if (pagination != null)
            {
                _pageNumber = pagination.pageNumber;
                _pageSize = pagination.pageSize;
                _totalPages = Math.Max(1, pagination.totalPages);
                _totalCount = pagination.totalCount;
            }
            else
            {
                _totalPages = Math.Max(1, _totalPages);
            }

            lblPageInfo.Text = $"Page {_pageNumber} of {_totalPages}";
            btnPrevPage.Enabled = _pageNumber > 1;
            btnNextPage.Enabled = _pageNumber < _totalPages;

            if (cmbPageSize.SelectedItem is int size && size != _pageSize)
                cmbPageSize.SelectedItem = _pageSize;
        }

        private void AlignHeaderControls()
        {
            if (panelHeader == null || panelActions == null || panelPagination == null)
                return;

            int rightPadding = 14;
            int spacing = 12;
            int paginationWidth = panelPagination.Width;

            panelPagination.Left = panelHeader.ClientSize.Width - rightPadding - paginationWidth;

            int maxActionsWidth = panelPagination.Left - panelActions.Left - spacing;
            panelActions.Width = Math.Max(200, maxActionsWidth);

            int rowHeight = 28;
            int baseFiltersTop = 120;
            int filtersTop = baseFiltersTop;
            int rowRight = panelHeader.ClientSize.Width - rightPadding;

            chkFilterByCreatedDate.Left = 14;
            chkFilterByCreatedDate.Top = filtersTop + 3;

            lblStartDate.Left = chkFilterByCreatedDate.Right + spacing;
            lblStartDate.Top = filtersTop + 3;
            dtStartDate.Left = lblStartDate.Right + spacing;
            dtStartDate.Top = filtersTop;

            lblEndDate.Left = dtStartDate.Right + spacing;
            lblEndDate.Top = filtersTop + 3;
            dtEndDate.Left = lblEndDate.Right + spacing;
            dtEndDate.Top = filtersTop;

            btnApplyFilters.Left = dtEndDate.Right + spacing;
            btnApplyFilters.Top = filtersTop;

            int timeBlockWidth = (lblTimeDisplay?.PreferredWidth ?? 80) + spacing + (cmbTimeDisplay?.Width ?? 100);
            int rightGroupWidth =
                (lblCardNumbers?.Width ?? 90) +
                spacing +
                160 +
                spacing +
                btnSearchCardNumbers.Width +
                spacing +
                btnClearFilters.Width +
                spacing +
                timeBlockWidth;

            int rightGroupLeft = rowRight - rightGroupWidth;
            int leftGroupRight = btnApplyFilters.Right;
            bool wrapRightGroup = rightGroupLeft < leftGroupRight + spacing;

            int rightGroupTop = wrapRightGroup ? filtersTop + rowHeight + 8 : filtersTop;

            int timeRight = rowRight;
            cmbTimeDisplay.Left = timeRight - cmbTimeDisplay.Width;
            cmbTimeDisplay.Top = rightGroupTop;
            lblTimeDisplay.Left = cmbTimeDisplay.Left - spacing - lblTimeDisplay.PreferredWidth;
            lblTimeDisplay.Top = rightGroupTop + 4;

            btnClearFilters.Left = lblTimeDisplay.Left - spacing - btnClearFilters.Width;
            btnClearFilters.Top = rightGroupTop;
            btnSearchCardNumbers.Left = btnClearFilters.Left - spacing - btnSearchCardNumbers.Width;
            btnSearchCardNumbers.Top = rightGroupTop;

            int cardInputRight = btnSearchCardNumbers.Left - spacing;
            int cardInputWidth = Math.Max(120, cardInputRight - spacing - (lblCardNumbers?.Width ?? 90));
            txtCardNumbers.Width = Math.Min(220, cardInputWidth);
            txtCardNumbers.Left = cardInputRight - txtCardNumbers.Width;
            txtCardNumbers.Top = rightGroupTop;
            lblCardNumbers.Left = txtCardNumbers.Left - spacing - lblCardNumbers.Width;
            lblCardNumbers.Top = rightGroupTop + 4;

            int bottom = rightGroupTop + rowHeight + 8;
            panelHeader.Height = Math.Max(156, bottom);
        }

        private async System.Threading.Tasks.Task ApplyDateFiltersAsync()
        {
            _filterByCreatedDate = chkFilterByCreatedDate.Checked;
            _startDate = DateTime.SpecifyKind(dtStartDate.Value, DateTimeKind.Local).ToUniversalTime();
            _endDate = DateTime.SpecifyKind(dtEndDate.Value, DateTimeKind.Local).ToUniversalTime();

            if (_endDate <= _startDate)
            {
                MessageBox.Show("End date must be greater than start date.");
                return;
            }

            _pageNumber = 1;
            await LoadEventsAsync();
        }

        private async System.Threading.Tasks.Task ApplyCardNumberFilterAsync()
        {
            var raw = txtCardNumbers.Text;
            _cardNumbers = ParseCardNumbers(raw);

            if (_cardNumbers.Count == 0)
            {
                MessageBox.Show("Enter at least one card number.");
                txtCardNumbers.Focus();
                return;
            }

            if (_cardNumbers.Any(n => !long.TryParse(n, out _)))
            {
                MessageBox.Show("Card numbers must be numeric.");
                txtCardNumbers.Focus();
                return;
            }

            _pageNumber = 1;
            await LoadEventsAsync();
        }

        private static List<string> ParseCardNumbers(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
                return new List<string>();

            return raw
                .Split(new[] { ',', ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private async System.Threading.Tasks.Task ClearFiltersAsync()
        {
            chkFilterByCreatedDate.Checked = false;
            txtCardNumbers.Text = string.Empty;
            _filterByCreatedDate = false;
            _cardNumbers = new List<string>();

            var now = DateTime.Now;
            dtStartDate.Value = now.AddDays(-1);
            dtEndDate.Value = now;
            _startDate = DateTime.SpecifyKind(dtStartDate.Value, DateTimeKind.Local).ToUniversalTime();
            _endDate = DateTime.SpecifyKind(dtEndDate.Value, DateTimeKind.Local).ToUniversalTime();

            _pageNumber = 1;
            await LoadEventsAsync();
        }
    }
}
