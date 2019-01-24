using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUBG_Mouse_Helper
{
    public partial class Form1 : Form, IOkButtonPressedInSavePresetDialog, IOnHotkeyPressed
    {
        private class Preset
        {
            public Preset(bool isUserDefined, string presetName, int dx, int dy, int timerDelay)
            {
                UserDefined = isUserDefined;
                PresetName = presetName;
                Dx = dx;
                Dy = dy;
                TimerDelay = timerDelay;
            }
            public Preset() { }
            public string PresetName { get; set; }
            public bool UserDefined { get; set; }
            public int Dx { get; set; }
            public int Dy { get; set; }
            public int TimerDelay { get; set; }
            public bool IsEmpty() => PresetName == null;
        }

        private Preset _currentPreset;

        private Preset CurrentPreset
        {
            get { return this._currentPreset; }
            set
            {
                this._currentPreset = value;
                this.UpdateTrackbarValuesAndGUI();
            }
        }

        private Poller poller;

        ToolStripMenuItem userPresetsMenuItem;

        private int presetSwitchHotkeyIndex = 0;

        private int activeWeaponSlot = 1;

        private Dictionary<int, int> weaponSlotPresetNumberDict = new Dictionary<int, int>()
        {
            { 1,0 },
            { 2,0 },
            { 3,0 }
        };

        public Form1()
        {
            InitializeComponent();
            trackBar1.Scroll += OnTrackBarScroll;
            trackBar2.Scroll += OnTrackBarScroll;
            trackBar4.Scroll += OnTrackBarScroll;
            poller = new Poller(this);
        }

        #region Interface methods

        public void OnOkButtonPressed(string presetName)
        {
            try
            {
                if (Savefilehandler.SavePresets(presetName, trackBar1.Value, trackBar2.Value, trackBar4.Value))
                {
                    toolStripStatusLabel1.Text = $"Saved preset {presetName}";
                    toolStripMenuItemPresets.DropDownItems.Remove(userPresetsMenuItem);
                    LoadUserPresetsNames();
                }
                else
                    throw new Exception("Return value false");
            }
            catch (Exception ex)
            {
                toolStripStatusLabel1.Text = $"Failed to save preset {presetName}";
            }


        }

        public void OnPresetSwitchHotkeyPressed()
        {
            List<ToolStripMenuItem> presetMenuItemsList = HelperFunctions.GetListOfAllPresetMenuItems(toolStripMenuItemPresets);

            presetSwitchHotkeyIndex++;
            presetSwitchHotkeyIndex = presetSwitchHotkeyIndex % presetMenuItemsList.Count; //circle back to the range of available presets

            weaponSlotPresetNumberDict[activeWeaponSlot] = presetSwitchHotkeyIndex;

            presetMenuItemsList[presetSwitchHotkeyIndex].PerformClick(); //call the corresponding method

            //show a message to user regarding the preset selected
            new MessageToast($"Weapon slot #{activeWeaponSlot}\n{presetMenuItemsList[presetSwitchHotkeyIndex].Text}").Show();
        }

        public void OnRightArrowPressed()
        {
            if (trackBar1.Value == trackBar1.Maximum)
                return;
            trackBar1.Value++;
            this.CurrentPreset = new Preset();
            new MessageToast($"dx = {trackBar1.Value}").Show(); //show a message to user regarding the new dx value
        }

        public void OnLeftArrowPressed()
        {
            if (trackBar1.Value == trackBar1.Minimum)
                return;
            trackBar1.Value--;
            this.CurrentPreset = new Preset();
            new MessageToast($"dx = {trackBar1.Value}").Show(); //show a message to user regarding the new dx value
        }

        public void OnUpArrowPressed()
        {
            if (trackBar2.Value == trackBar2.Minimum)
                return;
            trackBar2.Value--;
            this.CurrentPreset = new Preset();
            new MessageToast($"dy = {trackBar2.Value}").Show(); //show a message to user regarding the new dy value
        }

        public void OnDownArrowPressed()
        {
            if (trackBar2.Value == trackBar2.Maximum)
                return;
            trackBar2.Value++;
            this.CurrentPreset = new Preset();
            new MessageToast($"dy = {trackBar2.Value}").Show(); //show a message to user regarding the new dy value
        }

        public void OnToggleRecoilCompensationHotkeyPressed()
        {
            toolStripMenuItemEnableAntiRecoil.PerformClick();
            string enabledOrDisabled = toolStripMenuItemEnableAntiRecoil.Checked ? "enable" : "disable";
            string enabledOrDisabledANTI = !toolStripMenuItemEnableAntiRecoil.Checked ? "enable" : "disable";
            new MessageToast($"Recoil compensation {enabledOrDisabled}d.\nPress F7 to {enabledOrDisabledANTI}.", 50).Show();
        }

        public void OnToggleActivateProgramHotkeyPressed()
        {
            toolStripMenuItemActivate.PerformClick();
            string enabledOrDisabled = toolStripMenuItemActivate.Checked ? "activate" : "deactivate";
            string enabledOrDisabledANTI = !toolStripMenuItemActivate.Checked ? "activate" : "deactivate";
            new MessageToast($"Program {enabledOrDisabled}d.\nPress F6 to {enabledOrDisabledANTI}.", 50).Show();
        }

        public void OnWeaponSlotChangeHotkeyPressed(int slotNumber)
        {
            activeWeaponSlot = slotNumber;
            presetSwitchHotkeyIndex = weaponSlotPresetNumberDict[slotNumber];

            List<ToolStripMenuItem> presetMenuItemsList = HelperFunctions.GetListOfAllPresetMenuItems(toolStripMenuItemPresets);
            presetMenuItemsList[presetSwitchHotkeyIndex].PerformClick();

            new MessageToast($"Weapon slot #{activeWeaponSlot}\n{presetMenuItemsList[presetSwitchHotkeyIndex].Text}").Show();
        }

        #endregion


        private void UpdateTrackbarValuesAndGUI()
        {
            if (!this.CurrentPreset.IsEmpty()) //if preset is named
            {
                this.Text = HelperFunctions.GetApplicationName() + " - " + this.CurrentPreset.PresetName;
                toolStripStatusLabel1.Text = $"Loaded preset {this.CurrentPreset.PresetName}";

                if (!poller.Activated) //only allow deleting presets if monitoring is stopped
                {
                    if (this.CurrentPreset.UserDefined)
                    {
                        toolStripMenuItemDeletePreset.Enabled = true;
                    }
                    else
                    {
                        toolStripMenuItemDeletePreset.Enabled = false;
                    }
                }

                trackBar1.Value = this.CurrentPreset.Dx;
                trackBar2.Value = this.CurrentPreset.Dy;
                trackBar4.Value = this.CurrentPreset.TimerDelay;
            }
            else
            {
                this.Text = HelperFunctions.GetApplicationName();
                toolStripMenuItemDeletePreset.Enabled = false;
            }
        }

        private void OnTrackBarScroll(object sender, EventArgs e)
        {
            //show tooltip regarding the trackbar value
            TrackBar thisTrackBar = (TrackBar)sender;
            toolTip1.SetToolTip(thisTrackBar, thisTrackBar.Value + "");
            //empty out the current preset if any
            this.CurrentPreset = new Preset();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (toolStripMenuItemActivate.Checked)
            {
                toolStripStatusLabel1.Text = $"Monitor active : (dx={trackBar1.Value}, dy={trackBar2.Value}, TimerDelay={trackBar4.Value})";
                notifyIcon1.Text = toolStripStatusLabel1.Text;
            }
            else
            {
                notifyIcon1.Text = HelperFunctions.GetApplicationName();
            }
            timer1.Interval = trackBar4.Value;
            poller.Poll(trackBar1.Value, trackBar2.Value, toolStripComboBoxFireButton.Text);

        }



        private void toolStripMenuItemM16A4_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 2, 1);
            this.presetSwitchHotkeyIndex = 0;
        }

        private void toolStripMenuItemMini14_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 3, 52);
            this.presetSwitchHotkeyIndex = 1;
        }

        private void toolStripMenuItemScarL_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 3, 1);
            this.presetSwitchHotkeyIndex = 2;
        }

        private void toolStripMenuItemM416_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 2, 21);
            this.presetSwitchHotkeyIndex = 3;
        }

        private void toolStripMenuItemQBU_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 2, 24);
            this.presetSwitchHotkeyIndex = 4;
        }

        private void toolStripMenuItemAKM_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 3, 24);
            this.presetSwitchHotkeyIndex = 5;
        }

        private void toolStripMenuItemSLR_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 6, 60);
            this.presetSwitchHotkeyIndex = 6;
        }

        private void toolStripMenuItemSKS_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 3, 44);
            this.presetSwitchHotkeyIndex = 7;
        }

        private void toolStripMenuItemGroza_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 2, 24);
            this.presetSwitchHotkeyIndex = 8;
        }

        private void toolStripMenuItemM762_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 4, 13);
            this.presetSwitchHotkeyIndex = 9;
        }

        private void toolStripMenuItemMk14_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 3, 32);
            this.presetSwitchHotkeyIndex = 10;
        }

        private void toolStripMenuItemUMP9_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            this.CurrentPreset = new Preset(false, presetName, 0, 2, 9);
            this.presetSwitchHotkeyIndex = 11;
        }

        private void trayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }



        private void toolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItemSaveAsPreset_Click(object sender, EventArgs e)
        {
            new SavePresetAsDialog(this).Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = HelperFunctions.GetApplicationName();
            LoadUserPresetsNames();
            toolStripMenuItemPresets.DropDownItems[0].PerformClick(); //load the first preset by default
        }

        private void OnUserPresetClicked(object sender, EventArgs eventArgs)
        {
            try
            {
                string presetName = ((ToolStripItem)sender).Text;
                if (Savefilehandler.GetSavedPresetNamesList().Contains(presetName))
                {
                    List<int> presetValues = Savefilehandler.GetPresetValuesFromName(presetName);
                    if (presetValues.Count == 3)
                    {
                        this.CurrentPreset = new Preset(true, presetName, presetValues[0], presetValues[1], presetValues[2]);
                        this.presetSwitchHotkeyIndex = 12 + Savefilehandler.GetSavedPresetNamesList().IndexOf(presetName); //12=number of default presets+1


                    }
                    else
                    {
                        if (!Savefilehandler.DeletePreset(presetName))
                            throw new Exception($"Cannot delete preset {presetName}");
                    }
                }
                else
                {
                    toolStripStatusLabel1.Text = $"Preset {presetName} not found";
                }

            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
            }
        }



        private void toolStripMenuItemDeletePreset_Click(object sender, EventArgs e)
        {
            if (!this.CurrentPreset.IsEmpty() && this.CurrentPreset.UserDefined)
            {
                try
                {
                    if (!Savefilehandler.DeletePreset(this.CurrentPreset.PresetName))
                    {
                        throw new Exception($"Could not delete preset {this.CurrentPreset.PresetName}");
                    }
                    else
                    {
                        toolStripMenuItemDeletePreset.Enabled = false;
                        toolStripMenuItemPresets.DropDownItems.Remove(userPresetsMenuItem);
                        LoadUserPresetsNames();
                        toolStripStatusLabel1.Text = $"Deleted preset {this.CurrentPreset.PresetName}";
                    }
                }
                catch (Exception ex)
                {
                    toolStripStatusLabel1.Text = ex.Message;
                }

            }
        }

        private void LoadUserPresetsNames()
        {
            try
            {
                List<string> savedPresetNames = Savefilehandler.GetSavedPresetNamesList();
                if (savedPresetNames.Count > 0)
                {
                    userPresetsMenuItem = new ToolStripMenuItem("User presets");
                    toolStripMenuItemPresets.DropDownItems.Add(userPresetsMenuItem);

                    foreach (var presetName in savedPresetNames)
                    {
                        ToolStripItem newPreset = new ToolStripMenuItem(presetName);
                        newPreset.Click += OnUserPresetClicked;
                        userPresetsMenuItem.DropDownItems.Add(newPreset);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.Print("Exception in loading presets.");
            }
        }

        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Author : s0ft\nBlog : c0dew0rth.blogspot.com\nContact : yciloplabolg@gmail.com", HelperFunctions.GetApplicationName(), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void toolStripMenuItemInstructions_Click(object sender, EventArgs e)
        {

            string instructionMsg = @"The program should be pretty self-explanatory.

Here are a couple pro-tips anyway :

1. Try running as administrator if the program doesn't seem to work.
2. You can change the active preset while monitoring is on by pressing Enter key.
3. You can use the arrow keys to change the recoil correction parameters.
4. Use F7 key to toggle recoil compensation on and off.
5. Use F6 key to enable/disable the program.
6. Use weapon slot keys 1, 2 and 3 for different weapon presets.
7. The inbuilt presets are for iron-sight configs.
";

            MessageBox.Show(instructionMsg, "Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (toolStripMenuItemActivate.Checked)
            {
                //eat up shortcut key F7 for 'Anti-Recoil' when program is activated, lest there be a conflict
                if (keyData == Keys.F7)
                    return false;

            }

            //swallow the shortcut key F6 for 'Activate' menu, lest there be a conflict
            if (keyData == Keys.F6)
                return false;

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void toolStripMenuItemEnableAntiRecoil_Click(object sender, EventArgs e)
        {
            this.poller.PerformRecoilCompensation = toolStripMenuItemEnableAntiRecoil.Checked;
        }

        private void toolStripMenuItemActivate_Click(object sender, EventArgs e)
        {
            if (toolStripMenuItemActivate.Checked)
            {
                this.poller.Activated = true;
                toolStripMenuItemSaveAsPreset.Enabled = false;
                toolStripMenuItemDeletePreset.Enabled = false;
            }
            else
            {
                this.poller.Activated = false;
                toolStripStatusLabel1.Text = "Ready";
                notifyIcon1.Text = HelperFunctions.GetApplicationName();
                toolStripMenuItemSaveAsPreset.Enabled = true;
                if (!CurrentPreset.IsEmpty() && CurrentPreset.UserDefined)
                {
                    toolStripMenuItemDeletePreset.Enabled = true;
                }
            }
        }

        
    }


}
