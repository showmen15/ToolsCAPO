namespace PdfChecker
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.numBegin = new System.Windows.Forms.NumericUpDown();
            this.numEnd = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numBegin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(50, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 91);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(789, 170);
            this.txtLog.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(238, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // numBegin
            // 
            this.numBegin.Location = new System.Drawing.Point(349, 26);
            this.numBegin.Name = "numBegin";
            this.numBegin.Size = new System.Drawing.Size(120, 20);
            this.numBegin.TabIndex = 3;
            this.numBegin.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numEnd
            // 
            this.numEnd.Location = new System.Drawing.Point(491, 26);
            this.numEnd.Name = "numEnd";
            this.numEnd.Size = new System.Drawing.Size(117, 20);
            this.numEnd.TabIndex = 4;
            this.numEnd.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 273);
            this.Controls.Add(this.numEnd);
            this.Controls.Add(this.numBegin);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.numBegin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEnd)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.NumericUpDown numBegin;
        private System.Windows.Forms.NumericUpDown numEnd;
    }
}

