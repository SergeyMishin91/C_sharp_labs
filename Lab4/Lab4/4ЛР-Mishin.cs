using System;
using System.Collections;//берем ArrayList
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;//берем DirectoryIfo
using System.Text.RegularExpressions;//берем Regex
//1. Создать программу, которая ищет в указанном каталоге файлы, удовлетворяющие заданной маске,
// и дата последней модификации которых находится в указанном диапазоне. Поиск производится как в указанном каталоге,
// так и в его подкаталогах. Результаты поиска сбрасываются в файл отчета.
//2. Создать программу для поиска указанного текста в файлах, удовлетворяющих заданной маске, и замене этого
// тектса на другой указанный текст. Поиск производится как в указанном каталоге, так и в его подкаталогах.
//3. Создать программу для поиска по всему диску файлов и каталогов, удовлетворяющих заданной маске.
// Необходимо вывести найденную информацию на экран в компактном виде (с нумерацией объектов) и запросить у
// пользователя о дальнейших действиях. Варианты действий: удалить все найденное, удалить указанный файл (каталог),
// удалить диапазон файлов (каталогов).
namespace Lab_4
{
    class Lab4
    {
        public struct SlkDannie
        {
            public ArrayList flslk;
            public ArrayList dirslk;
        }
        public struct IshodnieDannie
        {
            public DateTime date1;//первая дата диапазона
            public DateTime date2;//вторая дата диапазона
            public Regex r;//маска для поиска
        }
        static void Zapis(SlkDannie slk, StringBuilder tpath)
        {
            StreamWriter resf;
            resf = new StreamWriter(tpath.Append("result_lab.log").ToString(), true, Encoding.Default);
            Console.WriteLine("Результаты сохранены в папке [{0}]", tpath);
            foreach (DirectoryInfo d in slk.dirslk)
            {
                Console.WriteLine("папка [{0}]", d.FullName);
                resf.WriteLine(d.FullName);
            }
            resf.WriteLine("++++++++files++++++++");
            foreach (FileInfo d in slk.flslk)
            {
                Console.WriteLine("файл  [{0}]", d.FullName);
                resf.WriteLine(d.FullName);
            }
            resf.Close();
        }
        static void Zamena(SlkDannie slk, IshodnieDannie isd)
        {
            isd = new IshodnieDannie();
            StringBuilder isdg;
            StringBuilder line = new StringBuilder(1000);
            Console.WriteLine("Введите маску для поиска в тексте");
            isd = VvodIshodnihDannih(2);
            Console.WriteLine("Введите текст для замены в тексте");
            isdg = new StringBuilder(Console.ReadLine(), 100);
            foreach (FileInfo f in slk.flslk)
            {
                Console.WriteLine("\nищем в файле [{0}]", f.FullName);
                line.Clear();
                StreamReader rd = new StreamReader(f.FullName, Encoding.Default);
                while (rd.ReadLine() != null)
                    line.Append(rd.ReadLine());
                line.Replace(isd.r.ToString(), isdg.ToString());
                rd.Close();
                StreamWriter wr = new StreamWriter(f.FullName, false, Encoding.Default);
                wr.WriteLine(line);
                wr.Close();
            }
            return;
        }
        static void Udalit(SlkDannie slk)
        {

            ConsoleKeyInfo hop;
            Console.WriteLine(@"Варианты действий:
-----------------------УДАЛЕНИЕ------------------------------------
    1 - удалить все найденное
    2 - удалить указанный файл (каталог)
    3 - удалить диапазон файлов (каталогов).");
            hop = Console.ReadKey();

            Console.WriteLine("   Test1  {0}", hop.Key);
            switch (int.Parse(hop.Key.ToString().Substring(hop.Key.ToString().Length - 1, 1)))
            {
                #region case 1:
                case 1:
                    {
                        foreach (FileInfo f in slk.flslk)
                        {
                            Console.WriteLine("Delete file   [{0}]", f.FullName);
                            f.Delete();
                        }

                        foreach (DirectoryInfo d in slk.dirslk)
                        {
                            VseUdalit(d);
                        }
                        break;
                    }
                #endregion
                #region case 2:
                case 2:
                    {
                        int strok = 0, kill = -1;//обнуляем выделение что б было вверху

                        do
                        {
                            Console.Clear();
                            int num = 0;//вывод содержимого папки и подсветка выделенного
                            foreach (DirectoryInfo d in slk.dirslk)
                            {
                                if (kill == num)
                                {
                                    Console.WriteLine("Delete:{0}", d);
                                    VseUdalit(d); kill = -1; return;
                                }
                                if (strok == num)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("{0,2}. {1}", ++num, d);
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                    Console.WriteLine("{0,2}. {1}", ++num, d);
                            }
                            foreach (FileInfo f in slk.flslk)
                            {
                                if (kill == num)
                                {
                                    Console.WriteLine("Delete:{0}", f);
                                    f.Delete(); kill = -1; return;
                                }
                                if (strok == num)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("{0,2}. {1}", ++num, f);
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                    Console.WriteLine("{0,2}. {1}", ++num, f);
                            }
                            Console.WriteLine("Выберите дейтсвие\n------------------------------------\n<Вверх>,<Вниз> - перемещение\n<Delete> - удалить <Esc> - выйти вначало");
                            hop = Console.ReadKey();
                            if (hop.Key.ToString() == "DownArrow")
                            { strok++; if (strok > num) strok = 0; }
                            else if (hop.Key.ToString() == "UpArrow")
                            { strok--; if (strok < 0) strok = num; }
                            if (hop.Key == ConsoleKey.Escape) break;//-----------------------------------выход
                            if (hop.Key.ToString() == "Delete") kill = strok;
                        } while (true);/**/
                        break;
                    }
                #endregion
                #region case 3:
                case 3:
                    {
                        int strok = 0;//обнуляем выделение что б было вверху
                        SlkDannie var = new SlkDannie();
                        int[] count = new int[50];
                        var.flslk = new ArrayList();
                        var.dirslk = new ArrayList();
                        do
                        {
                            Console.Clear();
                            int num = 0;//вывод содержимого папки и подсветка выделенного
                            foreach (DirectoryInfo d in slk.dirslk)
                                if (strok == num | count[num] == 1)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("{0,2}. [{1}]", ++num, d.FullName);
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                    Console.WriteLine("{0,2}. [{1}]", ++num, d.FullName);
                            foreach (FileInfo f in slk.flslk)
                                if (strok == num | count[num] == 1)
                                {
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.WriteLine("{0,2}. {1}", ++num, f.FullName);
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                else
                                    Console.WriteLine("{0,2}. {1}", ++num, f.FullName);
                            Console.WriteLine("Выберите дейтсвие\n------------------------------------\n<Вверх>,<Вниз> - перемещение\n<Delete> - удалить выделенное <Esc> - выйти вначало <Пробел> - выделить");
                            hop = Console.ReadKey();
                            switch (hop.Key)
                            {
                                case ConsoleKey.DownArrow:
                                    { strok++; if (strok > num) strok = 0; break; }
                                case ConsoleKey.UpArrow:
                                    { strok--; if (strok < 0) strok = num; break; }
                                case ConsoleKey.Escape: return;//-----------------------------------выход
                                case ConsoleKey.Spacebar:
                                    {
                                        count[strok] = (count[strok] != 1) ? 1 : 0;
                                        break;
                                    }
                                case ConsoleKey.Delete:
                                    {
                                        int i = slk.dirslk.Count, step = 0;
                                        for (int j = 0; j < i; j++)//перегоняем выбранные элементы в массив под удаление
                                            if (count[j] == 0)//только для  не выбранныхslk.dirslk.RemoveAt(j-step); step++;  //для папок
                                                step = 0;
                                        for (int j = 0; j < num - i; j++)//перегоняем выбранные элементы в массив под удаление
                                            if (count[j + i] == 0)//только для  не выбранных
                                                slk.flslk.RemoveAt(j - step); step++;//для файлов
                                        foreach (FileInfo f in slk.flslk)
                                        {
                                            Console.WriteLine("Delete file   [{0}]", f.FullName);
                                            f.Delete();
                                        }
                                        foreach (DirectoryInfo d in slk.dirslk)
                                        {
                                            Console.WriteLine("Delete folder   [{0}]", d.FullName);
                                            VseUdalit(d);
                                        }
                                        Console.ReadKey();
                                        break;
                                    }
                            }
                        } while (true);
                    }
                #endregion
                default: break;
            }
            return;
        }
        static void VseUdalit(DirectoryInfo dirslk)
        {
            try
            {
                if (dirslk.GetFiles().Length > 0)
                    foreach (FileInfo f in dirslk.GetFiles())
                    {
                        Console.WriteLine("Delete file   [{0}]", f.FullName);
                        f.Delete();
                    }
            }
            catch { return; }
            if (dirslk.GetDirectories().Length != 0)
                foreach (DirectoryInfo d in dirslk.GetDirectories())//мы в ввыбраном каталоге indir.GetDirectories()[i]
                    if ((d.GetDirectories().Length > 0) | (d.GetFiles().Length > -1))//если длинна имен подкаталогов больше нуля вызываем рекурсию
                        VseUdalit(d);
            Console.WriteLine("Delete folder [{0}]", dirslk.FullName);
            dirslk.Delete();
            return;
        }
        static IshodnieDannie VvodIshodnihDannih(int c)
        {//[c=1 -вводим isd.r isd.date1 isd.date2] [c=2 вводим isd.r]
            IshodnieDannie isd = new IshodnieDannie();
            ConsoleKeyInfo hop1, hop2;//для ввода цифр
            if (c >= 1)
            {
                #region --------------------------------------ВВОД МАСКИ--------------------------
                Console.Write("\nВведите маску\n");
                if (c == 1) Console.Write("(допускается использовать [?]- для одного символа и [*]- для нескольких)");
                Console.WriteLine("---------------------------------\n\n---------------------------------");
                Console.CursorTop = Console.CursorTop - 2;//перемещение курсора а значит места вывода на консоль
                StringBuilder mask = new StringBuilder(Console.ReadLine(), 100);
                mask.Replace("*", ".").Replace("?", ".*");//.Append('\0');
                Console.WriteLine("\n[{0}]", mask);
                isd.r = new Regex(mask.ToString(), RegexOptions.None);
                #endregion
            }
            if (c == 1)
            {
                Console.WriteLine("Желаете добавить в фильтр дату?(Y/any key)");
                ConsoleKeyInfo y = Console.ReadKey();
                if (y.Key.ToString() != "Y")
                {
                    isd.date1 = new DateTime(2000, 12, 31, 23, 59, 00);
                    isd.date2 = new DateTime(2015, 12, 31, 23, 59, 00);
                }
                else
                {
                    #region --------------------------------------ВВОД 1 ДАТЫ------------------
                    Console.CursorTop = Console.CursorTop + 2;
                    Console.WriteLine("\n\nВведите первую дату в формате дд.мм.гггг чч:мм:сс ,\n последней модификации файлов диапазоне(дд.мм.гггг-дд.мм.гггг)\n---------------------------------\n\n---------------------------------");
                    Console.CursorTop = Console.CursorTop - 2;//перемещение курсора а значит места вывода на консоль
                    int[] std = new int[5];//массив для ввода чисел
                md:
                    try
                    {
                        Console.Write("Введите дд.мм.гг чч:мм:00");
                        Console.CursorLeft = Console.CursorLeft - 18;
                        for (int i = 0; i < 5; i++)//цикл ввода первой даты
                        {
                            Console.CursorLeft = Console.CursorLeft + 1;//задаем шаг что бы оставались "." и ":"
                            hop1 = Console.ReadKey(); hop2 = Console.ReadKey();//подсекаем ввод с клавиатуры
                            std[i] = int.Parse(hop1.Key.ToString().Substring(hop1.Key.ToString().Length - 1, 1)) * 10 + int.Parse(hop2.Key.ToString().Substring(hop2.Key.ToString().Length - 1, 1));// геморные преобразования для ввода без нажатия клавиши "Enter"
                        }
                        if (std[0] == 0 | std[1] == 0) throw new Exception("\n\tДень и месяц не могут быть нулевыми");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n\t" + ex.Message);
                        Console.WriteLine("\tОшибка ввода, повторите ввод");
                        goto md;// для повторного ввода при ошибке
                    }
                    isd.date1 = new DateTime((std[2] + 2000), std[1], std[0], std[3], std[4], 00);// создаем формат для даты
                    #endregion
                    #region --------------------------------------ВВОД 2 ДАТЫ------------------------
                    Console.CursorLeft = Console.CursorLeft + 2;
                    Console.WriteLine("\n\nВведите вторую дату в формате дд.мм.гггг чч:мм:сс ,\n последней модификации файлов диапазоне(" + isd.date1 + "-дд.мм.гггг)\n---------------------------------\n\n---------------------------------");
                    Console.CursorTop = Console.CursorTop - 2;//перемещение курсора а значит места вывода на консоль
                md2:
                    try
                    {
                        Console.Write("Введите дд.мм.гг чч:мм:00");
                        Console.CursorLeft = Console.CursorLeft - 18;
                        for (int i = 0; i < 5; i++)
                        {
                            Console.CursorLeft = Console.CursorLeft + 1;
                            hop1 = Console.ReadKey(); hop2 = Console.ReadKey();
                            std[i] = int.Parse(hop1.Key.ToString().Substring(hop1.Key.ToString().Length - 1, 1)) * 10 + int.Parse(hop2.Key.ToString().Substring(hop2.Key.ToString().Length - 1, 1));// геморные преобразования для ввода без нажатия клавиши "Enter"
                        }
                        if (std[0] == 0 | std[1] == 0 | std[3] >= 24 | std[4] >= 60) throw new Exception("\n\tДень и месяц не могут быть нулевыми, а время не больше 23:59");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n\t" + ex.Message);
                        Console.WriteLine("\n\tОшибка ввода, повторите ввод");
                        goto md2;// для повторного ввода при ошибке
                    }
                    isd.date2 = new DateTime((std[2] + 2000), std[1], std[0], std[3], std[4], 00);
                    #endregion
                    Console.CursorTop = Console.CursorTop + 2;
                    Console.Write("\n Интервал с " + isd.date1 + " по " + isd.date2);
                    if (isd.date1 > isd.date2) throw new Exception("Получился отрицательный интервал!");//не будем вычислять в случае с неверным интервалом
                }
            }
            return isd;
        }
        static SlkDannie Poisk(DirectoryInfo indir, StringBuilder path, SlkDannie slk)
        {
            IshodnieDannie isd;
            ConsoleKeyInfo hop;
            Console.WriteLine(@"
-------------------------------------------------------------------
            Выберите действие:
    1 - Найти в указанном каталоге файлы, удовлетворяющие заданной маске,и дата последней модификации которых находится в указанном диапазоне.
    2 - Найти указанный текст в файлах, удовлетворяющих заданной маске, и замене этого тектса на другой указанный текст.
    3 - Найти по всему диску файлы и каталоги, удовлетворяющие заданной маске.");
            hop = Console.ReadKey();
            Console.Clear();
            try
            {
                switch (int.Parse(hop.Key.ToString().Substring(hop.Key.ToString().Length - 1, 1)))
                {
                    case 1:
                        {
                            isd = VvodIshodnihDannih(1);
                            Zapis(SubPoisk(indir, path, slk, isd), path);
                            return slk;//?
                        }
                    case 2:
                        {
                            isd = VvodIshodnihDannih(1);
                            SubPoisk(indir, path, slk, isd);
                            Zamena(slk, isd);
                            return slk;
                        }
                    case 3:
                        {
                            isd = VvodIshodnihDannih(1);
                            Udalit(SubPoisk(indir, path, slk, isd));
                            return slk;
                        }
                    default: break;
                }
            }
            catch (Exception e) { Console.Error.WriteLine(e); ; }
            return slk;
        }
        static SlkDannie SubPoisk(DirectoryInfo indir, StringBuilder path, SlkDannie slk, IshodnieDannie isd)

        {
            StringBuilder tpath;
            foreach (FileInfo f in indir.GetFiles())
                if ((isd.date1 < f.LastWriteTime) & (isd.date2 > f.LastWriteTime) & (isd.r.IsMatch(f.Name)))
                    slk.flslk.Add(f);
            if (indir.GetDirectories().Length != 0)
                foreach (DirectoryInfo d in indir.GetDirectories())//мы в ввыбраном каталоге indir.GetDirectories()[i]
                {
                    if ((isd.date1 < d.LastWriteTime) & (isd.date2 > d.LastWriteTime) & (isd.r.IsMatch(d.Name)))
                        slk.dirslk.Add(d);
                    if ((d.GetDirectories().Length > 0) | (d.GetFiles().Length > -1))//если длинна имен подкаталогов больше нуля вызываем рекурсию
                    {
                        tpath = new StringBuilder(50);//пока не придумал как обойти что б путь не перебивался при возврате из рекурсии
                        tpath.Clear().Append(path.ToString());
                        SubPoisk(d, tpath, slk, isd);
                    }
                }/**/
            return slk;
        }
        static StringBuilder SelEnter(DirectoryInfo indir, StringBuilder path, int strok)
        {
            if (strok == 0)//выбор подняться вверх по каталогу
            {
                if (path.Length <= 3) return path;//если выше перейтинельзя ничего не меняем
                path.Clear();//чистим строку с путем
                path.Append(indir.Parent.FullName);//забиваем заново
                return path;// возвращаемся и строим заново
            }
            if (indir.GetDirectories().Length < strok)//проверка что б не вызывать фаайлы
            {
                Console.WriteLine("Это файл!");
                Console.ReadKey();
                return path;
            }
            path.Append(indir.GetDirectories()[strok - 1]).Append(@"\");//изменяем путь если был энтер нажат
            return path;
        }
        static int Print(DirectoryInfo[] dir, FileInfo[] fl, int col)
        {
            Console.Clear();
            int i = 0;
            if (i == col)
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(" ..");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else Console.WriteLine(" ..");
            i++;
            if (dir.Length != 0)
                foreach (DirectoryInfo d in dir)
                {
                    if (i == col)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("{0,2}. [{1}]", i++, d.Name);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.WriteLine("{0,2}. [{1}]", i++, d.Name);
                }
            if (fl.Length != 0)
                foreach (FileInfo d in fl)
                {
                    if (i == col)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("{0,2}. {1}", i++, d.Name);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else Console.WriteLine("{0,2}. {1}", i++, d.Name);
                }
            Console.WriteLine("\n-----------------------------------\n<Ctrl+F> поиск в текущeй папке\n<Вверх>,<Вниз> - перемещение\n<Enter> Выбор\n<Escape> Выход");
            return i - 1;
        }
        static void Main(string[] args)
        {
            int num, strok;
            ConsoleKeyInfo hop;
            SlkDannie vihod = new SlkDannie();
            SlkDannie slk = new SlkDannie();
            slk.flslk = new ArrayList();
            slk.dirslk = new ArrayList();
            //Console.WriteLine("Введите путь");
            //StringBuilder path = new StringBuilder(Console.ReadLine(),100);
            /*test*/
            StringBuilder path = new StringBuilder(@"d:\", 100);//для основного пути
            StringBuilder tpath = new StringBuilder(100);//бэкап пути, для возврата после поиска в тот же каталог
            do
            {
                DirectoryInfo indir = new DirectoryInfo(path.ToString());
                strok = 0;//обнуляем выделение что б было вверху
                do
                {
                    num = Print(indir.GetDirectories(), indir.GetFiles(), strok);//вывод содержимого папки и подсветка выделенного
                    hop = Console.ReadKey();
                    if (hop.Key.ToString() == "DownArrow")
                    { strok++; if (strok > num) strok = 0; }
                    else if (hop.Key.ToString() == "UpArrow")
                    { strok--; if (strok < 0) strok = num; }
                    if (hop.Key == ConsoleKey.Escape) Environment.Exit(0);//-----------------------------------выход из прораммы
                } while (hop.Key.ToString() != "Enter" & !(hop.Key.ToString() == "F" & ((hop.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control)));
                if (hop.Key.ToString() == "Enter")
                    path = SelEnter(indir, path, strok);
                if (hop.Key.ToString() == "F" & ((hop.Modifiers & ConsoleModifiers.Control) == ConsoleModifiers.Control))
                    if ((strok != 0) & (indir.GetDirectories().Length >= strok)) //в пункте "вверх" или типе файл нельзя искать
                    {
                        //tpath буферная строка, что бы не затирался основной адрес и можно было вернуться в место поиска
                        vihod = Poisk(indir.GetDirectories()[strok - 1], tpath.Clear().Append(path.ToString()), slk);
                        // Console.WriteLine("\t\t Результаты поиска: в [{0}]\n-----------------------------------\n", indir.GetDirectories()[strok - 1]);
                        Console.ReadKey();
                    }
            } while (true);
            //Console.WriteLine("End");
            //Console.ReadKey();
        }
    }
}