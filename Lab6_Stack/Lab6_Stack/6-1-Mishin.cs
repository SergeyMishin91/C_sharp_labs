//1.	Решить следующие задачи с использованием класса Stack: 
    //1.	Дан файл, в котором записан набор чисел.Переписать в другой файл все числа в обратном порядке. 
    //2.	Создать текстовый файл.Распечатать гласные буквы этого файла в обратном порядке. 
    //3.	Напечатать содержимое текстового файла t, выписывая литеры каждой его строки в обратном порядке. 
    //4.	Даны 2 строки s1 и s2. Из каждой можно читать по одному символу.Выяснить, является ли строка s2 обратной s1. 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_Stack
{
    class Program
    {
        static void ReverseOrderOfNumbers()
        {
            try
            {
                string file = File.ReadAllText("inputStack1.txt");
                string path = "D:\\inputStack1.txt";
                StreamReader sr = new StreamReader(path);
                int size = File.ReadAllLines(path).Length;

                int[] nums = new int[10];
                int[] numsOutput = new int[8];

                for (int i = 0; i < size; i++)
                {
                    nums = file.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries).Select(n => int.Parse(n)).ToArray();
                }

                foreach (int n in nums)
                {
                    Console.Write("{0} ", n);
                }

                Stack myStack = new Stack();

                foreach (int n in nums)
                {
                    myStack.Push(n);
                }
                Console.WriteLine("\n");

                foreach (int n in nums)
                {
                    myStack.CopyTo(numsOutput, n - 1);
                    myStack.Pop();
                }

                File.WriteAllText("D:\\outStack.txt", "");                            //Опустошение файла
                foreach (int n in numsOutput)
                {
                    File.AppendAllText("D:\\outStack.txt", Convert.ToString(n) + " ");
                }
                Process.Start("D:\\outStack.txt");
            }
            catch (Exception e)
            {
                Console.Write("\n" + e.Message);
                Console.ReadKey();
            }
            Console.ReadKey();
        }

        static void ReverseVowels()
        {
            try
            {
                string file = "D:\\inputStack2.txt";

                //StreamWriter stream = new StreamWriter(file, true, Encoding.Default, 10);
                //Console.WriteLine("Напиши любое предложение и я запишу его в файл!");
                //string inputText = Convert.ToString(Console.ReadLine()); 
                //stream.Write(inputText);
                //stream.Close();
                //stream.Dispose();

                Stack myStack = new Stack();
                int size = File.ReadAllLines(file).Length;
                StreamReader sr = new StreamReader(file, System.Text.Encoding.Default);

                char[] ch = new char[8] { 'а', 'е', 'и', 'о', 'у', 'э', 'ю', 'я' };

                for (int i = 0; i < size; i++)
                {
                    char[] a = new char[100];
                    sr.Read(a, 0, 100);
                    foreach (char c in a)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (c.Equals(ch[j]))
                            {
                                myStack.Push(c);
                                break;
                            }
                        }
                    }
                }

                while (myStack.Count > 0)
                {
                    Console.Write("{0} ", myStack.Pop());
                }
            }
            catch (Exception e)
            {
                Console.Write("\n" + e.Message);
                Console.ReadKey();
            }

            Console.ReadKey();
        }

        static void Reverse()
        {
            try
            {
                string file = "D:\\inputStack3.txt";

                Stack myStack = new Stack();

                using (StreamReader sr = new StreamReader(file, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine("\nИсходная строка файла: ");
                        Console.WriteLine("{0}", line);

                        char[] a = line.ToCharArray();
                        for (int i = 0; i < a.Length; i++)
                        {
                            myStack.Push(a[i]);
                        }
                        Console.WriteLine("\nСкорректированная строка: ");
                        while (myStack.Count > 0)
                            Console.Write(myStack.Pop());
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("\n" + e.Message);
                Console.ReadKey();
            }
            Console.ReadKey();
        }

        static void ReverseLine()
        {
            Console.WriteLine("Введите строку s1: ");
            string s1 = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Введите строку s2: ");
            string s2 = Convert.ToString(Console.ReadLine());

            Stack myStack = new Stack();
            int count = s1.Length;
            char[] ch1 = new char[s1.Length];
            char[] ch2 = new char[s2.Length];

            ch1 = s1.ToCharArray();
            ch2 = s2.ToCharArray();

            foreach (char ch in ch1)
            {
                myStack.Push(ch);
            }

            while (myStack.Count > 0)
            {
                if (count != s2.Length)
                {
                    Console.WriteLine("Строки не являются обратными");
                    break;
                }
                for (int i = 0; i < s2.Length; i++)
                {
                    if (myStack.Peek().Equals(ch2[i]))
                    {
                        myStack.Pop();
                        count--;
                    }
                    else
                    {
                        Console.Write("Строки не равны!");
                        break;
                    }
                }
            }
            if (count == 0)
                Console.WriteLine("Строка s2 является обратной строке s1");

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("1.Дан файл, в котором записан набор чисел. Переписать в другой файл все числа в обратном порядке." +
                                "\n2.Создать текстовый файл.Распечатать гласные буквы этого файла в обратном порядке." +
                                "\n3.Напечатать содержимое текстового файла t, выписывая литеры каждой его строки в обратном порядке." + 
                                "\n4.Даны 2 строки s1 и s2.Из каждой можно читать по одному символу.Выяснить, является ли строка s2 обратной s1." + 
                                "\n\n Введите число от 1 до 4");
            byte n = byte.Parse(Console.ReadLine());

            switch (n)
            {
                case 1:
                    ReverseOrderOfNumbers();
                    break;
                case 2:
                    ReverseVowels();
                    break;
                case 3:
                    Reverse();
                    break;
                case 4:
                    ReverseLine();
                    break;
                default:
                    Console.WriteLine("Введите цифру от 1 до 4");
                    break;
            }

        }
    }
}
