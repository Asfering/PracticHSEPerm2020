using System;

/*
 * Постановка задачи: Даны действительные числа ai…a64. Получить действительную квадратную матрицу порядка 8, элементами которой являются числа ai…a64,
 * расположенное в ней по схеме, которая приведена на рисунке 5.1.
 */

namespace Praktika1KURSZada4a5___691v
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создание массивов
            int[] firstMas = new int[65];
            int[,] doubleMas = new int[8,8];
            for (int oIndex = 1; oIndex < 65; oIndex++)
                firstMas[oIndex] = oIndex;
            
            int allCount = 1;     //Все элементы
            int firstIndex = 0;    //Первый индекс
            int secondIndex = 0;   //Второй индекс
            int indexator = 1;   //Индекс 1-ого массива
            int counter = 0;   //Переменная для сравнения
            while (allCount != 65)  //Общий критерий для цикла
            {
                FirstWhileOnlyIndex(ref firstIndex, ref secondIndex, ref allCount, ref indexator, ref counter, ref firstMas, ref doubleMas);   //Вызов 1 функции
                SecondWhileUpToDown(ref firstIndex, ref secondIndex, ref allCount, ref indexator, ref counter, ref firstMas, ref doubleMas);   //Вызов 2 функции
                ThirdWhileOnlyJndex(ref firstIndex, ref secondIndex, ref allCount, ref indexator, ref counter, ref firstMas, ref doubleMas);   //Вызов 3 функции
                FourWhileDownToUp(ref firstIndex, ref secondIndex, ref allCount, ref indexator, ref counter, ref firstMas, ref doubleMas);   //Вызов 4 функции
            }    //Конец цикла


            //Выписывание элементов матрицы
            for (int oIndex = 0; oIndex < 8; oIndex++)
            {
                for (int jIndex = 0; jIndex < 8; jIndex++)
                    Console.Write("{0,3}", doubleMas[oIndex,jIndex]);
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        public static void FourWhileDownToUp(ref int firstIndex, ref int secondIndex, ref int allCount, ref int indexator, ref int counter, ref int[] firstMas, ref int[,] doubleMas)
        {
            while (firstIndex != 0 && secondIndex != 0 && firstIndex != 8 && secondIndex != 8)    //Четвёртый цикл. Заполняет внутренность матрицы снизу вверх
            {
                doubleMas[firstIndex, secondIndex] = firstMas[indexator];    //Добавление элемента
                //Работа с индексами
                firstIndex--;
                secondIndex++;
                allCount++;
                indexator++;
            }    //Конец цикла
        }

        public static void ThirdWhileOnlyJndex(ref int firstIndex, ref int secondIndex, ref int allCount, ref int indexator, ref int counter, ref int[] firstMas, ref int[,] doubleMas)
        {
            counter = allCount;   //Проверка на выход
            while (secondIndex == 0 || firstIndex > 7)                                                                    //Третий цикл. Проверяет низ и левую части матрицы
            {
                allCount += 1;   //Добавление к общему числу
                if (firstIndex > 7)   //Если кол-во строк > 7
                {
                    //Работа с индексами
                    while (firstIndex > 7)
                    {
                        secondIndex++;
                        firstIndex--;
                    }
                    doubleMas[firstIndex, secondIndex] = firstMas[indexator];    //Добавление элемента
                    //Работа с индексами
                    if (allCount - counter == 1 || allCount - counter == 2)
                    {
                        secondIndex++;
                        firstIndex -= 1;
                    }
                }
                else    //Иначе
                {
                    doubleMas[firstIndex, secondIndex] = firstMas[indexator];    //Добавление элемента
                    //Работа с индексами
                    firstIndex++;
                    if (allCount - counter == 2)
                    {
                        secondIndex++;
                        firstIndex -= 2;
                    }
                }
                indexator++;
            }    //Конец цикла
        }

        public static void SecondWhileUpToDown(ref int firstIndex, ref int secondIndex, ref int allCount, ref int indexator, ref int counter, ref int[] firstMas, ref int[,] doubleMas)
        {
            while (firstIndex != 0 && secondIndex != 0 && firstIndex != 8 && secondIndex != 8)    //Второй цикл. Заполняет внутренность матрицы сверху вниз
            {
                doubleMas[firstIndex, secondIndex] = firstMas[indexator];    //Добавление элемента
                //Работа с индексами
                allCount++;
                firstIndex++;
                secondIndex--;
                if (firstIndex == 8) secondIndex++;
                indexator++;
            }    //Конец цикла
        }

        public static void FirstWhileOnlyIndex(ref int firstIndex, ref int secondIndex, ref int allCount, ref int indexator, ref int counter, ref int[] firstMas, ref int[,] doubleMas)
        {
            counter = allCount;   //Проверка на выход
            while (firstIndex == 0 || secondIndex > 7)    //Первый цикл. Проверяет верх и правую части матрицы
            {
                allCount += 1;   //Добавление к общему числу
                if (secondIndex > 7)   //Если индекс столбца > 7
                {
                    //Работа с индексами
                    firstIndex++;
                    while (secondIndex > 7)
                    {
                        firstIndex++;
                        secondIndex--;
                    }
                    doubleMas[firstIndex, secondIndex] = firstMas[indexator];    //Добавление элемента
                    //Работа с индексами
                    if (allCount - counter == 1)
                    {
                        firstIndex++;
                        secondIndex -= 2;
                    }
                }
                else    //Иначе
                {
                    doubleMas[firstIndex, secondIndex] = firstMas[indexator];    //Добавление элемента
                    //Работа с индексами
                    if (allCount - counter == 2)
                    {
                        firstIndex++;
                        secondIndex -= 2;
                    }
                }
                secondIndex++;
                indexator++;
            }    //Конец цикла
        }
    }
}
