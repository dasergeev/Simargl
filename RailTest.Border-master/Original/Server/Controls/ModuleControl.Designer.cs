namespace RailTest.Border.Server
{
    partial class ModuleControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModuleControl));
            this._ListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this._ImageList = new System.Windows.Forms.ImageList(this.components);
            this._Timer = new System.Windows.Forms.Timer(this.components);
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // _ListView
            // 
            this._ListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._ListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader4});
            this._ListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ListView.FullRowSelect = true;
            this._ListView.GridLines = true;
            this._ListView.HideSelection = false;
            this._ListView.LabelWrap = false;
            this._ListView.Location = new System.Drawing.Point(0, 0);
            this._ListView.Name = "_ListView";
            this._ListView.Size = new System.Drawing.Size(1009, 497);
            this._ListView.SmallImageList = this._ImageList;
            this._ListView.TabIndex = 0;
            this._ListView.UseCompatibleStateImageBehavior = false;
            this._ListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Индекс";
            this.columnHeader1.Width = 64;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Адрес";
            this.columnHeader2.Width = 128;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Подключение";
            this.columnHeader3.Width = 96;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Частота";
            this.columnHeader5.Width = 64;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Блоки";
            this.columnHeader7.Width = 64;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Маркер";
            this.columnHeader8.Width = 128;
            // 
            // _ImageList
            // 
            this._ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_ImageList.ImageStream")));
            this._ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this._ImageList.Images.SetKeyName(0, "Flag_redHS.png");
            this._ImageList.Images.SetKeyName(1, "Flag_blueHS.png");
            this._ImageList.Images.SetKeyName(2, "Flag_greenHS.png");
            // 
            // _Timer
            // 
            this._Timer.Interval = 250;
            this._Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Индекс блока";
            this.columnHeader4.Width = 128;
            // 
            // ModuleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._ListView);
            this.Name = "ModuleControl";
            this.Size = new System.Drawing.Size(1009, 497);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView _ListView;
        private System.Windows.Forms.Timer _Timer;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ImageList _ImageList;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}
