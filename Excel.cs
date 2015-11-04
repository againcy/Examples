using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Microsoft.Office.Interop.Excel;//需要在项目中添加引用Microsoft Excel xx.0 Object Library


namespace Examples
{
    class Excel
    {
        private Application app;
        public Excel()
        {
            app = new Application();
        }

        /// <summary>
        /// 读入单个Excel文件
        /// </summary>
        /// <param name="file">文件路径</param>
        public void ReadExcel(string file)
        {
            Workbook wb = app.Workbooks.Open(file);
            Worksheet sheet = wb.Worksheets[1];//选择Excel文件中的sheet（从1开始）
            //以Web of Science的引文报告为例
            int rHead = 28;//表头的行号
            int cTitle = 1;//标题列号
            int cPublicationYear = 8;//出版年 列号
            int cDoi = 17;//doi号列号
            int cStart = 22;//引用数据起始列号

            //读取表格中每一行的数据
            const int MAX_LINES = 500;//WebOfScience每一个excel最多返回的数据行数
            for (int i = rHead + 1; i < rHead + MAX_LINES + 1; i++)
            {
                if (sheet.Cells[i, 1].Text == "") break; //全部读取完毕

                //防止错误数据造成程序终止
                try
                {
                    //读取文章标题，doi，出版年份信息
                    string title = sheet.Cells[i, cTitle].Text;
                    string doi = sheet.Cells[i, cDoi].Text;
                    int publicationYear;
                    if (int.TryParse(sheet.Cells[i, cPublicationYear].Text, out publicationYear)==false) continue;//转换成整形失败则跳过
                    
                    //查找文章发表年份在引用信息中的列数
                    int tmp = cStart;//引用数据起始位置
                    while (sheet.Cells[rHead, tmp].Text != "")
                    {
                        if (sheet.Cells[rHead, tmp].Text == sheet.Cells[i, cPublicationYear].Text) break;
                        tmp++;
                    }
                    //读取从发表年份开始，文章的引用信息
                    LinkedList<int> citation = new LinkedList<int>();//存放引用数据的链表
                    for (int j = tmp; sheet.Cells[rHead, j].Text != ""; j++)
                    {
                        int c;
                        int.TryParse(sheet.Cells[i, j].Text, out c);
                        citation.AddLast(c);
                    }
                    //存储获得的文章信息
                    //Journal.Add(title,doi,publicationYear,citation);
                }
                catch (Exception)
                {
                }
            }

            //释放资源
            System.Runtime.InteropServices.Marshal.ReleaseComObject(wb);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
            //关闭app
            app.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
            //调用GC的垃圾收集方法
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        
        /// <summary>
        /// 读入一个文件夹中的多个excel
        /// </summary>
        /// <param name="dir">文件夹路径</param>
        public void ReadExcels(string dir)
        {
            foreach (var file in Directory.GetFiles(dir,"*.xls")) 
            {
                ReadExcel(file);
            }
        }

    }
}
