namespace WinForms_Project
{
    partial class SimDisplay
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SimDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "SimDisplay";
            this.Size = new System.Drawing.Size(325, 518);
            this.Click += new System.EventHandler(this.SimDisplay_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SimDisplay_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SimDisplay_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
