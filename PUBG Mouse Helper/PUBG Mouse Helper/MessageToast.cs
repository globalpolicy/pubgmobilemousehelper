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
    public partial class MessageToast : Form
    {
        public MessageToast(string message)
        {
            InitializeComponent();
            this.labelMessage.Text = message;
            this.timerCloseForm.Enabled = true;
            this.Location = new Point(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width / 2 - this.Width / 2, 50);
        }

        private void timerCloseForm_Tick(object sender, EventArgs e)
        {
            double newOpacity = this.Opacity - 10.0 / 100;
            if (newOpacity <= 0)
            {
                this.Close();
            }
            else
            {
                this.Opacity = newOpacity;
            }
        }
    }
}
