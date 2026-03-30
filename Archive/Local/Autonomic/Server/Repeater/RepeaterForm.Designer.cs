namespace RailTest.Satellite.Autonomic.Server
{
    partial class RepeaterForm
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

            this._CounterLabel = new System.Windows.Forms.Label();
            this._OutputView = new RailTest.Controls.OutputView();
            this.SuspendLayout();
            // 
            // _CounterLabel
            // 
            this._CounterLabel.Location = new System.Drawing.Point(12, 9);
            this._CounterLabel.Name = "_CounterLabel";
            this._CounterLabel.Size = new System.Drawing.Size(256, 20);
            this._CounterLabel.TabIndex = 1;
            this._CounterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _OutputView
            // 
            this._OutputView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this._OutputView.Location = new System.Drawing.Point(12, 291);
            this._OutputView.Name = "_OutputView";
            this._OutputView.Size = new System.Drawing.Size(902, 278);
            this._OutputView.TabIndex = 0;

            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 581);
            this.Controls.Add(this._CounterLabel);
            this.Controls.Add(this._OutputView);
            this.Name = "MainForm";
            this.Text = "RepeaterForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.OutputView _OutputView;
        private System.Windows.Forms.Label _CounterLabel;
    }
}