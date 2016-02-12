using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Valentine.x16
{
    public class frmMain : Form
    {
        public frmMain()
        {
            Text = "Valentine Day In My Brain!";
            TopMost = true;
            MinimizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            WindowState = FormWindowState.Maximized;
            BackColor = Color.White;
            Controls.Add(App.GetInstance().Surface as Control);
        }

        protected override void OnLoad(EventArgs e)
        {
            MaximumSize = Size;
            MinimumSize = Size;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            App.DestroyInstance();
        }
    }
}
