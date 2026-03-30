namespace Simargl.AccelEth3T
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            _ToolStrip = new System.Windows.Forms.ToolStrip();
            _RichTextBoxButton = new System.Windows.Forms.ToolStripButton();
            _StoreButton = new System.Windows.Forms.ToolStripButton();
            _ControlButton = new System.Windows.Forms.ToolStripButton();
            _StatusStrip = new System.Windows.Forms.StatusStrip();
            _TreeView = new System.Windows.Forms.TreeView();
            _ImageList = new System.Windows.Forms.ImageList(components);
            _TreeViewSplitter = new System.Windows.Forms.Splitter();
            _Timer = new System.Windows.Forms.Timer(components);
            _RichTextBox = new System.Windows.Forms.RichTextBox();
            _RichTextBoxSplitter = new System.Windows.Forms.Splitter();
            _Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            _ToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_Chart).BeginInit();
            SuspendLayout();
            // 
            // _ToolStrip
            // 
            _ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { _RichTextBoxButton, _StoreButton, _ControlButton });
            _ToolStrip.Location = new System.Drawing.Point(0, 0);
            _ToolStrip.Name = "_ToolStrip";
            _ToolStrip.Size = new System.Drawing.Size(800, 25);
            _ToolStrip.TabIndex = 0;
            _ToolStrip.Text = "Панель управления";
            // 
            // _RichTextBoxButton
            // 
            _RichTextBoxButton.CheckOnClick = true;
            _RichTextBoxButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            _RichTextBoxButton.Image = (System.Drawing.Image)resources.GetObject("_RichTextBoxButton.Image");
            _RichTextBoxButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            _RichTextBoxButton.Name = "_RichTextBoxButton";
            _RichTextBoxButton.Size = new System.Drawing.Size(23, 22);
            _RichTextBoxButton.Text = "Окно вывода";
            _RichTextBoxButton.CheckedChanged += RichTextBoxButton_CheckedChanged;
            // 
            // _StoreButton
            // 
            _StoreButton.CheckOnClick = true;
            _StoreButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            _StoreButton.Image = (System.Drawing.Image)resources.GetObject("_StoreButton.Image");
            _StoreButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            _StoreButton.Name = "_StoreButton";
            _StoreButton.Size = new System.Drawing.Size(23, 22);
            _StoreButton.Text = "Записывать состояние";
            _StoreButton.CheckedChanged += StoreButton_CheckedChanged;
            // 
            // _ControlButton
            // 
            _ControlButton.CheckOnClick = true;
            _ControlButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            _ControlButton.Enabled = false;
            _ControlButton.Image = (System.Drawing.Image)resources.GetObject("_ControlButton.Image");
            _ControlButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            _ControlButton.Name = "_ControlButton";
            _ControlButton.Size = new System.Drawing.Size(23, 22);
            _ControlButton.Text = "Контролировать состояние";
            _ControlButton.Click += ControlButton_Click;
            // 
            // _StatusStrip
            // 
            _StatusStrip.Location = new System.Drawing.Point(0, 428);
            _StatusStrip.Name = "_StatusStrip";
            _StatusStrip.Size = new System.Drawing.Size(800, 22);
            _StatusStrip.TabIndex = 1;
            _StatusStrip.Text = "Строка состояния";
            // 
            // _TreeView
            // 
            _TreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _TreeView.Dock = System.Windows.Forms.DockStyle.Left;
            _TreeView.ImageIndex = 0;
            _TreeView.ImageList = _ImageList;
            _TreeView.Location = new System.Drawing.Point(0, 25);
            _TreeView.Name = "_TreeView";
            _TreeView.SelectedImageIndex = 0;
            _TreeView.Size = new System.Drawing.Size(192, 403);
            _TreeView.TabIndex = 2;
            _TreeView.AfterSelect += TreeView_AfterSelect;
            // 
            // _ImageList
            // 
            _ImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            _ImageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("_ImageList.ImageStream");
            _ImageList.TransparentColor = System.Drawing.Color.Transparent;
            _ImageList.Images.SetKeyName(0, "001_green_ball.ico");
            _ImageList.Images.SetKeyName(1, "001_red_ball.ico");
            _ImageList.Images.SetKeyName(2, "External Drive-2.ico");
            _ImageList.Images.SetKeyName(3, "Application.ico");
            _ImageList.Images.SetKeyName(4, "Activity Monitor.ico");
            _ImageList.Images.SetKeyName(5, "functions.ico");
            // 
            // _TreeViewSplitter
            // 
            _TreeViewSplitter.Location = new System.Drawing.Point(192, 25);
            _TreeViewSplitter.Name = "_TreeViewSplitter";
            _TreeViewSplitter.Size = new System.Drawing.Size(4, 403);
            _TreeViewSplitter.TabIndex = 3;
            _TreeViewSplitter.TabStop = false;
            // 
            // _RichTextBox
            // 
            _RichTextBox.AcceptsTab = true;
            _RichTextBox.BackColor = System.Drawing.SystemColors.Window;
            _RichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            _RichTextBox.DetectUrls = false;
            _RichTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            _RichTextBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            _RichTextBox.HideSelection = false;
            _RichTextBox.Location = new System.Drawing.Point(196, 300);
            _RichTextBox.Name = "_RichTextBox";
            _RichTextBox.ReadOnly = true;
            _RichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            _RichTextBox.ShowSelectionMargin = true;
            _RichTextBox.Size = new System.Drawing.Size(604, 128);
            _RichTextBox.TabIndex = 4;
            _RichTextBox.Text = "";
            _RichTextBox.Visible = false;
            _RichTextBox.WordWrap = false;
            // 
            // _RichTextBoxSplitter
            // 
            _RichTextBoxSplitter.Dock = System.Windows.Forms.DockStyle.Bottom;
            _RichTextBoxSplitter.Location = new System.Drawing.Point(196, 296);
            _RichTextBoxSplitter.Name = "_RichTextBoxSplitter";
            _RichTextBoxSplitter.Size = new System.Drawing.Size(604, 4);
            _RichTextBoxSplitter.TabIndex = 5;
            _RichTextBoxSplitter.TabStop = false;
            _RichTextBoxSplitter.Visible = false;
            // 
            // _Chart
            // 
            chartArea1.Name = "ChartArea1";
            _Chart.ChartAreas.Add(chartArea1);
            _Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            _Chart.Location = new System.Drawing.Point(196, 25);
            _Chart.Name = "_Chart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = System.Drawing.Color.FromArgb(64, 0, 64);
            series1.Name = "Series1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.Maroon;
            series2.Name = "Series2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = System.Drawing.Color.Maroon;
            series3.Name = "Series3";
            _Chart.Series.Add(series1);
            _Chart.Series.Add(series2);
            _Chart.Series.Add(series3);
            _Chart.Size = new System.Drawing.Size(604, 271);
            _Chart.TabIndex = 6;
            _Chart.Text = "Графики";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(_Chart);
            Controls.Add(_RichTextBoxSplitter);
            Controls.Add(_RichTextBox);
            Controls.Add(_TreeViewSplitter);
            Controls.Add(_TreeView);
            Controls.Add(_StatusStrip);
            Controls.Add(_ToolStrip);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "MainForm";
            Text = "Контроль вибрации";
            _ToolStrip.ResumeLayout(false);
            _ToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_Chart).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ToolStrip _ToolStrip;
        private System.Windows.Forms.StatusStrip _StatusStrip;
        private System.Windows.Forms.TreeView _TreeView;
        private System.Windows.Forms.Splitter _TreeViewSplitter;
        private System.Windows.Forms.Timer _Timer;
        private System.Windows.Forms.RichTextBox _RichTextBox;
        private System.Windows.Forms.Splitter _RichTextBoxSplitter;
        private System.Windows.Forms.ToolStripButton _RichTextBoxButton;
        private System.Windows.Forms.ImageList _ImageList;
        private System.Windows.Forms.DataVisualization.Charting.Chart _Chart;
        private System.Windows.Forms.ToolStripButton _StoreButton;
        private System.Windows.Forms.ToolStripButton _ControlButton;
    }
}
