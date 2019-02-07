using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUBG_Mouse_Helper
{
    public partial class WeaponSelectorForm : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        private IntPtr activeWindowhandle;

        public WeaponSelectorForm(List<ToolStripMenuItem> presetMenuItemsList)
        {
            InitializeComponent();
            comboBoxPresets.Items.AddRange(presetMenuItemsList.Select(presetMenuItem => presetMenuItem.Text).ToArray());
        }

        public string GetSelectedPresetName()
        {
            return comboBoxPresets.Text;
        }

        private void WeaponSelectorForm_Shown(object sender, EventArgs e)
        {
            this.activeWindowhandle = GetForegroundWindow();

            uint activeWindowThreadId = GetWindowThreadProcessId(this.activeWindowhandle, IntPtr.Zero);
            uint ourThreadId = GetCurrentThreadId();
            AttachThreadInput(ourThreadId, activeWindowThreadId, true);


            SetForegroundWindow(this.Handle);
            AttachThreadInput(ourThreadId, activeWindowThreadId, false);
        }

        private void WeaponSelectorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SetForegroundWindow(this.activeWindowhandle);
        }
    }
}
