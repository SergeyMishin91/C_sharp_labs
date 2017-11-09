//9 вариант.II.Дана строка, в которой содержится осмысленное текстовое сообщение.Слова сообщения разделяются пробелами и знаками препинания.
//    9.	Подсчитать сколько слов, состоящих только из прописных букв, содержится в сообщении.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1_6
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите строку: ");
            StringBuilder a = new StringBuilder(Console.ReadLine());
            Console.WriteLine("Исходная строка: " + a);

            String str = a.ToString();

            str = str.Trim (new char[] { '.',',' }); //удаление точек и запятых
            string[] textArray = str.Split(new char[] { ' ' }); //разбиваем текст на слова (массив строк)

            Console.WriteLine("words num: " + textArray.Length);
            int n = textArray.Length;
            int count = 0;


            for (int i = 0; i < n; ++i) {

                Console.WriteLine("{0} слово: {1}", i+1, textArray[i]);
                char[] b = new char[textArray[i].Length];
                for (int j = 0; j < b.Length; ++j) {
                    if ((b[j] >= 'A') && (b[j] <= 'Z'))
                    {
                        break;
                    }
                    else count++;
                }
               
            }
            Console.WriteLine("Количество слов в нижнем регистре: " + count);

                Console.ReadLine();
        }

    }
}
