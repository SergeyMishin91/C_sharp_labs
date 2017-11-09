using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExampleLab2
{
    class DemoArray
    {
        int[] MyArray;//закрытый массив

        public DemoArray(int size)//конструктор 1
        {
            MyArray = new int[size];
        }

        public DemoArray(params int[] arr)//конструктор 2
        {
            MyArray = new int[arr.Length];
            for (int i = 0; i < MyArray.Length; i++) MyArray[i] = arr[i];
        }

        public int LengthArray //свойство, возвращающее размерность
        {
            get { return MyArray.Length; }
        }

        public int this[int i] //индексатор
        {
            get
            {
                if (i < 0 || i >= MyArray.Length) throw new Exception("выход за границы массива");
                return MyArray[i];
            }
            set
            {
                if (i < 0 || i >= MyArray.Length) throw new Exception("выход за границы массива");
                else MyArray[i] = value;
            }
        }

        public static DemoArray operator -(DemoArray x) //перегрузка операции унарный минус
        {
            DemoArray temp = new DemoArray(x.LengthArray);
            for (int i = 0; i < x.LengthArray; ++i)
                temp[i] = -x[i];
            return temp;
        }

        public static DemoArray operator ++(DemoArray x) //перегрузка операции инкремента
        {
            DemoArray temp = new DemoArray(x.LengthArray);
            for (int i = 0; i < x.LengthArray; ++i)
                temp[i] = x[i] + 1;
            return temp;
        }

        public static bool operator true(DemoArray a) //перегрузка константы true
        {
            foreach (int i in a.MyArray)
            {
                if (i < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator false(DemoArray a)//перегрузка константы false
        {
            foreach (int i in a.MyArray)
            {
                if (i > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public void Print(string name) //метод - выводит поле-массив на экран
        {
            Console.WriteLine(name + ": ");
            for (int i = 0; i < MyArray.Length; i++)
                Console.Write(MyArray[i] + " ");
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {
                DemoArray Mas = new DemoArray(1, -4, 3, -5, 0); //вызов конструктора 2
                Mas.Print("Исходный массив");
                Console.WriteLine("\nУнарный минус");
                DemoArray newMas = -Mas; //применение операции унарного минуса 
                Mas.Print("Mассив Mas"); //обратите внимание, что создается новый объект и знаки меняются
                newMas.Print("Массив newMas"); //только у нового массива 
                Console.WriteLine("\nОперация префиксного инкремента");
                DemoArray Mas1 = ++Mas;
                Mas.Print("Mассив Mas");
                Mas1.Print("Mассив Mas1=++Mas");
                Console.WriteLine("\nОперация постфиксного инкремента");
                DemoArray Mas2 = Mas++;
                Mas.Print("Mассив Mas");
                Mas2.Print("Mассив Mas2=Mas++");
                if (Mas)
                    Console.WriteLine("\nВ массиве все элементы положительные\n");
                else Console.WriteLine("\nВ массиве есть не положительные элементы\n");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
