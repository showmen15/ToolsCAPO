namespace TaskRunner
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
            this.butRun = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.butStop = new System.Windows.Forms.Button();
            this.lblCaseName = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.butSkipNext = new System.Windows.Forms.Button();
            this.timerToEnd = new System.Windows.Forms.Timer(this.components);
            this.chkRecord = new System.Windows.Forms.CheckBox();
            this.chkVisualizer = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtServerName = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // butRun
            // 
            this.butRun.Location = new System.Drawing.Point(12, 53);
            this.butRun.Name = "butRun";
            this.butRun.Size = new System.Drawing.Size(66, 23);
            this.butRun.TabIndex = 0;
            this.butRun.Text = "Run";
            this.butRun.UseVisualStyleBackColor = true;
            this.butRun.Click += new System.EventHandler(this.butRun_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(78, 27);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 20);
            this.txtUser.TabIndex = 2;
            this.txtUser.Text = "szsz";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(250, 28);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.Text = "szsz";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "ServerName";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "User";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password";
            // 
            // butStop
            // 
            this.butStop.Enabled = false;
            this.butStop.Location = new System.Drawing.Point(84, 53);
            this.butStop.Name = "butStop";
            this.butStop.Size = new System.Drawing.Size(75, 23);
            this.butStop.TabIndex = 7;
            this.butStop.Text = "Stop";
            this.butStop.UseVisualStyleBackColor = true;
            this.butStop.Click += new System.EventHandler(this.butStop_Click);
            // 
            // lblCaseName
            // 
            this.lblCaseName.AutoSize = true;
            this.lblCaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblCaseName.Location = new System.Drawing.Point(12, 79);
            this.lblCaseName.Name = "lblCaseName";
            this.lblCaseName.Size = new System.Drawing.Size(77, 16);
            this.lblCaseName.TabIndex = 8;
            this.lblCaseName.Text = "CaseName";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 116);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(335, 43);
            this.textBox1.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.label5.Location = new System.Drawing.Point(247, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "label5";
            // 
            // butSkipNext
            // 
            this.butSkipNext.Location = new System.Drawing.Point(165, 53);
            this.butSkipNext.Name = "butSkipNext";
            this.butSkipNext.Size = new System.Drawing.Size(75, 23);
            this.butSkipNext.TabIndex = 12;
            this.butSkipNext.Text = "Skip";
            this.butSkipNext.UseVisualStyleBackColor = true;
            this.butSkipNext.Click += new System.EventHandler(this.butSkipNext_Click);
            // 
            // timerToEnd
            // 
            this.timerToEnd.Interval = 1000;
            this.timerToEnd.Tick += new System.EventHandler(this.timerToEnd_Tick);
            // 
            // chkRecord
            // 
            this.chkRecord.AutoSize = true;
            this.chkRecord.Location = new System.Drawing.Point(15, 169);
            this.chkRecord.Name = "chkRecord";
            this.chkRecord.Size = new System.Drawing.Size(61, 17);
            this.chkRecord.TabIndex = 13;
            this.chkRecord.Text = "Record";
            this.chkRecord.UseVisualStyleBackColor = true;
            // 
            // chkVisualizer
            // 
            this.chkVisualizer.AutoSize = true;
            this.chkVisualizer.Location = new System.Drawing.Point(78, 168);
            this.chkVisualizer.Name = "chkVisualizer";
            this.chkVisualizer.Size = new System.Drawing.Size(70, 17);
            this.chkVisualizer.TabIndex = 14;
            this.chkVisualizer.Text = "Visualizer";
            this.chkVisualizer.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(261, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Beep";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtServerName
            // 
            this.txtServerName.FormattingEnabled = true;
            this.txtServerName.Items.AddRange(new object[] {
            "WR-7-BASE-74\\SQLEXPRESS",
            "SZSZ\\SQLEXPRESS"});
            this.txtServerName.Location = new System.Drawing.Point(78, 3);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(272, 21);
            this.txtServerName.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 189);
            this.Controls.Add(this.txtServerName);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chkVisualizer);
            this.Controls.Add(this.chkRecord);
            this.Controls.Add(this.butSkipNext);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblCaseName);
            this.Controls.Add(this.butStop);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.butRun);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button butRun;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butStop;
        private System.Windows.Forms.Label lblCaseName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button butSkipNext;
        private System.Windows.Forms.Timer timerToEnd;
        private System.Windows.Forms.CheckBox chkRecord;
        private System.Windows.Forms.CheckBox chkVisualizer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox txtServerName;
    }
}

