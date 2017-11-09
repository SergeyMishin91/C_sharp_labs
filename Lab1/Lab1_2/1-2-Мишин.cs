//9 вариант.	Найти номер первого максимального элемента.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_2
{
    class Program2
    {
        static int[] Input()
        {
            Console.WriteLine("Введите размерность массива");
            int n = int.Parse(Console.ReadLine());
            int[] a = new int[n];
            for (int i = 0; i < n; ++i)
            {
                Console.Write("a[{0}]= ", i);
                a[i] = int.Parse(Console.ReadLine());
            }
            return a;
        }

        static int Max(int[] a)
        {
            int max = a[0];
            for (int i = 1; i < a.Length; ++i)
                if (a[i] > max) max = a[i];
            for (int i = 1; i < a.Length; ++i)
                if (a[i] == max) 
                { 
                    Console.WriteLine(i);
                    break;
                }
            return max;
        }

        static void Main()
        {
            int[] myArray = Input();
            int max = Max(myArray);
            Console.ReadLine();
        }

    }
}
