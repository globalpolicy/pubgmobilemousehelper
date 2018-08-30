using System.Windows.Forms;

namespace PUBG_Mouse_Helper
{
    public static class KeyboardHelperClass
    {
        public static string inactiveShootKey = "]";
        public static string shootKey = "]";
        public static bool canFire = true;
        public static void PressKeyboardShootKey()
        {
            try
            {
                if (canFire)
                {
                    canFire = false;
                    SendKeys.SendWait(shootKey);
                    canFire = true;
                }
            }
            catch { }
        }
    }
}
