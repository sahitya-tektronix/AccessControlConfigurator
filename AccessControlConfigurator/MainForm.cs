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
        private Stack<UserControl> _navigationStack = new Stack<UserControl>();
        private UserControl currentPage;

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
            tabControllers.Click += BtnControllers_Click;
            tabAcrs.Click += BtnAcrs_Click;
            tabTimeZones.Click += BtnTimeZones_Click;
            tabAccessLevels.Click += BtnAccessLevels_Click;
            tabCards.Click += BtnCards_Click;
            tabCardholders.Click += BtnCardholders_Click;
            tabEvents.Click += BtnEvents_Click;

            // Apply sidebar style
            StyleButton(tabControllers, "Controllers");
            StyleButton(tabAcrs, "ACRs (Doors)");
            StyleButton(tabTimeZones, "Time Zones");
            StyleButton(tabAccessLevels, "Access Levels");

            StyleButton(tabCards, "Cards");
            StyleButton(tabCardholders, "Cardholders");
            StyleButton(tabEvents, "Events");
            
        }

        // ================= PAGE LOADER =================

        public void LoadPage(UserControl page, bool addToHistory = true)
        {
            pnlPageContainer.SuspendLayout();

            if (currentPage != null && addToHistory)
                pageHistory.Push(currentPage);

            pnlPageContainer.Controls.Clear();

            currentPage = page;
            page.Dock = DockStyle.Fill;

            pnlPageContainer.Controls.Add(page);
            page.BringToFront();

            pnlPageContainer.ResumeLayout();
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
            LoadPage(new ControllersControl());
        }

        private void BtnAcrs_Click(object sender, EventArgs e)
        {
            HighlightButton(tabAcrs);
            LoadPage(new AcrsControl(_selectedController, _sioList, _selectedSio));
        }

        private void BtnTimeZones_Click(object sender, EventArgs e)
        {
            HighlightButton(tabTimeZones);
            LoadPage(new TimeZonesControl());
        }

        private void BtnAccessLevels_Click(object sender, EventArgs e)
        {
            HighlightButton(tabAccessLevels);
            LoadPage(new AccessLevel());
        }

        private void BtnCards_Click(object sender, EventArgs e)
        {
            HighlightButton(tabCards);
            LoadPage(new Cards());
        }

        private void BtnCardholders_Click(object sender, EventArgs e)
        {
            HighlightButton(tabCardholders);
            LoadPage(new CardholdersControl());
        }

        private void BtnEvents_Click(object sender, EventArgs e)
        {
            HighlightButton(tabEvents);
            LoadPage(new EventsControl());
        }

        // ================= OPEN FIRST PAGE =================

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            HighlightButton(tabControllers);
            LoadPage(new ControllersControl(), false);
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

            Type t = currentPage.GetType();
            UserControl newPage = (UserControl)Activator.CreateInstance(t);

            LoadPage(newPage, false);
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
            LoadPage(new ControllersControl());
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