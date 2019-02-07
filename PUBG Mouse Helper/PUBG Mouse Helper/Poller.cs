using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;
using System.Timers;

namespace PUBG_Mouse_Helper
{
    public class Poller
    {
        public bool PerformRecoilCompensation { get; set; } = true;
        public bool Activated { get; set; } = false;
        public bool PollKeyboardKeysForConfigChange { get; set; } = true;

        private bool firingState = false;

        private int _dy;
        private int _shotInterval = 1;
        private int _pullDelay;
        private string _fireButton;
        private IOnHotkeyPressed onHotkeyPressed;

        private System.Timers.Timer shootTimer;
        private System.Timers.Timer pullDownMouseTimer;


        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        public Poller(IOnHotkeyPressed onHotkeyPressed)
        {
            this.onHotkeyPressed = onHotkeyPressed;

            shootTimer = new System.Timers.Timer();
            shootTimer.Elapsed += (s, e) => PollFireButton();
            shootTimer.Interval = 1; //this will be updated later in realtime in the elapsed event itself

            pullDownMouseTimer = new System.Timers.Timer();
            pullDownMouseTimer.Elapsed += (s, e) => PullDownMouse();
            pullDownMouseTimer.AutoReset = false; //will restart manually
            pullDownMouseTimer.Interval = 1; //as realtime as possible to monitor and act on whether firebutton is being pressed
            pullDownMouseTimer.Start();
        }



        public void Poll(int dy, int shotInterval, int pullDelay, string fireButton)
        {
            this._dy = dy;
            this._shotInterval = shotInterval;
            this._pullDelay = pullDelay;
            this._fireButton = fireButton;

            if (this.Activated)
            {
                if (this.firingState == false) this.shootTimer.Interval = 1;
                this.shootTimer.Enabled = true;
                PollEnterKey();
                PollToggleRecoilCompensationHotkey();
                if (this.PollKeyboardKeysForConfigChange)
                {
                    PollTrackbarValuesChangeHotkey();
                    PollWeaponSlotChangeHotkey();
                }
            }
            else
            {
                this.shootTimer.Enabled = false;
            }

            PollToggleActivateProgramHotkey();
        }

        private void PollWeaponSlotChangeHotkey()
        {
            short weaponSlotNumberPressed = 0;

            short gaks = GetAsyncKeyState(Keys.D1);
            if ((gaks & 0b10000000_00000000) > 0)
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.D1) & 0b10000000_00000000) > 0, 100); //wait until 1 is released with timeout of 100ms
                weaponSlotNumberPressed = 1;
            }
            gaks = GetAsyncKeyState(Keys.D2);
            if ((gaks & 0b10000000_00000000) > 0)
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.D2) & 0b10000000_00000000) > 0, 100); //wait until 2 is released with timeout of 100ms
                weaponSlotNumberPressed = 2;
            }
            gaks = GetAsyncKeyState(Keys.D3);
            if ((gaks & 0b10000000_00000000) > 0)
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.D3) & 0b10000000_00000000) > 0, 100); //wait until 3 is released with timeout of 100ms
                weaponSlotNumberPressed = 3;
            }

            if (weaponSlotNumberPressed > 0)
                this.onHotkeyPressed.OnWeaponSlotChangeHotkeyPressed(weaponSlotNumberPressed);
        }

        private void PollEnterKey()
        {
            short gaks = GetAsyncKeyState(Keys.Enter);
            if ((gaks & 0b10000000_00000000) > 0) //if Enter was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.Enter) & 0b10000000_00000000) > 0, 100); //wait until Enter is released with timeout of 100ms
                this.onHotkeyPressed.OnEnterPressed();
            }
        }

        private void PollTrackbarValuesChangeHotkey()
        {
            short gaks;

            gaks = GetAsyncKeyState(Keys.Up);
            if ((gaks & 0b10000000_00000000) > 0)//if Up arrow key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.Up) & 0b10000000_00000000) > 0, 100); //wait until Up arrow key is released with timeout of 100ms
                this.onHotkeyPressed.OnUpArrowPressed();
            }

            gaks = GetAsyncKeyState(Keys.Down);
            if ((gaks & 0b10000000_00000000) > 0)//if Down arrow key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.Down) & 0b10000000_00000000) > 0, 100); //wait until Down arrow key is released with timeout of 100ms
                this.onHotkeyPressed.OnDownArrowPressed();
            }

            gaks = GetAsyncKeyState(Keys.OemOpenBrackets);
            if ((gaks & 0b10000000_00000000) > 0)//if [ key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.OemOpenBrackets) & 0b10000000_00000000) > 0, 10); //wait until [ key is released with timeout of 10ms
                this.onHotkeyPressed.OnLeftSquareBracketKeyPressed();
            }

            gaks = GetAsyncKeyState(Keys.OemCloseBrackets);
            if ((gaks & 0b10000000_00000000) > 0)//if ] key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.OemCloseBrackets) & 0b10000000_00000000) > 0, 10); //wait until ] key is released with timeout of 10ms
                this.onHotkeyPressed.OnRightSquareBracketKeyPressed();
            }

            gaks = GetAsyncKeyState(Keys.OemSemicolon);
            if ((gaks & 0b10000000_00000000) > 0)//if ; key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.OemSemicolon) & 0b10000000_00000000) > 0, 100); //wait until ; key is released with timeout of 100ms
                this.onHotkeyPressed.OnSemicolonKeyPressed();
            }

            gaks = GetAsyncKeyState(Keys.OemQuotes);
            if ((gaks & 0b10000000_00000000) > 0)//if ' key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.OemQuotes) & 0b10000000_00000000) > 0, 100); //wait until ' key is released with timeout of 100ms
                this.onHotkeyPressed.OnSingleQuoteKeyPressed();
            }
        }

        private void PollToggleRecoilCompensationHotkey()
        {
            short gaks = GetAsyncKeyState(Keys.F7);
            if ((gaks & 0b10000000_00000000) > 0)//if F7 key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.F7) & 0b10000000_00000000) > 0, 100); //wait until F7 key is released with timeout of 100ms
                this.onHotkeyPressed.OnToggleRecoilCompensationHotkeyPressed();
            }
        }

        private void PollToggleActivateProgramHotkey()
        {
            short gaks = GetAsyncKeyState(Keys.F6);
            if ((gaks & 0b10000000_00000000) > 0)//if F6 key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.F6) & 0b10000000_00000000) > 0, 100); //wait until F6 key is released with timeout of 100ms
                this.onHotkeyPressed.OnToggleActivateProgramHotkeyPressed();
            }
        }


        #region These methods run in their own individual timer threads

        private void PollFireButton()
        {
            this.shootTimer.Interval = this._shotInterval;
            Keys fireKey = Keys.MButton; //default is middle mouse button
            fireKey = HelperFunctions.GetFireKeyFromString(this._fireButton);
            short gaks = GetAsyncKeyState(fireKey);
            if ((gaks & 0b10000000_00000000) > 0) //if the key is set (non-zero) i.e. the key is being held down
            {
                MouseHelperClass.LeftClickDown();
                MouseHelperClass.LeftClickUp();

                this.firingState = true;

            }
            else
            {
                this.firingState = false;
            }
        }

        private void PullDownMouse()
        {

            Keys fireKey = HelperFunctions.GetFireKeyFromString(this._fireButton);
            short gaks = GetAsyncKeyState(fireKey);
            bool fireKeyPressed = (gaks & 0b10000000_00000000) > 0;

            while (this.Activated && this.PerformRecoilCompensation && fireKeyPressed)
            {
                MouseHelperClass.MouseMove(this._dy, this._pullDelay);

                fireKey = HelperFunctions.GetFireKeyFromString(this._fireButton);
                gaks = GetAsyncKeyState(fireKey);
                fireKeyPressed = (gaks & 0b10000000_00000000) > 0;
            }

            this.pullDownMouseTimer.Start(); //reset the timer

        }

        #endregion
    }
}
