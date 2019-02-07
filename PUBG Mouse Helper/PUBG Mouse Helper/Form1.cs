using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUBG_Mouse_Helper
{
    public partial class Form1 : Form, IOkButtonPressedInSavePresetDialog, IOnHotkeyPressed
    {
        private class Preset
        {
            public Preset(bool isUserDefined, string presetName, int dy, int shotInterval, int pullDelay)
            {
                UserDefined = isUserDefined;
                PresetName = presetName;
                Dy = dy;
                ShotInterval = shotInterval;
                PullDelay = pullDelay;
            }
            public Preset() { }
            public string PresetName { get; set; }
            public bool UserDefined { get; set; }
            public int Dy { get; set; }
            public int PullDelay { get; set; }
            public int ShotInterval { get; set; }
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
        private WeaponSelectorForm weaponSelectorForm;

        ToolStripMenuItem userPresetsMenuItem;


        private int activeWeaponSlot = 1;

        private Dictionary<int, string> weaponSlotPresetNameDict = new Dictionary<int, string>()
        {
            { 1,"" },
            { 2,"" },
            { 3,"" }
        };

        public Form1()
        {
            InitializeComponent();
            trackBarShotInterval.Scroll += OnTrackBarScroll;
            trackBarDy.Scroll += OnTrackBarScroll;
            trackBarPullDelay.Scroll += OnTrackBarScroll;
            poller = new Poller(this);
        }

        #region Interface methods

        public void OnOkButtonPressed(string presetName)
        {
            try
            {
                if (Savefilehandler.SavePresets(presetName, trackBarDy.Value, trackBarShotInterval.Value, trackBarPullDelay.Value))
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

        public void OnEnterPressed()
        {
            if (weaponSelectorForm != null)
            {
                this.poller.PollKeyboardKeysForConfigChange = true;
                if (weaponSelectorForm.GetSelectedPresetName() != "")
                {
                    weaponSlotPresetNameDict[activeWeaponSlot] = weaponSelectorForm.GetSelectedPresetName();
                    HelperFunctions.GetToolStripMenuItemFromText(toolStripMenuItemPresets, weaponSlotPresetNameDict[activeWeaponSlot]).PerformClick();
                    new MessageToast($"Weapon slot #{activeWeaponSlot}\n{weaponSlotPresetNameDict[activeWeaponSlot]}", 50).Show();
                }
                weaponSelectorForm.Close();
                weaponSelectorForm = null;

            }
            else
            {
                List<ToolStripMenuItem> presetMenuItemsList = HelperFunctions.GetListOfAllPresetMenuItems(toolStripMenuItemPresets);

                if (presetMenuItemsList.Count > 0)
                {
                    this.poller.PollKeyboardKeysForConfigChange = false;
                    weaponSelectorForm = new WeaponSelectorForm(presetMenuItemsList);
                    weaponSelectorForm.Show();
                }
            }

        }

        public void OnUpArrowPressed()
        {
            if (trackBarDy.Value == trackBarDy.Minimum)
                return;
            trackBarDy.Value--;
            this.CurrentPreset = new Preset();
            new MessageToast($"dy = {trackBarDy.Value}").Show(); //show a message to user regarding the new dy value
        }

        public void OnDownArrowPressed()
        {
            if (trackBarDy.Value == trackBarDy.Maximum)
                return;
            trackBarDy.Value++;
            this.CurrentPreset = new Preset();
            new MessageToast($"dy = {trackBarDy.Value}").Show(); //show a message to user regarding the new dy value
        }

        public void OnLeftSquareBracketKeyPressed()
        {
            if (trackBarShotInterval.Value - 10 < trackBarShotInterval.Minimum)
                return;
            trackBarShotInterval.Value -= 10;
            this.CurrentPreset = new Preset();
            new MessageToast($"Shot interval = {trackBarShotInterval.Value}").Show();
        }

        public void OnRightSquareBracketKeyPressed()
        {
            if (trackBarShotInterval.Value + 10 > trackBarShotInterval.Maximum)
                return;
            trackBarShotInterval.Value += 10;
            this.CurrentPreset = new Preset();
            new MessageToast($"Shot interval = {trackBarShotInterval.Value}").Show();
        }

        public void OnSemicolonKeyPressed()
        {
            if (trackBarPullDelay.Value == trackBarPullDelay.Minimum)
                return;
            trackBarPullDelay.Value--;
            this.CurrentPreset = new Preset();
            new MessageToast($"Pull delay = {trackBarPullDelay.Value}").Show();
        }

        public void OnSingleQuoteKeyPressed()
        {
            if (trackBarPullDelay.Value == trackBarPullDelay.Maximum)
                return;
            trackBarPullDelay.Value++;
            this.CurrentPreset = new Preset();
            new MessageToast($"Pull delay = {trackBarPullDelay.Value}").Show();
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
            try
            {
                HelperFunctions.GetToolStripMenuItemFromText(toolStripMenuItemPresets, weaponSlotPresetNameDict[slotNumber]).PerformClick();
            }
            catch (PresetNotFoundException pnfex)
            {
                Logger.Log(pnfex.Message);
            }
            catch (PresetMenuNotPopulatedException pmnpex)
            {
                Logger.Log(pmnpex.Message);
            }
            finally
            {
                new MessageToast($"Weapon slot #{activeWeaponSlot}\n{weaponSlotPresetNameDict[slotNumber]}",50).Show();
            }

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

                trackBarShotInterval.Value = this.CurrentPreset.ShotInterval;
                trackBarDy.Value = this.CurrentPreset.Dy;
                trackBarPullDelay.Value = this.CurrentPreset.PullDelay;
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
                toolStripStatusLabel1.Text = $"Monitor active : (dy={trackBarDy.Value}, ShotInterval={trackBarShotInterval.Value}, PullDelay={trackBarPullDelay.Value})";
                notifyIcon1.Text = toolStripStatusLabel1.Text;
            }
            else
            {
                notifyIcon1.Text = HelperFunctions.GetApplicationName();
            }

            poller.Poll(trackBarDy.Value, trackBarShotInterval.Value, trackBarPullDelay.Value, toolStripComboBoxFireButton.Text);

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
2. Use F6 key to enable/disable the program.
3. Use F7 key to toggle recoil compensation on and off.
4. You can change the active preset while monitoring is on by pressing Enter key and making a selection.
5. You can use the up and down arrow keys to change the vertical recoil correction.
6. You can use the [ and ] keys to change the shot interval value.
7. You can use the ; and ' keys to change the pull delay value.
8. Use weapon slot keys 1, 2 and 3 for different weapon presets.
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
                if (CurrentPreset != null && CurrentPreset.UserDefined)
                {
                    toolStripMenuItemDeletePreset.Enabled = true;
                }
            }
        }


    }


}
