//9 Вариант.	Если количество столбцов в массиве четное, то поменять столбцы  местами по правилу: 
//первый столбец со вторым, третий – с четвертым и т.д.
//    Если количество столбцов в массиве нечетное, то оставить массив без изменений.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_3
{
    class Program3
    {
        

        static int[,] Input(out int n)
        {
            
            Console.WriteLine("введите размерность массива");
            Console.Write("n = ");
            n = int.Parse(Console.ReadLine());
            int[,] a = new int[n, n];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
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

        static void Rezult(int[,] a)
        {
            int stolb=0;
            int k = 0;

            for (int i = 0; i < a.GetLength(0); ++i)
                for (int j = i + 1; j < a.GetLength(1); ++j)
                    stolb = j + 1;
            Console.WriteLine("Stolb = {0}", stolb);

            double s = 0;
            int tmp = 0;
            if (stolb % 2 == 0)
            {
                for (int i = 0; i < a.GetLength(0); i++)
                    for (int j = 0; j < a.GetLength(1); j++)
                        if ((j%2 == 0)||(j==0))
                        {
                            tmp = a[i, j+1];
                            a[i, j+1] = a[i, j];
                            a[i, j] = tmp;
                        }
              }
        }

        static void Main()
        {
            int n;
            int[,] myArray = Input(out n);
            Console.WriteLine("Исходный массив:");
            Print(myArray);
            Rezult(myArray);
            Console.WriteLine("New array:");
            Print(myArray);
            Console.ReadLine();
        }

    }
}
