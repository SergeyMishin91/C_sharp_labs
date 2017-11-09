using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sam2_4
{
    class Program
    {
        static int [,] Input (out int n, out int m)
		{
			Console.WriteLine("введите размерность массива");
			Console.Write("n = ");
			n=int.Parse(Console.ReadLine());
			Console.Write("m = ");
			m=int.Parse(Console.ReadLine());
			int [,]a=new int[n, m];
			for (int i = 0; i < n; ++i) 
				for (int j = 0; j < m; ++j)
				{
					Console.Write("a[{0},{1}]= ", i, j);
					a[i, j]=int.Parse(Console.ReadLine());
				}
			return a;
		}

		static void Print(int[,] a, int n, int m) 
		{
			for (int i = 0; i < n; ++i,Console.WriteLine() )
				for (int j = 0; j < m; ++j)
					Console.Write("{0,5} ", a[i, j]);
		}

		static void DeleteArray(int[,] a, int n, ref int m, int k)
		{
			for (int j = k; j < m-1; ++j)
				for (int i = 0; i < n; ++i)
					a[i, j] = a[i, j+1];
			--m;
		}

        static void Decision (int[,] a, int n, ref int m)
        {
            int z=0;
            int k = 0;
            int count = 0;
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (a[i, j] > 0)
                        count++;
                }

                if ((count-z)==m)
                {
                    k = j--;
                    DeleteArray(a, n, ref m, k);
                    z++;
                }
                count = 0;
            }
        }

		static void Main()
		{
			int n,m;
			int[,] myArray=Input(out n, out m);
			Console.WriteLine("Исходный массив:");
			Print(myArray, n, m);
            Decision(myArray, n, ref m);
			Console.WriteLine("Измененный массив:");
			Print(myArray, n, m);
            Console.ReadLine();
		}
    }
}
