using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining_Assignment_4
{
    /// <summary>
    /// 均匀随机数生成器
    /// 生成从0到N-1之间的均匀随机数
    /// </summary>
    class UniRandomGenerator
    {
        private int N;//随机数个数
        private int[] numList;//随机数表
        private int cur;//记录当前取到第几个随机数
        private Random rand;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="n">生成从0到n-1之间的均匀随机数</param>
        public UniRandomGenerator(int n)
        {
            N = n;
            numList = new int[n];
            rand = new Random();
            GenerateNewList();
        }

        /// <summary>
        /// 生成新的随机数表
        /// </summary>
        private void GenerateNewList()
        {
            for (int i = 0; i < N; i++) numList[i] = i;
            //对一个0到N-1顺序排列的数列做N次随机位置的两两交换以打乱顺序
            for (int i = 0; i < N; i++)
            {
                int i1 = rand.Next() % N;
                int i2 = rand.Next() % N;
                var tmp = numList[i1];
                numList[i1] = numList[i2];
                numList[i2] = tmp;
            }
            cur = 0;
        }

        /// <summary>
        /// 获取下一个随机数
        /// </summary>
        /// <returns>一个随机0到N-1之间的整数</returns>
        public int GetNext()
        {
            if (cur==N)
            {
                GenerateNewList();
            }
            int result = numList[cur];
            cur++;
            return result;
        }
    }
}
