using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUBG_Mouse_Helper
{
    public interface IOnHotkeyPressed
    {
        void OnPresetSwitchHotkeyPressed();
        void OnRightArrowPressed();
        void OnLeftArrowPressed();
        void OnUpArrowPressed();
        void OnDownArrowPressed();
        void OnToggleRecoilCompensationHotkeyPressed();
        void OnF8Pressed();
    }
}
