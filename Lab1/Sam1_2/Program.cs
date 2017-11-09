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

        static void AddArray(int[] a, ref int n, int k)
        {
            int m = 0; //позиция элемента
            Console.WriteLine("Введите значение нового элемента");
            int num = int.Parse(Console.ReadLine()); //вводим ручками значение нового элемента
            for (int j = 0; j < a.Length; j++) // перебираем массив
            {
                if ((a[j] % 10 == k) || (a[j] == k)) // условие если заданное число есть последняя цифра элемента массива
                {
                    m = j + 1; // присваиваем позиции нового элемента, следующую после искомого
                    n++; // увеличиваем размерность массива
                    for (int i = n; i >= m; --i) // освобождаем позиции для вставки нового элемента
                        a[i] = a[i - 1];
                    a[m] = num; // вставляем искомые значения на осовбодившиеся позиции
                }
            }            
        }

        static void Main()
        {
            int n;
            int[] myArray = Input(out n);
            Console.WriteLine("Исходный массив:");
            Print(myArray, n);
            Console.WriteLine("Введите цифру на которую должен заканчивататься элемент массива:");
            int k = int.Parse(Console.ReadLine());
            AddArray(myArray, ref n, k);
            Console.WriteLine("Измененный массив:");
            Print(myArray, n);
            Console.ReadLine();
        }
    }
}
