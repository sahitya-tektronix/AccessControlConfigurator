using System;
using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator.Helpers
{
    /// <summary>
    /// Provides consistent UI styling and responsive design utilities for all forms.
    /// Ensures buttons, labels, and inputs have uniform appearance across the application.
    /// </summary>
    public static class UIStyleHelper
    {
        // =======================
        // STANDARD SIZES
        // =======================
        public static class StandardSizes
        {
            // Button sizes
            public static readonly int ButtonHeight = 35;
            public static readonly int ButtonWidth = 100;
            public static readonly int SmallButtonWidth = 80;
            
            // Label sizes
            public static readonly int LabelHeight = 23;
            public static readonly int LabelFontSize = 10;
            
            // Input field sizes
            public static readonly int InputFieldHeight = 30;
            public static readonly int InputFieldMinWidth = 200;
            
            // Spacing
            public static readonly int Padding = 10;
            public static readonly int Margin = 8;
            public static readonly int VerticalSpacing = 20;
        }

        // =======================
        // COLORS
        // =======================
        public static class StandardColors
        {
            public static readonly Color PrimaryBlue = Color.FromArgb(0, 120, 215);
            public static readonly Color SuccessGreen = Color.Green;
            public static readonly Color DangerRed = Color.Red;
            public static readonly Color WarningOrange = Color.Orange;
            public static readonly Color HeaderBackground = Color.FromArgb(45, 62, 80);
            public static readonly Color HeaderForeground = Color.White;
            public static readonly Color LightBackground = Color.WhiteSmoke;
            public static readonly Color BorderGray = Color.LightGray;
        }

        // =======================
        // FONTS
        // =======================
        public static class StandardFonts
        {
            public static readonly Font HeaderFont = new Font("Segoe UI", 14F, FontStyle.Bold);
            public static readonly Font TitleFont = new Font("Segoe UI", 12F, FontStyle.Bold);
            public static readonly Font LabelFont = new Font("Segoe UI", 10F, FontStyle.Regular);
            public static readonly Font ButtonFont = new Font("Segoe UI", 10F, FontStyle.Regular);
            public static readonly Font InputFont = new Font("Segoe UI", 10F, FontStyle.Regular);
        }

        // =======================
        // BUTTON STYLING
        // =======================
        public static void StyleButton(Button btn, ButtonStyle style = ButtonStyle.Primary, bool isSmall = false)
        {
            if (btn == null) return;

            btn.Font = StandardFonts.ButtonFont;
            btn.Height = StandardSizes.ButtonHeight;
            btn.Width = isSmall ? StandardSizes.SmallButtonWidth : StandardSizes.ButtonWidth;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Cursor = Cursors.Hand;
            btn.AutoSize = false;

            ApplyButtonColorScheme(btn, style);
        }

        // -------------------------------------------------------
        // Module toolbar button — unified style for Add/Edit/Delete/Back/Refresh/Sync
        // All buttons same height, consistent font, flat outline look
        // -------------------------------------------------------
        public static void StyleOutlineToolbarButton(Button btn, int width = 100)
        {
            if (btn == null) return;

            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btn.Height = 36;
            btn.Width = width;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 120, 215);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(0, 120, 215);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            btn.Margin = new Padding(0, 0, 10, 0);

            AddHoverEffect(btn,
                hoverBack: Color.FromArgb(0, 120, 215), hoverFore: Color.White,
                normalBack: Color.White, normalFore: Color.FromArgb(0, 120, 215));
        }

        // Primary filled button (e.g. Add)
        public static void StylePrimaryToolbarButton(Button btn, int width = 100)
        {
            if (btn == null) return;

            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btn.Height = 36;
            btn.Width = width;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 90, 170);
            btn.BackColor = Color.FromArgb(0, 120, 215);
            btn.ForeColor = Color.White;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            btn.Margin = new Padding(0, 0, 10, 0);

            AddHoverEffect(btn,
                hoverBack: Color.FromArgb(0, 100, 190), hoverFore: Color.White,
                normalBack: Color.FromArgb(0, 120, 215), normalFore: Color.White);
        }

        // Danger outline button (Delete)
        public static void StyleDangerToolbarButton(Button btn, int width = 100)
        {
            if (btn == null) return;

            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btn.Height = 36;
            btn.Width = width;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(196, 43, 28);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(196, 43, 28);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            btn.Margin = new Padding(0, 0, 10, 0);

            AddHoverEffect(btn,
                hoverBack: Color.FromArgb(196, 43, 28), hoverFore: Color.White,
                normalBack: Color.White, normalFore: Color.FromArgb(196, 43, 28));
        }

        // Neutral button (Back, ClearFilter)
        public static void StyleNeutralToolbarButton(Button btn, int width = 100)
        {
            if (btn == null) return;

            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            btn.Height = 36;
            btn.Width = width;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 1;
            btn.FlatAppearance.BorderColor = Color.FromArgb(160, 160, 160);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.FromArgb(60, 60, 60);
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            btn.Margin = new Padding(0, 0, 10, 0);

            AddHoverEffect(btn,
                hoverBack: Color.FromArgb(230, 230, 230), hoverFore: Color.FromArgb(30, 30, 30),
                normalBack: Color.White, normalFore: Color.FromArgb(60, 60, 60));
        }

        // Adds hover enter/leave colour toggle. Guards against double-registration.
        private static void AddHoverEffect(Button btn, Color hoverBack, Color hoverFore, Color normalBack, Color normalFore)
        {
            if (btn.Tag is string tag && tag.Contains("hover-styled")) return;
            btn.Tag = (btn.Tag?.ToString() ?? "") + "hover-styled";

            btn.MouseEnter += (s, _) => { ((Button)s).BackColor = hoverBack; ((Button)s).ForeColor = hoverFore; };
            btn.MouseLeave += (s, _) => { ((Button)s).BackColor = normalBack; ((Button)s).ForeColor = normalFore; };
        }

        private static void ApplyButtonColorScheme(Button btn, ButtonStyle style)
        {
            switch (style)
            {
                case ButtonStyle.Primary:
                    btn.BackColor = StandardColors.PrimaryBlue;
                    btn.ForeColor = Color.White;
                    break;
                case ButtonStyle.Success:
                    btn.BackColor = StandardColors.SuccessGreen;
                    btn.ForeColor = Color.White;
                    break;
                case ButtonStyle.Danger:
                    btn.BackColor = StandardColors.DangerRed;
                    btn.ForeColor = Color.White;
                    break;
                case ButtonStyle.Warning:
                    btn.BackColor = StandardColors.WarningOrange;
                    btn.ForeColor = Color.Black;
                    break;
                default:
                    btn.BackColor = Color.FromArgb(200, 200, 200);
                    btn.ForeColor = Color.Black;
                    break;
            }
        }

        // =======================
        // LABEL STYLING
        // =======================
        public static void StyleLabel(Label lbl, LabelStyle style = LabelStyle.Regular)
        {
            if (lbl == null) return;

            lbl.AutoSize = false;
            lbl.Height = StandardSizes.LabelHeight;
            lbl.Font = StandardFonts.LabelFont;
            lbl.TextAlign = ContentAlignment.MiddleLeft;

            switch (style)
            {
                case LabelStyle.Header:
                    lbl.Font = StandardFonts.HeaderFont;
                    lbl.ForeColor = StandardColors.HeaderForeground;
                    break;
                case LabelStyle.Title:
                    lbl.Font = StandardFonts.TitleFont;
                    lbl.ForeColor = Color.Black;
                    break;
                case LabelStyle.Regular:
                default:
                    lbl.Font = StandardFonts.LabelFont;
                    lbl.ForeColor = Color.Black;
                    break;
            }
        }

        // =======================
        // INPUT FIELD STYLING
        // =======================
        public static void StyleTextBox(TextBox txt)
        {
            if (txt == null) return;

            txt.Font = StandardFonts.InputFont;
            txt.Height = StandardSizes.InputFieldHeight;
            txt.MinimumSize = new Size(StandardSizes.InputFieldMinWidth, StandardSizes.InputFieldHeight);
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = Color.White;
            txt.ForeColor = Color.Black;
            txt.Margin = new Padding(StandardSizes.Margin);
            txt.Padding = new Padding(5);
        }

        public static void StyleComboBox(ComboBox cmb)
        {
            if (cmb == null) return;

            cmb.Font = StandardFonts.InputFont;
            cmb.Height = StandardSizes.InputFieldHeight;
            cmb.MinimumSize = new Size(StandardSizes.InputFieldMinWidth, StandardSizes.InputFieldHeight);
            cmb.BackColor = Color.White;
            cmb.ForeColor = Color.Black;
            cmb.Margin = new Padding(StandardSizes.Margin);
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public static void StyleNumericUpDown(NumericUpDown num)
        {
            if (num == null) return;

            num.Font = StandardFonts.InputFont;
            num.Height = StandardSizes.InputFieldHeight;
            num.BackColor = Color.White;
            num.ForeColor = Color.Black;
            num.Margin = new Padding(StandardSizes.Margin);
        }

        // =======================
        // PANEL STYLING
        // =======================
        public static void StyleHeaderPanel(Panel pnl)
        {
            if (pnl == null) return;

            pnl.BackColor = StandardColors.HeaderBackground;
            pnl.Dock = DockStyle.Top;
            pnl.Height = 60;
        }

        public static void StyleContentPanel(Panel pnl)
        {
            if (pnl == null) return;

            pnl.BackColor = Color.White;
            pnl.Dock = DockStyle.Fill;
            pnl.AutoScroll = true;
            pnl.Padding = new Padding(StandardSizes.Padding);
        }

        public static void StyleFilterPanel(Panel pnl)
        {
            if (pnl == null) return;

            pnl.BackColor = StandardColors.LightBackground;
            pnl.BorderStyle = BorderStyle.FixedSingle;
            pnl.Padding = new Padding(StandardSizes.Padding);
        }

        // =======================
        // RESPONSIVE UTILITIES
        // =======================
        public static void MakeResponsive(Control parent)
        {
            if (parent == null) return;

            parent.Resize += (s, e) => AdjustLayoutForSize(parent);
        }

        public static void AdjustLayoutForSize(Control parent)
        {
            if (parent == null) return;

            bool isMobile = parent.Width < 768;
            
            // Adjust layout for mobile
            if (isMobile && parent is Form form)
            {
                AdjustFormForMobile(form);
            }
        }

        private static void AdjustFormForMobile(Form form)
        {
            // Reduce padding on mobile
            form.Padding = new Padding(5);

            foreach (Control control in GetAllControls(form))
            {
                if (control is Button btn)
                {
                    btn.Width = Math.Min(btn.Width, form.ClientSize.Width - 20);
                }
                else if (control is TextBox || control is ComboBox)
                {
                    control.Width = Math.Min(control.Width, form.ClientSize.Width - 20);
                }
            }
        }

        private static System.Collections.Generic.List<Control> GetAllControls(Control parent)
        {
            var controls = new System.Collections.Generic.List<Control>();
            
            foreach (Control control in parent.Controls)
            {
                controls.Add(control);
                controls.AddRange(GetAllControls(control));
            }

            return controls;
        }

        // =======================
        // TIME FORMATTING UTILITIES
        // =======================
        /// <summary>
        /// Converts an integer time value (0-86399 seconds in a day) to HH:MM:SS format.
        /// </summary>
        public static string FormatTimeFromSeconds(int seconds)
        {
            if (seconds < 0 || seconds >= 86400)
                return "00:00:00";

            int hours = seconds / 3600;
            int minutes = (seconds % 3600) / 60;
            int secs = seconds % 60;

            return $"{hours:D2}:{minutes:D2}:{secs:D2}";
        }

        /// <summary>
        /// Converts HH:MM:SS format back to integer seconds.
        /// </summary>
        public static bool TryParseTimeToSeconds(string timeString, out int seconds)
        {
            seconds = 0;
            if (string.IsNullOrWhiteSpace(timeString))
                return false;

            var parts = timeString.Split(':');
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], out var hours) ||
                !int.TryParse(parts[1], out var minutes) ||
                !int.TryParse(parts[2], out var secs))
                return false;

            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59 || secs < 0 || secs > 59)
                return false;

            seconds = hours * 3600 + minutes * 60 + secs;
            return true;
        }

        // =======================
        // CENTER AND ALIGN
        // =======================
        public static void CenterFormOnScreen(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        public static void CenterFormOnParent(Form form)
        {
            form.StartPosition = FormStartPosition.CenterParent;
        }

        // =======================
        // ENUMS
        // =======================
        public enum ButtonStyle
        {
            Primary,
            Success,
            Danger,
            Warning,
            Default
        }

        public enum LabelStyle
        {
            Header,
            Title,
            Regular
        }
    }
}
