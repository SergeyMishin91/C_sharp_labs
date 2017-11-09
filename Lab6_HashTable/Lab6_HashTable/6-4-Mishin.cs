//Коллекции общего назначения: стек, очередь, динамический массив, хеш-таблица
//4.	Решить задачу, используя класс HashTable: реализовать простейший каталог музыкальных компакт-дисков, который позволяет: 
    //1.	Добавлять и удалять диски.
    //2.	Добавлять и удалять песни. 
    //3.	Просматривать содержимое целого каталога и каждого диска в отдельности.
    //4.	Осуществлять поиск всех записей заданного исполнителя по всему каталогу.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6_HashTable
{
    class Program
    {
        public static Hashtable CreateList()
        {
            Hashtable catalog = new Hashtable(100);

            catalog.Add("disc1", new Hashtable());
            {
                ((Hashtable)catalog["disc1"]).Add("Singer1-t1", "track1");
                ((Hashtable)catalog["disc1"]).Add("Singer1-t2", "track2");
                ((Hashtable)catalog["disc1"]).Add("Singer1-t3", "track3");
                ((Hashtable)catalog["disc1"]).Add("Singer1-t4", "track4");
                ((Hashtable)catalog["disc1"]).Add("Singer1-t5", "track5");
                ((Hashtable)catalog["disc1"]).Add("Singer1-t6", "track6");
                ((Hashtable)catalog["disc1"]).Add("Singer1-t7", "track7");
            }

            catalog.Add("disc2", new Hashtable());
            {
                ((Hashtable)catalog["disc2"]).Add("Singer2-t1", "track1");
                ((Hashtable)catalog["disc2"]).Add("Singer2-t2", "track2");
                ((Hashtable)catalog["disc2"]).Add("Singer2-t3", "track3");
                ((Hashtable)catalog["disc2"]).Add("Singer2-t4", "track4");
                ((Hashtable)catalog["disc2"]).Add("Singer2-t5", "track5");
                ((Hashtable)catalog["disc2"]).Add("Singer2-t6", "track6");
                ((Hashtable)catalog["disc2"]).Add("Singer2-t7", "track7");
                ((Hashtable)catalog["disc2"]).Add("Singer2-t8", "track8");
            }

            catalog.Add("disc3", new Hashtable());
            {
                ((Hashtable)catalog["disc3"]).Add("Singer3-t1", "track1");
                ((Hashtable)catalog["disc3"]).Add("Singer3-t2", "track2");
                ((Hashtable)catalog["disc3"]).Add("Singer3-t3", "track3");
                ((Hashtable)catalog["disc3"]).Add("Singer3-t4", "track4");
                ((Hashtable)catalog["disc3"]).Add("Singer3-t5", "track5");
                ((Hashtable)catalog["disc3"]).Add("Singer3-t6", "track6");
            }
            return catalog;
        }
        public static Hashtable AddSong(Hashtable cd)
        {
            int cdKol, songKol;
            Console.WriteLine("Введите количество дисков");
            cdKol = int.Parse(Console.ReadLine());
            for (int i = 0; i < cdKol; i++)
            {
                StringBuilder cdName = new StringBuilder(50);
                Hashtable song = new Hashtable();
                Console.WriteLine("Введите название нового диска");
                cdName.Append(Console.ReadLine());
                Console.WriteLine("Введите количество песен в этом диске");
                songKol = int.Parse(Console.ReadLine());
                for (int j = 0; j < songKol; j++)
                {
                    StringBuilder songName = new StringBuilder(50);
                    Console.WriteLine("Введите название песни");
                    songName.Append(Console.ReadLine());
                    song.Add((cdName + "***" + songName), songName);
                }
                cd.Add(cdName, song);
            }
            return cd;
        }
        public static string Choise(Hashtable catalog, int choise)
        {

            #region  костыль для обращения к ключу из Hashtable
            int iTemp = 0;
            StringBuilder sTemp = new StringBuilder();
            foreach (DictionaryEntry b in catalog)
            {
                iTemp++;
                sTemp.Clear().Append(b.Key);
                if (iTemp >= choise) break;
            }
            #endregion
            return sTemp.ToString();
        }
        public static void ShowList(Hashtable catalog)
        {
            int count = 0;
            Console.WriteLine("Список дисков: \n");
            foreach (DictionaryEntry a in catalog)
            {
                Console.WriteLine(count++ + ".   " + a.Key + "    " + a.Value);
            }
            Console.WriteLine("Нажмите любую клавишу что бы продолжить");
            Console.ReadKey();
            Console.WriteLine("----------------------------------------------------");
        }
        public static Hashtable FindSong(Hashtable catalog, string mask)
        {
            Hashtable result = new Hashtable();
            int u = 0;
            foreach (DictionaryEntry pp in catalog)
            {
                foreach (DictionaryEntry ppp in (Hashtable)pp.Value)
                {

                    //Console.WriteLine("+++++++" + (ppp.Key.ToString()).Substring(0,mask.Length));
                    if (mask.Contains((ppp.Key.ToString()).Substring(0, mask.Length)))
                    {
                        result.Add(u++, ppp.Value);
                    }
                }
            }


            return result;
        }
        static void Main(string[] args)
        {
            Hashtable catalog = CreateList();
            try
            {
                do
                {
                    ShowList(catalog);
                    Console.WriteLine("Выберите ноомер диска");
                    int choise = int.Parse(Console.ReadLine());
                    Console.WriteLine("Выберите действие:\n1-открыть\n2-удалить\n3-добавить\n4-поиск\n0-выход\n...другая клавиша");
                    int vibor = int.Parse(Console.ReadLine());

                    switch (vibor)
                    {
                        case 1:
                            {
                                Hashtable inTemp = (Hashtable)catalog[Choise(catalog, choise)];
                                ShowList(inTemp);
                                break;
                            }
                        case 2:
                            {
                                catalog.Remove(Choise(catalog, choise).ToString());
                                break;
                            }
                        case 3:
                            {
                                catalog = AddSong(catalog);
                                break;

                            }
                        case 4:
                            {
                                StringBuilder mask = new StringBuilder();
                                Console.WriteLine("Введите имя исполнителя");
                                mask.Append(Console.ReadLine());
                                ShowList(FindSong(catalog, mask.ToString()));
                                break;
                            }
                        case 0:
                            {
                                Environment.Exit(0);
                                break;
                            }
                        default:
                            break;
                    }
                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            Console.ReadKey();
        }
    }
}
