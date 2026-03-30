namespace RailTest.Border.Support.Courier
{
    partial class CourierForm
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
            this._OutputView = new RailTest.Controls.OutputView();
            this._PerformerButton = new System.Windows.Forms.Button();
            this._TestingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _OutputView
            // 
            this._OutputView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._OutputView.Location = new System.Drawing.Point(12, 41);
            this._OutputView.Name = "_OutputView";
            this._OutputView.Size = new System.Drawing.Size(776, 397);
            this._OutputView.TabIndex = 0;
            // 
            // _PerformerButton
            // 
            this._PerformerButton.Location = new System.Drawing.Point(12, 12);
            this._PerformerButton.Name = "_PerformerButton";
            this._PerformerButton.Size = new System.Drawing.Size(75, 23);
            this._PerformerButton.TabIndex = 1;
            this._PerformerButton.Text = "Performer";
            this._PerformerButton.UseVisualStyleBackColor = true;
            this._PerformerButton.Click += new System.EventHandler(this.PerformerButton_Click);
            // 
            // _TestingButton
            // 
            this._TestingButton.Location = new System.Drawing.Point(93, 12);
            this._TestingButton.Name = "_TestingButton";
            this._TestingButton.Size = new System.Drawing.Size(75, 23);
            this._TestingButton.TabIndex = 2;
            this._TestingButton.Text = "Testing";
            this._TestingButton.UseVisualStyleBackColor = true;
            this._TestingButton.Click += new System.EventHandler(this.TestingButton_Click);
            // 
            // CourierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._TestingButton);
            this.Controls.Add(this._PerformerButton);
            this.Controls.Add(this._OutputView);
            this.Name = "CourierForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.OutputView _OutputView;
        private System.Windows.Forms.Button _PerformerButton;
        private System.Windows.Forms.Button _TestingButton;
    }
}