using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2v3
{
    class Program
    {
        class Rectangle
        {
            //задаем поля
            private int a, b;

            //конструктор
            //public Rectangle(int a1, int b1)
            //{
            //    a = a1;
            //    b = b1;
            //}

            public void ZnachenieStoron()
            {
                Console.Write("Длина = {0}, ширина = {1}", A, b);
            }

            public void Perimetr ()
            {
                Console.Write("\nПериметр прямоугольника  = {0}", A + b);
            }

            public void Area()
            {
                Console.Write("\nПлощадь прямоугольника  = {0}", A * b);
            }
            //свойство 1. Получить установить длины сторон прямоугольника (чтение+запись)
            public int A
            {
                get { return a; }
                set
                {
                    if (value > 0)
                        a = value;
                    else
                    {
                        while (true)
                        {
                            a = value;
                            if (value <= 0)
                            {
                                Console.WriteLine("Неверное число. Введите значение еще раз!");
                                value = Int32.Parse(Console.ReadLine());
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }

            public int B
            {
                get { return b; }
                set
                {
                    if (value > 0)
                        b = value;
                    else
                    {
                        while (true)
                        {
                            b = value;
                            if (value <= 0)
                            {
                                Console.WriteLine("Неверное число. Введите значение еще раз!");
                                value = Int32.Parse(Console.ReadLine());
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            public bool IsSquare
            {
                set
                {
                    if (A == B)
                    {
                        value = true;
                    }
                    else
                        value = false;
                }
            }

            public int this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 1:
                            return a;
                        case 2:
                            return b;
                        default:
                            throw new Exception("idndex can be only 1 or 2");
                    }
                }
                set
                {
                    if (index==1)
                            a = value;
                    if (index == 2)
                        b = value;
                    else
                        throw new Exception("idndex can be only 1 or 2");
                    
                }
            } 
        }
        static void Main(string[] args)
        {
            Rectangle rect = new Rectangle();
            Console.WriteLine("Введите длину: ");
            rect.A = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите ширину: ");
            rect.B = Int32.Parse(Console.ReadLine());

            //Console.WriteLine("Длина: {0}", rect.Dlina);
            //Console.WriteLine("Ширина: {0}", rect.Shirina);
            rect.ZnachenieStoron();
            rect.Perimetr();
            rect.Area();

            Console.ReadKey();
        }
    }
}
