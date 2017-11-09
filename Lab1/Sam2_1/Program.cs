using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sam2_1
{
    class Program
    {
        static int[,] Input(out int n, out int m)
        {
            Console.WriteLine("введите размерность массива");
            Console.Write("n = ");
            n = int.Parse(Console.ReadLine());
            Console.Write("m = ");
            m = int.Parse(Console.ReadLine());
            //выделяем памяти больше чем необходимо
            int[,] a = new int[2 * n, m];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                {
                    Console.Write("a[{0},{1}]= ", i, j);
                    a[i, j] = int.Parse(Console.ReadLine());
                }
            return a;
        }


        static void Print(int[,] a, int n, int m)
        {
            for (int i = 0; i < n; ++i, Console.WriteLine())
                for (int j = 0; j < m; ++j)
                    Console.Write("{0,5} ", a[i, j]);
        }

        static void AddArray(int[,] a, ref int n, int m, int k)
        {
            for (int i = n; i >= k; --i)
                for (int j = 0; j < m; ++j)
                    a[i + 1, j] = a[i, j];
            ++n;
            Console.WriteLine("Введите элементы новой строки");
            for (int j = 0; j < m; ++j)
            {
                Console.Write("a[{0},{1}]=", k, j);
                a[k, j] = int.Parse(Console.ReadLine());
            }
        }

        static int decision (int[,] a, ref int n, int m)
        {
            int min = 100000;
            int i1 = 0, j1 = 0;
            // search min 
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    if (min > a[i, j])
                        min = a[i, j];
            Console.WriteLine("min={0}", min);

            // search indexes of first min value
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < m; j++)
                {
                    if (a[i, j].Equals(min))
                    {
                        i1 = i;
                        j1 = j;
                        count++;
                        break;
                    }   
                }
                if (count.Equals(1)) 
                    break;
            }
            i1++;
            return i1;
        }

        static void Main()
        {
            int n, m;
            int[,] myArray = Input(out n, out m);
            Console.WriteLine("Исходный массив:");
            Print(myArray, n, m);
            int k = decision(myArray, ref n, m);
            AddArray(myArray, ref n, m, k);
            Console.WriteLine("Измененный массив:");
            Print(myArray, n, m);
            Console.ReadLine();
        }
    }
}
