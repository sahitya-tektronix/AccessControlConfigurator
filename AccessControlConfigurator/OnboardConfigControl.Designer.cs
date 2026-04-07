using System.Drawing;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    partial class OnboardConfigControl
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitle;
        private Button btnBack;
        private Button btnSave;

        private GroupBox grpCommon;
        private Label lblProtocol;
        private ComboBox cmbProtocol;
        private CheckBox chkCardInCard;

        private Panel panelReader1;
        private Panel panelReader2;

        private CheckBox chkEnableR1;
        private CheckBox chkEnableR2;

        private TableLayoutPanel tblReader1;
        private TableLayoutPanel tblReader2;

        private Label lblR1Header;
        private Label lblR2Header;

        private TextBox txt1Rname;
        private ComboBox cb1RT;
        private TextBox tb1Acr;
        private Label lblR1DefaultMode;
        private TextBox txtDefaultMode1;
        private Label lblR1Position;
        private TextBox txtReaderPosition1;
        private Label lblR1Strike;
        private TextBox txtStrikeNumber1;
        private Label lblR1Door;
        private TextBox txtDoorNumber1;
        private Label lblR1Rex;
        private TextBox txtRexNumber1;

        private TextBox txt2Rname;
        private ComboBox cb2RT;
        private TextBox tb2Acr;
        private Label lblR2DefaultMode;
        private TextBox txtDefaultMode2;
        private Label lblR2Position;
        private TextBox txtReaderPosition2;
        private Label lblR2Strike;
        private TextBox txtStrikeNumber2;
        private Label lblR2Door;
        private TextBox txtDoorNumber2;
        private Label lblR2Rex;
        private TextBox txtRexNumber2;


        private ComboBox cb1Direction;
        private ComboBox cb2Direction;
        private Label lblR1Direction;
        private Label lblR2Direction;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblR2Direction = new Label();
            cb2Direction = new ComboBox();
            lblR1DefaultMode = new Label();
            txtDefaultMode1 = new TextBox();
            lblR1Position = new Label();
            txtReaderPosition1 = new TextBox();
            lblR1Strike = new Label();
            txtStrikeNumber1 = new TextBox();
            lblR1Door = new Label();
            txtDoorNumber1 = new TextBox();
            lblR1Rex = new Label();
            txtRexNumber1 = new TextBox();
            lblR2DefaultMode = new Label();
            txtDefaultMode2 = new TextBox();
            lblR2Position = new Label();
            txtReaderPosition2 = new TextBox();
            lblR2Strike = new Label();
            txtStrikeNumber2 = new TextBox();
            lblR2Door = new Label();
            txtDoorNumber2 = new TextBox();
            lblR2Rex = new Label();
            txtRexNumber2 = new TextBox();
            panelHeader = new Panel();
            lblSioNumber = new Label();
            lblControllerName = new Label();
            lblTitle = new Label();
            btnBack = new Button();
            btnSave = new Button();
            panelReader1 = new Panel();
            lblR1Header = new Label();
            chkEnableR1 = new CheckBox();
            tblReader1 = new TableLayoutPanel();
            cbRdir1 = new ComboBox();
            lblRdir1 = new Label();
            lblR1Name = new Label();
            lblR1Type = new Label();
            txt1Rname = new TextBox();
            cb1RT = new ComboBox();
            tb1Acr = new TextBox();
            lblR1Acr = new Label();
            panelReader2 = new Panel();
            lblR2Header = new Label();
            chkEnableR2 = new CheckBox();
            tblReader2 = new TableLayoutPanel();
            lblRdir2 = new Label();
            lblR2Name = new Label();
            lblR2Type = new Label();
            txt2Rname = new TextBox();
            cb2RT = new ComboBox();
            lblR2Acr = new Label();
            tb2Acr = new TextBox();
            cbRdir2 = new ComboBox();
            lblR1Direction = new Label();
            cb1Direction = new ComboBox();
            panelHeader.SuspendLayout();
            panelReader1.SuspendLayout();
            tblReader1.SuspendLayout();
            panelReader2.SuspendLayout();
            tblReader2.SuspendLayout();
            SuspendLayout();
            // 
            // lblR2Direction
            // 
            lblR2Direction.Location = new Point(0, 0);
            lblR2Direction.Name = "lblR2Direction";
            lblR2Direction.Size = new Size(100, 23);
            lblR2Direction.TabIndex = 0;
            // 
            // cb2Direction
            // 
            cb2Direction.Location = new Point(0, 0);
            cb2Direction.Name = "cb2Direction";
            cb2Direction.Size = new Size(121, 28);
            cb2Direction.TabIndex = 0;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.WhiteSmoke;
            panelHeader.Controls.Add(lblSioNumber);
            panelHeader.Controls.Add(lblControllerName);
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(btnBack);
            panelHeader.Controls.Add(btnSave);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1200, 70);
            panelHeader.TabIndex = 0;
            // 
            // lblSioNumber
            // 
            lblSioNumber.AutoSize = true;
            lblSioNumber.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSioNumber.ForeColor = Color.Black;
            lblSioNumber.Location = new Point(674, 21);
            lblSioNumber.Name = "lblSioNumber";
            lblSioNumber.Size = new Size(143, 32);
            lblSioNumber.TabIndex = 4;
            lblSioNumber.Text = "SioNumber";
            // 
            // lblControllerName
            // 
            lblControllerName.AutoSize = true;
            lblControllerName.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblControllerName.ForeColor = Color.Black;
            lblControllerName.Location = new Point(247, 21);
            lblControllerName.Name = "lblControllerName";
            lblControllerName.Size = new Size(197, 32);
            lblControllerName.TabIndex = 3;
            lblControllerName.Text = "ControllerName";
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 16);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(246, 42);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Onboard Config";
            // 
            // btnBack
            // 
            btnBack.Location = new Point(982, 21);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(103, 39);
            btnBack.TabIndex = 1;
            btnBack.Text = "Back";
            btnBack.Click += btnBack_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.WhiteSmoke;
            btnSave.ForeColor = Color.Black;
            btnSave.Location = new Point(1091, 21);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 39);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.MouseClick += btnSave_Click;
            // 
            // panelReader1
            // 
            panelReader1.BorderStyle = BorderStyle.FixedSingle;
            panelReader1.Controls.Add(lblR1Header);
            panelReader1.Controls.Add(chkEnableR1);
            panelReader1.Controls.Add(tblReader1);
            panelReader1.Location = new Point(20, 200);
            panelReader1.Name = "panelReader1";
            panelReader1.Size = new Size(550, 390);
            panelReader1.TabIndex = 2;
            // 
            // lblR1Header
            // 
            lblR1Header.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblR1Header.Location = new Point(10, 10);
            lblR1Header.Name = "lblR1Header";
            lblR1Header.Size = new Size(100, 23);
            lblR1Header.TabIndex = 0;
            lblR1Header.Text = "Reader 1 (Address 0)";
            // 
            // chkEnableR1
            // 
            chkEnableR1.Location = new Point(480, 10);
            chkEnableR1.Name = "chkEnableR1";
            chkEnableR1.Size = new Size(104, 24);
            chkEnableR1.TabIndex = 1;
            // 
            // tblReader1
            // 
            tblReader1.ColumnCount = 2;
            tblReader1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tblReader1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblReader1.Controls.Add(lblR1Name, 0, 0);
            tblReader1.Controls.Add(txt1Rname, 1, 0);
            tblReader1.Controls.Add(lblR1Type, 0, 1);
            tblReader1.Controls.Add(cb1RT, 1, 1);
            tblReader1.Controls.Add(lblRdir1, 0, 2);
            tblReader1.Controls.Add(cbRdir1, 1, 2);
            tblReader1.Controls.Add(lblR1Acr, 0, 3);
            tblReader1.Controls.Add(tb1Acr, 1, 3);
            tblReader1.Controls.Add(lblR1DefaultMode, 0, 4);
            tblReader1.Controls.Add(txtDefaultMode1, 1, 4);
            tblReader1.Controls.Add(lblR1Position, 0, 5);
            tblReader1.Controls.Add(txtReaderPosition1, 1, 5);
            tblReader1.Controls.Add(lblR1Strike, 0, 6);
            tblReader1.Controls.Add(txtStrikeNumber1, 1, 6);
            tblReader1.Controls.Add(lblR1Door, 0, 7);
            tblReader1.Controls.Add(txtDoorNumber1, 1, 7);
            tblReader1.Controls.Add(lblR1Rex, 0, 8);
            tblReader1.Controls.Add(txtRexNumber1, 1, 8);
            tblReader1.Location = new Point(20, 50);
            tblReader1.Name = "tblReader1";
            tblReader1.RowCount = 9;
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader1.Size = new Size(500, 320);
            tblReader1.TabIndex = 2;
            // 
            // cbRdir1
            // 
            cbRdir1.Location = new Point(153, 73);
            cbRdir1.Name = "cbRdir1";
            cbRdir1.Size = new Size(200, 28);
            cbRdir1.TabIndex = 7;
            // 
            // lblRdir1
            // 
            lblRdir1.Location = new Point(3, 70);
            lblRdir1.Name = "lblRdir1";
            lblRdir1.Size = new Size(144, 23);
            lblRdir1.TabIndex = 6;
            lblRdir1.Text = "Reader Direction";
            // 
            // lblR1Name
            // 
            lblR1Name.Location = new Point(3, 0);
            lblR1Name.Name = "lblR1Name";
            lblR1Name.Size = new Size(100, 23);
            lblR1Name.TabIndex = 0;
            lblR1Name.Text = "Reader Name";
            // 
            // lblR1Type
            // 
            lblR1Type.Location = new Point(3, 35);
            lblR1Type.Name = "lblR1Type";
            lblR1Type.Size = new Size(100, 23);
            lblR1Type.TabIndex = 1;
            lblR1Type.Text = "Reader Type";
            // 
            // txt1Rname
            // 
            txt1Rname.Location = new Point(153, 3);
            txt1Rname.Name = "txt1Rname";
            txt1Rname.Size = new Size(280, 27);
            txt1Rname.TabIndex = 3;
            // 
            // cb1RT
            // 
            cb1RT.Location = new Point(153, 38);
            cb1RT.Name = "cb1RT";
            cb1RT.Size = new Size(200, 28);
            cb1RT.TabIndex = 4;
            // 
            // tb1Acr
            // 
            tb1Acr.Location = new Point(153, 108);
            tb1Acr.Name = "tb1Acr";
            tb1Acr.Size = new Size(280, 27);
            tb1Acr.TabIndex = 5;
            // 
            // lblR1Acr
            //
            lblR1Acr.Location = new Point(3, 105);
            lblR1Acr.Name = "lblR1Acr";
            lblR1Acr.Size = new Size(100, 23);
            lblR1Acr.TabIndex = 2;
            lblR1Acr.Text = "ACR Number";
            // lblR1DefaultMode
            lblR1DefaultMode.Name = "lblR1DefaultMode";
            lblR1DefaultMode.Size = new Size(144, 23);
            lblR1DefaultMode.Text = "Default Mode";
            // txtDefaultMode1
            txtDefaultMode1.Name = "txtDefaultMode1";
            txtDefaultMode1.Size = new Size(200, 27);
            txtDefaultMode1.Enabled = false;
            txtDefaultMode1.BackColor = System.Drawing.Color.WhiteSmoke;
            // lblR1Position
            lblR1Position.Name = "lblR1Position";
            lblR1Position.Size = new Size(144, 23);
            lblR1Position.Text = "Reader Position";
            // txtReaderPosition1
            txtReaderPosition1.Name = "txtReaderPosition1";
            txtReaderPosition1.Size = new Size(200, 27);
            txtReaderPosition1.Enabled = false;
            txtReaderPosition1.BackColor = System.Drawing.Color.WhiteSmoke;
            // lblR1Strike
            lblR1Strike.Name = "lblR1Strike";
            lblR1Strike.Size = new Size(144, 23);
            lblR1Strike.Text = "Strike Number";
            // txtStrikeNumber1
            txtStrikeNumber1.Name = "txtStrikeNumber1";
            txtStrikeNumber1.Size = new Size(200, 27);
            txtStrikeNumber1.Enabled = false;
            txtStrikeNumber1.BackColor = System.Drawing.Color.WhiteSmoke;
            // lblR1Door
            lblR1Door.Name = "lblR1Door";
            lblR1Door.Size = new Size(144, 23);
            lblR1Door.Text = "Door Number";
            // txtDoorNumber1
            txtDoorNumber1.Name = "txtDoorNumber1";
            txtDoorNumber1.Size = new Size(200, 27);
            txtDoorNumber1.Enabled = false;
            txtDoorNumber1.BackColor = System.Drawing.Color.WhiteSmoke;
            // lblR1Rex
            lblR1Rex.Name = "lblR1Rex";
            lblR1Rex.Size = new Size(144, 23);
            lblR1Rex.Text = "REX Number";
            // txtRexNumber1
            txtRexNumber1.Name = "txtRexNumber1";
            txtRexNumber1.Size = new Size(200, 27);
            txtRexNumber1.Enabled = false;
            txtRexNumber1.BackColor = System.Drawing.Color.WhiteSmoke;
            //
            // panelReader2
            // 
            panelReader2.BorderStyle = BorderStyle.FixedSingle;
            panelReader2.Controls.Add(lblR2Header);
            panelReader2.Controls.Add(chkEnableR2);
            panelReader2.Controls.Add(tblReader2);
            panelReader2.Location = new Point(600, 200);
            panelReader2.Name = "panelReader2";
            panelReader2.Size = new Size(550, 390);
            panelReader2.TabIndex = 3;
            // 
            // lblR2Header
            // 
            lblR2Header.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblR2Header.Location = new Point(10, 10);
            lblR2Header.Name = "lblR2Header";
            lblR2Header.Size = new Size(100, 23);
            lblR2Header.TabIndex = 0;
            lblR2Header.Text = "Reader 2 (Address 1)";
            // 
            // chkEnableR2
            // 
            chkEnableR2.Location = new Point(480, 10);
            chkEnableR2.Name = "chkEnableR2";
            chkEnableR2.Size = new Size(104, 24);
            chkEnableR2.TabIndex = 1;
            // 
            // tblReader2
            // 
            tblReader2.ColumnCount = 2;
            tblReader2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            tblReader2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblReader2.Controls.Add(lblR2Name, 0, 0);
            tblReader2.Controls.Add(txt2Rname, 1, 0);
            tblReader2.Controls.Add(lblR2Type, 0, 1);
            tblReader2.Controls.Add(cb2RT, 1, 1);
            tblReader2.Controls.Add(lblRdir2, 0, 2);
            tblReader2.Controls.Add(cbRdir2, 1, 2);
            tblReader2.Controls.Add(lblR2Acr, 0, 3);
            tblReader2.Controls.Add(tb2Acr, 1, 3);
            tblReader2.Controls.Add(lblR2DefaultMode, 0, 4);
            tblReader2.Controls.Add(txtDefaultMode2, 1, 4);
            tblReader2.Controls.Add(lblR2Position, 0, 5);
            tblReader2.Controls.Add(txtReaderPosition2, 1, 5);
            tblReader2.Controls.Add(lblR2Strike, 0, 6);
            tblReader2.Controls.Add(txtStrikeNumber2, 1, 6);
            tblReader2.Controls.Add(lblR2Door, 0, 7);
            tblReader2.Controls.Add(txtDoorNumber2, 1, 7);
            tblReader2.Controls.Add(lblR2Rex, 0, 8);
            tblReader2.Controls.Add(txtRexNumber2, 1, 8);
            tblReader2.Location = new Point(20, 50);
            tblReader2.Name = "tblReader2";
            tblReader2.RowCount = 9;
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tblReader2.Size = new Size(500, 320);
            tblReader2.TabIndex = 2;
            // 
            // lblRdir2
            // 
            lblRdir2.Location = new Point(3, 70);
            lblRdir2.Name = "lblRdir2";
            lblRdir2.Size = new Size(144, 23);
            lblRdir2.TabIndex = 7;
            lblRdir2.Text = "Reader Direction";
            // 
            // lblR2Name
            // 
            lblR2Name.Location = new Point(3, 0);
            lblR2Name.Name = "lblR2Name";
            lblR2Name.Size = new Size(100, 23);
            lblR2Name.TabIndex = 0;
            lblR2Name.Text = "Reader Name";
            // 
            // lblR2Type
            // 
            lblR2Type.Location = new Point(3, 35);
            lblR2Type.Name = "lblR2Type";
            lblR2Type.Size = new Size(100, 23);
            lblR2Type.TabIndex = 1;
            lblR2Type.Text = "Reader Type";
            // 
            // txt2Rname
            // 
            txt2Rname.Location = new Point(153, 3);
            txt2Rname.Name = "txt2Rname";
            txt2Rname.Size = new Size(280, 27);
            txt2Rname.TabIndex = 3;
            // 
            // cb2RT
            // 
            cb2RT.Location = new Point(153, 38);
            cb2RT.Name = "cb2RT";
            cb2RT.Size = new Size(200, 28);
            cb2RT.TabIndex = 4;
            // 
            // lblR2Acr
            // 
            lblR2Acr.Location = new Point(3, 105);
            lblR2Acr.Name = "lblR2Acr";
            lblR2Acr.Size = new Size(100, 23);
            lblR2Acr.TabIndex = 2;
            lblR2Acr.Text = "ACR Number";
            // 
            // tb2Acr
            // 
            tb2Acr.Location = new Point(153, 108);
            tb2Acr.Name = "tb2Acr";
            tb2Acr.Size = new Size(280, 27);
            tb2Acr.TabIndex = 5;
            // 
            // cbRdir2
            //
            cbRdir2.Location = new Point(153, 73);
            cbRdir2.Name = "cbRdir2";
            cbRdir2.Size = new Size(200, 28);
            cbRdir2.TabIndex = 8;
            // lblR2DefaultMode
            lblR2DefaultMode.Name = "lblR2DefaultMode";
            lblR2DefaultMode.Size = new Size(144, 23);
            lblR2DefaultMode.Text = "Default Mode";
            // txtDefaultMode2
            txtDefaultMode2.Name = "txtDefaultMode2";
            txtDefaultMode2.Size = new Size(200, 27);
            txtDefaultMode2.Enabled = false;
            txtDefaultMode2.BackColor = System.Drawing.Color.WhiteSmoke;
            // lblR2Position
            lblR2Position.Name = "lblR2Position";
            lblR2Position.Size = new Size(144, 23);
            lblR2Position.Text = "Reader Position";
            // txtReaderPosition2
            txtReaderPosition2.Name = "txtReaderPosition2";
            txtReaderPosition2.Size = new Size(200, 27);
            txtReaderPosition2.Enabled = false;
            txtReaderPosition2.BackColor = System.Drawing.Color.WhiteSmoke;
            // lblR2Strike
            lblR2Strike.Name = "lblR2Strike";
            lblR2Strike.Size = new Size(144, 23);
            lblR2Strike.Text = "Strike Number";
            // txtStrikeNumber2
            txtStrikeNumber2.Name = "txtStrikeNumber2";
            txtStrikeNumber2.Size = new Size(200, 27);
            txtStrikeNumber2.Enabled = false;
            txtStrikeNumber2.BackColor = System.Drawing.Color.WhiteSmoke;
            // lblR2Door
            lblR2Door.Name = "lblR2Door";
            lblR2Door.Size = new Size(144, 23);
            lblR2Door.Text = "Door Number";
            // txtDoorNumber2
            txtDoorNumber2.Name = "txtDoorNumber2";
            txtDoorNumber2.Size = new Size(200, 27);
            txtDoorNumber2.Enabled = false;
            txtDoorNumber2.BackColor = System.Drawing.Color.WhiteSmoke;
            // lblR2Rex
            lblR2Rex.Name = "lblR2Rex";
            lblR2Rex.Size = new Size(144, 23);
            lblR2Rex.Text = "REX Number";
            // txtRexNumber2
            txtRexNumber2.Name = "txtRexNumber2";
            txtRexNumber2.Size = new Size(200, 27);
            txtRexNumber2.Enabled = false;
            txtRexNumber2.BackColor = System.Drawing.Color.WhiteSmoke;
            //
            // lblR1Direction
            // 
            lblR1Direction.Location = new Point(3, 105);
            lblR1Direction.Name = "lblR1Direction";
            lblR1Direction.Size = new Size(100, 23);
            lblR1Direction.TabIndex = 0;
            lblR1Direction.Text = "Reader Direction";
            // 
            // cb1Direction
            // 
            cb1Direction.Location = new Point(0, 0);
            cb1Direction.Name = "cb1Direction";
            cb1Direction.Size = new Size(200, 28);
            cb1Direction.TabIndex = 0;
            // 
            // OnboardConfigControl
            // 
            BackColor = Color.White;
            Controls.Add(panelHeader);
            Controls.Add(panelReader1);
            Controls.Add(panelReader2);
            Name = "OnboardConfigControl";
            Size = new Size(1200, 700);
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelReader1.ResumeLayout(false);
            tblReader1.ResumeLayout(false);
            tblReader1.PerformLayout();
            panelReader2.ResumeLayout(false);
            tblReader2.ResumeLayout(false);
            tblReader2.PerformLayout();
            ResumeLayout(false);
        }
        private Label lblR1Name;
        private Label lblR1Type;
        private Label lblR1Acr;
        private Label lblR2Name;
        private Label lblR2Type;
        private Label lblR2Acr;
        private Label lblSioNumber;
        private Label lblControllerName;
        private ComboBox cbRdir1;
        private Label lblRdir1;
        private Label lblRdir2;
        private ComboBox cbRdir2;
    }
}