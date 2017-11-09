using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sam2_2
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
            int[,] a = new int[n, 2 * m];
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

        static void AddArray(int[,] a, int n, ref int m, int k)
        {
            
            for (int j = m; j >= k; --j) 
            {
                Console.WriteLine("ok");
                for (int i = 0; i < n; ++i)
                {
                    a[i, j + 1] = a[i, j];
                }
            }
            ++m;
           
            Console.WriteLine("Введите элементы нового столбца:");
            for (int i = 0; i < n; ++i)
            {
                Console.Write("a[{0},{1}]=", i, k);
                a[i, k] = int.Parse(Console.ReadLine());
            }
        }

        static void Decision(int[,] a, int n, ref int m) 
        {
            int count = 0;
            int c = 0;
            int k = 0;
            Console.WriteLine("Введите число, которое имеется в массиве:");
            int value = int.Parse(Console.ReadLine());
            
            //перебираем значения массива
            for (int j = 0; j < m; j++)
            {
                Console.WriteLine("j = {0}, m = {1}", j, m);
                for (int i = 0; i < n; i++)
                {
                    if (a[i, j] == value)
                    {
                       c++;
                       k = j+1;
                       break;
                    }
                }
                Print(a, n, m);
                j++;
                if (count < c)
                {
                    AddArray(a, n, ref m, k);
                    count++;
                }
            }
        }

        static void Main ()
        {
            int n, m;
            int[,] myArray = Input(out n, out m);
            Console.WriteLine("Исходный массив:");
            Print(myArray, n, m);
            //Console.WriteLine("Введите число, которое имеется в массиве:");
            //int value = int.Parse(Console.ReadLine());
            //AddArray(myArray, n, ref m, value);
            Decision (myArray, n, ref m);
            Console.WriteLine("Измененный массив:");
            Print(myArray, n, m);
            Console.ReadLine();
        }
    } 
}
