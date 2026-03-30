namespace RailTest.Border.Server
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this._MenuStrip = new System.Windows.Forms.MenuStrip();
            this._FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ServerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._StartMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._StopMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._StartButton = new System.Windows.Forms.ToolStripButton();
            this._StopButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._ZeroButton = new System.Windows.Forms.ToolStripButton();
            this._ProcessingButton = new System.Windows.Forms.ToolStripButton();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._TimeStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._OutputLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this._MainPanel = new System.Windows.Forms.Panel();
            this._Timer = new System.Windows.Forms.Timer(this.components);
            this._MenuStrip.SuspendLayout();
            this._ToolStrip.SuspendLayout();
            this._StatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _MenuStrip
            // 
            this._MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._FileMenuItem,
            this._ServerMenuItem});
            this._MenuStrip.Location = new System.Drawing.Point(0, 0);
            this._MenuStrip.Name = "_MenuStrip";
            this._MenuStrip.Size = new System.Drawing.Size(1008, 24);
            this._MenuStrip.TabIndex = 0;
            this._MenuStrip.Text = "Главное меню";
            // 
            // _FileMenuItem
            // 
            this._FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ExitMenuItem});
            this._FileMenuItem.Name = "_FileMenuItem";
            this._FileMenuItem.Size = new System.Drawing.Size(48, 20);
            this._FileMenuItem.Text = "Файл";
            // 
            // _ExitMenuItem
            // 
            this._ExitMenuItem.Name = "_ExitMenuItem";
            this._ExitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this._ExitMenuItem.Size = new System.Drawing.Size(151, 22);
            this._ExitMenuItem.Text = "Выход";
            this._ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // _ServerMenuItem
            // 
            this._ServerMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StartMenuItem,
            this._StopMenuItem});
            this._ServerMenuItem.Name = "_ServerMenuItem";
            this._ServerMenuItem.Size = new System.Drawing.Size(59, 20);
            this._ServerMenuItem.Text = "Сервер";
            // 
            // _StartMenuItem
            // 
            this._StartMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_StartMenuItem.Image")));
            this._StartMenuItem.Name = "_StartMenuItem";
            this._StartMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this._StartMenuItem.Size = new System.Drawing.Size(175, 22);
            this._StartMenuItem.Text = "Запустить";
            this._StartMenuItem.Click += new System.EventHandler(this.StartMenuItem_Click);
            // 
            // _StopMenuItem
            // 
            this._StopMenuItem.Enabled = false;
            this._StopMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("_StopMenuItem.Image")));
            this._StopMenuItem.Name = "_StopMenuItem";
            this._StopMenuItem.Size = new System.Drawing.Size(175, 22);
            this._StopMenuItem.Text = "Остановить";
            this._StopMenuItem.Click += new System.EventHandler(this.StopMenuItem_Click);
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StartButton,
            this._StopButton,
            this.toolStripSeparator1,
            this._ZeroButton,
            this._ProcessingButton});
            this._ToolStrip.Location = new System.Drawing.Point(0, 24);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(1008, 25);
            this._ToolStrip.TabIndex = 1;
            this._ToolStrip.Text = "Панель управления";
            // 
            // _StartButton
            // 
            this._StartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StartButton.Image = ((System.Drawing.Image)(resources.GetObject("_StartButton.Image")));
            this._StartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StartButton.Name = "_StartButton";
            this._StartButton.Size = new System.Drawing.Size(23, 22);
            this._StartButton.Text = "Запустить сервер";
            this._StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // _StopButton
            // 
            this._StopButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StopButton.Enabled = false;
            this._StopButton.Image = ((System.Drawing.Image)(resources.GetObject("_StopButton.Image")));
            this._StopButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StopButton.Name = "_StopButton";
            this._StopButton.Size = new System.Drawing.Size(23, 22);
            this._StopButton.Text = "Остановить сервер";
            this._StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _ZeroButton
            // 
            this._ZeroButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZeroButton.Enabled = false;
            this._ZeroButton.Image = ((System.Drawing.Image)(resources.GetObject("_ZeroButton.Image")));
            this._ZeroButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZeroButton.Name = "_ZeroButton";
            this._ZeroButton.Size = new System.Drawing.Size(23, 22);
            this._ZeroButton.Text = "Установить ноль";
            this._ZeroButton.Click += new System.EventHandler(this.ZeroButton_Click);
            // 
            // _ProcessingButton
            // 
            this._ProcessingButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ProcessingButton.Enabled = false;
            this._ProcessingButton.Image = ((System.Drawing.Image)(resources.GetObject("_ProcessingButton.Image")));
            this._ProcessingButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ProcessingButton.Name = "_ProcessingButton";
            this._ProcessingButton.Size = new System.Drawing.Size(23, 22);
            this._ProcessingButton.Text = "Начать или остановить обработку";
            this._ProcessingButton.Click += new System.EventHandler(this.ProcessingButton_Click);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._TimeStatusLabel,
            this._OutputLabel});
            this._StatusStrip.Location = new System.Drawing.Point(0, 707);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(1008, 22);
            this._StatusStrip.TabIndex = 2;
            this._StatusStrip.Text = "Строка состояния";
            // 
            // _TimeStatusLabel
            // 
            this._TimeStatusLabel.Name = "_TimeStatusLabel";
            this._TimeStatusLabel.Size = new System.Drawing.Size(93, 17);
            this._TimeStatusLabel.Text = "TimeStatusLabel";
            this._TimeStatusLabel.Visible = false;
            // 
            // _OutputLabel
            // 
            this._OutputLabel.Name = "_OutputLabel";
            this._OutputLabel.Size = new System.Drawing.Size(118, 17);
            this._OutputLabel.Text = "toolStripStatusLabel1";
            this._OutputLabel.Visible = false;
            // 
            // _MainPanel
            // 
            this._MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._MainPanel.Location = new System.Drawing.Point(0, 49);
            this._MainPanel.Name = "_MainPanel";
            this._MainPanel.Size = new System.Drawing.Size(1008, 658);
            this._MainPanel.TabIndex = 3;
            // 
            // _Timer
            // 
            this._Timer.Tick += new System.EventHandler(this.Timer_Tick_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this._MainPanel);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._ToolStrip);
            this.Controls.Add(this._MenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this._MenuStrip;
            this.Name = "MainForm";
            this.Text = "Рубеж";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this._MenuStrip.ResumeLayout(false);
            this._MenuStrip.PerformLayout();
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip _MenuStrip;
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.Panel _MainPanel;
        private System.Windows.Forms.ToolStripMenuItem _FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _ServerMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _StartMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _StopMenuItem;
        private System.Windows.Forms.ToolStripButton _StartButton;
        private System.Windows.Forms.ToolStripButton _StopButton;
        private System.Windows.Forms.ToolStripStatusLabel _TimeStatusLabel;
        private System.Windows.Forms.Timer _Timer;
        private System.Windows.Forms.ToolStripStatusLabel _OutputLabel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _ZeroButton;
        private System.Windows.Forms.ToolStripButton _ProcessingButton;
    }
}