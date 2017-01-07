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
            antiAliasBase.Dispose();
            antialiasTarget.Dispose();

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.SalinityBar = new XComponent.SliderBar.MACTrackBar();
            this.SunlightBar = new XComponent.SliderBar.MACTrackBar();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 29);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(45, 13);
            label1.TabIndex = 0;
            label1.Text = "Sunlight";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(40, 13);
            label2.TabIndex = 4;
            label2.Text = "Salinity";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(325, 518);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(label2);
            this.flowLayoutPanel1.Controls.Add(this.SalinityBar);
            this.flowLayoutPanel1.Controls.Add(label1);
            this.flowLayoutPanel1.Controls.Add(this.SunlightBar);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 421);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(319, 94);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // SalinityBar
            // 
            this.SalinityBar.BackColor = System.Drawing.Color.Transparent;
            this.SalinityBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.SalinityBar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SalinityBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.SalinityBar.IndentHeight = 0;
            this.SalinityBar.LargeChange = 20;
            this.SalinityBar.Location = new System.Drawing.Point(3, 16);
            this.SalinityBar.Maximum = 60;
            this.SalinityBar.Minimum = 2;
            this.SalinityBar.Name = "SalinityBar";
            this.SalinityBar.Size = new System.Drawing.Size(319, 10);
            this.SalinityBar.TabIndex = 5;
            this.SalinityBar.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.SalinityBar.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.SalinityBar.TickFrequency = 5;
            this.SalinityBar.TickHeight = 4;
            this.SalinityBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.SalinityBar.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.SalinityBar.TrackerSize = new System.Drawing.Size(10, 10);
            this.SalinityBar.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.SalinityBar.TrackLineHeight = 3;
            this.SalinityBar.Value = 40;
            this.SalinityBar.ValueChanged += new XComponent.SliderBar.ValueChangedHandler(this.SalinityBar_ValueChanged);
            // 
            // SunlightBar
            // 
            this.SunlightBar.BackColor = System.Drawing.Color.Transparent;
            this.SunlightBar.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.SunlightBar.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SunlightBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.SunlightBar.IndentHeight = 0;
            this.SunlightBar.Location = new System.Drawing.Point(3, 45);
            this.SunlightBar.Maximum = 120;
            this.SunlightBar.Minimum = 0;
            this.SunlightBar.Name = "SunlightBar";
            this.SunlightBar.Size = new System.Drawing.Size(319, 10);
            this.SunlightBar.TabIndex = 3;
            this.SunlightBar.TextTickStyle = System.Windows.Forms.TickStyle.None;
            this.SunlightBar.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.SunlightBar.TickHeight = 4;
            this.SunlightBar.TickStyle = System.Windows.Forms.TickStyle.None;
            this.SunlightBar.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.SunlightBar.TrackerSize = new System.Drawing.Size(10, 10);
            this.SunlightBar.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.SunlightBar.TrackLineHeight = 3;
            this.SunlightBar.Value = 80;
            this.SunlightBar.ValueChanged += new XComponent.SliderBar.ValueChangedHandler(this.SunlightBar_ValueChanged);
            // 
            // SimDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SimDisplay";
            this.Size = new System.Drawing.Size(325, 518);
            this.Resize += new System.EventHandler(this.SimDisplay_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private XComponent.SliderBar.MACTrackBar SunlightBar;
        private XComponent.SliderBar.MACTrackBar SalinityBar;
    }
}
