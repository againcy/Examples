using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examples
{
    public partial class BackgroundWorkerForm : Form
    {
        private struct ProgressState
        {
            public int curNumber;
        }
        private ProgressState progressState;

        public BackgroundWorkerForm()
        {
            InitializeComponent();
            textBox_bound.Focus();
            
            
            //bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            //bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            progressState = new ProgressState();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int bound = 10;
            int.TryParse(textBox_bound.Text, out bound);
            int tmp;
            for (tmp = 1; tmp <= bound; tmp++)
            {
                progressState.curNumber = tmp;
                int percentage = (int)((double)tmp / (double)bound * 100);
                worker.ReportProgress(percentage, progressState);
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressState tmp = (ProgressState)e.UserState;
            this.progressBar.Value = e.ProgressPercentage;
            label_progress.Text = "当前秒数："+tmp.curNumber.ToString();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                label_progress.Text = "错误: " + e.Error.Message;
            }
            else
            {
                label_progress.Text = "完成";
            }
        }

        private void button_start_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy == false)
            {
                bw.RunWorkerAsync();
            }
        }

        private void progressBar_Click(object sender, EventArgs e)
        {

        }
    }
}
