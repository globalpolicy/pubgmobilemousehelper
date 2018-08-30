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
            public Preset(bool isUserDefined, string presetName, int dx, int dy, int waitMs, int delayMs, char shootKey)
            {
                UserDefined = isUserDefined;
                PresetName = presetName;
                Dx = dx;
                Dy = dy;
                WaitMs = waitMs;
                DelayMs = delayMs;
                ShootKey = shootKey;
            }
            public Preset() { }
            public string PresetName { get; set; }
            public bool UserDefined { get; set; }
            public int Dx { get; set; }
            public int Dy { get; set; }
            public int WaitMs { get; set; }
            public int DelayMs { get; set; }
            public char ShootKey { get; set; }
            public bool IsEmpty() => PresetName == null;
        }

        private Preset _currentPreset;

        private Preset CurrentPreset
        {
            get { return this._currentPreset; }
            set
            {
                this._currentPreset = value;
                this.OnCurrentPresetChanged();
            }
        }

        private Poller poller;

        ToolStripMenuItem userPresetsMenuItem;

        private int presetSwitchHotkeyIndex = 0;

        KeyboardHook kHook;

        bool isAppActivated = false;

        public Form1()
        {
            InitializeComponent();
            kHook = new KeyboardHook();
            kHook.KeyUp += ActivateDeactivateHotkey;
            trackBar1.Scroll += OnTrackBarScroll;
            trackBar2.Scroll += OnTrackBarScroll;
            trackBar3.Scroll += OnTrackBarScroll;
            trackBar4.Scroll += OnTrackBarScroll;
            poller = new Poller(this);
            kHook.Install();
        }

        private void ActivateDeactivateHotkey(KeyboardHook.VKeys key)
        {
            if (key == KeyboardHook.VKeys.F8)
            {
                if (isAppActivated)
                    DeActivateApp();
                else
                    ActivateApp();
                OnF8Pressed();
            }
        }

        #region Interface methods
        public void OnOkButtonPressed(string presetName)
        {
            try
            {
                if (Savefilehandler.SavePresets(presetName, trackBar1.Value, trackBar2.Value, trackBar3.Value, trackBar4.Value, textBox1.Text.ElementAt(0)))
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
            List<ToolStripMenuItem> presetMenuItemsList = new List<ToolStripMenuItem>();

            foreach (ToolStripMenuItem presetsMenuItem in toolStripMenuItemPresets.DropDownItems)
            {
                if (!presetsMenuItem.HasDropDownItems)
                {
                    presetMenuItemsList.Add(presetsMenuItem);
                }
                else
                {
                    foreach (ToolStripMenuItem userPresetMenuItem in presetsMenuItem.DropDownItems)
                    {
                        presetMenuItemsList.Add(userPresetMenuItem);
                    }
                }
            }

            presetSwitchHotkeyIndex++;
            presetSwitchHotkeyIndex = presetSwitchHotkeyIndex % presetMenuItemsList.Count; //circle back to the range of available presets

            presetMenuItemsList[presetSwitchHotkeyIndex].PerformClick(); //call the corresponding method

            //show a message to user regarding the preset selected
            new MessageToast($"{presetMenuItemsList[presetSwitchHotkeyIndex].Text}").Show();
        }

        public void OnRightArrowPressed()
        {
            if (trackBar1.Value == trackBar1.Maximum)
                return;
            trackBar1.Value++;
            CurrentPreset = new Preset();
            new MessageToast($"dx = {trackBar1.Value}").Show(); //show a message to user regarding the new dx value
        }

        public void OnLeftArrowPressed()
        {
            if (trackBar1.Value == trackBar1.Minimum)
                return;
            trackBar1.Value--;
            CurrentPreset = new Preset();
            new MessageToast($"dx = {trackBar1.Value}").Show(); //show a message to user regarding the new dx value
        }

        public void OnUpArrowPressed()
        {
            if (trackBar2.Value == trackBar2.Minimum)
                return;
            trackBar2.Value--;
            CurrentPreset = new Preset();
            new MessageToast($"dy = {trackBar2.Value}").Show(); //show a message to user regarding the new dy value
        }

        public void OnDownArrowPressed()
        {
            if (trackBar2.Value == trackBar2.Maximum)
                return;
            trackBar2.Value++;
            CurrentPreset = new Preset();
            new MessageToast($"dy = {trackBar2.Value}").Show(); //show a message to user regarding the new dy value
        }

        public void OnToggleRecoilCompensationHotkeyPressed()
        {
            toolStripMenuItemEnableAntiRecoil.PerformClick();
            string enabledOrDisabled = toolStripMenuItemEnableAntiRecoil.Checked ? "enable" : "disable";
            string enabledOrDisabledANTI = !toolStripMenuItemEnableAntiRecoil.Checked ? "enable" : "disable";
            new MessageToast($"Recoil compensation {enabledOrDisabled}d.\nPress F7 to {enabledOrDisabledANTI}.", 50).Show();
        }

        public void OnF8Pressed()
        {
            if (isAppActivated)
                new MessageToast("App Activated").Show();
            else
                new MessageToast("App Deactivated").Show();
        }
        #endregion


        private void OnCurrentPresetChanged()
        {
            if (!CurrentPreset.IsEmpty()) //if preset is named
            {
                Text = HelperFunctions.GetApplicationName() + " - " + CurrentPreset.PresetName;
                if (!timer1.Enabled) //only allow deleting presets if timer is off i.e. monitoring is stopped
                {
                    if (CurrentPreset.UserDefined)
                    {
                        toolStripMenuItemDeletePreset.Enabled = true;
                    }
                    else
                    {
                        toolStripMenuItemDeletePreset.Enabled = false;
                    }
                }

                trackBar1.Value = CurrentPreset.Dx;
                trackBar2.Value = CurrentPreset.Dy;
                trackBar3.Value = CurrentPreset.WaitMs;
                trackBar4.Value = CurrentPreset.DelayMs;
                KeyboardHelperClass.shootKey = CurrentPreset.ShootKey.ToString();
                textBox1.Text = KeyboardHelperClass.shootKey;
            }
            else
            {
                Text = HelperFunctions.GetApplicationName();
                toolStripMenuItemDeletePreset.Enabled = false;
            }
        }

        private void OnTrackBarScroll(object sender, EventArgs e)
        {
            if (isAppActivated)
            {
                //show tooltip regarding the trackbar value
                TrackBar thisTrackBar = (TrackBar)sender;
                toolTip1.SetToolTip(thisTrackBar, thisTrackBar.Value + "");
                //empty out the current preset if any
                CurrentPreset = new Preset();
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ActivateApp();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = trackBar4.Value;
            poller.Poll(trackBar1.Value, trackBar2.Value, (uint)trackBar3.Value);
            toolStripStatusLabel1.Text = $"Monitor active : (dx={trackBar1.Value}, dy={trackBar2.Value}, WaitMs={trackBar3.Value}, DelayMs={trackBar4.Value})";
            notifyIcon1.Text = toolStripStatusLabel1.Text;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            DeActivateApp();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            CurrentPreset = new Preset(false, presetName, 0, 6, 4, 8, ']');
            this.presetSwitchHotkeyIndex = 0;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            CurrentPreset = new Preset(false, presetName, 0, 8, 2, 6, ']');
            this.presetSwitchHotkeyIndex = 1;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            CurrentPreset = new Preset(false, presetName, 0, 6, 2, 6, ']');
            this.presetSwitchHotkeyIndex = 2;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            CurrentPreset = new Preset(false, presetName, 1, 10, 2, 6, ']');
            this.presetSwitchHotkeyIndex = 3;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            CurrentPreset = new Preset(false, presetName, 0, 9, 2, 6, ']');
            this.presetSwitchHotkeyIndex = 4;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            string presetName = ((ToolStripItem)sender).Text;
            CurrentPreset = new Preset(false, presetName, 1, 8, 2, 6, ']');
            this.presetSwitchHotkeyIndex = 5;
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
                    if (presetValues.Count == 4)
                    {
                        CurrentPreset = new Preset(true, presetName, presetValues[0], presetValues[1], presetValues[2], presetValues[3], KeyboardHelperClass.inactiveShootKey.ElementAt(0));
                        presetSwitchHotkeyIndex = 6 + Savefilehandler.GetSavedPresetNamesList().IndexOf(presetName); //6=number of default presets+1
                        toolStripStatusLabel1.Text = $"Loaded preset {CurrentPreset.PresetName}";
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
            if (!CurrentPreset.IsEmpty() && CurrentPreset.UserDefined)
            {
                try
                {
                    if (!Savefilehandler.DeletePreset(CurrentPreset.PresetName))
                    {
                        throw new Exception($"Could not delete preset {CurrentPreset.PresetName}");
                    }
                    else
                    {
                        toolStripMenuItemDeletePreset.Enabled = false;
                        toolStripMenuItemPresets.DropDownItems.Remove(userPresetsMenuItem);
                        LoadUserPresetsNames();
                        toolStripStatusLabel1.Text = $"Deleted preset {CurrentPreset.PresetName}";
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
                Debug.Print("Exception while loading presets.");
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
5. Use F8 key to activate/deactivate the app.";

            MessageBox.Show(instructionMsg, "Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (timer1.Enabled == true)
            {
                //eat up shortcut keys when timer is running i.e. monitoring is active lest there be a conflict
                switch (keyData)
                {
                    case Keys.F7:
                        return false;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void toolStripMenuItemEnableAntiRecoil_Click(object sender, EventArgs e)
        {
            poller.PerformRecoilCompensation = toolStripMenuItemEnableAntiRecoil.Checked;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var key = e.KeyChar.ToString();
            if (textBox1.Text != key)
            {
                textBox1.Text = key;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mainKey = textBox1.Text;
            switch (mainKey)
            {
                case "~":
                    mainKey = "{~}";
                    break;
                case "%":
                    mainKey = "{%}";
                    break;
                case "^":
                    mainKey = "{^}";
                    break;
                case "+":
                    mainKey = "{+}";
                    break;
                default:
                    break;
            }
            KeyboardHelperClass.shootKey = mainKey;
        }

        private void ActivateApp()
        {
            if (!isAppActivated)
            {
                toolStripMenuItemStop.Enabled = true;
                toolStripMenuItemSaveAsPreset.Enabled = false;
                toolStripMenuItemDeletePreset.Enabled = false;
                toolStripMenuItemActivate.Enabled = false;
                timer1.Start();
                isAppActivated = true;
            }
        }

        private void DeActivateApp()
        {
            if (isAppActivated)
            {
                timer1.Stop();
                toolStripStatusLabel1.Text = "Ready";
                notifyIcon1.Text = HelperFunctions.GetApplicationName();
                toolStripMenuItemSaveAsPreset.Enabled = true;
                if (!CurrentPreset.IsEmpty() && CurrentPreset.UserDefined)
                {
                    toolStripMenuItemDeletePreset.Enabled = true;
                }
                toolStripMenuItemActivate.Enabled = true;
                toolStripMenuItemStop.Enabled = false;
                isAppActivated = false;
            }
        }

        ~Form1()
        {
            kHook.Uninstall();
        }
    }


}
