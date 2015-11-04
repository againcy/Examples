namespace Examples
{
    partial class BackgroundWorkerForm
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
            this.bw = new System.ComponentModel.BackgroundWorker();
            this.label_progress = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.textBox_bound = new System.Windows.Forms.TextBox();
            this.button_start = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // bw
            // 
            this.bw.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_DoWork);
            this.bw.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bw_ProgressChanged);
            this.bw.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bw_RunWorkerCompleted);
            // 
            // label_progress
            // 
            this.label_progress.AutoSize = true;
            this.label_progress.Location = new System.Drawing.Point(168, 48);
            this.label_progress.Name = "label_progress";
            this.label_progress.Size = new System.Drawing.Size(89, 12);
            this.label_progress.TabIndex = 0;
            this.label_progress.Text = "label_progress";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 74);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(382, 23);
            this.progressBar.Step = 2;
            this.progressBar.TabIndex = 1;
            this.progressBar.Click += new System.EventHandler(this.progressBar_Click);
            // 
            // textBox_bound
            // 
            this.textBox_bound.Location = new System.Drawing.Point(12, 45);
            this.textBox_bound.Name = "textBox_bound";
            this.textBox_bound.Size = new System.Drawing.Size(69, 21);
            this.textBox_bound.TabIndex = 2;
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(87, 43);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(60, 23);
            this.button_start.TabIndex = 3;
            this.button_start.Text = "Start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "输入数字，开始计时";
            // 
            // BackgroundWorkerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 111);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.textBox_bound);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label_progress);
            this.Name = "BackgroundWorkerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BackgroundWorkerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bw;
        private System.Windows.Forms.Label label_progress;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox textBox_bound;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label1;
    }
}