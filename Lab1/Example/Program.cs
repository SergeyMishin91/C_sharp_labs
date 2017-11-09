using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace zrecurs02
{
   
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите целое число: ");
            int number = int.Parse(Console.ReadLine());
            Recurs_1(number, 1, number, " ");
            Console.ReadKey();
        }

        private static void Recurs_1(int a, int one, int b, string rezult)
        {
            if (b == 0)
            {
                Console.WriteLine(rezult + "=" + a);
                return;
            }
            int index = one;
            while (index <= b && index != a)
            {
                Recurs_1(a, index, b - index, rezult == "" ? rezult + index : rezult + "+" + index);
                index++;
            }
        }
    }
}


