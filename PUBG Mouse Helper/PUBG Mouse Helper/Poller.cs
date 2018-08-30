using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace PUBG_Mouse_Helper
{
    public class Poller
    {
        public bool PerformRecoilCompensation { get; set; } = true;

        private IOnHotkeyPressed onHotkeyPressed;
        
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(Keys vKey);

        public Poller(IOnHotkeyPressed onHotkeyPressed)
        {
            this.onHotkeyPressed = onHotkeyPressed;
        }

        public void Poll(int dx, int dy, uint sleep)
        {
            PollMButton(dx, dy, sleep);
            if (PerformRecoilCompensation)
            {
                PollPresetChangeHotkey();
                PollTrackbarValuesChangeHotkey();
            }
            PollToggleRecoilCompensationHotkey();
        }

        private void PollMButton(int dx, int dy, uint sleep)
        {
            short gaks = GetAsyncKeyState(Keys.LButton);
            if ((gaks & 0b10000000_00000000) > 0) //if MSB is set (non-zero) i.e. middle button is being held down
            {
                //MouseHelperClass.LeftClickDown();
                //MouseHelperClass.LeftClickUp();
                
                KeyboardHelperClass.PressKeyboardShootKey();

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
            if((gaks & 0b10000000_00000000)>0)//if Right arrow key was pressed and held
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
            if ((gaks & 0b10000000_00000000) > 0)//if F7 arrow key was pressed and held
            {
                HelperFunctions.WaitUntilTimeoutWhileTrue(() => (GetAsyncKeyState(Keys.F7) & 0b10000000_00000000) > 0, 100); //wait until F7 arrow key is released without timeout of 100ms
                this.onHotkeyPressed.OnToggleRecoilCompensationHotkeyPressed();
            }
        }

    }
}
