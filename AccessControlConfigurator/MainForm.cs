using AccessControlConfigurator.Controls;
using AccessControlConfigurator.Forms;
using AccessControlSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class MainForm : Form
    {
        private UserControl currentPage;
        private readonly Dictionary<string, UserControl> _pageCache = new Dictionary<string, UserControl>();
        private string _currentPageKey;

        private ControllerDto _controller;
        private Stack<UserControl> pageHistory = new Stack<UserControl>();

        private ControllerDto _selectedController;
        private BindingList<SioModel> _sioList;
        private SioModel _selectedSio;

        public static MainForm Instance { get; private set; }

        public EditControllerControl CurrentPage { get; internal set; }

        public MainForm()
        {
            InitializeComponent();

            Instance = this;

            splitMain.FixedPanel = FixedPanel.Panel1;
            splitMain.IsSplitterFixed = true;

            // Toolbar
            btnBack.Click += BtnBack_Click;
            btnRefresh.Click += BtnRefresh_Click;
            btnFind.Click += BtnFind_Click;
            btnOpenDoor.Click += BtnOpenDoor_Click;

            // Sidebar Tab Click Events
            tabEvents.Click += BtnEvents_Click;
            tabControllers.Click += BtnControllers_Click;
            tabAcrs.Click += BtnAcrs_Click;
            tabTimeZones.Click += BtnTimeZones_Click;
            tabAccessLevels.Click += BtnAccessLevels_Click;
            tabCards.Click += BtnCards_Click;
            tabCardholders.Click += BtnCardholders_Click;
            tabWiegand.Click += BtnWiegand_Click;
            tabEventReport.Click += BtnEventReport_Click;

            // Apply sidebar style
            StyleButton(tabEvents, "Events");
            StyleButton(tabControllers, "Controllers");
            StyleButton(tabAcrs, "ACRs (Doors)");
            StyleButton(tabTimeZones, "Time Zones");
            StyleButton(tabAccessLevels, "Access Levels");

            StyleButton(tabCards, "Cards");
            StyleButton(tabCardholders, "Cardholders");
            StyleButton(tabWiegand, "Wiegand");
            StyleButton(tabEventReport, "Event Report");
            
        }

        // ================= PAGE LOADER =================

        public void LoadPage(UserControl page, bool addToHistory = true, string pageKey = null)
        {
            if (page == null)
                return;

            if (ReferenceEquals(currentPage, page))
                return;

            pnlPageContainer.SuspendLayout();

            if (currentPage != null && addToHistory)
                pageHistory.Push(currentPage);

            pnlPageContainer.Controls.Clear();

            currentPage = page;
            _currentPageKey = pageKey ?? page.GetType().FullName ?? page.GetType().Name;
            page.Dock = DockStyle.Fill;

            pnlPageContainer.Controls.Add(page);
            page.BringToFront();

            pnlPageContainer.ResumeLayout();
        }

        private T GetOrCreatePage<T>(string cacheKey, Func<T> factory) where T : UserControl
        {
            if (_pageCache.TryGetValue(cacheKey, out var existing) && existing is T typedPage && !typedPage.IsDisposed)
            {
                return typedPage;
            }

            var page = factory();
            _pageCache[cacheKey] = page;
            return page;
        }

        private UserControl CreateFreshPage(string pageKey)
        {
            if (string.IsNullOrWhiteSpace(pageKey))
                return null;

            if (_pageCache.TryGetValue(pageKey, out var existingPage))
            {
                _pageCache.Remove(pageKey);
                existingPage.Dispose();
            }

            return pageKey switch
            {
                "Controllers" => GetOrCreatePage(pageKey, () => new ControllersControl()),
                "Acrs" => GetOrCreatePage(pageKey, () => new AcrsControl(_selectedController, _sioList, _selectedSio)),
                "TimeZones" => GetOrCreatePage(pageKey, () => new TimeZonesControl()),
                "AccessLevels" => GetOrCreatePage(pageKey, () => new AccessLevel()),
                "Cards" => GetOrCreatePage(pageKey, () => new Cards()),
                "Cardholders" => GetOrCreatePage(pageKey, () => new CardholdersControl()),
                "Events" => GetOrCreatePage(pageKey, () => new EventsControl()),
                "Wiegand" => GetOrCreatePage(pageKey, () => new WiegandControl()),
                "EventReport" => GetOrCreatePage(pageKey, () => new EventReportControl()),
                _ => null
            };
        }

        // ================= SIDEBAR TAB HIGHLIGHT =================

        private void HighlightButton(Button active)
        {
            foreach (Control c in sidebar.Controls)
            {
                if (c is Button b)
                    b.BackColor = Color.FromArgb(45, 60, 80);
            }

            active.BackColor = Color.FromArgb(70, 100, 140);
        }

        // ================= TAB BUTTON EVENTS =================

        private void BtnControllers_Click(object sender, EventArgs e)
        {
            HighlightButton(tabControllers);
            LoadPage(GetOrCreatePage("Controllers", () => new ControllersControl()), true, "Controllers");
        }

        private void BtnAcrs_Click(object sender, EventArgs e)
        {
            HighlightButton(tabAcrs);
            LoadPage(GetOrCreatePage("Acrs", () => new AcrsControl(_selectedController, _sioList, _selectedSio)), true, "Acrs");
        }

        private void BtnTimeZones_Click(object sender, EventArgs e)
        {
            HighlightButton(tabTimeZones);
            LoadPage(GetOrCreatePage("TimeZones", () => new TimeZonesControl()), true, "TimeZones");
        }

        private void BtnAccessLevels_Click(object sender, EventArgs e)
        {
            HighlightButton(tabAccessLevels);
            LoadPage(GetOrCreatePage("AccessLevels", () => new AccessLevel()), true, "AccessLevels");
        }

        private void BtnCards_Click(object sender, EventArgs e)
        {
            HighlightButton(tabCards);
            LoadPage(GetOrCreatePage("Cards", () => new Cards()), true, "Cards");
        }

        private void BtnCardholders_Click(object sender, EventArgs e)
        {
            HighlightButton(tabCardholders);
            LoadPage(GetOrCreatePage("Cardholders", () => new CardholdersControl()), true, "Cardholders");
        }

        private void BtnEvents_Click(object sender, EventArgs e)
        {
            HighlightButton(tabEvents);
            LoadPage(GetOrCreatePage("Events", () => new EventsControl()), true, "Events");
        }

        private void BtnWiegand_Click(object sender, EventArgs e)
        {
            HighlightButton(tabWiegand);
            LoadPage(GetOrCreatePage("Wiegand", () => new WiegandControl()), true, "Wiegand");
        }

        private void BtnEventReport_Click(object sender, EventArgs e)
        {
            HighlightButton(tabEventReport);
            LoadPage(GetOrCreatePage("EventReport", () => new EventReportControl()), true, "EventReport");
        }

        // ================= OPEN FIRST PAGE =================

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            HighlightButton(tabControllers);
            LoadPage(GetOrCreatePage("Controllers", () => new ControllersControl()), false, "Controllers");
        }

        // ================= BACK BUTTON =================

        private void BtnBack_Click(object sender, EventArgs e)
        {
            if (pageHistory.Count == 0)
                return;

            var previous = pageHistory.Pop();
            LoadPage(previous, false);
        }

        // ================= REFRESH BUTTON =================

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            if (currentPage == null)
                return;

            UserControl newPage = CreateFreshPage(_currentPageKey);

            LoadPage(newPage, false, _currentPageKey);
        }

        // ================= FIND BUTTON =================

        private void BtnFind_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Find user feature coming next");
        }

        // ================= OPEN DOOR =================

        private void BtnOpenDoor_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Open door command will be implemented");
        }

        // ================= LOAD CONTROLLERS PAGE =================

        internal void LoadControllersPage()
        {
            HighlightButton(tabControllers);
            LoadPage(GetOrCreatePage("Controllers", () => new ControllersControl()), true, "Controllers");
        }

        // ================= BUTTON STYLE =================

        private void StyleButton(Button btn, string text)
        {
            btn.Text = text;

            btn.Width = sidebar.Width;
            btn.Height = 45;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            btn.ForeColor = Color.White;
            btn.BackColor = Color.FromArgb(45, 60, 80);

            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(15, 0, 0, 0);

            btn.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
        }
    }
}
