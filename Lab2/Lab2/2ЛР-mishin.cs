//Классы: основные понятия, данные, методы, конструкторы, свойства
//    9.	Создать класс для работы с регулярными выражениями.Разработать следующие элементы класса: 
//o Поля: 
//	Regex r; 
//	string text;
//o Методы, позволяющие: 
//	определить, содержит ли текст фрагменты, соответствующие шаблону поля; 
//	вывести на экран все фрагменты текста, соответствующие шаблону поля; 
//	удалить из текста все фрагменты, соответствующие шаблону поля; 
//o Свойства: 
//	позволяющее установить или получить строковое поле класса(доступно для чтения и записи)
//	позволяющее установить или получить регулярное выражение, хранящееся в соответствующем поле класса(доступно для чтения и записи)

//Классы: деструкторы, индексаторы, операции класса, операции преобразования типов
//    9.	Добавить в класс для работы с регулярными выражениями: 
//o Индексатор, позволяющий по индексу 0 обращаться к полю r, по индексу 1 - к полю text.
//o Перегрузку: 
//	операции унарного -: удаляет из поля text все фрагменты, соответствующие регулярному выражению поля r.
//	констант true и false: обращение к экземпляру класса дает значение true, если поле text не пустое, иначе false; 
//	операции бинарного +: дописывает в конец поля text строку.
//	преобразования класса Regex в тип string (и наоборот). 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab2
{
    class RegExspression
    {
        //поля
        private Regex r;
        private string text;

        public RegExspression() { }

        //конструктор 
        public RegExspression(string pattern, string txt) 
        { 
            r = new Regex (pattern);
            text = txt;
        }

        //проверка на соответствие шаблону
        public bool Exist()
        {

            if (r.IsMatch(text) == true)
            {
                Console.WriteLine("текст содержит следующие фрагменты, соответствующие шаблону поля: ");
                return r.IsMatch(text);
            }
            else
            {
                Console.WriteLine("текст не содержит искомых фрагментов, соответствующих заданному шаблону");
                return false;
            }
        }

        //вывод на экран фрагментов соответствующих шаблону
        public void ShowMatches()
        {
            MatchCollection m = r.Matches(text);
            foreach (Match x in m)
                Console.Write(x.Value);
        }

        //удаление из текста элементов соответствующих шаблону
        public string DeleteMatches()
        {
            MatchCollection m = r.Matches(text);
            string text1 = text;
            foreach (Match x in m)
            {
                int i = text1.IndexOf(x.Value);
                int l = x.Value.Length;

                text1 = text1.Remove(i, l);
            }
            return text1;
        }

        //свойства
        // 1. позволяющее установить или получить строковое поле класса (доступно для чтения и записи) 
        public string S
        {
            get { return text; }
            set { text = value; }
        }

        //2. позволяющее установить или получить регулярное выражение, хранящееся в соответствующем 
        //   поле класса (доступно для чтения и записи) 
        public Regex R
        {
            get { return r; }
            set { r = value; }
        }
        // установка индексов:
        public Object CheckType(int n)
        {
            if (n == 0)
                return new Regex("");
            else if (n == 1)
                return "string";
            return null;
        }

        public object this[int i]
        {
            set
            {
                var result = CheckType(i);
                if (result.GetType() == typeof(Regex))
                {
                    //вариант set для регекса
                    R = (Regex)value;
                }
                else
                {
                    //вариант set для стринга
                    text = value.ToString();
                }
            }
            //теперь get
            get
            {
                var result = CheckType(i);
                if (result.GetType() == typeof(Regex))
                    //вариант set для регекса
                    return R;
                else
                    //вариант set для стринга
                    return text;
            }
        }


       // перегрузка операции унарного -: удаляет из поля text все фрагменты,
       // соответствующие регулярному выражению поля r.
        public static RegExspression operator -(RegExspression reg)
        {
            RegExspression tmp = new RegExspression();
            tmp.R = reg.R;
            tmp.S = reg.S;
            //Console.WriteLine("reg.R = {0}, reg.S = {1}, tmp.R = {2}, tmp.S = {3}",
            //    reg.R, reg.S, tmp.R, tmp.S);
            tmp.S = tmp.DeleteMatches();

            return tmp;
        }

        //    перегрузка констант true и false: обращение к экземпляру класса дает значение true, 
        //    если поле text не пустое, иначе false;
        public static bool operator true(RegExspression reg)
        {
            RegExspression tmp = new RegExspression();
            tmp.S = reg.S;
            if (tmp.S == null)
                return true;
            else
                return false;
        }

        public static bool operator false(RegExspression reg)
        {
            RegExspression tmp = new RegExspression();
            tmp.S = reg.S;
            if (tmp.S != null)
                return true;
            else
                return false;
        }
        // перегрузка операции бинарного +: дописывает в конец поля text строку. 
        public static RegExspression operator +(RegExspression reg)
        {
            RegExspression tmp = new RegExspression();
            tmp.S = reg.S;

            string newText;
            Console.WriteLine("Введите текст: ");
            newText = Convert.ToString(Console.ReadLine());

            tmp.S += newText;

            return tmp;
        }

        //преобразование класса Regex в тип string 
        public static implicit operator string(RegExspression r)
        {
            return r.ToString();
        }

        //и наоборот
        public static implicit operator Regex(RegExspression text)
        {
            return new Regex(text);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            RegExspression A = new RegExspression(@"[а-я]", "");

            Console.WriteLine("Введите фразу: ");
            A.S = Convert.ToString(Console.ReadLine());

            A.Exist();
            A.ShowMatches();
            Console.WriteLine("\nОбращение по индексу к регулярному выражению: ");
            Console.WriteLine(A[0]);
            Console.WriteLine("Обращение по индексу к строковому полю: ");
            Console.WriteLine(A[1]);
            A.S = A.DeleteMatches();
            Console.WriteLine("\nТекст после удаления: \n{0}", A.S);

            RegExspression B = new RegExspression(@"[a-z]", "");
            Console.WriteLine("Введите фразу латиницей: ");
            B.S = Convert.ToString(Console.ReadLine());
            // Задача 2 работа с перегрузкой
            B = -B;
            Console.WriteLine("\nТекст после удаления (перегрузка унарный минус): \n{0}", B.S);

            if (B) Console.WriteLine("Поле text не пустое");
            else Console.WriteLine("Поле text пустое");

            B = +B;
            Console.WriteLine("\nТекст после добавления (перегрузка унарный плюс): \n{0}", B.S);
            Console.ReadKey();
        }
    }
}
