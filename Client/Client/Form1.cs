using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class FormMain : Form
    {
        bool timeTick = false;
        Icon iconNotification;
        Icon iconNotificationBlank;
        public FormMain()
        {
            InitializeComponent();
            iconNotification = Icon.FromHandle(((Bitmap)imageListMain.Images[0]).GetHicon());
            iconNotificationBlank = Icon.FromHandle(((Bitmap)imageListMain.Images[1]).GetHicon());
            this.Icon = iconNotification;
            this.notifyIconMain.Icon = iconNotification;
            //timerMain.Start();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }
        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void FormMain_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide(); 
                this.notifyIconMain.Visible = true;
            }
        }

        private void toolStripMenuItemNotifications_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void notifyIconMain_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.notifyIconMain.Visible = true;
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            if(timeTick)
            {
                this.notifyIconMain.Icon = iconNotification;
                timeTick = false;
            }
            else
            {
                this.notifyIconMain.Icon = iconNotificationBlank;
                timeTick = true;
            }
        }
    }
}
