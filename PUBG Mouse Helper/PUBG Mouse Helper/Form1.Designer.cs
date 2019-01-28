namespace PUBG_Mouse_Helper
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.trackBarPullDelay = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBarDy = new System.Windows.Forms.TrackBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSaveAsPreset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemDeletePreset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemPresets = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemFireButton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxFireButton = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItemEnableAntiRecoil = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemActivate = new System.Windows.Forms.ToolStripMenuItem();
            this.trayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemInstructions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.trackBarShotInterval = new System.Windows.Forms.TrackBar();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPullDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDy)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarShotInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.trackBarShotInterval);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.statusStrip1);
            this.panel1.Controls.Add(this.trackBarPullDelay);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.trackBarDy);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(355, 156);
            this.panel1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 134);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(355, 22);
            this.statusStrip1.TabIndex = 8;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel1.Text = "Ready";
            // 
            // trackBarPullDelay
            // 
            this.trackBarPullDelay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarPullDelay.Location = new System.Drawing.Point(87, 91);
            this.trackBarPullDelay.Maximum = 100;
            this.trackBarPullDelay.Minimum = 1;
            this.trackBarPullDelay.Name = "trackBarPullDelay";
            this.trackBarPullDelay.Size = new System.Drawing.Size(256, 45);
            this.trackBarPullDelay.TabIndex = 6;
            this.trackBarPullDelay.TickFrequency = 10;
            this.trackBarPullDelay.Value = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Pull Delay";
            this.toolTip1.SetToolTip(this.label4, "Mouse pull delay");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "dy";
            // 
            // trackBarDy
            // 
            this.trackBarDy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarDy.Location = new System.Drawing.Point(87, 13);
            this.trackBarDy.Maximum = 100;
            this.trackBarDy.Name = "trackBarDy";
            this.trackBarDy.Size = new System.Drawing.Size(256, 45);
            this.trackBarDy.TabIndex = 1;
            this.trackBarDy.Value = 1;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemPresets,
            this.toolStripMenuItemEdit,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(355, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSaveAsPreset,
            this.toolStripMenuItemDeletePreset,
            this.toolStripMenuItemExit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItemFile.Text = "File";
            // 
            // toolStripMenuItemSaveAsPreset
            // 
            this.toolStripMenuItemSaveAsPreset.Name = "toolStripMenuItemSaveAsPreset";
            this.toolStripMenuItemSaveAsPreset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItemSaveAsPreset.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemSaveAsPreset.Text = "Save as preset";
            this.toolStripMenuItemSaveAsPreset.Click += new System.EventHandler(this.toolStripMenuItemSaveAsPreset_Click);
            // 
            // toolStripMenuItemDeletePreset
            // 
            this.toolStripMenuItemDeletePreset.Enabled = false;
            this.toolStripMenuItemDeletePreset.Name = "toolStripMenuItemDeletePreset";
            this.toolStripMenuItemDeletePreset.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemDeletePreset.Text = "Delete preset";
            this.toolStripMenuItemDeletePreset.Click += new System.EventHandler(this.toolStripMenuItemDeletePreset_Click);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(187, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // toolStripMenuItemPresets
            // 
            this.toolStripMenuItemPresets.Name = "toolStripMenuItemPresets";
            this.toolStripMenuItemPresets.Size = new System.Drawing.Size(56, 20);
            this.toolStripMenuItemPresets.Text = "Presets";
            // 
            // toolStripMenuItemEdit
            // 
            this.toolStripMenuItemEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFireButton,
            this.toolStripMenuItemEnableAntiRecoil,
            this.toolStripSeparator1,
            this.toolStripMenuItemActivate,
            this.trayToolStripMenuItem});
            this.toolStripMenuItemEdit.Name = "toolStripMenuItemEdit";
            this.toolStripMenuItemEdit.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItemEdit.Text = "Edit";
            // 
            // toolStripMenuItemFireButton
            // 
            this.toolStripMenuItemFireButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxFireButton});
            this.toolStripMenuItemFireButton.Name = "toolStripMenuItemFireButton";
            this.toolStripMenuItemFireButton.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemFireButton.Text = "Fire button";
            // 
            // toolStripComboBoxFireButton
            // 
            this.toolStripComboBoxFireButton.Items.AddRange(new object[] {
            "MMB",
            "CTRL",
            "SHIFT",
            "RMB",
            "V"});
            this.toolStripComboBoxFireButton.Name = "toolStripComboBoxFireButton";
            this.toolStripComboBoxFireButton.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxFireButton.Text = "MMB";
            // 
            // toolStripMenuItemEnableAntiRecoil
            // 
            this.toolStripMenuItemEnableAntiRecoil.Checked = true;
            this.toolStripMenuItemEnableAntiRecoil.CheckOnClick = true;
            this.toolStripMenuItemEnableAntiRecoil.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItemEnableAntiRecoil.Name = "toolStripMenuItemEnableAntiRecoil";
            this.toolStripMenuItemEnableAntiRecoil.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.toolStripMenuItemEnableAntiRecoil.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemEnableAntiRecoil.Text = "Anti-recoil";
            this.toolStripMenuItemEnableAntiRecoil.Click += new System.EventHandler(this.toolStripMenuItemEnableAntiRecoil_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripMenuItemActivate
            // 
            this.toolStripMenuItemActivate.CheckOnClick = true;
            this.toolStripMenuItemActivate.Name = "toolStripMenuItemActivate";
            this.toolStripMenuItemActivate.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.toolStripMenuItemActivate.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItemActivate.Text = "Activate";
            this.toolStripMenuItemActivate.ToolTipText = "Activate auto fire";
            this.toolStripMenuItemActivate.Click += new System.EventHandler(this.toolStripMenuItemActivate_Click);
            // 
            // trayToolStripMenuItem
            // 
            this.trayToolStripMenuItem.Name = "trayToolStripMenuItem";
            this.trayToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.trayToolStripMenuItem.Text = "Tray";
            this.trayToolStripMenuItem.Click += new System.EventHandler(this.trayToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemInstructions,
            this.toolStripMenuItemAbout});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStripMenuItemInstructions
            // 
            this.toolStripMenuItemInstructions.Name = "toolStripMenuItemInstructions";
            this.toolStripMenuItemInstructions.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemInstructions.Text = "Instructions";
            this.toolStripMenuItemInstructions.Click += new System.EventHandler(this.toolStripMenuItemInstructions_Click);
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(136, 22);
            this.toolStripMenuItemAbout.Text = "About";
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "PUBG Mouse Helper";
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Shot Interval";
            this.toolTip1.SetToolTip(this.label1, "Delay between shots");
            // 
            // trackBarShotInterval
            // 
            this.trackBarShotInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarShotInterval.Location = new System.Drawing.Point(87, 49);
            this.trackBarShotInterval.Maximum = 1000;
            this.trackBarShotInterval.Minimum = 1;
            this.trackBarShotInterval.Name = "trackBarShotInterval";
            this.trackBarShotInterval.Size = new System.Drawing.Size(254, 45);
            this.trackBarShotInterval.TabIndex = 10;
            this.trackBarShotInterval.TickFrequency = 10;
            this.trackBarShotInterval.Value = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 180);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUBG Mouse Helper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPullDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarDy)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarShotInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TrackBar trackBarPullDelay;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBarDy;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPresets;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemActivate;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem trayToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSaveAsPreset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemDeletePreset;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemInstructions;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEnableAntiRecoil;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFireButton;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxFireButton;
        private System.Windows.Forms.TrackBar trackBarShotInterval;
        private System.Windows.Forms.Label label1;
    }
}

