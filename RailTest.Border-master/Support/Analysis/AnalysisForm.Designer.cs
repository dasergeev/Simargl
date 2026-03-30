namespace RailTest.Border.Support.Analysis
{
    partial class AnalysisForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnalysisForm));
            this._ToolStrip = new System.Windows.Forms.ToolStrip();
            this._StartButton = new System.Windows.Forms.ToolStripButton();
            this._StopButton = new System.Windows.Forms.ToolStripButton();
            this._Separator = new System.Windows.Forms.ToolStripSeparator();
            this._WorkersBox = new System.Windows.Forms.ToolStripComboBox();
            this._StatusStrip = new System.Windows.Forms.StatusStrip();
            this._OutputView = new RailTest.Controls.OutputView();
            this._ToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _ToolStrip
            // 
            this._ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._StartButton,
            this._StopButton,
            this._Separator,
            this._WorkersBox});
            this._ToolStrip.Location = new System.Drawing.Point(0, 0);
            this._ToolStrip.Name = "_ToolStrip";
            this._ToolStrip.Size = new System.Drawing.Size(800, 25);
            this._ToolStrip.TabIndex = 0;
            this._ToolStrip.Text = "Панель управления";
            // 
            // _StartButton
            // 
            this._StartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._StartButton.Image = ((System.Drawing.Image)(resources.GetObject("_StartButton.Image")));
            this._StartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._StartButton.Name = "_StartButton";
            this._StartButton.Size = new System.Drawing.Size(23, 22);
            this._StartButton.Text = "Начать";
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
            // _Separator
            // 
            this._Separator.Name = "_Separator";
            this._Separator.Size = new System.Drawing.Size(6, 25);
            // 
            // _WorkersBox
            // 
            this._WorkersBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._WorkersBox.Name = "_WorkersBox";
            this._WorkersBox.Size = new System.Drawing.Size(121, 25);
            // 
            // _StatusStrip
            // 
            this._StatusStrip.Location = new System.Drawing.Point(0, 428);
            this._StatusStrip.Name = "_StatusStrip";
            this._StatusStrip.Size = new System.Drawing.Size(800, 22);
            this._StatusStrip.TabIndex = 1;
            this._StatusStrip.Text = "Строка состояния";
            // 
            // _OutputView
            // 
            this._OutputView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OutputView.Location = new System.Drawing.Point(0, 25);
            this._OutputView.Name = "_OutputView";
            this._OutputView.Size = new System.Drawing.Size(800, 403);
            this._OutputView.TabIndex = 2;
            // 
            // AnalysisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._OutputView);
            this.Controls.Add(this._StatusStrip);
            this.Controls.Add(this._ToolStrip);
            this.Name = "AnalysisForm";
            this.Text = "AnalysisForm";
            this._ToolStrip.ResumeLayout(false);
            this._ToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.ToolStripButton _StartButton;
        private System.Windows.Forms.ToolStripButton _StopButton;
        private System.Windows.Forms.ToolStripSeparator _Separator;
        private System.Windows.Forms.ToolStripComboBox _WorkersBox;
        private Controls.OutputView _OutputView;
    }
}