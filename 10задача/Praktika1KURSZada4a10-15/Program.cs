using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Постановка задачи: «Считалка». Даны натуральные n, m. Предполагается, что n человек встают в круг и получают номера, считая против часовой стрелки, 1, 2, ..., n.
 * Затем, начиная с первого, также против часовой стрелки отсчитывается m-й человек (поскольку люди стоят по кругу, то за n-м человеком стоит первый).
 * Этот человек выходит из круга, после чего, начиная со следующего, снова отсчитывается m-й человек и так до тех пор, пока из всего круга не остается один человек.
 * Определить его номер.
 */

namespace Praktika1KURSZada4a10_15
{
    class Program
    {
        /// <summary>
        /// Основная функция
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Переменные
            int n, m, i;
            bool okN, okM;
            do   //Ввод размера массива N
            {
                Console.Write("Введите количество человек в кругу: ");
                okN = Int32.TryParse(Console.ReadLine(), out n);
                if(!okN || n<1) Console.WriteLine("Введите целочисленные цифры больше 0");
            } while (!okN || n < 1);
            int[] mas = new int[n];   //Объявление массива
            Console.WriteLine("Распределение номеров против часовой стрелки: ");    //Сообщение пользователю
            for (i = 0; i < n; i++)    //Выдача номеров каждому человеку
                mas[i] = 1;
            do    //Ввод номера необходимого человека M
            {
                Console.WriteLine("Введите m");
                okM = Int32.TryParse(Console.ReadLine(), out m);
                if (!okM || m < 1) Console.WriteLine("Введите целочисленные цифры больше 0");
            } while (!okM || m < 1);
            //Объявление переменных

            mas = Massive(mas, n, m);

            for (i = 0; i < n; i++)   //Вывод номера массива с 1 в значении
                if (mas[i] == 1)
                {
                    Console.WriteLine(i + 1);
                    break;
                }

            Console.ReadLine();
        }

        /// <summary>
        /// Работа с массивом
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="n"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public static int[] Massive(int[] mas,int n, int m)
        {
            int i, countPersons = n, temp = -1;
            while (countPersons > 1)   //Пока количество людей > 1
            {
                int tempСountPersons = 0;   //Пустая переменная
                while (tempСountPersons < m)   //Пока не дойдём до M-го человека
                {
                    for (i = temp + 1; i < n; i++)   //Идём по кругу
                        if (mas[i] == 1)
                        {
                            tempСountPersons++;    //Счётчик для поиска M-го человека
                            temp = i;   //Индекс на случай удаления
                            break;
                        }
                    if (i == n)   //Если i == n => обнуляем и идём далее по кругу
                        for (i = 0; i < temp; i++)
                            if (mas[i] == 1)
                            {
                                tempСountPersons++;    //Счётчик для поиска M-го человека
                                temp = i;   //Индекс на случай удаления
                                break;
                            }
                }
                mas[temp] = 0;    //Заменяем элемент с 1 на 0, тем самым выводим человека из круга
                countPersons--;   //Убираем одного человека из общего количества людей
            }

            return mas;
        }
    }
}
