using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Постановка задачи: Используя рекурсию, реализовать программу, решающую задачу:
Ввести а1, а2, а3, М. Построить последовательность чисел ак = ак-1 * ак-2 + ак-3. Довести ее до аN >= М. Напечатать последовательность, N. Сообщить, выполняется ли равенство аN = М.
 */

namespace Praktika1KURSZada4a6___1
{
    class Program
    {
        /// <summary>
        /// Рекурсивная функция
        /// </summary>
        /// <param name="n"></param>
        /// <param name="list"></param>
        /// <param name="m"></param>
        static void Function(int n, List<double> list, double m)    
        {
            double newElement = list[n - 1] * list[n - 2] + list[n - 3];    //Создание нового элемента
            list.Add(newElement);    //Добавление элемента в LIST
            if (list[n] >= m)     //Проверка на >= M
            {
                Console.WriteLine($"\nЭлемент номер {n + 1} является финальным");    //Сообщение пользователю
                return;
            }
            else
                Function(n + 1, list, m);    //Новый вызов функции
        }
    
        /// <summary>
        /// Главная функция
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)   
        {
            //Переменные и список
            double a1 = 0, a2 = 0, a3 = 0, m = 0;
            int n = 3;
            List<double> listSenquences = new List<double>();  
            Input:   //Проверка на ввод
            try
            {
                //Ввод элементов
                Console.WriteLine("Введите элементы a1, a2, a3, M");
                Console.Write("a1 - ");
                a1 = Convert.ToDouble(Console.ReadLine());
                Console.Write("a2 - ");
                a2 = Convert.ToDouble(Console.ReadLine());
                Console.Write("a3 - ");
                a3 = Convert.ToDouble(Console.ReadLine());
                Console.Write("M - ");
                m = Convert.ToDouble(Console.ReadLine());
                if (a1 == 0 && a2 == 0 && a3 == 0 && m != 0)
                {
                    Console.WriteLine("Ошибка: Решений нет. Измените");    //Сообщение пользователю в случае нулевых решений
                    goto Input;   //Отправка в начало Input
                }
            }
            catch (FormatException)    //Ошибка ввода данных
            {
                Console.WriteLine("Ошибка ввода данных");    //Сообщение пользователю
                goto Input;   //Отправка в начало Input
            }
            
            //Добавление элементов в LIST
            listSenquences.Add(a1);
            listSenquences.Add(a2);
            listSenquences.Add(a3);

            Function(n, listSenquences,m);   //Вызов рекурсивной функции

            Console.Write($"Полученная последовательность: ");
            for (int i = 0; i < listSenquences.Count; i++) Console.Write($"{listSenquences[i]}; ");   //Вывод последовательности
            if (listSenquences[listSenquences.Count-1] == m) Console.WriteLine($"\nAn, равная {listSenquences[listSenquences.Count - 1]} равна M, равным {m}");    //Вывод о An == M
            else
                Console.WriteLine($"\nAn, равная {listSenquences[listSenquences.Count - 1]} не равна M, равным {m}");   //Вывод о An != M
            Console.ReadKey();
        }

    }
}