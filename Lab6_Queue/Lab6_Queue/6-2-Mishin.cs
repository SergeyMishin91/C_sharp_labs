//Коллекции общего назначения: стек, очередь, динамический массив, хеш-таблица
//2.	Решить следующие задачи с использованием класса Queue: 
//    1.	Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты.За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках, зарплата которых меньше 10000, потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников.
//    2.	Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты. За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках младше 30 лет, потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников.
//    3.	Дан файл, содержащий информацию о студентах: фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии. За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о студентах, успешно сдавших сессию, потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников.
//    4.	Дан файл, содержащий информацию о студентах: фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии. За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о студентах, успешно обучающихся на 4 и 5, потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников.


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_Queue
{
    class Program
    {
        public static Queue<string> ReadChelov(StringBuilder path)
        {
            Queue<string> spisChelov = new Queue<string>();
            using (StreamReader spisFile = new StreamReader(path.ToString(), Encoding.Default))
                for (int i = 0; ; i++)
                {
                    string line = spisFile.ReadLine();
                    if (line == null) break;
                    spisChelov.Enqueue(line);
                }
            return spisChelov;
        }
    
        public static void Nayti(int i, int dopusk)
        {
            StringBuilder path = new StringBuilder(100);
            Console.WriteLine("1.\nВведите путь к текстовому файлу");
            path.Append(Console.ReadLine());
            //path.Append(@"D:\work\l6.txt");
            Queue<string> rab = ReadChelov(path);
            Queue<string> doZp = new Queue<string>();
            Queue<string> posleZp = new Queue<string>();
            while (rab.Count > 0)
            {
                string[] tempSplit = rab.Dequeue().Split(' ');


                if (tempSplit.Length <= 6)
                {
                    if (Convert.ToDouble(tempSplit[i]) < dopusk)
                        doZp.Enqueue(String.Join(" ", tempSplit));
                    else
                        posleZp.Enqueue(String.Join(" ", tempSplit));
                }
                else
                {
                    if ((Convert.ToDouble(tempSplit[4]) > dopusk) & (Convert.ToDouble(tempSplit[5]) > dopusk) & (Convert.ToDouble(tempSplit[6]) > dopusk))
                        doZp.Enqueue(String.Join(" ", tempSplit));
                    else
                        posleZp.Enqueue(String.Join(" ", tempSplit));
                }
            }
            Console.WriteLine("Люди до " + dopusk);
            while (doZp.Count > 0)
                Console.WriteLine(" " + doZp.Dequeue());
            Console.WriteLine("Люди после " + dopusk);
            while (posleZp.Count > 0)
                Console.WriteLine(" " + posleZp.Dequeue());
        }
        public static void PervoeZadanie()
        {
            Console.Write("1.\n");
            int i = 5;
            int dopusk = 10000;
            Nayti(i, dopusk);
        }
        public static void VtoroeZadanie()
        {
            Console.Write("2.\n");
            int i = 4;
            int dopusk = 30;
            Nayti(i, dopusk);
        }
        public static void TretieZadanie()
        {
            Console.Write("3.\n");
            int i = -1;
            int dopusk = 2;
            Nayti(i, dopusk);
        }
        public static void ChetvertoeZadanie()
        {
            Console.Write("4.\n");
            int i = -1;
            int dopusk = 3;
            Nayti(i, dopusk);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("1.Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты.За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках, зарплата которых меньше 10000, потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников.\n" +
                                "2.Дан файл, содержащий информацию о сотрудниках фирмы: фамилия, имя, отчество, пол, возраст, размер зарплаты.За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о сотрудниках младше 30 лет, потом данные об остальных сотрудниках, сохраняя исходный порядок в каждой группе сотрудников.\n" +
                                "3.Дан файл, содержащий информацию о студентах: фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии.За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о студентах, успешно сдавших сессию, потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников.\n" +
                                "4.Дан файл, содержащий информацию о студентах: фамилия, имя, отчество, номер группы, оценки по трем предметам текущей сессии.За один просмотр файла напечатать элементы файла в следующем порядке: сначала все данные о студентах, успешно обучающихся на 4 и 5, потом данные об остальных студентах, сохраняя исходный порядок в каждой группе сотрудников.\n" +
                                "Выберите одно из 4 заданий");
            int n = int.Parse(Console.ReadLine());

            switch (n)
            {
                case 1:
                    PervoeZadanie();
                    break;
                case 2:
                    VtoroeZadanie();
                    break;
                case 3:
                    TretieZadanie();
                    break;
                case 4:
                    ChetvertoeZadanie();
                    break;
                default:
                    Console.WriteLine("Нужно нажать цифру от 1 до 4");
                    break;
            }
            Console.ReadKey();
        }
    }
}

