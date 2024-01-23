using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathMod_Non_Linear
{
    //Общее для всех методов
    public class General
    {
        protected double a, b, e, h;
        protected int n;
        public double Func(double x)
        {
            return Math.Sin(x) * x;
        }
        public General()
        {
            Console.Write("Введите левую границу а = ");
            a = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите правую границу b = ");
            b = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите погрешность e = ");
            e = Convert.ToDouble(Console.ReadLine());
            n = Convert.ToInt32(Math.Truncate((b - a) / e)) + 1;
            h = (b - a) / n;
        }
        public void Info()
        {
            Console.WriteLine($"Шаг h - {h}, количество делений n - {n}");
        }
        virtual public void Calculate(){}
    }
    //Метод перебора
    public class UniSearch : General
    {
        public UniSearch() : base() {;}
        public override void Calculate()
        {
            double x_min = a;
            double y_min = Func(x_min);
            double x;
            double y;
            for (int i = 1; i <= n; i++)
            {
                x = a + (h * i);
                y = Func(x);
                if (y < y_min)
                {
                    y_min = y;
                    x_min = x;
                }
            }
            Console.WriteLine($"Минимальное значение функции: F({x_min}) = {y_min}");
        }
    }
    //Метод дихотомий
    public class Dichotomy : General
    {
        public Dichotomy() : base() {Console.WriteLine("Убедитесь, что функция унимодальна на указанном отрезке."); }
        public override void Calculate()
        {
            double x1, x2;
            while (true)
            {
                //Условие достижения точности
                if ((b - a)/2 <= 2 * e)
                {
                    double x_res = (a + b) / 2;
                    Console.WriteLine($"Минимальное значение функции: F({x_res}) = {Func(x_res)}");
                    return;
                }
                x1 = ((a + b) / 2) - e;
                x2 = ((a + b) / 2) + e;
                if (Func(x1) <= Func(x2)) { b = x2; }
                else { a = x1; }
                //Для дебага кода
                //Console.WriteLine($"x1 - {x1}; x2 - {x2}; b-a - {b - a}");
            }
        }
    }
    public class GoldenRatio : General
    {
        public GoldenRatio() : base() {;}
        public override void Calculate()
        {
            double x1, x2, y1, y2;
            x1 = a + ((b - 1)) * ((3 - Math.Sqrt(5)) / 2);
            x2 = a + b - x1;
            y1 = Func(x1);
            y2 = Func(x2);
            while (true)
            {
                //Условие достижения точности
                if (b - a < 2*e)
                {
                    double x_res = (a + b) / 2;
                    Console.WriteLine($"Минимальное значение функции: F({x_res}) = {Func(x_res)}");
                    return;
                }
                if(y1 <= y2)
                {
                    b = x2;
                    x2 = x1;
                    x1 = a + b - x2;
                    y2 = Func(x2);
                    y1 = Func(x1);
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    x2 = a + b - x1;
                    y1 = Func(x1);
                    y2 = Func(x2);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            GoldenRatio us1 = new GoldenRatio();
            us1.Calculate();
            Console.ReadKey();
        }
    }
}
