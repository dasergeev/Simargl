namespace RailTest.Border.Server
{
    partial class MainControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this._TreeView = new System.Windows.Forms.TreeView();
            this._LeftSplitter = new System.Windows.Forms.Splitter();
            this._OutputPanel = new System.Windows.Forms.Panel();
            this._BottomSplitter = new System.Windows.Forms.Splitter();
            this._WorkPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // _TreeView
            // 
            this._TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._TreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this._TreeView.Location = new System.Drawing.Point(0, 0);
            this._TreeView.Name = "_TreeView";
            this._TreeView.Size = new System.Drawing.Size(250, 592);
            this._TreeView.TabIndex = 0;
            this._TreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_AfterSelect);
            // 
            // _LeftSplitter
            // 
            this._LeftSplitter.Location = new System.Drawing.Point(250, 0);
            this._LeftSplitter.Name = "_LeftSplitter";
            this._LeftSplitter.Size = new System.Drawing.Size(4, 592);
            this._LeftSplitter.TabIndex = 1;
            this._LeftSplitter.TabStop = false;
            // 
            // _OutputPanel
            // 
            this._OutputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._OutputPanel.Location = new System.Drawing.Point(254, 336);
            this._OutputPanel.Name = "_OutputPanel";
            this._OutputPanel.Size = new System.Drawing.Size(572, 256);
            this._OutputPanel.TabIndex = 2;
            // 
            // _BottomSplitter
            // 
            this._BottomSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._BottomSplitter.Location = new System.Drawing.Point(254, 332);
            this._BottomSplitter.Name = "_BottomSplitter";
            this._BottomSplitter.Size = new System.Drawing.Size(572, 4);
            this._BottomSplitter.TabIndex = 3;
            this._BottomSplitter.TabStop = false;
            // 
            // _WorkPanel
            // 
            this._WorkPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._WorkPanel.Location = new System.Drawing.Point(254, 0);
            this._WorkPanel.Name = "_WorkPanel";
            this._WorkPanel.Size = new System.Drawing.Size(572, 332);
            this._WorkPanel.TabIndex = 4;
            // 
            // MainControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._WorkPanel);
            this.Controls.Add(this._BottomSplitter);
            this.Controls.Add(this._OutputPanel);
            this.Controls.Add(this._LeftSplitter);
            this.Controls.Add(this._TreeView);
            this.Name = "MainControl";
            this.Size = new System.Drawing.Size(826, 592);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView _TreeView;
        private System.Windows.Forms.Splitter _LeftSplitter;
        private System.Windows.Forms.Panel _OutputPanel;
        private System.Windows.Forms.Splitter _BottomSplitter;
        private System.Windows.Forms.Panel _WorkPanel;
    }
}
