//9 вариант.	Подсчитать сумму элементов, кратных 9.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Посчитать сумму элементов кратных 9

namespace Lab1
{
    class Program1
    {
        static int[] Input()
        {
            Console.WriteLine("введите размерность массива");
            int n = int.Parse(Console.ReadLine());
            int[] a = new int[n];
            for (int i = 0; i < n; ++i)
            {
                Console.Write("a[{0}]= ", i);
                a[i] = int.Parse(Console.ReadLine());
            }
            return a;
        }

        static void Print(int[] a)
        {
            for (int i = 0; i < a.Length; ++i) Console.Write("{0} ", a[i]);
            Console.WriteLine();
        }

        static void Summa(int[] a)
        {
            int sum=0;
            for (int i = 0; i < a.Length; ++i)
                if (a[i] % 9 == 0) sum++;
            Console.WriteLine("{0}", sum);
        }

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

        static void Print(int[,] a)
        {
            for (int i = 0; i < a.GetLength(0); ++i, Console.WriteLine())
                for (int j = 0; j < a.GetLength(1); ++j)
                    Console.Write("{0,5} ", a[i, j]);
        }

        static void Summa2(int[,] a)
        {
            int sum2=0;
            for (int i = 0; i < a.GetLength(0); ++i)
                for (int j = 0; j < a.GetLength(1); ++j)
                    if (a[i, j] % 9 == 0) sum2++;
            Console.Write(sum2);
        }


        static void Main()
        {
            int x;
            Console.WriteLine("Одномерный или двумерный (1 или 0):");
            x = int.Parse(Console.ReadLine());
            if (x==1)
            {
                int[] myArray = Input();
                Console.WriteLine("Исходный массив:");
                Print(myArray);
                Console.WriteLine("Количество элементов кратных девяти:");
                Summa(myArray);
                Console.ReadLine();
            } else if (x==0)
            {
                int n2, m2;
                int[,] myArray2 = Input(out n2, out m2);
                Console.WriteLine("Исходный массив:");
                Print(myArray2);
                Console.WriteLine("Количество элементов кратных девяти:");
                Summa2(myArray2);
                Console.ReadLine();
            }
        }

    }
}

