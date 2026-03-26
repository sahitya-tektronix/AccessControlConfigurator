namespace AccessControlConfigurator
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem mFile;
        private System.Windows.Forms.ToolStripMenuItem mView;
        private System.Windows.Forms.ToolStripMenuItem mTools;

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton btnBack;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnFind;
        private System.Windows.Forms.ToolStripButton btnOpenDoor;

        private System.Windows.Forms.SplitContainer splitMain;

        private System.Windows.Forms.FlowLayoutPanel sidebar;

        private System.Windows.Forms.Button tabControllers;
        private System.Windows.Forms.Button tabAcrs;
        private System.Windows.Forms.Button tabTimeZones;
        private System.Windows.Forms.Button tabAccessLevels;
        private System.Windows.Forms.Button tabCards;
        private System.Windows.Forms.Button tabCardholders;
        private System.Windows.Forms.Button tabEvents;

        private System.Windows.Forms.Panel pnlPageContainer;
        private System.Windows.Forms.Panel pnlMain;

        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblServer;
        private System.Windows.Forms.ToolStripStatusLabel lblUser;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            menuMain = new MenuStrip();
            mFile = new ToolStripMenuItem();
            mView = new ToolStripMenuItem();
            mTools = new ToolStripMenuItem();
            toolBar = new ToolStrip();
            btnBack = new ToolStripButton();
            btnRefresh = new ToolStripButton();
            btnFind = new ToolStripButton();
            btnOpenDoor = new ToolStripButton();
            splitMain = new SplitContainer();
            sidebar = new FlowLayoutPanel();
            tabControllers = new Button();
            tabAcrs = new Button();
            tabTimeZones = new Button();
            tabAccessLevels = new Button();
            tabCards = new Button();
            tabCardholders = new Button();
            tabEvents = new Button();
            pnlPageContainer = new Panel();
            pnlMain = new Panel();
            statusBar = new StatusStrip();
            lblServer = new ToolStripStatusLabel();
            lblUser = new ToolStripStatusLabel();
            menuMain.SuspendLayout();
            toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            sidebar.SuspendLayout();
            pnlPageContainer.SuspendLayout();
            statusBar.SuspendLayout();
            SuspendLayout();
            // 
            // menuMain
            // 
            menuMain.BackColor = Color.FromArgb(30, 45, 70);
            menuMain.ForeColor = Color.White;
            menuMain.ImageScalingSize = new Size(20, 20);
            menuMain.Items.AddRange(new ToolStripItem[] { mFile, mView, mTools });
            menuMain.Location = new Point(0, 0);
            menuMain.Name = "menuMain";
            menuMain.Size = new Size(900, 28);
            menuMain.TabIndex = 2;
            // 
            // mFile
            // 
            mFile.Name = "mFile";
            mFile.Size = new Size(46, 24);
            mFile.Text = "File";
            // 
            // mView
            // 
            mView.Name = "mView";
            mView.Size = new Size(55, 24);
            mView.Text = "View";
            // 
            // mTools
            // 
            mTools.Name = "mTools";
            mTools.Size = new Size(58, 24);
            mTools.Text = "Tools";
            // 
            // toolBar
            // 
            toolBar.BackColor = Color.FromArgb(220, 225, 235);
            toolBar.ImageScalingSize = new Size(20, 20);
            toolBar.Items.AddRange(new ToolStripItem[] { btnBack, btnRefresh, btnFind, btnOpenDoor });
            toolBar.Location = new Point(0, 28);
            toolBar.Name = "toolBar";
            toolBar.Size = new Size(900, 27);
            toolBar.TabIndex = 1;
            // 
            // btnBack
            // 
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(44, 24);
            btnBack.Text = "Back";
            // 
            // btnRefresh
            // 
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(62, 24);
            btnRefresh.Text = "Refresh";
            // 
            // btnFind
            // 
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(41, 24);
            btnFind.Text = "Find";
            // 
            // btnOpenDoor
            // 
            btnOpenDoor.Name = "btnOpenDoor";
            btnOpenDoor.Size = new Size(87, 24);
            btnOpenDoor.Text = "Open Door";
            // 
            // splitMain
            // 
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 55);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(sidebar);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(pnlPageContainer);
            splitMain.Size = new Size(900, 449);
            splitMain.SplitterDistance = 120;
            splitMain.TabIndex = 0;
            // 
            // sidebar
            // 
            sidebar.BackColor = Color.FromArgb(45, 60, 80);
            sidebar.Controls.Add(tabControllers);
            sidebar.Controls.Add(tabAcrs);
            sidebar.Controls.Add(tabTimeZones);
            sidebar.Controls.Add(tabAccessLevels);
            sidebar.Controls.Add(tabCards);
            sidebar.Controls.Add(tabCardholders);
            sidebar.Controls.Add(tabEvents);
            sidebar.Dock = DockStyle.Fill;
            sidebar.FlowDirection = FlowDirection.TopDown;
            sidebar.Location = new Point(0, 0);
            sidebar.Name = "sidebar";
            sidebar.Size = new Size(120, 449);
            sidebar.TabIndex = 0;
            sidebar.WrapContents = false;
            // 
            // tabControllers
            // 
            tabControllers.Location = new Point(3, 3);
            tabControllers.Name = "tabControllers";
            tabControllers.Size = new Size(75, 23);
            tabControllers.TabIndex = 0;
            tabControllers.Click += BtnControllers_Click;
            // 
            // tabAcrs
            // 
            tabAcrs.Location = new Point(3, 32);
            tabAcrs.Name = "tabAcrs";
            tabAcrs.Size = new Size(75, 23);
            tabAcrs.TabIndex = 1;
            tabAcrs.Click += BtnAcrs_Click;
            // 
            // tabTimeZones
            // 
            tabTimeZones.Location = new Point(3, 61);
            tabTimeZones.Name = "tabTimeZones";
            tabTimeZones.Size = new Size(75, 23);
            tabTimeZones.TabIndex = 2;
            // 
            // tabAccessLevels
            // 
            tabAccessLevels.Location = new Point(3, 90);
            tabAccessLevels.Name = "tabAccessLevels";
            tabAccessLevels.Size = new Size(75, 23);
            tabAccessLevels.TabIndex = 3;
            // 
            // tabCards
            // 
            tabCards.Location = new Point(3, 119);
            tabCards.Name = "tabCards";
            tabCards.Size = new Size(75, 23);
            tabCards.TabIndex = 4;
            // 
            // tabCardholders
            // 
            tabCardholders.Location = new Point(3, 148);
            tabCardholders.Name = "tabCardholders";
            tabCardholders.Size = new Size(75, 23);
            tabCardholders.TabIndex = 5;
            // 
            // tabEvents
            // 
            tabEvents.Location = new Point(3, 177);
            tabEvents.Name = "tabEvents";
            tabEvents.Size = new Size(75, 23);
            tabEvents.TabIndex = 6;
            // 
            // pnlPageContainer
            // 
            pnlPageContainer.BackColor = Color.White;
            pnlPageContainer.Controls.Add(pnlMain);
            pnlPageContainer.Dock = DockStyle.Fill;
            pnlPageContainer.Location = new Point(0, 0);
            pnlPageContainer.Name = "pnlPageContainer";
            pnlPageContainer.Size = new Size(776, 449);
            pnlPageContainer.TabIndex = 0;
            // 
            // pnlMain
            // 
            pnlMain.Dock = DockStyle.Fill;
            pnlMain.Location = new Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Size = new Size(776, 449);
            pnlMain.TabIndex = 0;
            // 
            // statusBar
            // 
            statusBar.BackColor = Color.FromArgb(30, 45, 70);
            statusBar.ForeColor = Color.White;
            statusBar.ImageScalingSize = new Size(20, 20);
            statusBar.Items.AddRange(new ToolStripItem[] { lblServer, lblUser });
            statusBar.Location = new Point(0, 504);
            statusBar.Name = "statusBar";
            statusBar.Size = new Size(900, 26);
            statusBar.TabIndex = 3;
            // 
            // lblServer
            // 
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(147, 20);
            lblServer.Text = "Server: Disconnected";
            // 
            // lblUser
            // 
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(87, 20);
            lblUser.Text = "User: admin";
            // 
            // MainForm
            // 
            ClientSize = new Size(900, 530);
            Controls.Add(splitMain);
            Controls.Add(toolBar);
            Controls.Add(menuMain);
            Controls.Add(statusBar);
            MainMenuStrip = menuMain;
            Name = "MainForm";
            Text = "Access Control Configurator";
            WindowState = FormWindowState.Maximized;
            menuMain.ResumeLayout(false);
            menuMain.PerformLayout();
            toolBar.ResumeLayout(false);
            toolBar.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            sidebar.ResumeLayout(false);
            pnlPageContainer.ResumeLayout(false);
            statusBar.ResumeLayout(false);
            statusBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

       
    }
}