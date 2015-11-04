using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    class KD_DataType
    {
        public double[] value;
        public int index;
        public KD_DataType()
        {
            value = null;
            index = 0;
        }
    }

    class KD_TreeNode
    {
        /// <summary>
        /// 分裂的维度
        /// </summary>
        public int split;
        //public KD_TreeNode parent;
        public KD_TreeNode left, right;
        public KD_DataType data;
    }
    
    /// <summary>
    /// 实现D维数据的最近邻查找
    /// </summary>
    class KD_Tree
    {
        /*
         参考
         百度百科KD-tree
         http://blog.csdn.net/zhjchengfeng5/article/details/7855241
         用于在一个拥有N个点的图中，快速查找任意一个点最近的K个邻居，每个点的位置是一个D维向量
        */
        //public KD_TreeNode Root
        private KD_TreeNode root;
        private KD_DataType[] dataset;
        private int dimension;
        private List<int> check;//记录已返回过的最近邻

        private int curNearestNode;//记录当前最近节点的index
        private double curNearestDist;//记录当前最近的距离
        private KD_DataType curQuery;//当前的查询点

        public KD_Tree(KD_DataType[] s)
        {
            dataset = s;
            dimension = s[0].value.Count();
            root = BuildTree(0, s.Count() - 1);
        }

        /// <summary>
        /// 将head到tail之间的数据按d维排序
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <param name="d">排序关键字</param>
        private void sort(int head,int tail,int d)
        {
            int h = head;
            int t = tail;
            while(h<t)
            {
                while (h < t && dataset[h].value[d] <= dataset[t].value[d]) h++;
                var tmp = dataset[h];dataset[h] = dataset[t];dataset[t] = tmp;
                while (h < t && dataset[h].value[d] <= dataset[t].value[d]) t--;
                tmp = dataset[h]; dataset[h] = dataset[t]; dataset[t] = tmp;
            }
            if (head<tail)
            {
                sort(head, h - 1, d);
                sort(h + 1, tail, d);
            }
        }

        /// <summary>
        /// 对区间head到tail之间的点建树
        /// </summary>
        /// <param name="head"></param>
        /// <param name="tail"></param>
        /// <returns>当前节点</returns>
        private KD_TreeNode BuildTree(int head, int tail)
        {
            if (head > tail) return null;

            //计算每一维上的方差，并找到方差最大的维度
            double maxVariance = 0;
            int bestDimension = 0;
            for (int s = 0; s < dimension; s++)
            {
                double variance = 0;
                double sum = 0;
                for (int i = head; i <= tail; i++) sum += dataset[i].value[s];
                double avr = sum / (double)(tail - head + 1);
                for(int i=head;i<=tail; i++) variance += Math.Pow(dataset[i].value[s] - avr, 2);
                variance /= (double)(tail - head + 1);
                if (variance>maxVariance)
                {
                    maxVariance = variance;
                    bestDimension = s;
                }
            }
            //新节点
            KD_TreeNode newNode = new KD_TreeNode();
            newNode.split = bestDimension;
            //将区间内的点按best dimension排序
            Dictionary<KD_DataType, double> unsort = new Dictionary<KD_DataType, double>();

            for (int i = head; i <= tail; i++)
            {
                unsort.Add(dataset[i], dataset[i].value[bestDimension]);
            }
            var sorted = from pair in unsort
                         orderby pair.Value ascending
                         select pair;
            var arrSorted = sorted.ToArray();
            for(int i = head;i<=tail;i++)
            {
                dataset[i] = arrSorted[i - head].Key;
            }
            //建立左右子树
            //sort(head, tail, bestDimension);
            int mid = head + (tail - head) / 2;
            newNode.data = dataset[mid];
            newNode.left = BuildTree(head, mid - 1);
            newNode.right = BuildTree(mid + 1, tail);
            return newNode;
        }

        /// <summary>
        /// 向量x和y的欧式距离，若两向量维度不同则返回-1
        /// </summary>
        /// <param name="x">向量x</param>
        /// <param name="y">向量y</param>
        /// <returns>x和y的欧氏距离的平方或-1</returns>
        public double Dist(double[] x, double[] y)
        {
            if (x.Count() != y.Count()) return -1;
            else
            {
                double sum = 0;
                for (int i = 0; i < x.Count(); i++)
                {
                    sum += Math.Pow((x[i] - y[i]), 2);
                }
                return sum;
            }
        }

        /// <summary>
        /// 从cur节点开始向子树中寻找x的最近邻
        /// </summary>
        /// <param name="cur">开始查找的根节点</param>
        private void Query(KD_TreeNode cur)
        {
            if (cur == null) return;
            //求出目标x到当前节点的距离
            double dist = Dist(cur.data.value, curQuery.value);
            if (check.Contains(cur.data.index)==false && dist<curNearestDist)
            {
                //当前节点未被提取过且dist小于当前最近距离
                curNearestDist = dist;
                curNearestNode = cur.data.index;
            }
            //计算x到分裂平面的距离
            double radius = Math.Pow(curQuery.value[cur.split] - cur.data.value[cur.split], 2);
            //对子区间进行查询
            if (curQuery.value[cur.split]<cur.data.value[cur.split])
            {
                Query(cur.left);
                if (radius <= curNearestDist) Query(cur.right);
            }
            else
            {
                Query(cur.right);
                if (radius <= curNearestDist) Query(cur.left);
            }
        }
            
        /// <summary>
        /// 返回距离x最近的k个点（不包括x本身）
        /// </summary>
        /// <param name="k">近邻数量</param>
        /// <param name="x">需要查询的点</param>
        /// <returns>近邻点的index数组</returns>
        public int[] KQuery(int k, KD_DataType x)
        {
            check = new List<int>();
            check.Add(x.index);
            int[] result = new int[k];
            curQuery = x;
            for (int i = 0; i < k; i++)
            {
                curNearestDist = double.MaxValue;
                Query(root);
                check.Add(curNearestNode);
                result[i] = curNearestNode;
            }
            return result;
        }

    }
}
