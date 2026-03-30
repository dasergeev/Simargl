namespace RailTest.Satellite.Autonomic
{
    partial class AssistantForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssistantForm));
            this._LoggerPanel = new System.Windows.Forms.Panel();
            this._BottomSplitter = new System.Windows.Forms.Splitter();
            this._OscillogramPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _LoggerPanel
            // 
            this._LoggerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._LoggerPanel.Location = new System.Drawing.Point(0, 404);
            this._LoggerPanel.Name = "_LoggerPanel";
            this._LoggerPanel.Size = new System.Drawing.Size(941, 256);
            this._LoggerPanel.TabIndex = 3;
            // 
            // _BottomSplitter
            // 
            this._BottomSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._BottomSplitter.Location = new System.Drawing.Point(0, 400);
            this._BottomSplitter.Name = "_BottomSplitter";
            this._BottomSplitter.Size = new System.Drawing.Size(941, 4);
            this._BottomSplitter.TabIndex = 4;
            this._BottomSplitter.TabStop = false;
            // 
            // _OscillogramPanel
            // 
            this._OscillogramPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._OscillogramPanel.Location = new System.Drawing.Point(0, 0);
            this._OscillogramPanel.Name = "_OscillogramPanel";
            this._OscillogramPanel.Size = new System.Drawing.Size(941, 400);
            this._OscillogramPanel.TabIndex = 5;
            // 
            // AssistantForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 660);
            this.Controls.Add(this._OscillogramPanel);
            this.Controls.Add(this._BottomSplitter);
            this.Controls.Add(this._LoggerPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AssistantForm";
            this.Text = "Помощник";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel _LoggerPanel;
        private System.Windows.Forms.Splitter _BottomSplitter;
        private System.Windows.Forms.Panel _OscillogramPanel;
    }
}