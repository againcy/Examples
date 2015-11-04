using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Examples
{
    class Program
    {
        static private void TestExcel()
        {
            Excel excel = new Excel();
            excel.ReadExcels(@"G:\引文报告\ACM COMPUT SURV\savedrecs.xls");
        }

        static private void TestBackgroundWorker()
        {
            Application.Run(new BackgroundWorkerForm());
        }

        static void Main(string[] args)
        {
            //TestExcel();
            //TestBackgroundWorker();
            Console.WriteLine("End...");
            Console.ReadLine();
        }
    }
}
