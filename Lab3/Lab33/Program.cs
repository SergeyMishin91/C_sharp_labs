using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab33
{
    abstract class Figure
    {
        #region values
        private float width; //ширина
        private float length; //длина
        private string figureName; //название фигуры
        #endregion
        #region property
        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        public float Length
        {
            get { return length; }
            set { length = value; }
        }

        public string FigureName
        {
            get { return figureName; }
            set { figureName = value; }
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
