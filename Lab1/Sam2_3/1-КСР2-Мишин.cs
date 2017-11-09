//КСР. Двумерные массивы. 9 вариант.
//II.В двумерном массиве, элементы которого – целые числа, произвести следующие действия:
//3.	Удалить все строки, в которых нет ни одного четного элемента.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sam2_3 
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
            int[,] a = new int[n, m];
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

        static void DeleteArray(int[,] a, ref int n, int m, int k)
        {
            for (int i = k; i < n - 1; ++i)
                for (int j = 0; j < m; ++j)
                    a[i, j] = a[i + 1, j];
            --n;
        }

        static void decision(int[,] a, ref int n, int m)
        {
            int count = 0;
            int k = 0;
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < m; ++j)
                {
                    if ((a[i, j] % 2) == 0)
                        count++;
                }
                
                if (count == 0)
                {
                    k = i--;
                    DeleteArray(a, ref n, m, k);
                }
                count = 0;
            }
        }

        static void Main()
        {
            int n, m;
            int[,] myArray = Input(out n, out m);
            Console.WriteLine("Исходный массив:");
            Print(myArray, n, m);
            decision (myArray, ref n, m);  
            Console.WriteLine("Измененный массив:");
            Print(myArray, n, m);
            Console.ReadLine();
        }
    }
}
