using AccessControlSystem.Models;
using AccessControlSystem.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class EditTimezoneForm : Form
    {
        private readonly ApiService _apiService = new ApiService();
        private readonly TimezoneDto _timezone;

        private readonly string[] _scheduleDays =
        {
            "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"
        };

        public EditTimezoneForm(TimezoneDto timezone)
        {
            InitializeComponent();
            _timezone = timezone ?? throw new ArgumentNullException(nameof(timezone));

            cmbMode.Items.Clear();
            cmbMode.Items.Add("Always (1)");
            cmbMode.Items.Add("Scheduled (0)");
            cmbMode.Items.Add("Scan (2)");

            dtStart.Format = DateTimePickerFormat.Time;
            dtStart.ShowUpDown = true;
            dtEnd.Format = DateTimePickerFormat.Time;
            dtEnd.ShowUpDown = true;

            btnClearAll.Click += btnClearAll_Click;
            btnExample.Click  += btnExample_Click;
            btnCancel.Click   += btnCancel_Click;

            dtStart.ValueChanged += ScheduleInputs_ValueChanged;
            dtEnd.ValueChanged   += ScheduleInputs_ValueChanged;
            cmbMode.SelectedIndexChanged += cmbMode_SelectedIndexChanged;

            pnlSchedule.Paint      += pnlSchedule_Paint;
            pnlSchedule.MouseClick += pnlSchedule_MouseClick;

            HideLegacyDayCheckboxes();
            LoadTimezoneValues();
            ApplyModeUI();
            InvalidateSchedule();
        }

        private void HideLegacyDayCheckboxes()
        {
            chkMonday.Visible = chkTuesday.Visible = chkWednesday.Visible =
            chkThursday.Visible = chkFriday.Visible = chkSaturday.Visible =
            chkSunday.Visible = chkPublicHoliday.Visible = false;
        }

        private void ScheduleInputs_ValueChanged(object sender, EventArgs e) => InvalidateSchedule();
        private void InvalidateSchedule() => pnlSchedule.Invalidate();

        private void LoadTimezoneValues()
        {
            txtName.Text = _timezone.name ?? string.Empty;
            numNumber.Value = ClampToRange(_timezone.number <= 0 ? 1 : _timezone.number, numNumber.Minimum, numNumber.Maximum);

            cmbMode.SelectedIndex = _timezone.mode switch
            {
                1 => 0, 0 => 1, 2 => 2, _ => 0
            };

            int safeStart, safeEnd;
            if (_timezone.mode == 1) // Always
            {
                safeStart = 0;
                safeEnd = 1439;
            }
            else
            {
                safeStart = Math.Max(0, Math.Min(1439, _timezone.iStart));
                safeEnd = _timezone.iEnd > safeStart ? _timezone.iEnd : safeStart + 60;
                safeEnd = Math.Max(1, Math.Min(1439, safeEnd));
            }

            dtStart.Value = DateTime.Today.AddMinutes(safeStart);
            dtEnd.Value   = DateTime.Today.AddMinutes(safeEnd);

            LoadDayMask(_timezone.iDays);

            lblHeader.Text = string.IsNullOrWhiteSpace(_timezone.name)
                ? "Edit Time Zone"
                : $"Edit Time Zone — {_timezone.name}";
        }

        private decimal ClampToRange(int value, decimal min, decimal max)
        {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        private void LoadDayMask(int mask)
        {
            chkMonday.Checked    = (mask & (1 << 0)) != 0;
            chkTuesday.Checked   = (mask & (1 << 1)) != 0;
            chkWednesday.Checked = (mask & (1 << 2)) != 0;
            chkThursday.Checked  = (mask & (1 << 3)) != 0;
            chkFriday.Checked    = (mask & (1 << 4)) != 0;
            chkSaturday.Checked  = (mask & (1 << 5)) != 0;
            chkSunday.Checked    = (mask & (1 << 6)) != 0;
            chkPublicHoliday.Checked = (mask & (1 << 7)) != 0;
        }

        private int BuildDayMask()
        {
            int mask = 0;
            if (chkMonday.Checked)       mask |= 1 << 0;
            if (chkTuesday.Checked)      mask |= 1 << 1;
            if (chkWednesday.Checked)    mask |= 1 << 2;
            if (chkThursday.Checked)     mask |= 1 << 3;
            if (chkFriday.Checked)       mask |= 1 << 4;
            if (chkSaturday.Checked)     mask |= 1 << 5;
            if (chkSunday.Checked)       mask |= 1 << 6;
            if (chkPublicHoliday.Checked)mask |= 1 << 7;
            return mask;
        }

        private int TimeToMinutes(DateTime dt) => (dt.Hour * 60) + dt.Minute;

        private bool HasAnyDaySelected() =>
            chkMonday.Checked || chkTuesday.Checked || chkWednesday.Checked ||
            chkThursday.Checked || chkFriday.Checked || chkSaturday.Checked ||
            chkSunday.Checked || chkPublicHoliday.Checked;

        private CheckBox GetDayCheckboxByIndex(int index) => index switch
        {
            0 => chkMonday,    1 => chkTuesday,  2 => chkWednesday,
            3 => chkThursday,  4 => chkFriday,   5 => chkSaturday,
            6 => chkSunday,    7 => chkPublicHoliday,
            _ => null
        };

        private int GetSelectedModeValue() => cmbMode.SelectedIndex switch
        {
            0 => 1, 1 => 0, 2 => 2, _ => 1
        };

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyModeUI();
            InvalidateSchedule();
        }

        private void ApplyModeUI()
        {
            bool isAlways = cmbMode.SelectedIndex == 0;
            dtStart.Enabled = !isAlways;
            dtEnd.Enabled   = !isAlways;
            pnlSchedule.BackColor = isAlways ? Color.FromArgb(248, 250, 252) : Color.White;
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            numNumber.Value = 1;
            cmbMode.SelectedIndex = 0;
            dtStart.Value = DateTime.Today.AddHours(9);
            dtEnd.Value   = DateTime.Today.AddHours(17);
            chkMonday.Checked = chkTuesday.Checked = chkWednesday.Checked =
            chkThursday.Checked = chkFriday.Checked = chkSaturday.Checked =
            chkSunday.Checked = chkPublicHoliday.Checked = false;
            InvalidateSchedule();
        }

        private void btnExample_Click(object sender, EventArgs e)
        {
            chkMonday.Checked = chkTuesday.Checked = chkWednesday.Checked =
            chkThursday.Checked = chkFriday.Checked = true;
            chkSaturday.Checked = chkSunday.Checked = chkPublicHoliday.Checked = false;
            dtStart.Value = DateTime.Today.AddHours(9);
            dtEnd.Value   = DateTime.Today.AddHours(17);
            cmbMode.SelectedIndex = 1;
            InvalidateSchedule();
        }

        // ── Schedule painter ──────────────────────────────────────────────

        private Rectangle GetTimelineBounds()
        {
            int left = 150, top = 28, rightPadding = 30, bottomPadding = 12;
            int width  = Math.Max(300, pnlSchedule.Width  - left - rightPadding);
            int height = Math.Max(160, pnlSchedule.Height - top  - bottomPadding);
            return new Rectangle(left, top, width, height);
        }

        private int GetRowHeight()
        {
            Rectangle tl = GetTimelineBounds();
            return Math.Max(24, tl.Height / _scheduleDays.Length);
        }

        private int GetRowTop(int rowIndex) => GetTimelineBounds().Top + (rowIndex * GetRowHeight());

        private int MinutesToX(int minutes, Rectangle tl)
        {
            minutes = Math.Max(0, Math.Min(1440, minutes));
            return tl.Left + (int)Math.Round((minutes / 1440.0) * tl.Width);
        }

        private void pnlSchedule_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < _scheduleDays.Length; i++)
            {
                Rectangle clickRect = new Rectangle(0, GetRowTop(i), pnlSchedule.Width, GetRowHeight());
                if (clickRect.Contains(e.Location))
                {
                    CheckBox chk = GetDayCheckboxByIndex(i);
                    if (chk != null) { chk.Checked = !chk.Checked; InvalidateSchedule(); }
                    return;
                }
            }
        }

        private void pnlSchedule_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(pnlSchedule.BackColor);

            using Pen gridPen   = new Pen(Color.FromArgb(220, 226, 234));
            using Pen borderPen = new Pen(Color.FromArgb(190, 200, 212));
            using SolidBrush labelBrush       = new SolidBrush(Color.FromArgb(45, 62, 80));
            using SolidBrush mutedBrush        = new SolidBrush(Color.FromArgb(120, 132, 145));
            using SolidBrush barBrush          = new SolidBrush(Color.FromArgb(74, 196, 104));
            using SolidBrush selectedRowBrush  = new SolidBrush(Color.FromArgb(230, 255, 240));
            using SolidBrush rowBrush          = new SolidBrush(Color.FromArgb(248, 250, 252));
            using Font hourFont = new Font("Segoe UI", 8.5F, FontStyle.Regular);
            using Font dayFont  = new Font("Segoe UI", 9F,   FontStyle.Regular);

            Rectangle tl = GetTimelineBounds();

            int startMinutes, endMinutes;
            if (cmbMode.SelectedIndex == 0) { startMinutes = 0; endMinutes = 1440; }
            else { startMinutes = TimeToMinutes(dtStart.Value); endMinutes = TimeToMinutes(dtEnd.Value); }

            for (int h = 0; h <= 24; h += 3)
            {
                int x = MinutesToX(h * 60, tl);
                g.DrawLine(gridPen, x, 20, x, tl.Bottom + 8);
                if (h < 24)
                {
                    string text = $"{h:00}:00";
                    SizeF sz = g.MeasureString(text, hourFont);
                    g.DrawString(text, hourFont, labelBrush, x - sz.Width / 2, 2);
                }
            }

            for (int i = 0; i < _scheduleDays.Length; i++)
            {
                CheckBox chk = GetDayCheckboxByIndex(i);
                bool selected = chk != null && chk.Checked;

                Rectangle rowRect = new Rectangle(tl.Left, GetRowTop(i), tl.Width, GetRowHeight());
                g.FillRectangle(selected ? selectedRowBrush : rowBrush, rowRect);
                g.DrawRectangle(borderPen, rowRect);
                g.DrawString(_scheduleDays[i], dayFont, selected ? labelBrush : mutedBrush, 10, rowRect.Top + 3);

                if (selected && endMinutes > startMinutes)
                {
                    int x1 = MinutesToX(startMinutes, tl);
                    int x2 = MinutesToX(endMinutes, tl);
                    Rectangle activeRect = new Rectangle(x1, rowRect.Top + 3, Math.Max(8, x2 - x1), rowRect.Height - 6);
                    g.FillRectangle(barBrush, activeRect);
                    g.DrawRectangle(Pens.ForestGreen, activeRect);
                }
            }

            g.DrawRectangle(borderPen, tl);
        }

        // ── Save / Update ─────────────────────────────────────────────────

        private async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text))
                {
                    MessageBox.Show("Timezone name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                if (!HasAnyDaySelected())
                {
                    MessageBox.Show("Please select at least one day.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int startMinutes, endMinutes;
                if (cmbMode.SelectedIndex == 0) // Always
                {
                    startMinutes = 0;
                    endMinutes   = 1439;
                }
                else
                {
                    startMinutes = TimeToMinutes(dtStart.Value);
                    endMinutes   = TimeToMinutes(dtEnd.Value);
                    if (endMinutes <= startMinutes)
                    {
                        MessageBox.Show("End time must be later than start time.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                var dto = new TimezoneUpdateRequest
                {
                    id        = _timezone.id,
                    name      = txtName.Text.Trim(),
                    number    = (int)numNumber.Value,
                    mode      = GetSelectedModeValue(),
                    intervals = 1,
                    iDays     = BuildDayMask(),
                    iStart    = startMinutes,
                    iEnd      = endMinutes,
                    actTime   = startMinutes,
                    deactTime = endMinutes
                };

                await _apiService.UpdateTimezone(_timezone.id, dto);

                MessageBox.Show("Timezone updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update timezone: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
