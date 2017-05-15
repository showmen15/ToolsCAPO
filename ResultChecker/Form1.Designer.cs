namespace ResultChecker
{
    partial class Form1
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
            this.butStart = new System.Windows.Forms.Button();
            this.lblErrorCount = new System.Windows.Forms.Label();
            this.timerRunner = new System.Windows.Forms.Timer(this.components);
            this.butStop = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // butStart
            // 
            this.butStart.Location = new System.Drawing.Point(22, 21);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(75, 23);
            this.butStart.TabIndex = 0;
            this.butStart.Text = "Start";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblErrorCount
            // 
            this.lblErrorCount.AutoSize = true;
            this.lblErrorCount.Location = new System.Drawing.Point(117, 41);
            this.lblErrorCount.Name = "lblErrorCount";
            this.lblErrorCount.Size = new System.Drawing.Size(57, 13);
            this.lblErrorCount.TabIndex = 1;
            this.lblErrorCount.Text = "ErrorCount";
            // 
            // timerRunner
            // 
            this.timerRunner.Interval = 300000;
            this.timerRunner.Tick += new System.EventHandler(this.timerRunner_Tick);
            // 
            // butStop
            // 
            this.butStop.Enabled = false;
            this.butStop.Location = new System.Drawing.Point(22, 51);
            this.butStop.Name = "butStop";
            this.butStop.Size = new System.Drawing.Size(75, 23);
            this.butStop.TabIndex = 2;
            this.butStop.Text = "Stop";
            this.butStop.UseVisualStyleBackColor = true;
            this.butStop.Click += new System.EventHandler(this.butStop_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 92);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(268, 152);
            this.textBox1.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.butStop);
            this.Controls.Add(this.lblErrorCount);
            this.Controls.Add(this.butStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butStart;
        private System.Windows.Forms.Label lblErrorCount;
        private System.Windows.Forms.Timer timerRunner;
        private System.Windows.Forms.Button butStop;
        private System.Windows.Forms.TextBox textBox1;
    }
}

