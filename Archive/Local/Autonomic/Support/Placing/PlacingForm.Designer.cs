namespace RailTest.Satellite.Autonomic.Support
{
    partial class PlacingForm
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
            this._RegistrarButton = new System.Windows.Forms.Button();
            this._ServerButton = new System.Windows.Forms.Button();
            this._OutputView = new RailTest.Controls.OutputView();
            this._TestButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _RegistrarButton
            // 
            this._RegistrarButton.Location = new System.Drawing.Point(12, 12);
            this._RegistrarButton.Name = "_RegistrarButton";
            this._RegistrarButton.Size = new System.Drawing.Size(75, 23);
            this._RegistrarButton.TabIndex = 0;
            this._RegistrarButton.Text = "Registrar";
            this._RegistrarButton.UseVisualStyleBackColor = true;
            this._RegistrarButton.Click += new System.EventHandler(this.RegistrarButton_Click);
            // 
            // _ServerButton
            // 
            this._ServerButton.Location = new System.Drawing.Point(93, 12);
            this._ServerButton.Name = "_ServerButton";
            this._ServerButton.Size = new System.Drawing.Size(75, 23);
            this._ServerButton.TabIndex = 1;
            this._ServerButton.Text = "Server";
            this._ServerButton.UseVisualStyleBackColor = true;
            this._ServerButton.Click += new System.EventHandler(this.ServerButton_Click);
            // 
            // _OutputView
            // 
            this._OutputView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._OutputView.Location = new System.Drawing.Point(12, 41);
            this._OutputView.Name = "_OutputView";
            this._OutputView.Size = new System.Drawing.Size(776, 397);
            this._OutputView.TabIndex = 2;
            // 
            // _TestButton
            // 
            this._TestButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._TestButton.Location = new System.Drawing.Point(713, 12);
            this._TestButton.Name = "_TestButton";
            this._TestButton.Size = new System.Drawing.Size(75, 23);
            this._TestButton.TabIndex = 3;
            this._TestButton.Text = "Test";
            this._TestButton.UseVisualStyleBackColor = true;
            this._TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // PlacingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._TestButton);
            this.Controls.Add(this._OutputView);
            this.Controls.Add(this._ServerButton);
            this.Controls.Add(this._RegistrarButton);
            this.Name = "PlacingForm";
            this.Text = "PlacingForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _RegistrarButton;
        private System.Windows.Forms.Button _ServerButton;
        private RailTest.Controls.OutputView _OutputView;
        private System.Windows.Forms.Button _TestButton;
    }
}