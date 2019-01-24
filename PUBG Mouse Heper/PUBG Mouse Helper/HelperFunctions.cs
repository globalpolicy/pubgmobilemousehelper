using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUBG_Mouse_Helper
{
    public static class HelperFunctions
    {
        public static void WaitUntilTimeoutWhileTrue(Func<bool> condition, int timeoutMs)
        {
            int startTime = Environment.TickCount;
            while (Environment.TickCount - startTime < timeoutMs && condition.Invoke())
            {
            }
        }

        public static string GetApplicationName()
        {
            return "PUBG Mouse Helper 2.3";
        }

        public static List<ToolStripMenuItem> GetListOfAllPresetMenuItems(ToolStripMenuItem toolStripMenuItemPresets)
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

            return presetMenuItemsList;
        }
    }
}
