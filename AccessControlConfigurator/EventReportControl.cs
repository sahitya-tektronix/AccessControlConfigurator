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

        public EventReportControl()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            InitializeComponent();
            InitializeGrid();
            InitializeColumnsChooser();

            btnReload.Click += async (s, e) => await LoadEventsAsync();
            btnExportExcel.Click += (s, e) => ExportExcel();
            btnExportPdf.Click += (s, e) => ExportPdf();

            Load += async (s, e) => await LoadEventsAsync();
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

        private async System.Threading.Tasks.Task LoadEventsAsync()
        {
            try
            {
                var request = BuildFilterRequest();

                var response = await _apiService.GetEventReportAsync(request);
                _allRows = response.data ?? new List<EventReportItem>();

                BindGrid(_allRows);
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
                pageNumber = 1,
                pageSize = 100,
                isFilterByCreatedDate = false
            };
        }

        private void BindGrid(IEnumerable<EventReportItem> data)
        {
            dgvEvents.Rows.Clear();
            foreach (var row in data)
            {
                dgvEvents.Rows.Add(
                    row.eventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                    row.cardNumber,
                    row.cardHolder,
                    row.controllerName,
                    row.acrName,
                    row.eventDescription,
                    row.eventDetails,
                    row.scpId,
                    row.acrNumber,
                    row.createdAt.ToString("yyyy-MM-dd HH:mm:ss"));
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
                    eventDateTime = DateTime.TryParse(row.Cells["EventDateTime"].Value?.ToString(), out var t)
                        ? t
                        : DateTime.MinValue,
                    cardNumber = row.Cells["CardNumber"].Value?.ToString(),
                    cardHolder = row.Cells["CardHolder"].Value?.ToString(),
                    controllerName = row.Cells["ControllerName"].Value?.ToString(),
                    acrName = row.Cells["AcrName"].Value?.ToString(),
                    eventDescription = row.Cells["EventDescription"].Value?.ToString(),
                    eventDetails = row.Cells["EventDetails"].Value?.ToString(),
                    scpId = TryGetInt(row.Cells["ScpId"].Value?.ToString()),
                    acrNumber = TryGetInt(row.Cells["AcrNumber"].Value?.ToString()),
                    createdAt = DateTime.TryParse(row.Cells["CreatedAt"].Value?.ToString(), out var created)
                        ? created
                        : DateTime.MinValue
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
                                    header.Cell().Background(Colors.Grey.Lighten3)
                                        .Padding(4).Text(column.HeaderText).SemiBold();
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

        private static string GetValueByHeader(EventReportItem row, string header)
        {
            return header switch
            {
                "Event Time" => row.eventDateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                "Card Number" => row.cardNumber ?? string.Empty,
                "Card Holder" => row.cardHolder ?? string.Empty,
                "Controller Name" => row.controllerName ?? string.Empty,
                "ACR Name" => row.acrName ?? string.Empty,
                "Event Description" => row.eventDescription ?? string.Empty,
                "Event Details" => row.eventDetails ?? string.Empty,
                "SCP ID" => row.scpId.ToString(),
                "ACR Number" => row.acrNumber.ToString(),
                "Created At" => row.createdAt.ToString("yyyy-MM-dd HH:mm:ss"),
                _ => string.Empty
            };
        }

        private static int TryGetInt(string value)
        {
            return int.TryParse(value, out var parsed) ? parsed : 0;
        }

    }
}
