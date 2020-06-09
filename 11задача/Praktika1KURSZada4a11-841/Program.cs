using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Постановка задачи: «Исправление ошибок».
 * Пусть по некоторому каналу связи передается сообщение, имеющее вид последовательности нулей и единиц (или, аналогично, точек и тире).
 * Из-за помех возможен ошибочный прием некоторых сигналов: нуль может быть воспринят как единица и наоборот.
 * Можно передавать каждый сигнал трижды, заменяя, например, последовательность 1, 0, 1 последовательностью 1.1,1,0,0,0,1,1,1.
 * Три последовательные цифры при расшифровке заменяются той цифрой, которая встречается среди них по крайней мере дважды.
 * Такое утраивание сигналов существенно повышает вероятность правильного приема сообщения. Написать программу расшифровки.
 */

namespace Praktika1KURSZada4a11_841
{
    class Program
    {
        static void Main(string[] args)
        {
            //Объявление переменных
            bool ok = false;
            string nullString = "";
            char[] elements = new char[nullString.Length];    //Массив единииц и нулей
            Input:
            Console.WriteLine("Введите числовую строку нулей и единиц");   //Обращение к пользователю
            nullString = Console.ReadLine();   //Ввод строки
            elements = nullString.ToCharArray();   //Перевод элементов строки в единичные элементы
            for (int i = 0; i < elements.Length; i++)   //Проверка ввода
                if (elements[i] < 47 || elements[i] > 50)   //Если элемент != 0 или 1
                {
                    Console.WriteLine("Числовая последовательность имеет элементы неравные 0 и 1");   //Обращение к пользователю
                    goto Input;   //Отправка на ввод
                }
            if (elements.Length % 3 != 0 || elements.Length == 0)   //Если строка не делиться нацело на 3, то
            {
                Console.WriteLine("Ошибка: Входные данные некратны 3");   //Обращение к пользователю
                goto Input;    //Отправка на ввод
            }

            Console.WriteLine("Полученная строка - " + nullString);   //Вывод строки

            Console.Write("Ответ - ");   //Вывод ответа
            for (int i = 0; i < elements.Length; i+=3)    //Цикл с шагом 3
            {
                int counter = 0;    //Счётчик
                for (int j = i; j < i+3; j++)    //Проверка 3-ёх элементов
                    if (elements[j] == '1')    //Если 1, то счётчик +
                        counter++;
                if(counter > 1) Console.Write("1");    //Если дважды 1, то вывод 1
                else Console.Write("0");    //Иначе 0
            }
            Console.WriteLine();
            Console.ReadLine();
        }
    }
}
