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
        private IOnHotkeyPressed onHotkeyPressed;
        
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(System.Windows.Forms.Keys vKey);

        bool firstPress = true;

        public Poller(IOnHotkeyPressed onHotkeyPressed)
        {
            this.onHotkeyPressed = onHotkeyPressed;
        }

        public void Poll(int dx, int dy, uint sleep)
        {
            PollMButton(dx, dy, sleep);
            PollPresetChangeHotkey();
            PollTrackbarValuesChangeHotkey();
        }

        private void PollMButton(int dx, int dy, uint sleep)
        {
            short gaks = GetAsyncKeyState(System.Windows.Forms.Keys.MButton);
            if ((gaks & 0b10000000_00000000) > 0) //if MSB is set (non-zero) i.e. middle button is being held down
            {
                if (firstPress)
                {
                    MouseHelperClass.TypeGraveAccent();
                    MouseHelperClass.MouseMove(getDesktopCenterPosition().Item1 - Cursor.Position.X, getDesktopCenterPosition().Item2 - Cursor.Position.Y, 0);
                    MouseHelperClass.TypeGraveAccent();
                }
                MouseHelperClass.LeftClickDown();
                MouseHelperClass.LeftClickUp();
                MouseHelperClass.TypeGraveAccent();
                MouseHelperClass.LeftClickDown();
                MouseHelperClass.MouseMove(dx, dy, sleep);
                MouseHelperClass.LeftClickUp();
                MouseHelperClass.TypeGraveAccent();
                firstPress = false;
            }
            else
            {
                firstPress = true;
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

        public Tuple<int, int> getDesktopCenterPosition()
        {
            return new Tuple<int, int>(Screen.PrimaryScreen.Bounds.Height / 2, Screen.PrimaryScreen.Bounds.Width / 2);
        }
    }
}
