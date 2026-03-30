using AccessControlConfigurator.Forms;
using AccessControlSystem.Models;
using AccessControlSystem.Models.Acr;
using AccessControlSystem.Services;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessControlConfigurator
{
    public partial class OnboardConfigControl : UserControl
    {
        private readonly ApiService _api = new ApiService();

        private ControllerDto _controller;
        private BindingList<SioModel> _sioList;
        private SioModel _selectedSio;

        private List<AcrDto> acrlist;
        private int? leftAcrId = null;
        private int? rightAcrId = null;

        private static readonly HttpClient _http = new HttpClient();

        public OnboardConfigControl(
            ControllerDto controller,
            BindingList<SioModel> sioList,
            SioModel selectedSio)
        {
            InitializeComponent();

            _controller = controller;
            _sioList = sioList;
            _selectedSio = selectedSio;

            lblControllerName.Text = $"Controller: {_controller.Name}";
            lblSioNumber.Text = $"SIO No: {_selectedSio.SioNumber}";

            InitializeDropdowns();

            ToggleReader(tblReader1, false);
            ToggleReader(tblReader2, false);

            chkEnableR1.CheckedChanged += chkEnableR1_CheckedChanged;
            chkEnableR2.CheckedChanged += chkEnableR2_CheckedChanged;

            tb1Acr.KeyPress += tb1Acr_KeyPress;
            tb2Acr.KeyPress += tb2Acr_KeyPress;

            LoadSioData();
        }

        // ================= DROPDOWNS =================
        private void InitializeDropdowns()
        {
            cb1RT.Items.Clear();
            cb1RT.DropDownStyle = ComboBoxStyle.DropDownList;
            cb1RT.Items.Add("Signo Reader");
            cb1RT.SelectedIndex = 0;
            cb1RT.Enabled = false;

            cb2RT.Items.Clear();
            cb2RT.DropDownStyle = ComboBoxStyle.DropDownList;
            cb2RT.Items.Add("Signo Reader");
            cb2RT.SelectedIndex = 0;
            cb2RT.Enabled = false;

            cbRdir1.Items.Clear();
            cbRdir1.Items.AddRange(new object[] { "In", "Out", "In/Out" });
            cbRdir1.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRdir1.SelectedIndex = 0;

            cbRdir2.Items.Clear();
            cbRdir2.Items.AddRange(new object[] { "In", "Out", "In/Out" });
            cbRdir2.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRdir2.SelectedIndex = 0;
        }

        // ================= LOAD =================
        private async void LoadSioData()
        {
            if (_selectedSio == null)
                return;

            acrlist = await _api.GetAcrs(_selectedSio.ControllerID, _selectedSio.SioNumber);

            leftAcrId = null;
            rightAcrId = null;
            chkEnableR1.Checked = false;
            chkEnableR2.Checked = false;
            txt1Rname.Text = "";
            txt2Rname.Text = "";
            tb1Acr.Text = "";
            tb2Acr.Text = "";

            if (acrlist != null && acrlist.Count > 0)
            {
                foreach (var item in acrlist)
                {
                    if (item.readerNumber == 0)
                        ApplyAcrToReader(item, isLeft: true);
                    else if (item.readerNumber == 1)
                        ApplyAcrToReader(item, isLeft: false);
                    else if (leftAcrId == null)
                        ApplyAcrToReader(item, isLeft: true);
                    else if (rightAcrId == null)
                        ApplyAcrToReader(item, isLeft: false);
                }
            }
        }
        private void ApplyAcrToReader(AcrDto item, bool isLeft)
        {
            if (isLeft)
            {
                chkEnableR1.Checked = true;
                txt1Rname.Text = item.name;
                tb1Acr.Text = item.acrNumber.ToString();
                cb1RT.SelectedIndex = item.readerType == 2201 ? 0 : 0;
                cbRdir1.Text = item.readerDirection switch
                {
                    1 => "In",
                    2 => "Out",
                    3 => "In/Out",
                    _ => "In"
                };
                leftAcrId = item.id;
            }
            else
            {
                chkEnableR2.Checked = true;
                txt2Rname.Text = item.name;
                tb2Acr.Text = item.acrNumber.ToString();
                cb2RT.SelectedIndex = item.readerType == 2201 ? 0 : 0;
                cbRdir2.Text = item.readerDirection switch
                {
                    1 => "In",
                    2 => "Out",
                    3 => "In/Out",
                    _ => "In"
                };
                rightAcrId = item.id;
            }
        }

        private int GetReaderDirection(ComboBox cb)
        {
            switch (cb.Text)
            {
                case "In": return 1;
                case "Out": return 2;
                case "In/Out": return 3;
                default: return 1;
            }
        }

        // ================= VALIDATION =================
        private bool ValidateForm()
        {
            if (!chkEnableR1.Checked && !chkEnableR2.Checked)
            {
                MessageBox.Show("Please enable at least one reader.");
                return false;
            }

            if (chkEnableR1.Checked)
            {
                if (string.IsNullOrWhiteSpace(txt1Rname.Text))
                {
                    MessageBox.Show("Enter Reader 1 Name");
                    txt1Rname.Focus();
                    return false;
                }

                if (!int.TryParse(tb1Acr.Text, out _))
                {
                    MessageBox.Show("Enter valid ACR for Reader 1");
                    return false;
                }
            }

            if (chkEnableR2.Checked)
            {
                if (string.IsNullOrWhiteSpace(txt2Rname.Text))
                {
                    MessageBox.Show("Enter Reader 2 Name");
                    txt2Rname.Focus();
                    return false;
                }

                if (!int.TryParse(tb2Acr.Text, out _))
                {
                    MessageBox.Show("Enter valid ACR for Reader 2");
                    return false;
                }
            }

            if (chkEnableR1.Checked && chkEnableR2.Checked &&
                tb1Acr.Text == tb2Acr.Text)
            {
                MessageBox.Show("ACR numbers must be different");
                return false;
            }

            return true;
        }

        // ================= ENABLE =================
        private void chkEnableR1_CheckedChanged(object sender, EventArgs e)
        {
            ToggleReader(tblReader1, chkEnableR1.Checked);
        }

        private void chkEnableR2_CheckedChanged(object sender, EventArgs e)
        {
            ToggleReader(tblReader2, chkEnableR2.Checked);
        }

        private void ToggleReader(TableLayoutPanel table, bool enabled)
        {
            foreach (Control c in table.Controls)
            {
                if (c is Label) continue;

                c.Enabled = enabled;
                c.BackColor = enabled
                    ? System.Drawing.Color.White
                    : System.Drawing.Color.Gainsboro;
            }
        }

        // ================= SAVE =================
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            try
            {
                if (chkEnableR1.Checked)
                {
                    var acr = new AcrDto
                    {
                        name = txt1Rname.Text,
                        acrNumber = Convert.ToInt32(tb1Acr.Text),
                        defaultAcrName = txt1Rname.Text,
                        readerNumber = 0,
                        readerType = 2201,
                        readerDirection = GetReaderDirection(cbRdir1),
                        controllerID = _selectedSio.ControllerID,
                        sioNumber = _selectedSio.SioNumber,
                        rex0Number = 3,
                        doorNumber = 3,
                        strikeNumber = 3
                    };

                    if (leftAcrId.HasValue)
                        await _api.UpdateAcrAsync(_selectedSio.ControllerID, _selectedSio.SioNumber, leftAcrId.Value, acr);
                    else
                        await _api.CreateAcrAsync(_selectedSio.ControllerID, _selectedSio.SioNumber, acr);
                }
                else if (leftAcrId.HasValue)
                {
                    await _api.DeleteAcrAsync(_selectedSio.ControllerID, _selectedSio.SioNumber, leftAcrId.Value);
                }

                if (chkEnableR2.Checked)
                {
                    var acr = new AcrDto
                    {
                        name = txt2Rname.Text,
                        acrNumber = Convert.ToInt32(tb2Acr.Text),
                        defaultAcrName = txt2Rname.Text,
                        readerNumber = 1,
                        readerType = 2201,
                        readerDirection = GetReaderDirection(cbRdir2),
                        controllerID = _selectedSio.ControllerID,
                        sioNumber = _selectedSio.SioNumber,
                        rex0Number = 3,
                        doorNumber = 3,
                        strikeNumber = 3
                    };

                    if (rightAcrId.HasValue)
                        await _api.UpdateAcrAsync(_selectedSio.ControllerID, _selectedSio.SioNumber, rightAcrId.Value, acr);
                    else
                        await _api.CreateAcrAsync(_selectedSio.ControllerID, _selectedSio.SioNumber, acr);
                }
                else if (rightAcrId.HasValue)
                {
                    await _api.DeleteAcrAsync(_selectedSio.ControllerID, _selectedSio.SioNumber, rightAcrId.Value);
                }

                MessageBox.Show("ACRs Updated Successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // ================= NUMERIC ONLY =================
        private void tb1Acr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void tb2Acr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            MainForm.Instance.LoadPage(
                new EditControllerControl(_controller, _sioList),
                false
            );
        }
    }
}
