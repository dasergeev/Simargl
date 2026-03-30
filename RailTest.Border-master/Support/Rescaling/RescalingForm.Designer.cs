namespace RailTest.Border.Support.Rescaling
{
    partial class RescalingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RescalingForm));
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._StartButton = new System.Windows.Forms.ToolStripButton();
            this._StopButton = new System.Windows.Forms.ToolStripButton();
            this._ProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this._OutputView = new RailTest.Controls.OutputView();
            this._StatusStrip.SuspendLayout();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._ProgressBar});
            this._StatusStrip.Location = new System.Drawing.Point(0, 428);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(800, 22);
            this._StatusStrip.TabIndex = 0;
            this._StatusStrip.Text = "Строка состояния";
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StartButton,
            this._StopButton});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(800, 25);
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
            this._StartButton.Text = "Запустить";
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
            this._StopButton.Text = "Остановить";
            this._StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // _ProgressBar
            // 
            this._ProgressBar.Name = "_ProgressBar";
            this._ProgressBar.Size = new System.Drawing.Size(100, 16);
            this._ProgressBar.Step = 1;
            this._ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // _OutputView
            // 
            this._OutputView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OutputView.Location = new System.Drawing.Point(0, 25);
            this._OutputView.Name = "_OutputView";
            this._OutputView.Size = new System.Drawing.Size(800, 403);
            this._OutputView.TabIndex = 2;
            // 
            // RescalingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._OutputView);
            this.Controls.Add(this._ToolStrip);
            this.Controls.Add(this._StatusStrip);
            this.Name = "RescalingForm";
            this.Text = "Масштабирование файлов";
            this._StatusStrip.ResumeLayout(false);
            this._StatusStrip.PerformLayout();
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripProgressBar _ProgressBar;
        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.ToolStripButton _StartButton;
        private System.Windows.Forms.ToolStripButton _StopButton;
        private Controls.OutputView _OutputView;
    }
}