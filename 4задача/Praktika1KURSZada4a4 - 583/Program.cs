using System;

/*
 *Постановка задачи: Получить последовательность dk, dk-1, ..., d0 десятичных цифр числа 100!, т. е.
 * такую целочисленную последовательность, в которой каждый член di удовлетворяет условию 0≤ di ≤9 и,
 * дополнительно, dk * 10k + dk-1 • 10k-1 + ... d0 = 100!.
 */

namespace Praktika1KURSZada4a4___583
{
    class Program
    {
        /// <summary>
        /// основная функция
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            const int fact = 100;   //константа 100!
            string str = "1";   //работающая строка
            for (int i = 2; i <= fact; i++)    //цикл с факториалом
                str = factorial(str,i);
            Console.WriteLine(str);   //вывод строки
            Console.ReadLine();
        }

        /// <summary>
        /// факториал
        /// </summary>
        /// <param name="x">строка с текущим факториалом</param>
        /// <param name="y">элемент, на который происходит умножение</param>
        /// <returns></returns>
        static string factorial(string x, int y)
        {
            //объявление переменных
            int intPart = 0;
            int thatElem;
            string result = "";
            for (int i = x.Length - 1; i >= 0; i--)    //цикл по текущему числу
            {
                thatElem = Convert.ToInt32(x[i].ToString()) * y + intPart;   //идём с конца текущего числа факториала, умножая на элемент y и добавляя целую часть от прошлого шага цикла
                intPart = thatElem / 10;   //выделяем целую часть для добавления в число после или, в случае конца, в результат.
                result = Convert.ToString(thatElem % 10) + result;   //результат = окончание от thatElem + результат
            }

            if (intPart != 0) result = Convert.ToString(intPart % 10) + result;   //если целая часть != 0, то добавляем остаток от целой части в начало результата
            if (intPart > 9) result = Convert.ToString(intPart / 10) + result;    //если целая часть > 9, то добавляем от этого числа целую часть в начало результата
            return result;    //возвращаем результат
        }
    }
}
