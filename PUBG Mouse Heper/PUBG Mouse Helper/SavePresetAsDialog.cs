using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PUBG_Mouse_Helper
{
    public partial class SavePresetAsDialog : Form
    {
        private IOkButtonPressedInSavePresetDialog okButtonPressed;

        public SavePresetAsDialog(IOkButtonPressedInSavePresetDialog okButtonPressedPresetDialog)
        {
            this.okButtonPressed = okButtonPressedPresetDialog;
            InitializeComponent();
        }

        

        private void buttonOkSave_Click(object sender, EventArgs e)
        {
            this.okButtonPressed.OnOkButtonPressed(textBoxPresetName.Text);
            this.Close();
        }

        private void buttonCancelSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxPresetName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                buttonOkSave.PerformClick();
            }
            else if (e.KeyCode.Equals(Keys.Escape))
            {
                buttonCancelSave.PerformClick();
            }
        }
    }
}
