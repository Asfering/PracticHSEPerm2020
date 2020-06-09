using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *Постановка задачи: Даны действительные числа x, y. Определить, принадлежит ли точка с координатами x, y заштрихованной части плоскости. Необходимая область показана на рисунке 3.1.
 */
namespace Praktika1KURSZada4a3_599d
{
    class Program
    {
        static void Main(string[] args)
        {
            //Объявление переменных
            double x, y;
            bool ok;
            Console.WriteLine("Введите координаты по осям X и Y точки");   //Обращение к пользователю
            do
            {
                Console.Write("Введите координату X ");
                ok = double.TryParse(Console.ReadLine(), out x);
                if (!ok) Console.WriteLine("Ошибка. Введите цифры"); 
            } while (!ok);   //Если x != double
            do
            {
                Console.Write("Введите координату Y "); 
                ok = double.TryParse(Console.ReadLine(), out y);
                if (!ok) Console.WriteLine("Ошибка. Введите цифры"); 
            } while (!ok);   //Если y != double

            if (Math.Abs(x) + Math.Abs(y)/2 <= 0.5 && Math.Abs(x) < 0.5 && Math.Abs(y) < 1) Console.WriteLine("Входит в график");    //Проверка входимости
            else Console.WriteLine("Не входит в график");
            Console.WriteLine("Нажмите, чтобы закрыть программу");    //Обращение к пользователю
            Console.ReadLine();
        }
    }
}
