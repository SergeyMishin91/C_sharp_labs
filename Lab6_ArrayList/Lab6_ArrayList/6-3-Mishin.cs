//Коллекции общего назначения: стек, очередь, динамический массив, хеш-таблица
//3.	Решить следующие задачи с использованием класса ArrayList: 
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

namespace Lab6_ArrayList
{
    class Program
    {
        public static ArrayList ReadChelov(StringBuilder path)
        {
            ArrayList spisChelov = new ArrayList();
            using (StreamReader spisFile = new StreamReader(path.ToString(), Encoding.Default))
                for (int i = 0; ; i++)
                {
                    string line = spisFile.ReadLine();
                    if (line == null) break;
                    spisChelov.Add(line);
                }
            return spisChelov;
        }
        public static void Nayti(int i, int dopusk)
        {
            StringBuilder path = new StringBuilder(100);
            Console.WriteLine("1.\nВведите путь к текстовому файлу");
            path.Append(Console.ReadLine());
            //path.Append(@"D:\work\l6.txt");
            ArrayList rab = ReadChelov(path);
            ArrayList doZp = new ArrayList();
            ArrayList posleZp = new ArrayList();
            foreach (string pp in rab)
            {
                string[] tempSplit = pp.Split(' ');


                if (tempSplit.Length <= 6)
                {
                    if (Convert.ToDouble(tempSplit[i]) < dopusk)
                        doZp.Add(String.Join(" ", tempSplit));
                    else
                        posleZp.Add(String.Join(" ", tempSplit));
                }
                else
                {
                    if ((Convert.ToDouble(tempSplit[4]) > dopusk) & (Convert.ToDouble(tempSplit[5]) > dopusk) & (Convert.ToDouble(tempSplit[6]) > dopusk))
                        doZp.Add(String.Join(" ", tempSplit));
                    else
                        posleZp.Add(String.Join(" ", tempSplit));
                }
            }
            Console.WriteLine("Люди до " + dopusk);
            foreach (string pp in doZp)
                Console.WriteLine(" " + pp);
            Console.WriteLine("Люди после " + dopusk);
            foreach (string pp in posleZp)
                Console.WriteLine(" " + pp);
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

    
        public static void Main(string[] args)
        {
            try
            {
                do
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
                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
