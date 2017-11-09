//9 вариант. 	I. Разработать программу, которая для заданной строки s удаляет все подстроки substr;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; //библиотека для использования стрингбилдера

namespace Lab1_5
{
    class Program
    {
       static void Main()
        {

            Console.WriteLine("Введите строку: ");
            StringBuilder a = new StringBuilder(Console.ReadLine());
            Console.WriteLine("Исходная строка: " + a);
            
            Console.WriteLine("Введите заданную подстроку: ");
            string substr = Console.ReadLine();
             /* for (int i = 0; i < a.Length; )
                  if (char.IsPunctuation(a[i])) a.Remove(i, 1);
                  else ++i;*/
              string str = a.ToString(); //перевод из стрингбилдера в стринг
              str = str.Trim(); // удаляем пробелы
              string[] s = str.Split(' ');            

              int n = str.IndexOf(substr);
              str = str.Remove(n, substr.Length);

              Console.WriteLine("подстрока удалена");
              Console.WriteLine("полученная строка: " + str);
              
            Console.ReadLine();
        }
    }
}
