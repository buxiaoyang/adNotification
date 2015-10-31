namespace Client
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemNotifications = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewNotification = new System.Windows.Forms.ListView();
            this.imageListMain = new System.Windows.Forms.ImageList(this.components);
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.ContextMenuStrip = this.contextMenuStripMain;
            this.notifyIconMain.Text = "Notifications";
            this.notifyIconMain.Visible = true;
            this.notifyIconMain.DoubleClick += new System.EventHandler(this.notifyIconMain_DoubleClick);
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemNotifications});
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(172, 28);
            // 
            // toolStripMenuItemNotifications
            // 
            this.toolStripMenuItemNotifications.Name = "toolStripMenuItemNotifications";
            this.toolStripMenuItemNotifications.Size = new System.Drawing.Size(171, 24);
            this.toolStripMenuItemNotifications.Text = "Notifications";
            this.toolStripMenuItemNotifications.Click += new System.EventHandler(this.toolStripMenuItemNotifications_Click);
            // 
            // listViewNotification
            // 
            this.listViewNotification.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewNotification.GridLines = true;
            this.listViewNotification.Location = new System.Drawing.Point(0, 0);
            this.listViewNotification.MultiSelect = false;
            this.listViewNotification.Name = "listViewNotification";
            this.listViewNotification.Size = new System.Drawing.Size(470, 307);
            this.listViewNotification.TabIndex = 1;
            this.listViewNotification.UseCompatibleStateImageBehavior = false;
            // 
            // imageListMain
            // 
            this.imageListMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListMain.ImageStream")));
            this.imageListMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListMain.Images.SetKeyName(0, "Message.ico");
            this.imageListMain.Images.SetKeyName(1, "MessageBlank.ico");
            // 
            // timerMain
            // 
            this.timerMain.Interval = 500;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 307);
            this.Controls.Add(this.listViewNotification);
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notifications";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.SizeChanged += new System.EventHandler(this.FormMain_SizeChanged);
            this.contextMenuStripMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemNotifications;
        private System.Windows.Forms.ListView listViewNotification;
        private System.Windows.Forms.ImageList imageListMain;
        private System.Windows.Forms.Timer timerMain;
    }
}

