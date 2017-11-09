using System;

namespace ConsoleApplication
{
    class Class
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

        static void Print(int[] a, int n)
        {
            for (int i = 0; i < n; ++i) Console.Write("{0} ", a[i]);
            Console.WriteLine();
        }

        static void DeleteArray(int[] a, ref int n, int m)
        {
            for (int i = m; i < n - 1; ++i)
                a[i] = a[i + 1];
            --n;

        }

        static void Main()
        {
            int[] myArray = Input();
            int n = myArray.Length;
            Console.WriteLine("Исходный массив:");
            Print(myArray, n);
            int m = 0;
            for (int i = 0; i < n; ++i)
            {
                if (myArray[i] % 2 == 0)
                {
                    m = i;
                    DeleteArray(myArray, ref n, m);
                    i--;
                }
            }
            Console.WriteLine("Измененный массив:");
            Print(myArray, n);
            Console.ReadLine();
        }
    }
}
