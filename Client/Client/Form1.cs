using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Client
{
    public partial class FormMain : Form
    {
        //indicate has message or not
        bool hasMessage = false;
        string messageID = "";
        //used for icon flash
        bool timeTick = false;
        Icon iconNotification;
        Icon iconNotificationBlank;
        //login user
        string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToLower();
        //seeting
        MySettings setting = MySettings.Load();
        public FormMain()
        {
            InitializeComponent();
            iconNotification = Icon.FromHandle(((Bitmap)imageListMain.Images[0]).GetHicon());
            iconNotificationBlank = Icon.FromHandle(((Bitmap)imageListMain.Images[1]).GetHicon());
            this.Icon = iconNotification;
            this.notifyIconMain.Icon = iconNotification;
            this.notifyIconMain.Text = userName;
            timerMain.Start();
            getMessage();
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
            confirmMessage();
        }

        private void timerIcon_Tick(object sender, EventArgs e)
        {
            if (timeTick)
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

        private void timerMain_Tick(object sender, EventArgs e)
        {
            getMessage();
        }


        WebClient wc = new WebClient();
        Encrypt encrypt = new Encrypt();
        private void getMessage()
        {
            if (!hasMessage)
            {
                try
                {
                    var json = wc.DownloadString(setting.server + "/Message/Get?user=" + userName.ToLower());
                    dynamic result = (new JavaScriptSerializer()).Deserialize<Object>(json);
                    if (result["result"])
                    {
                        messageID = encrypt.DecryptString(result["ID"]);
                        String TITLE = encrypt.DecryptString(result["TITLE"]);
                        String CONTENT = encrypt.DecryptString(result["CONTENT"]);
                        String CREATE_USER = encrypt.DecryptString(result["CREATE_USER"]);
                        DateTime CREATE_DATETIME = result["CREATE_DATETIME"];
                        CONTENT = CONTENT.Replace("<img src=\"/", "<img src=\"" + setting.server + "/");
                        this.webBrowserMain.DocumentText = "<h1>" + TITLE + "</h1><div>" + CONTENT + "</div>";
                        //flash the icon
                        hasMessage = true;
                        timerIcon.Start();
                    }
                    else
                    {
                        this.webBrowserMain.DocumentText = "<h1>No Message</h1>";
                        hasMessage = false;
                        timerIcon.Stop();
                        this.notifyIconMain.Icon = iconNotification;
                        timeTick = false;
                    }
                }
                catch (Exception ex)
                {
                    this.webBrowserMain.DocumentText = "<h1>No Message</h1>";
                    hasMessage = false;
                    timerIcon.Stop();
                    this.notifyIconMain.Icon = iconNotification;
                    timeTick = false;
                }
            }
        }

        private void confirmMessage()
        {
            if (hasMessage)
            {
                //confirm message
                try
                {
                    var json = wc.DownloadString(setting.server + "/Message/Confirm?user=" + userName.ToLower() + "&announcement=" + messageID);
                    this.webBrowserMain.DocumentText = "<h1>No Message</h1>";
                    hasMessage = false;
                    timerIcon.Stop();
                    this.notifyIconMain.Icon = iconNotification;
                    timeTick = false;
                }
                catch (Exception ex)
                {
                    this.webBrowserMain.DocumentText = "<h1>No Message</h1>";
                    hasMessage = false;
                    timerIcon.Stop();
                    this.notifyIconMain.Icon = iconNotification;
                    timeTick = false;
                }
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
            this.notifyIconMain.Visible = true;
            confirmMessage();
        }
    }
}
