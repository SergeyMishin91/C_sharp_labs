//4. Разработать рекурсивный метод для вывода на экран всех возможных разложений натурального числа n на слагаемые(без повторений). 
//Например, для n = 5 на экран должно быть выведено:
//1+1+1+1+1=5
//1+1+1+2=5
//1+1+3=5
//1+4=5
//2+1+2=5
//2+3=5


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


