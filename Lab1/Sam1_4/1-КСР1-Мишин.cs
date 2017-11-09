//КСР. Одномерные массивы. 9 вариант.
//I.В одномерном массиве, элементы которого – целые числа, произвести следующие действия:
//4.	Вставить новый элемент между всеми парами элементов, имеющими разные знаки.


using System;

namespace ConsoleApplication
{
    class Class
    {
        static int[] Input(out int n)
        {
            Console.WriteLine("введите размерность массива");
            n = int.Parse(Console.ReadLine());
            int[] a = new int[2 * n]; //выделяем памяти больше чем требуется
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

        static void AddArray(int[] a, ref int n, int m, int value)
        {
            for (int i = n; i >= m; --i)
                a[i] = a[i - 1];
            ++n;

            a[m] = value;
        }

        static void Main()
        {
            int n;
            int[] myArray = Input(out n);
            Console.WriteLine("Исходный массив:");
            Print(myArray, n);
            Console.WriteLine("Введите значение нового элемента");
            int value = int.Parse(Console.ReadLine());
            int m = 0;
            for (int i = 0; i < myArray.Length; ++i) 
            {
                for (int j = i + 1; j < myArray.Length; ++j) 
                {
                    if ((myArray[i] > 0) && (myArray[j] < 0) || (myArray[i] < 0) && (myArray[j] > 0)) 
                    {
                        m = j;
                        AddArray(myArray, ref n, m, value);
                        i++;
                        break;
                    }
                    break;
                }
            }
                
            Console.WriteLine("Измененный массив:");
            Print(myArray, n);
            Console.ReadLine();
        }
    }
}
