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
        void OnUpArrowPressed();
        void OnDownArrowPressed();
        void OnLeftSquareBracketKeyPressed();
        void OnRightSquareBracketKeyPressed();
        void OnSemicolonKeyPressed();
        void OnSingleQuoteKeyPressed();
        void OnToggleRecoilCompensationHotkeyPressed();
        void OnToggleActivateProgramHotkeyPressed();
        void OnWeaponSlotChangeHotkeyPressed(int slotNumber);
    }
}
