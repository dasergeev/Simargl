namespace RailTest.Satellite.Autonomic
{
    partial class LoggerView
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
            this._ListView = new System.Windows.Forms.ListView();
            this._ColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _ListView
            // 
            this._ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this._ColumnHeader});
            this._ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.HideSelection = false;
            this._ListView.Location = new System.Drawing.Point(0, 0);
            this._ListView.MultiSelect = false;
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(908, 585);
            this._ListView.TabIndex = 0;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            this._ListView.Resize += new System.EventHandler(this.ListView_Resize);
            // 
            // _ColumnHeader
            // 
            this._ColumnHeader.Text = "Сообщение";
            this._ColumnHeader.Width = 256;
            // 
            // _Timer
            // 
            this._Timer.Enabled = true;
            this._Timer.Interval = 250;
            this._Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // LoggerView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListView);
            this.Name = "LoggerView";
            this.Size = new System.Drawing.Size(908, 585);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _ListView;
        private System.Windows.Forms.Timer _Timer;
        private System.Windows.Forms.ColumnHeader _ColumnHeader;
    }
}
