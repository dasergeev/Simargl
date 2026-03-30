namespace RailTest.Border.Server
{
    partial class OscillogramControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this._Timer = new System.Windows.Forms.Timer(this.components);
            this._ListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._Splitter = new System.Windows.Forms.Splitter();
            this._Chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this._Chart)).BeginInit();
            this.SuspendLayout();
            // 
            // _Timer
            // 
            this._Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // _ListView
            // 
            this._ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._ListView.CheckBoxes = true;
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this._ListView.Dock = System.Windows.Forms.DockStyle.Right;
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.Location = new System.Drawing.Point(537, 0);
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(256, 441);
            this._ListView.TabIndex = 0;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            this._ListView.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListView_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Канал";
            this.columnHeader1.Width = 128;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Среднее";
            this.columnHeader2.Width = 64;
            // 
            // _Splitter
            // 
            this._Splitter.Dock = System.Windows.Forms.DockStyle.Right;
            this._Splitter.Location = new System.Drawing.Point(533, 0);
            this._Splitter.Name = "_Splitter";
            this._Splitter.Size = new System.Drawing.Size(4, 441);
            this._Splitter.TabIndex = 1;
            this._Splitter.TabStop = false;
            // 
            // _Chart
            // 
            chartArea1.AxisX.ScaleView.Zoomable = false;
            chartArea1.Name = "ChartArea1";
            this._Chart.ChartAreas.Add(chartArea1);
            this._Chart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this._Chart.Legends.Add(legend1);
            this._Chart.Location = new System.Drawing.Point(0, 0);
            this._Chart.Name = "_Chart";
            this._Chart.Size = new System.Drawing.Size(533, 441);
            this._Chart.TabIndex = 2;
            this._Chart.Text = "chart1";
            // 
            // OscillogramControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._Chart);
            this.Controls.Add(this._Splitter);
            this.Controls.Add(this._ListView);
            this.Name = "OscillogramControl";
            this.Size = new System.Drawing.Size(793, 441);
            ((System.ComponentModel.ISupportInitialize)(this._Chart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer _Timer;
        private System.Windows.Forms.ListView _ListView;
        private System.Windows.Forms.Splitter _Splitter;
        private System.Windows.Forms.DataVisualization.Charting.Chart _Chart;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}
