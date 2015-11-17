using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataMining_Assignment_4
{
    class Vector
    {
        public double[] value;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="v">向量</param>
        public Vector(double[] v)
        {
            value = v;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="v">向量</param>
        public Vector(Array v)
        {
            value = new double[v.Length];
            for (int i = 0; i < v.Length; i++) value[i] = Convert.ToDouble( v.GetValue(i));
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="v">向量</param>
        public Vector(Vector v)
        {
            this.value = new double[v.value.Length];
            for (int i = 0; i < value.Length; i++) value[i] = v.value[i];
        }

        /// <summary>
        /// 构造函数，创建n维0向量
        /// </summary>
        /// <param name="n">向量维数</param>
        public Vector(int n)
        {
            value = new double[n];
            value.Initialize();
        }

        /// <summary>
        /// 向量内积
        /// </summary>
        /// <param name="v1">向量1</param>
        /// <param name="v2">向量2</param>
        /// <returns>内积</returns>
        public static double operator *(Vector v1, Vector v2)
        {

            double innerProduction = 0;
            try
            {
                for (int i = 0; i < v1.value.Length; i++) innerProduction += v1.value[i] * v2.value[i];
            }
            catch
            {
                Console.WriteLine("Error at inner production");
            }
            return innerProduction;
        }

        /// <summary>
        /// 向量与实数乘积
        /// </summary>
        /// <param name="num">实数</param>
        /// <param name="v">向量</param>
        /// <returns>乘积</returns>
        public static Vector operator *(double num, Vector v)
        {
            try
            {
                for (int i = 0; i < v.value.Length; i++) v.value[i] *= num;
            }
            catch
            {
                Console.WriteLine("Error at multiply");
            }
            return v;
        }

        /// <summary>
        /// 向量减法
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator -(Vector v1, Vector v2)
        {
            Vector result = new Vector(v1);
            try
            {
                for (int i = 0; i < result.value.Length; i++) result.value[i] -= v2.value[i];
            }
            catch
            {
                Console.WriteLine("Error at minus");
            }
            return result;
        }

        /// <summary>
        /// 向量加法
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector operator +(Vector v1, Vector v2)
        {
            Vector result = new Vector(v1);
            try
            {
                for (int i = 0; i < result.value.Length; i++) result.value[i] += v2.value[i];
            }
            catch
            {
                Console.WriteLine("Error at plus");
            }
            return result;
        }


    }
}
