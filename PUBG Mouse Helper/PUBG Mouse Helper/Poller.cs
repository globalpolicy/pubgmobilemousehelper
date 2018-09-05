using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace PUBG_Mouse_Helper
{
    public class Poller
    {
        public bool PerformRecoilCompensation { get; set; } = true;
        public bool Activated { get; set; } = false;

        private IOnHotkeyPressed onHotkeyPressed;

        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        public Poller(IOnHotkeyPressed onHotkeyPressed)
        {
            this.onHotkeyPressed = onHotkeyPressed;
        }

        public void Poll(int dx, int dy, uint sleep, string fireButton)
        {
            if (this.Activated)
            {
                PollFireButton(dx, dy, sleep, fireButton);
                if (this.PerformRecoilCompensation)
                {
                    PollPresetChangeHotkey();
                    PollTrackbarValuesChangeHotkey();
                }
                PollToggleRecoilCompensationHotkey();
            }

            PollToggleActivateProgramHotkey();
        }

        private void PollFireButton(int dx, int dy, uint sleep, string fireKeyString)
        {
            Keys fireKey = Keys.MButton; //default is middle mouse button
            switch (fireKeyString)
            {
                case "CTRL":
                    fireKey = Keys.ControlKey;
                    break;
                case "SHIFT":
                    fireKey = Keys.ShiftKey;
                    break;
                case "RMB":
                    fireKey = Keys.RButton;
                    break;
                case "MMB":
                    fireKey = Keys.MButton;
                    break;
            }
            PollMouseButton(dx, dy, sleep, fireKey);
        }

        private void PollMouseButton(int dx, int dy, uint sleep, Keys key)
        {
            short gaks = GetAsyncKeyState(key);
            if ((gaks & 0b10000000_00000000) > 0) //if the key is set (non-zero) i.e. the key is being held down
            {
                MouseHelperClass.LeftClickDown();
                MouseHelperClass.LeftClickUp();

                if (this.PerformRecoilCompensation)
                {
                    MouseHelperClass.MouseMove(dx, dy, sleep);
                }

            }

        }

        private void PollPresetChangeHotkey()
        {
            short gaks = GetAsyncKeyState(Keys.Enter);
            if ((gaks & 0b10000000_00000000) > 0) //if Enter was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.Enter) & 0b10000000_00000000) > 0, 100); //wait until Enter is released without timeout of 100ms
                this.onHotkeyPressed.OnPresetSwitchHotkeyPressed();
            }
        }

        private void PollTrackbarValuesChangeHotkey()
        {
            short gaks = GetAsyncKeyState(Keys.Right);
            if ((gaks & 0b10000000_00000000) > 0)//if Right arrow key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.Right) & 0b10000000_00000000) > 0, 100); //wait until Right arrow key is released without timeout of 100ms
                this.onHotkeyPressed.OnRightArrowPressed();
            }

            gaks = GetAsyncKeyState(Keys.Left);
            if ((gaks & 0b10000000_00000000) > 0)//if Left arrow key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.Left) & 0b10000000_00000000) > 0, 100); //wait until Left arrow key is released without timeout of 100ms
                this.onHotkeyPressed.OnLeftArrowPressed();
            }

            gaks = GetAsyncKeyState(Keys.Up);
            if ((gaks & 0b10000000_00000000) > 0)//if Up arrow key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.Up) & 0b10000000_00000000) > 0, 100); //wait until Up arrow key is released without timeout of 100ms
                this.onHotkeyPressed.OnUpArrowPressed();
            }

            gaks = GetAsyncKeyState(Keys.Down);
            if ((gaks & 0b10000000_00000000) > 0)//if Down arrow key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.Down) & 0b10000000_00000000) > 0, 100); //wait until Down arrow key is released without timeout of 100ms
                this.onHotkeyPressed.OnDownArrowPressed();
            }
        }

        private void PollToggleRecoilCompensationHotkey()
        {
            short gaks = GetAsyncKeyState(Keys.F7);
            if ((gaks & 0b10000000_00000000) > 0)//if F7 key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.F7) & 0b10000000_00000000) > 0, 100); //wait until F7 key is released without timeout of 100ms
                this.onHotkeyPressed.OnToggleRecoilCompensationHotkeyPressed();
            }
        }

        private void PollToggleActivateProgramHotkey()
        {
            short gaks = GetAsyncKeyState(Keys.F6);
            if ((gaks & 0b10000000_00000000) > 0)//if F6 key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.F6) & 0b10000000_00000000) > 0, 100); //wait until F6 key is released without timeout of 100ms
                this.onHotkeyPressed.OnToggleActivateProgramHotkeyPressed();
            }
        }

    }
}
