//3. Разработать рекурсивный метод для вывода на экран всех возможных разложений натурального числа n на множители(без повторений). Например, для n = 12 на экран должно быть выведено:
//2*2*3=12
//2*6=12
//3*4=12 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace recurs01
{
    class SR_1_main
    {
        static readonly int MaxDeep = 10000;
        static int[] Decomposition = new int[MaxDeep];
        static void Decompose(int multiplier, int n, int deep, int result)
        {
            int i, mlim = 0;
            if (n == 1)
            {
                for (i = 0; i < deep - 1; i++)
                    Console.Write(Decomposition[i] + "*");
                if (deep > 0)
                { 
                    Console.WriteLine("{0}={1}", Decomposition[deep - 1], result);   
                }
                return;
            }
            
            if (deep == 0)
                mlim = n - 1;
            else
                mlim = n;
            for (Decomposition[deep] = multiplier; Decomposition[deep] <= mlim; Decomposition[deep]++)
            {
                if ((n % Decomposition[deep]) == 0)
                {
                    Decompose(Decomposition[deep], n / Decomposition[deep], deep + 1, result);
                }
            }
        }
        static void Main(string[] args)
        {
            int n;
            Console.WriteLine("Разложение числа на множители");
            Console.Write("Введите число: ");
            n = int.Parse(Console.ReadLine());
            int result = n;
            SR_1_main.Decompose(2, n, 0, result);
            Console.ReadKey();
        }
    }
}
