
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//	Мишин Сергей Игоревич
//	50326-2 
//	Вариант 9
//	
//Предметная область: Интернет оператор.Провайдер имеет различные тарифы доступа в Интернет за 1Мбайт в зависимости от 
//величины абонентской платы.Информационная система провайдера хранит данные о клиентах.
//Все клиенты имеют скидки. Одни клиенты имеют скидки в процентах, другие имеют фиксированную скидку.
//Система должна позволять выполнять следующие задачи:
//•	ввод тарифов;
//•	регистрация пользователя;
//•	ввод данных о потребленном трафике для конкретного пользователя;
//•	подсчет общей стоимости реализованного трафика;
//•	поиск клиента, заплатившего наибольшую стоимость за услуги.
//Добавить обработку исключительных ситуаций:
//•	имя клиента более 10 символов
//•	стоимость с учетом скидки отрицательна.
//Добавить перегруженный унарный оператор для увеличения величины тарифа.
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


using System;										//Namespace contains fundamental classes and base classes that define commonly-used value and reference data types, events and event handlers, interfaces, attributes, and processing exceptions.
using System.IO;									//Namespace contains types that allow reading and writing to files and data streams, and types that provide basic file and directory support.
using System.Collections.Generic;                   //Namespace contains interfaces and classes that define generic collections, which allow users to create strongly typed collections that provide better type safety and performance than non-generic strongly typed collections.
using System.Text;

class Tarifs                                        //Класс для работы с тарифами 
{                                       
    public struct Tarif
    {                                   //Структура хранящая информацию о тарифе
        public string name;                         //Наименование тарифа
        public ushort speed;                        //Скорость траффика
        public double price;                     //Стоимость в месяц за пользование Интернетом

        public Tarif(string Name, ushort Speed, double Price)  //Конструктор для структуры хранящей инфо о тарифе
        {
            this.name = Name;
            this.speed = Speed;
            this.price = Price;
        }
        public override string ToString()
        {           //Метод приводящий информацию структуры в строку для вывода в консоль
            return " " + name + "\0\t" + speed + " Мбит/с" + "\t" + price + " руб.";
        }

        public string ToFile()
        {                   //Метод приводящий информацию структуры в строку для записи в файл
            return name + " " + speed + " " + price;
        }

        public static Tarif operator +(Tarif t)  //перегруженный унарный оператор для увеличения цены тарифа
        {
            Tarif tmp = new Tarif();             // создаем временный экземпляр структуры Тариф
            Console.Write("\n\tНа сколько процентов нужно увеличить стоимость тарифа \"{0}\"? ", t.name);   //Вывод сообщения на экран консоли
            double n = Convert.ToDouble(Console.ReadLine());                //объявление переменной для указания процента увеличения цены тарифа
            tmp.name = t.name;                                              //задаем значение наименованию тарифа
            tmp.speed = t.speed;                                            //задаем значение скорости тарифа
            tmp.price = t.price + n * 0.001;                                //увеличиваем цену тарифа на указанный процент
            return tmp;                                                     //возвращаем измененный экземпляр структуры Тариф
         }
    }

    public static List<Object> GetTarifs()
    {       //Метод получающий информацию из файла возвращиющий её в виде нумерованного списка структур
        List<Object> tarifs = new List<Object>();   //Объявление нумерованного списка
        Tarif new_tarif = new Tarif();              //Объявление объекта структуры
        try
        {                                       //Обработка исключений
            string[] lines = File.ReadAllText("Tarifs.txt", Encoding.GetEncoding(1251)).Split('\n');        //Считывание текста в массив строк из файла 
            foreach (string line in lines)
            {                                       //Перечисление массива строк
                if (line.Equals(string.Empty)) continue;                             //Если строка пустая, то пропустить эту строку
                string[] info = line.Split(' ');                                //Разбить текущую стрку на массив подстрок по пробелам
                new_tarif.name = info[0];                                       //Присвоение элементов подстрок полям структуры
                new_tarif.speed = ushort.Parse(info[1]);
                new_tarif.price = double.Parse(info[2]);
                tarifs.Add(new_tarif);                                          //Добавление структуры в нумерованный список
            }
        }
        catch (Exception)
        {                                                       //Обработка пойманых ошибок
            Console.WriteLine("\n\tОшибка чтения данных из файла");                //Вывод сообщения об ошибке
            Console.ReadLine();                                                 //Пауза
        }
        return tarifs;                                                          //Возвращение нумерованного списка
    }
    public static void ShowAll()
    {                   //Метод вывода всех доступных тарифов в консоль
        Console.Clear();                            //Очиска экрана консоли
        Console.WriteLine("\n\tСписок тарифов для подключения: ");
        Console.WriteLine("\n\t---Тариф--------Скорость---------Цена---\n");
        ushort i = 1;                                   //Инициализация нумератора
        List<Object> tarifs = GetTarifs();          //Получение списка всех тарифов
        foreach (Tarif t in tarifs)
        {               //Перечисление тарифов
            Console.WriteLine("\t" + i++ + "." + t.ToString());    //Вывод текущего тарифа
        }
    }
    public static void Add()
    {                       //Метод добавления нового тарифа
        try
        {                                       //Обработка исключений
            while (true)
            {                           //Бесконечный цикл ввода новых тарифов
                Tarif new_tarif;                    //Декларация новой структуры
                Console.Clear();                    //Очиска экрана консоли
                Console.Write("\n\tДобавление нового тарифа" +          //Сообщение в консоль
                              "\n\n\tВведите название тарифа - ");
                new_tarif.name = Console.ReadLine();                    //Присвоение введённой пользователем строки в поле name
                if (new_tarif.name == string.Empty) break;              //Если строка пустая то прервать цикл ввода
                if (new_tarif.name.Length > 10) throw new ArgumentException("Наименование тарифа не может превышать 10 символов");  //Если длинна введённых строк фамилии и имени менее 10 символов, то сгеннерировать исключение
                Console.Write("\tВведите скорость приема/передачи (Мбит/с) - ");              //Сообщение а консоль
                new_tarif.speed = ushort.Parse(Console.ReadLine());     //Присвоить введённое пользователем значение в поле speed приведя его к типу ushort
                Console.Write("\tВведите стоимость - ");          //Сообщение в консоль
                new_tarif.price = double.Parse(Console.ReadLine());  //Присвоить введённое пользователем значение в поле price приведя его к типу ushort
                
                StreamWriter file = new StreamWriter("Tarifs.txt", true);//Инициализировать поток записи в файл текстовой информации
                file.WriteLine(new_tarif.ToFile());                     //Записать в файл структуру в виде строки
                file.Close();                                           //Закрыть поток записи в файл
                Console.WriteLine("\n\tНовый тариф добавлен в базу");   //Сообщение в консоль
                Console.ReadKey();                                      //Пауза до нажатия любой клавиши
                break;
            }
        }
        catch (Exception e)
        {                               //Обработка пойманных ошибок
            Console.WriteLine("\n\t" + e.Message);          //Вывоб сообщения об ошибке в консоль
            Console.ReadKey();                              //Пауза до нажатия любой клавиши
        }
    }
    public static void Edit()
    {                       //Метод редактирования тарифов
        ShowAll();                                          //Вызов метода вывод в консоль всех тарифов
        List<Object> tarifs = GetTarifs();                  //Получение нумерованного списка всех тарифов
        Console.Write("\n\tВведите номер тарифа который необходимо редактировать - ");
        try
        {                                               //Обработка исключений
            ushort edit = ushort.Parse(Console.ReadLine()); //Приведение введеённой пользователем строки к типу ushort и присвоение переменной edit
            Tarif buffer = (Tarif)tarifs[edit - 1];         //Копирование в буфер из списка структуры, которую необходимо редактировать
            Console.Clear();                                //Очиска экрана консоли
            Console.Write("\n" + buffer.ToString() + "\n\n\tКакое поле вы хотить изменить?\n\t");  //Предложение пользователю выбрать поле для редактирования  
            int top = Console.CursorTop;                    //Сохранение позиции курсора в консоли по вертикали
            int left = Console.CursorLeft;                  //и по горизонтали
            for (int q = 1; q < 4; q++)
            {                       //Пятикратный цикл со счётчиком q
                Console.CursorTop = q;                      //Передвижение курсора в строку под номером q
                Console.CursorLeft = 3;                     //и на третью позицию слева
                Console.Write(q + " - ");                   //Вывод значения q и тире        
            }
            Console.CursorTop = top;                        //Возвращение курсора в исходное положение повертикали
            Console.CursorLeft = left;                      //И по горизонтали

            switch (Console.ReadLine())
            {                   //Считывается строка введённая в консоль и вызывается соответствующий метод реализующая выбранный пункт меню
                case "1":
                    Console.Write("\n\tВведите изменённое название тарифа - "); //Сообщение в консоль
                    buffer.name = Console.ReadLine();                           //Присвоение нового значения полю name 
                    break;
                case "2":
                    Console.Write("\n\tВведите изменённое значение скорости приема/передачи - ");   //Сообщение в консоль	
                    buffer.speed = ushort.Parse(Console.ReadLine());            //Присвоение нового значения полю speed
                    break;
                case "3":
                    Console.Write("\n\tВведите изменённую стоимость - ");//Сообщение в консоль
                    buffer.price = double.Parse(Console.ReadLine());         //Присвоение нового значения полю price
                    break;
                default:
                    Console.WriteLine("\n\tТакого поля не существует");         //Сообщение в консоль о том что введено несуществующие значение
                    break;
            }
            tarifs.RemoveAt(edit - 1);                          //Удалить редактируемую структуру из списка								
            tarifs.Insert(edit - 1, buffer);                        //Скопировать в список буферную структуру

            StreamWriter file = new StreamWriter("Tarifs.txt"); //Создать поток записи в файл текстовой информации
            foreach (Tarif t in tarifs)
            {                       //Перечисление списка тарифов
                file.WriteLine(t.ToFile());                     //Запись в файл стекущую структуру в виде строки
            }
            file.Close();                                       //Закрыть поток записи в файл
            Console.WriteLine("\n\tТариф успешно изменён");     //Сообщение в консоль
        }
        catch (Exception e)
        {                                   //Обработка пойманных ошибок
            Console.Write("\n\t" + e.Message);                  //Вывод сообщения об ошибке
            Console.ReadLine();                                 //Пауза
        }
    }
    public static void Delete()
    {                   //Метод удаления тарифов
        ShowAll();                                              //Вызов метода вывод в консоль всех тарифов
        Console.Write("\n\tВведите номер тарифа который необходимо удалить - ");        //Сообщение в консоль
        try
        {                                                   //Обработка исключений
            ushort del = ushort.Parse(Console.ReadLine());      //Присвоение переменной del значения введённого пользователем
            List<Object> tarifs = GetTarifs();                  //Получение нуменрованного списка тарифов
            tarifs.RemoveAt(del - 1);                               //Удаление тарифва из списком под номером del-1

            StreamWriter file = new StreamWriter("Tarifs.txt"); //Создание потока перезаписывающего файл
            foreach (Tarif t in tarifs)
            {                       //Перечисление тарифов
                file.WriteLine(t.ToFile());                     //Запись строки тарифа в файл
            }
            file.Close();                                       //Закрыть поток записи файла
            Console.WriteLine("\n\tТариф успешно удалён");      //Сообщение в консоль
        }
        catch (Exception e)
        {                                   //Обработка пойманных ошибок
            Console.WriteLine("\n\t" + e.Message);              //Вывести сообщение об ошибке в консоль
        }
        Console.ReadKey();                                      //Пауза до нажатия любой клавиши	
    }
    public static void ChangeOrders() //Метод изменеие всех цен
    {              
        List<Object> tarifs = GetTarifs();          //Получение нуменрованного списка тарифов
        Console.Clear();                            //Очистка экрана консоли
        for (ushort i = 0; i < tarifs.Count; i++)   //Перечисление структур нумерованного списка
        {
            Tarif t = (Tarif)tarifs[i];
            t = +t;                                 //использование переопределенного унарного оператора
            tarifs.RemoveAt(i);                     //Удаляем структуру из списка по текущему индексу
            tarifs.Insert(i, t);                    //Вставляем буферную структуру в список по текущему индексу
        }

        StreamWriter file = new StreamWriter("Tarifs.txt"); //Создание потока перезаписывающего файл
        foreach (Tarif t in tarifs)
        {                       //Перечисление тарифов
            file.WriteLine(t.ToFile());                     //Запись строки тарифа в файл
        }
        file.Close();                                       //Закрыть поток записи файла
        Console.WriteLine("\n\tТарифы успешно изменены ");  //Сообщение в консоль			
    }

}
class Coupons   //Класс для работы со скидочными купонами
{                                       
    public struct coupon                //Структура для хранения информации о купоне
    {                                   	
        public string name;                         //Поле названия купона
        public ushort value;                        //Поле значения скидки
        public char type;                           //Поле типа скидки (проценты или единицы)
        public string description;                  //Описание скидки

        public override string ToString()
        {           //Метод приводящий информацию структуры в строку для вывода в консоль
            return name +
                   "\0\t" + value + type +
                   "\0\0\0\0\0\t" + description;
        }

        public string ToFile()
        {                   //Метод приводящий информацию структуры в строку для записи в файл
            return name + " " + value + " " + type + " - " + description;
        }
    }

    public static List<Object> GetCoupons()
    {                       //Метод считывания информации из файла в нумерованный список 
        List<Object> coupons = new List<Object>();                  //Инициализация нового нумерованного списка купонов	
        coupon new_coupon = new coupon();                           //Инициальзация новой структуры купон
        try
        {                                                       //Обработка исключительных ситуаций
            string[] lines = File.ReadAllText("Coupons.txt").Split('\n');   //Считывание текста из файла в массив строк
            foreach (string line in lines)
            {                                   //Перечисление массива строк
                if (line == string.Empty) continue;                         //Если строка пуста то пропустить
                string[] parts = line.Split('-');                           //Разбить текущую строку на подстроки по тире
                new_coupon.description = parts[1];                          //Присвоить описанию купона вторую подстроку
                string[] info = parts[0].Split(' ');                        //Разбить первую подстроку на подстроки по пробелам
                new_coupon.name = info[0];                                  //Первая подстрока это название купона
                new_coupon.value = ushort.Parse(info[1]);                   //Вторая подстрока это значение купона
                new_coupon.type = char.Parse(info[2]);                      //Третья подстрока это тип купона
                coupons.Add(new_coupon);                                    //Добавить купон в нумерованный список
            }
        }
        catch (Exception)
        {                                                   //Обработка пойманных ошибок
            Console.WriteLine("\n\tОшибка чтения файла данных");            //Вывод сообщения об ошибке
            Console.ReadLine();                                             //Пауза
        }
        return coupons;                                                     //Возвращение нумерованного списка
    }

    public static void ShowAll()
    {                                           //Метод вывода в консоль всех доступных купонов
        Console.Clear();                                                    //Очиска экрана консоли
        Console.WriteLine("\n\tДействующие купоны: \n");
        ushort i = 1;
        Console.WriteLine("\n\tНаименование--Скидка--------Описание--------------------------------------------------------------\n");//Декларирование нумератора
        foreach (coupon c in GetCoupons())
        {                                   //Перечисление купонов в списке
            Console.WriteLine("\t" + i++ + "." + c.ToString());    //Вывод нумератора с инкрементациией
        }
    }

    public static void Add()
    {                                               //Метод добавления новых купонов
        try
        {                                                               //Обработка исключений
            while (true)
            {                                                   //Бесконечный цикл добавления новых купонов
                coupon new_coupon;                                          //Инициализация нового объекта структуры купона
                Console.Clear();                                            //Очиска экрана консоли
                Console.Write("\n\tДобавление нового купона" +              //Сообщение в консоль 
                              "\n\n\tВведите код купона - ");
                new_coupon.name = Console.ReadLine();                       //Присвоение введённой пользователем строки переменной name
                if (new_coupon.name == string.Empty) break;                 //Если строка пустая то прервать бесконечный цикл ввода
                Console.Write("\tВведите величину скидки купона - ");
                new_coupon.value = ushort.Parse(Console.ReadLine());        //Присвоение введённой пользователем строки переменной value
                Console.Write("\tВведите тип купона (% или р) - ");
                new_coupon.type = char.Parse(Console.ReadLine());           //Присвоение введённой пользователем строки переменной type 
                Console.Write("\tВведите описание купона - ");
                new_coupon.description = Console.ReadLine();                //Присвоение введённой пользователем строки переменной description

                StreamWriter file = new StreamWriter("Coupons.txt", true);  //Инициализация потока записи текстовой информации в файл
                file.WriteLine(new_coupon.ToFile());                        //Добваыление структуры купона в файл в виде строки
                file.Close();                                               //Закрытия файла			
                Console.WriteLine("\n\tНовый купон добавлен в базу");       //Сообщение в консоль
                break;
            }
        }
        catch (Exception e)
        {                                               //Обработка пойманных ошибок
            Console.WriteLine("\n\t" + e.Message);                          //Сообщение об ошибки в консоль
        }                                                 
    }

    public static void Delete()
    {                                   //Метод удаления купона
        ShowAll();                                                  //Вызов метода вывода всех купонов в консоль
        Console.Write("\n\tВведите номер купона который необходимо удалить - ");
        try
        {                                                       //Обработка исключений
            ushort del = ushort.Parse(Console.ReadLine());          //Сохранение в переменной del номера купона который необходимо удалить
            List<Object> coupons = GetCoupons();                    //Получение нумерованного списка структур
            coupons.RemoveAt(del - 1);                              //Удаление из списка структуры под номером del-1

            StreamWriter file = new StreamWriter("Coupons.txt");    //Инициализация потока записи текстовой информации в файл
            foreach (coupon c in coupons)
            {                           //Перечисление нумерованного списка
                file.WriteLine(c.ToFile());                         //Запись текущий структуры в файл в виде строки
            }
            file.Close();                                           //Закрытие потока записи в файл
            Console.WriteLine("\n\tКупон успешно удалён");          //Сообщение об удалении
        }
        catch (Exception e)
        {                                           //Обработка пойманных ошибок
            Console.WriteLine("\n\t" + e.Message);                  //Вывод сообщения в консоль
        }                                       
    }
}

class Users     //Класс для работы с пользователями и потребленным траффиком
{                                      
    public struct User
    {                                   //Структура хранящая информацию о пользователях и потребленном траффике
        public string fname;                      //Поле имени 
        public string secname;                     //Поле фамилии
        public string city;                         //Город проживания
        public string serialPassNumber;             //Серия паспорта
        public string selectedTarif;                //Выбранный тариф
        public double consumedTraffic;                //Потребленный интернет траффик
        public double totalCost;                      //Общая стоимость реализованного траффика
        public double costDiscounting;                //Итоговая стоимость со скидкой

        public User(string fname, string secname, string city, string serialPassNumber, string selectedTarif,
            double consumedTraffic, double totalCost, double costDiscounting)       // Конструктор для структуры хранящей информацию о пользователях
        {
            this.fname = fname;
            this.secname = secname;
            this.city = city;
            this.serialPassNumber = serialPassNumber;
            this.selectedTarif = selectedTarif;
            this.consumedTraffic = consumedTraffic;
            this.totalCost = totalCost;
            this.costDiscounting = costDiscounting;
        }

        public override string ToString()
        {       //Метод приводящий информацию структуры в строку для вывода в консоль
            return "\tФамилия - " + secname +
                   "\n\tИмя - " + fname +
                   "\n\tГород - " + city +
                   "\n\tСерия паспорта - " + serialPassNumber +
                   "\n\tВыбранный тариф - " + selectedTarif +
                   "\n\tПотребленный траффик - " + consumedTraffic +
                   "\n\tК оплате - " + totalCost +
                   "\n\tК оплате с учетом скидки - " + costDiscounting;
        }

        public string ToFile()
        {       //Метод приводящий информацию структуры в строку для записи в файл
            return fname + " " + secname + " " + city + " " + serialPassNumber + " " + selectedTarif 
                + " " + consumedTraffic + " " + totalCost + " " + costDiscounting;
        }
    }
    public static List<Object> GetUsers()
    {       //Метод получающий информацию из файла и возвращающий её в виде нумерованного списка
        List<Object> users = new List<Object>();  //Инициализация нумерованного списка билетов
        User new_user = new User();           //Инициализация нового объекта структуры билета
        try
        {                                       //Обработка исключений
            string[] lines = File.ReadAllText("Users.txt").Split('\n');   //Считывание текста из файл в массив строк
            foreach (string line in lines)
            {                                   //Перечисление массива строк
                if (line == string.Empty) continue;                         //Если строка пустая, то продолжить
                string[] parts = line.Split(' ');                           //Разбить текущую строку на подстроки по пробелам
                new_user.fname = parts[0];                              //Первую подстроку присвоить полю имя
                new_user.secname = parts[1];                             //Вторую подстроку присвоить полю фамилия
                new_user.city = parts[2];                                 //Третью подстроку присвоить полю город
                new_user.serialPassNumber = parts[3];                   //Четвертую подстроку присвоить полю серия паспорта
                new_user.selectedTarif = parts[4];                  //Пятую подстроку присвоить полю выбранный тариф
                new_user.consumedTraffic = double.Parse(parts[5]);  //Шестую подстроку присвоить полю использованный траффик и привести к типу double
                new_user.totalCost = double.Parse(parts[6]);        //Ctlmve. подстроку присвоить полю итоговая стоимость и привести к типу double
                new_user.costDiscounting = double.Parse(parts[7]);  //Шестую подстроку присвоить полю стоимость с учетом скидки и привести к типу double
                users.Add(new_user);                                    //Добавить в нумерованный список текущую стрктуру
            }
        }
        catch (Exception)
        {                                                   //Обработка пойманных ошибок
            Console.WriteLine("\n\tОшибка чтения файла данных");            //Сообщение об ошибке в консоль
            Console.ReadLine();                                             //Пауза
        }
        return users;                                                     //Вернуть нумерованный список
    }
    public static void ShowUsers()//метоод получающий информацию о зарегистрированных пользователях
    {
        Console.Clear();                                                         //Очистка экрана
        Console.WriteLine("\n\tСписок зарегистрированных пользователей: ");     //Сообщение на консоль
        ushort i = 1;                                   //Инициализация нумератора
        List<Object> users = GetUsers();          //Получение списка всех тарифов
        Console.WriteLine("\n\t---Имя----------Фамилия---------Город-----------Паспорт-------Выбранный тариф"); //Сообщение на консоль
        foreach (User u in users)               // Цикл перебора пользователей в списке
        {
            Console.Write("\n\t" + i++ + ". " + u.fname + "\0\0\0\t" + u.secname + "\0\0\0\t" + u.city + "\0\0\0\0\t" + u.serialPassNumber + "\t" + u.selectedTarif); //Вывод сообщения на консоль
        }
    }

    public static void ShowUsersTraffic()
    {                   //Метод вывода всех доступных тарифов в консоль
        Console.Clear();                            //Очиска экрана консоли
        Console.WriteLine("\n\tСписок зарегистрированных пользователей: ");
        ushort i = 1;                                   //Инициализация нумератора
        List<Object> users = GetUsers();          //Получение списка всех тарифов
        Console.WriteLine("\n\t---Имя----------Фамилия---------Тариф----Траффик--------Цена-------Итого со скидкой--");
        foreach (User u in users)
        {
            Console.Write("\n\t" + i++ + ". " + u.fname + "\0\0\0\t" + u.secname + "\0\0\0\t" + u.selectedTarif + "\t" + 
                u.consumedTraffic + " Мбайт \t" + u.totalCost + "руб. \0\0\0\0\0\t" + u.costDiscounting + " руб.");
        }
    }

    public static void UserRegister()
    {       //Метод покупки билета
        Console.Clear();                                    //Очиска экрана консоли
        try                                                  //Обработка исключений
        {                  
            while (true)
            {                           //Бесконечный цикл ввода новых тарифов
                User new_user;                    //Декларация новой структуры
                Console.Clear();                    //Очиска экрана консоли
                List<Object> users = GetUsers();
                Console.Write("\n\tРегистрация нового пользователя" +          //Сообщение в консоль
                              "\n\n\tФамилия: ");
                new_user.secname = Console.ReadLine();              //присвоение введенной строки в поле фамилия
                if (new_user.secname == string.Empty) break;       // если строка пустая - прервать цикл ввода
                Console.Write("\tИмя: ");                           //Сообщение в консоль
                new_user.fname = Console.ReadLine();                    //Присвоение введённой пользователем строки в поле имя
                if (new_user.fname == string.Empty) break;              //Если строка пустая то прервать цикл ввода
                if (new_user.fname.Length + new_user.secname.Length < 10)  //если длина имени и фамилии меньше 10 символов
                    throw new ArgumentException("Имя и фамилия пользователя не может быть менеее 10 букв");  //сгенерировать исключение
                Console.Write("\tГород: ");              //Сообщение а консоль
                new_user.city = Console.ReadLine();                 //присвоение введенной строки в поле город
                if (new_user.city == string.Empty) break;              //Если строка пустая то прервать цикл ввода 
                Console.Write("\tСерия паспорта: ");              //Сообщение а консоль
                new_user.serialPassNumber = Console.ReadLine();         //присвоение введенной строки в поле паспорт
                int chek = 0;                                           //переменная для проверки на существование одинаковых пользователей
                foreach (User u in users)                               // перебор всех пользователей
                {
                    if (new_user.serialPassNumber == string.Empty)           //Если строка пустая то прервать цикл ввода 
                        break;              
                    else if (new_user.serialPassNumber.Equals(u.serialPassNumber))  //если серия паспорта нового пользователя совпадает с имеющимся
                    {
                        Console.Clear();                                            //очистка экрана
                        Console.Write("Такой пользователь уже существует.");        // вывод сообщения о существовании пользователя
                        chek++;                                                     //увеличение переменной проверки
                        break;
                    }
                }
                if (chek == 1)                                      //Если проверка прошла успешно прервать цикл регистрации нового пользователя
                    break;
                Console.Write("\tВыбранный тариф: ");              //Сообщение а консоль
                new_user.selectedTarif = Console.ReadLine();                   //присвоение введенной строки в поле выбранный тариф
                if (new_user.selectedTarif == string.Empty) break;              //Если строка пустая то прервать цикл ввода 
                List<Object> tarifs = Tarifs.GetTarifs();
                while (true)
                {
                    int y = 0;
                    foreach (Tarifs.Tarif t in tarifs)
                    {
                        if (new_user.selectedTarif.Equals(t.name))
                        {
                            y++;
                            break;    
                        }
                    }
                    if (y <= 0)
                    {
                        Console.Write("\n\tТакого тарифа не существует!\n\tВыбранный тариф: ");              //Сообщение а консоль
                        new_user.selectedTarif = Console.ReadLine();                   //присвоение введенной строки в поле выбранный тариф

                    }
                    else break;
                }
                new_user.consumedTraffic = 0;
                new_user.totalCost = 0;
                new_user.costDiscounting = 0;

                StreamWriter file = new StreamWriter("Users.txt", true);//Инициализировать поток записи в файл текстовой информации
                file.WriteLine(new_user.ToFile());                     //Записать в файл структуру в виде строки
                file.Close();                                           //Закрыть поток записи в файл

                Console.WriteLine("\n\tНовый пользователь зарегистрирован!");   //Сообщение в консоль 
                Console.ReadKey();                                      //Пауза до нажатия любой клавиши
                break;
            }
        }
        catch (Exception e)
        {                               //Обработка пойманных ошибок
            Console.WriteLine("\n\t" + e.Message);          //Вывоб сообщения об ошибке в консоль
            Console.ReadKey();                              //Пауза до нажатия любой клавиши
        }
    }

    public static void ConsumedTraffic()
    {
        Console.Clear();    // очистка экрана
        try                 // обработка исключений
        {
            Console.Write("\n\tВведите фамилию пользователя, для ввода данных о потребленном траффике: ");  //Сообщение на консоль
            string secondName = Convert.ToString(Console.ReadLine());   //объявление строковой переменной и присвоение введенного с консоли значения
            List<Object> tarifs = Tarifs.GetTarifs();   //Получение списка тарифов
            List<Object> coupons = Coupons.GetCoupons();    //Получение списка скидочных купонов
            List<Object> users = Users.GetUsers();  //Получение списка пользователей
            int edit = 1;   //Объявление переменной структуры которую необходимо редактировать
            foreach (User u in users)   //цикл перебора подключившихся пользователей
            {
                byte chek = 0;  //переменная для проверки условия
                if (secondName.Equals(u.secname))   //условие при котором введенная и имеющаяся фамилии совпадают
                {
                    Console.Write("\n\tВведите данные о потребленном траффике (Мбайт) пользователя {0} {1}: ", u.fname, u.secname); //Сообщение в консоль
                    double ct = Convert.ToInt32(Console.ReadLine());    //Объявление переменной и присвоение значения о потребленном траффике

                    User buffer = (User)users[edit - 1];         //Копирование в буфер из списка структуры, которую необходимо редактировать
                    Console.Clear();                                //Очиска экрана консоли

                    buffer.consumedTraffic = ct;                //Присвоение временной переменной введенного значения о портебленном траффике 

                    foreach (Tarifs.Tarif tar in tarifs)    //Перебор имеющихся тарифов
                    {
                        if (u.selectedTarif.Equals(tar.name.ToString()))    //Поиска тарифа искомого пользователя
                        {
                            buffer.totalCost = ct * Convert.ToDouble(tar.price.ToString()); //Присвоение переменной временной структуры значения цены  оплаты
                            Console.ReadKey();  //Задержка экрана
                        }
                    }

                    Console.WriteLine("\n\t----------Цена с учетом скидки------------");    //Вывод на консоль имеющихся скидочных купонов
                    Coupons.ShowAll();
                    Console.Write("\n\tВведите название скидочного купона: ");  //Вывод сообщения на консоль
                    string answer = Convert.ToString(Console.ReadLine());   //Объявление переменной и присвоение значения ответа с названием скидочного купона

                    foreach (Coupons.coupon c in coupons)   //Перебор скидочных купонов
                    {
                        if (answer.Equals(c.name.ToString()))   //Если ответ соответствует имеющимуся скидочному купону
                        {
                            if (c.type.Equals('%')) // Если тип купона содержит скидку в процентах
                                buffer.costDiscounting = buffer.totalCost - buffer.totalCost * Convert.ToDouble(c.value) / 100;     // Присвоить значение цене, с учетом скидки
                            else            //Если скидка в рублях
                            {
                                if (buffer.totalCost > Convert.ToDouble(c.value))   //если цена без скидки больше скидки
                                    buffer.costDiscounting = buffer.totalCost - Convert.ToDouble(c.value);  // вычесть из цены без скидки стоимость скидки
                                else    //иначе
                                    buffer.costDiscounting = buffer.totalCost;  //оставить цену прежней
                            }
                        }
                        else
                        {
                            Console.Write("\n\tТакого скидочного купона нет! Попробуйте ввести данные снова!"); //Сообщение в консоль
                            chek++; //Увеличение переменной проверки
                            break;  //Выход из цикла
                        }
                    }

                    if (chek > 0)   // Если переменная проверки больше нуля выйти из цикла
                        break;

                    users.RemoveAt(edit - 1);                          //Удалить редактируемую структуру из списка								
                    users.Insert(edit - 1, buffer);                        //Скопировать в список буферную структуру

                    StreamWriter file = new StreamWriter("Users.txt"); //Создать поток записи в файл текстовой информации
                    foreach (User user in users)
                    {                       //Перечисление списка тарифов
                        file.WriteLine(user.ToFile());                     //Запись в файл стекущую структуру в виде строки
                    }
                    file.Close();                                       //Закрыть поток записи в файл

                    Console.WriteLine("\n\tДанные внесены успешно!");     //Сообщение в консоль
                    break;
                }
                else
                    edit++;
            }
            
        }
        catch (Exception e)
        {                               //Обработка пойманных ошибок
            Console.WriteLine("\n\t" + e.Message);          //Вывоб сообщения об ошибке в консоль
            Console.ReadKey();                              //Пауза до нажатия любой клавиши
        }
    }
    // Метод поиска клиента заплатившего наибольшую стоимость за услуги

    public static void FindUserHighestCost()    
    {
        Console.Clear();    //очитска экрана
        Console.WriteLine("Пользователь(-и) заплативший(-ие) наибольшую стоимость за услугу: \n");  //Вывод сообщения на консоль
        List<Object> gut = GetUsers();  //Получение списка пользователей
        double max = 0; //Ввод переменной максимума
        foreach (User ut in gut)    //Перебор пользователей
        {
            if (ut.costDiscounting > max)   //Если цена со скидкой больше максимума
            {
                max = ut.costDiscounting;   //Присвоить значение макимума значению из списка
            }
        }

        foreach (User ut in gut)    //Перебор пользователей
        {
            if (ut.costDiscounting.Equals(max))     //Если значение макимальной стоимости со скидкой равно максимальному значению вывести пользователя на экран
            {
                Console.WriteLine(ut.ToString());
                Console.WriteLine("\n\t--------------------------------------------------------");
            }
        }
    }

    //Метод просмотра зарегистрированным пользователем использованного траффика
    public static void ShowToUserTrafficAndCost()
    {
        Console.Clear();    //очитска экрана
        List<Object> users = Users.GetUsers();  //Получение списка пользователей
        try     //обработка исключений
        {
            Console.Write("\n\tВы зарегистрированный пользователь (y-да/n-нет)? "); //Вывод сообщения на консоль
            char answer = Convert.ToChar(Console.ReadLine());   //Получение ответа от пользователя

            while (true)    //Пока истина
            {
                byte counter = 0;   //Объявление переменной счетчика
                if (answer.Equals('y')) //Если да, то:
                {
                    Console.Write("\n\tВведите серию паспорта: ");  //Сообщение на консоль
                    string passport = Convert.ToString(Console.ReadLine()); // Получение серии паспорта
                    foreach (User user in users)    // Перебор списка пользователей
                    {
                        if (passport.Equals(user.serialPassNumber)) //Если паспорт соответствует значению паспорта зарегистированного пользователя
                        {
                            Console.Write(user.ToString()); //Вывод данных о траффике
                            counter++;  //Увеличение счетчика
                            break;  //Выход из цикла
                        }
                    }
                }
                else    //Иначе
                    break;  //Выход из цикла
                if (counter > 0)    //Если счетчик больше нуля
                    break; //Выход из цикла
                else //иначе
                {
                    Console.Write("\n\tПользователь с указанными данными в системе не зарегистрирован. Обратитесь к администратору за подключением."); //Вывод сообщения на консоль
                    break; //Выход из цикла
                }
            }
        }
        catch (Exception e) //обработка исклю
        {
            Console.WriteLine(e.Message);
        }
    }
}

class Menu //Класс отображения меню
{
    //Статический метод вывода на экран меню администратора                          
    static void Admin() 
    {
        while (true)    //пока истина
        {
            Console.Clear();    //очистка экрана
            Console.Write(      // вывод сообщения на экран
                "\n\t1 - Тарифы" +
                "\n\t2 - Скидки" +
                "\n\t3 - Пользователи" +
                "\n\t4 - Траффик" +
                "\n\tEsc - Назад\n\n\t"
                );

            switch (Console.ReadKey().Key)  //Перебор значений при нажатии определенной клавиши и вход в соответствующее меню
            {
                case ConsoleKey.D1: MenuTarif(); break; //Вызов метода MenuTarif
                case ConsoleKey.D2: MenuDiscount(); break;  //Вызов метода MenuDiscount
                case ConsoleKey.D3: MenuUsers();  break;    //Вызов метода MenuUsers
                case ConsoleKey.D4: MenuTraffic();  break;  //Вызов метода MenuTraffic
                case ConsoleKey.Escape: Main(); break;  //Вызов метода Main
                default:                                    // иначе
                    Console.WriteLine("\n\tТакого пункта не существует");   //Сообщение в консоль
                    break;
            }
        }                         
    }
    //Метод отображающий меню о траффике
    private static void MenuTraffic()
    {
        while (true)                    //Бесконечный цикл меню администратора
        {
            Console.Clear();                        //Очиска экрана консоли
            Console.Write(                          //Вывод в консоль меню администратора
                "\n\n\t1 - Показать использованный пользователем траффик" +
                "\n\t2 - Добавить траффик использованный пользователем" +
                "\n\t3 - Найти пользователя заплатившего наибольшую стоимость за услуги" +
                "\n\tEsc - Назад\n\n\t");
            switch (Console.ReadKey().Key)
            {                               //Считывается строка введённая в консоль и вызывается соответствующий метод реализующая выбранный пункт меню
                case ConsoleKey.D1: Users.ShowUsersTraffic(); break;
                case ConsoleKey.D2: Users.ConsumedTraffic(); break;
                case ConsoleKey.D3: Users.FindUserHighestCost(); break;
                case ConsoleKey.Escape: Admin(); break;
                default:
                    Console.WriteLine("\n\tТакого пункта не существует");
                    break;
            }
            Console.ReadKey();  //Пауза до нажатия любой клавиши
        }
    }
    //Метод отображающий меню о пользователях
    private static void MenuUsers()
    {
        while (true)                    //Бесконечный цикл меню администратора
        {
            Console.Clear();                        //Очиска экрана консоли
            Console.Write(                          //Вывод в консоль меню администратора
                "\n\n\t1 - Показать список зарегистрированных пользователей" +
                "\n\t2 - Зарегистрировать нового пользователя" +
                "\n\tEsc - Назад\n\n\t");
            switch (Console.ReadKey().Key)
            {                               //Считывается строка введённая в консоль и вызывается соответствующий метод реализующая выбранный пункт меню
                case ConsoleKey.D1: Users.ShowUsers(); break;
                case ConsoleKey.D2: Users.UserRegister(); break;
                case ConsoleKey.Escape: Admin(); break;
                default:
                    Console.WriteLine("\n\tТакого пункта не существует");
                    break;
            }
            Console.ReadKey();  //Пауза до нажатия любой клавиши
        }
    }
    //Метод отображающий меню о скидках
    private static void MenuDiscount()
    {
        while (true)                    //Бесконечный цикл меню администратора
        {
            Console.Clear();                        //Очиска экрана консоли
            Console.Write(                          //Вывод в консоль меню администратора
                "\n\n\t1 - Показать список промокодов" +
                "\n\t2 - Добавить промокод" +
                "\n\t3 - Удалить промокод" +
                "\n\tEsc - Назад\n\n\t");
            switch (Console.ReadKey().Key)
            {                               //Считывается строка введённая в консоль и вызывается соответствующий метод реализующая выбранный пункт меню
                case ConsoleKey.D1: Coupons.ShowAll(); break;
                case ConsoleKey.D2: Coupons.Add(); break;
                case ConsoleKey.D3: Coupons.Delete(); break;
                case ConsoleKey.Escape: Admin(); break;
                default:
                    Console.WriteLine("\n\tТакого пункта не существует");
                    break;
            }
            Console.ReadKey();  //Пауза до нажатия любой клавиши
        }
    }
    //Метод отображающий меню о тарифе
    private static void MenuTarif()
    {
        while (true)                    //Бесконечный цикл меню администратора
        {
            Console.Clear();                        //Очиска экрана консоли
            Console.Write(                          //Вывод в консоль меню администратора
                "\n\t1 - Показать тарифы" +
                "\n\t2 - Добавить тариф" +
                "\n\t3 - Редактировать тариф" +
                "\n\t4 - Удалить тариф" +
                "\n\t5 - Изменить цены" +
                "\n\tEsc - Назад\n\n\t");
            switch (Console.ReadKey().Key)
            {                               //Считывается строка введённая в консоль и вызывается соответствующий метод реализующая выбранный пункт меню
                case ConsoleKey.D1: Tarifs.ShowAll(); break;
                case ConsoleKey.D2: Tarifs.Add(); break;
                case ConsoleKey.D3: Tarifs.Edit(); break;
                case ConsoleKey.D4: Tarifs.Delete(); break;
                case ConsoleKey.D5: Tarifs.ChangeOrders(); break;
                case ConsoleKey.Escape: Admin(); break;
                default:
                    Console.WriteLine("\n\tТакого пункта не существует");
                    break;
            }
            Console.ReadKey();  //Пауза до нажатия любой клавиши
        }
    }
    //Метод пользовательского меню
    static void MenuUser()
    {
        while (true)
        {
            Console.Clear();        //Очиска экрана консоли
            Console.Write(          //Вывод меню пассажира в консоль
                   "\n\t-----------------------------------" +
                   "\n\n\t      Провайдер <<Провайдер>>" +
                   "\n\n\t-----------------------------------" +
                   "\n\n\t1 - Показать тарифы" +
                   "\n\t2 - Показать скидочные купоны" +
                   "\n\t3 - Просмотр использованного траффика (для зарегистрированных пользователей)" +
                   "\n\tEsc - Назад\n\n\t");

            switch (Console.ReadKey().Key)
            {                               //Считывается строка введённая в консоль и вызывается соответствующий метод реализующая выбранный пункт меню
                case ConsoleKey.D1: Tarifs.ShowAll(); break;
                case ConsoleKey.D2: Coupons.ShowAll(); break;
                case ConsoleKey.D3: Users.ShowToUserTrafficAndCost(); break;                                          
                case ConsoleKey.Escape: Main(); break;                                      
                default: Console.Write("\tТакого пункта не существует"); break;     //Вывод сообщения в ответ на вызов несуществующего пункта меню
            }
            Console.ReadKey();  //Пауза до нажатия любой клавиши
        }
    }

    static void Main()
    {           //Статичесекий метотд вывода меню для пассажиров
        while (true)
        {               //Бесконечный цикл меню пассажиров
            Console.Clear();        //Очиска экрана консоли
            Console.Write(          //Вывод меню пассажира в консоль
                "\n\t-----------------------------------" +
                "\n\n\t      Провайдер <<Провайдер>>" +
                "\n\n\t-----------------------------------" +
                "\n\n\t1 - Пользователь" +
                "\n\t2 - Администратор" +
                "\n\tEsc - Выход\n\n\t");

            switch (Console.ReadKey().Key)
            {                               //Считывается строка введённая в консоль и вызывается соответствующий метод реализующая выбранный пункт меню
                case ConsoleKey.D1: MenuUser(); break;        
                case ConsoleKey.D2: Admin(); break;                      
                case ConsoleKey.Escape: Environment.Exit(0); break;                   //Выход из программы
                default: Console.Write("\tТакого пункта не существует"); break;     //Вывод сообщения в ответ на вызов несуществующего пункта меню
            }
            Console.ReadKey();  //Пауза до нажатия любой клавиши
        }
    }
}