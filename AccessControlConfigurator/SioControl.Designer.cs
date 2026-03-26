using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AccessControlSystem.Forms
{
    partial class SioControl
    {
        private IContainer components = null;

        private TableLayoutPanel mainLayout;
        private Panel headerPanel;

        private Label lblTitle;
        private Button btnB;
        private Button btnS;

        private CheckBox chkOnBoard;

        private TableLayoutPanel busesLayout;

        private GroupBox grpBus1;
        private GroupBox grpBus2;

        private DataGridView gridRS485_1;
        private DataGridView gridRS485_2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mainLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            btnSave = new Button();
            btnBack = new Button();
            lblTitle = new Label();
            btnB = new Button();
            btnS = new Button();
            chkOnBoard = new CheckBox();
            busesLayout = new TableLayoutPanel();
            grpBus1 = new GroupBox();
            gridRS485_1 = new DataGridView();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            chk1 = new DataGridViewCheckBoxColumn();
            model1 = new DataGridViewComboBoxColumn();
            grpBus2 = new GroupBox();
            gridRS485_2 = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            chk2 = new DataGridViewCheckBoxColumn();
            model2 = new DataGridViewComboBoxColumn();
            mainLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            busesLayout.SuspendLayout();
            grpBus1.SuspendLayout();
            ((ISupportInitialize)gridRS485_1).BeginInit();
            grpBus2.SuspendLayout();
            ((ISupportInitialize)gridRS485_2).BeginInit();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(chkOnBoard, 0, 1);
            mainLayout.Controls.Add(busesLayout, 0, 2);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.RowCount = 3;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(1304, 810);
            mainLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(btnSave);
            headerPanel.Controls.Add(btnBack);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(btnB);
            headerPanel.Controls.Add(btnS);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(3, 3);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1298, 54);
            headerPanel.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.RoyalBlue;
            btnSave.Location = new Point(1106, 25);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 4;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.RoyalBlue;
            btnBack.Location = new Point(1006, 25);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 3;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(186, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add New SIO";
            // 
            // btnB
            // 
            btnB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnB.Location = new Point(2098, 12);
            btnB.Name = "btnB";
            btnB.Size = new Size(90, 32);
            btnB.TabIndex = 1;
            btnB.Text = "Back";
            btnB.Click += btnBack_Click;
            // 
            // btnS
            // 
            btnS.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnS.Location = new Point(2198, 12);
            btnS.Name = "btnS";
            btnS.Size = new Size(90, 32);
            btnS.TabIndex = 2;
            btnS.Text = "Save";
            btnS.Click += btnSave_Click;
            // 
            // chkOnBoard
            // 
            chkOnBoard.Dock = DockStyle.Left;
            chkOnBoard.Location = new Point(3, 63);
            chkOnBoard.Name = "chkOnBoard";
            chkOnBoard.Padding = new Padding(10, 0, 0, 0);
            chkOnBoard.Size = new Size(104, 34);
            chkOnBoard.TabIndex = 1;
            chkOnBoard.Text = "OnBoard";
            // 
            // busesLayout
            // 
            busesLayout.ColumnCount = 2;
            busesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            busesLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            busesLayout.Controls.Add(grpBus1, 0, 0);
            busesLayout.Controls.Add(grpBus2, 1, 0);
            busesLayout.Dock = DockStyle.Fill;
            busesLayout.Location = new Point(3, 103);
            busesLayout.Name = "busesLayout";
            busesLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            busesLayout.Size = new Size(1298, 704);
            busesLayout.TabIndex = 2;
            // 
            // grpBus1
            // 
            grpBus1.Controls.Add(gridRS485_1);
            grpBus1.Dock = DockStyle.Fill;
            grpBus1.Location = new Point(3, 3);
            grpBus1.Name = "grpBus1";
            grpBus1.Size = new Size(643, 698);
            grpBus1.TabIndex = 0;
            grpBus1.TabStop = false;
            grpBus1.Text = "RS-485-1";
            // 
            // gridRS485_1
            // 
            gridRS485_1.AllowUserToAddRows = false;
            gridRS485_1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRS485_1.ColumnHeadersHeight = 29;
            gridRS485_1.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, chk1, model1 });
            gridRS485_1.Dock = DockStyle.Fill;
            gridRS485_1.Location = new Point(3, 23);
            gridRS485_1.Name = "gridRS485_1";
            gridRS485_1.RowHeadersVisible = false;
            gridRS485_1.RowHeadersWidth = 51;
            gridRS485_1.Size = new Size(637, 672);
            gridRS485_1.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.MinimumWidth = 6;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.MinimumWidth = 6;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // chk1
            // 
            chk1.HeaderText = "Type";
            chk1.MinimumWidth = 6;
            chk1.Name = "chk1";
            // 
            // model1
            // 
            model1.HeaderText = "";
            model1.Items.AddRange(new object[] { "None", "HID Aero X100", "HID Aero X200", "HID Aero X300" });
            model1.MinimumWidth = 6;
            model1.Name = "model1";
            // 
            // grpBus2
            // 
            grpBus2.Controls.Add(gridRS485_2);
            grpBus2.Dock = DockStyle.Fill;
            grpBus2.Location = new Point(652, 3);
            grpBus2.Name = "grpBus2";
            grpBus2.Size = new Size(643, 698);
            grpBus2.TabIndex = 1;
            grpBus2.TabStop = false;
            grpBus2.Text = "RS-485-2";
            // 
            // gridRS485_2
            // 
            gridRS485_2.AllowUserToAddRows = false;
            gridRS485_2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridRS485_2.ColumnHeadersHeight = 29;
            gridRS485_2.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, chk2, model2 });
            gridRS485_2.Dock = DockStyle.Fill;
            gridRS485_2.Location = new Point(3, 23);
            gridRS485_2.Name = "gridRS485_2";
            gridRS485_2.RowHeadersVisible = false;
            gridRS485_2.RowHeadersWidth = 51;
            gridRS485_2.Size = new Size(637, 672);
            gridRS485_2.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // chk2
            // 
            chk2.HeaderText = "Type";
            chk2.MinimumWidth = 6;
            chk2.Name = "chk2";
            // 
            // model2
            // 
            model2.HeaderText = "";
            model2.Items.AddRange(new object[] { "None", "HID Aero X100", "HID Aero X200", "HID Aero X300" });
            model2.MinimumWidth = 6;
            model2.Name = "model2";
            // 
            // SioControl
            // 
            Controls.Add(mainLayout);
            Name = "SioControl";
            Size = new Size(1304, 810);
            mainLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            busesLayout.ResumeLayout(false);
            grpBus1.ResumeLayout(false);
            ((ISupportInitialize)gridRS485_1).EndInit();
            grpBus2.ResumeLayout(false);
            ((ISupportInitialize)gridRS485_2).EndInit();
            ResumeLayout(false);
        }
        private Button btnSave;
        private Button btnBack;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewCheckBoxColumn chk1;
        private DataGridViewComboBoxColumn model1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewCheckBoxColumn chk2;
        private DataGridViewComboBoxColumn model2;
    }
}