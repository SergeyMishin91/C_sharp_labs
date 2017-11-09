using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;

class Lab_5
{
    public struct Row : IComparable
    {
        private string F;
        private string I;
        private string O;
        private ushort group;
        private byte a;
        private byte b;
        private byte c;

        public Row(string F, string I, string O, ushort group, byte a, byte b, byte c)
        {
            this.F = F;
            this.I = I;
            this.O = O;
            this.group = group;
            this.a = a;
            this.b = b;
            this.c = c;
        }

        public int CompareTo(object obj)
        {
            Row r = (Row)obj;
            if (this.group > r.group) return 1;
            if (this.group < r.group) return -1;
            return 0;
        }

        public override string ToString()
        {
            return this.F + " " + this.I + " " + this.O + " " + this.group + " " + this.a + " " + this.b + " " + this.c + "\r\n";
        }

        public static bool operator true(Row r)
        {
            return (r.a > 3 && r.b > 3 && r.c > 3);
        }
        public static bool operator false(Row r)
        {
            return (r.a < 3 || r.b < 3 || r.c < 3);
        }
    }

    static void Main()
    {
        string path = "D:\\LAB_5_input.txt";

        try
        {

            int size = File.ReadAllLines(path).Length;
            ArrayList students = new ArrayList();
            StreamReader file = new StreamReader(path);

            for (int i = 0; i < size; i++)
            {
                string[] a = file.ReadLine().Split(' ');
                Row student = new Row(a[0], a[1], a[2], ushort.Parse(a[3]), Convert.ToByte(a[4]), Convert.ToByte(a[5]), Convert.ToByte(a[6]));
                if (student) students.Add(student);
            }

            Array.Sort(students.ToArray());                                 //Сортировка массива структур используя метод CompareTo

            File.WriteAllText("D:\\output.txt", "");                            //Опустошение файла
            foreach (Row student in students)
            {                               //Перечисление массива структур
                File.AppendAllText("D:\\output.txt", student.ToString());       //Запись текущей структу в файл как строки
            }
            Process.Start("D:\\output.txt");                                    //Открыть выходной файл

        }
        catch (Exception e)
        {
            Console.Write("\n" + e.Message);
            Console.ReadKey();
        }
    }
}