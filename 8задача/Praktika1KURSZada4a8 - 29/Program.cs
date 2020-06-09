using System;
using System.Collections.Generic;

/*
 * Постановка задачи: Граф задан матрицей инцидентности. Найти в нём какой-либо пустой подграф из K вершин.
 */

namespace Praktika1KURSZada4a8___29
{
    class Program
    {
        /// <summary>
        /// Начальная функция
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //переменные
            Random rnd = new Random();
            int points = rnd.Next(2, 10);   //кол-во вершин
            int maxLines = (points * (points - 1)) / 2;   //максимум рёбер по формуле (n(n-1))/2
            int lines = rnd.Next(1, maxLines);    //кол-во  рёбер
            int findedK = rnd.Next(0, points);    //необходимое К вершин для пустого подграфа
            int temp = 0;    //пустая переменная
            int[,] matrix = new int[points, lines];   //объявление матрицы
            for (int i = 0; i < points; i++)   //заполнение матрицы нулями и единицами
                for (int j = 0; j < lines; j++)
                    matrix[i, j] = rnd.Next(0, 2);

            #region Работа с матрицей инциденций
            CreateMatrixWithOne(lines, ref matrix, points);    //Вызов функции - Необходимая проверка на только 2 единицы в столбце
            CreateMatrixOnlyTwoFirstNumbers(lines, points, ref matrix);   //Вызов функции - Проверка на равные столбцы, удаление второго+ столбца
            NeedFirstNumbers(lines, points, ref matrix);    //Вызов функции - Если единиц в столбце меньше 2, добавляем

            List<int> tempList = new List<int>();   //создание пустого листа для работы

            ReduceMatrixFirstPart(lines, points, ref tempList, ref matrix);    //Вызов функции - Удаление пустах столбцов из матрицы, ч.1. Поиск количества рёбер финальной матрицы

            int finalCountMatrix = tempList.Count / points;    //находим окончательное количество рёбер
            int[,] finalMatrix = new int[points, finalCountMatrix];   //новая матрица с окончательным количеством рёбер

            ReduceMatrixPartTwo(lines, points, ref matrix, ref finalMatrix, finalCountMatrix);    //Вызов функции - Удаление пустых столбцов из матрицы, ч.2 Заполнение новой матрицы
            PrintMatrix(finalCountMatrix, points, finalMatrix);   //выписывание окончательной матрицы инциденций пользователю
            #endregion

            #region Работа с поиском пустых подграфом в графе
            int[,] tempMas = new int[points, finalCountMatrix];    //Создание пустого массива для поиска вершин пустого подграфа

            FindPoints(finalCountMatrix, finalMatrix, ref tempMas, points);     //Вызов функции - Поиск вершин для выделения пустого подграфа

            List<int> indexes = new List<int>();    //Создание списка для вывода результата

            PreFinalWork(points, finalCountMatrix, tempMas, ref indexes);    //Вызов функции - Работа с записыванием необходимых вершин в список
            Overall(indexes, findedK);    //Вызов функции - Функция вывода результата
            #endregion

            Console.ReadLine();
        }

        /// <summary>
        /// Удаление пустых столбцов из матрицы, ч.2 Заполнение новой матрицы
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="points"></param>
        /// <param name="matrix"></param>
        /// <param name="finalMatrix"></param>
        /// <param name="finalCountMatrix"></param>
        static void ReduceMatrixPartTwo(int lines, int points, ref int[,] matrix, ref int[,] finalMatrix, int finalCountMatrix)
        {
            int k = 0;   //пустышка
            for (int j = 0; j < lines; j++)
            {
                //переменные
                int index = 0;
                int count = 0;
                while (index < points)   //идём до низу
                { 
                    if (matrix[index, j] == 1)   //нашли 1?
                    {
                        count++;
                        break;   //выход из цикла
                    }
                    index++;   //иначе +индекс
                }

                index = 0; 
                if (count != 0)    //нашли 1 -> записываем значения в финальную матрицу.
                { 
                    while (index < points && k < finalCountMatrix)    
                    {
                        finalMatrix[index, k] = matrix[index, j];     //записываем финальную матрицу без столбцов с нулями
                        index++;
                    }
                    k++;
                }
            }
        }

        /// <summary>
        /// Удаление пустах столбцов из матрицы, ч.1. Поиск количества рёбер финальной матрицы
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="points"></param>
        /// <param name="tempList"></param>
        /// <param name="matrix"></param>
        static void ReduceMatrixFirstPart(int lines, int points, ref List<int> tempList, ref int[,] matrix)
        {
            for (int j = 0; j < lines; j++)
            {
                //переменные
                int index = 0;
                int count = 0;
                while (index < points)   //идём до низу
                {
                    if (matrix[index, j] == 1)   //нашли 1?
                    {
                        count++;
                        break;   //выход из цикла
                    }
                    index++;   //иначе +индекс
                }

                index = 0;
                if (count != 0)   //нашли 1 -> записываем значения. Необходимо для поиска количества рёбер финальной матрицы.
                    while (index < points)
                    {
                        tempList.Add(matrix[index, j]);
                        index++;
                    }
            }
        }

        /// <summary>
        /// Проверка на равные столбцы, удаление второго+ столбца
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="points"></param>
        /// <param name="matrix"></param>
        static void CreateMatrixOnlyTwoFirstNumbers(int lines, int points, ref int[,] matrix)
        {
            //переменные
            int IndRemember = 0;
            int secondIndRemember = 0;
            int firstIndex = 0;
            for (int secIndex = 0; secIndex < lines - 1; secIndex++)    
            {
                //циклические переменные
                firstIndex = 0;
                int second = secIndex + 1;   
                while (matrix[firstIndex, secIndex] != 1 && firstIndex < points - 1)   //идём вниз
                    firstIndex++;
                IndRemember = firstIndex;    //запомнили индекс элемента равного 1
                if (firstIndex != points - 1)   //передаём значение индексу
                    firstIndex++;
                while (matrix[firstIndex, secIndex] != 1 && firstIndex < points - 1)    //идём далее вниз
                    firstIndex++;
                secondIndRemember = firstIndex;   //запомнили индекс элемента равного 1
                //итог предыдущих операций - запомнили индексы двух единиц в столбцах. Далее следует проверять, нет ли подобных столбцов. В случае нахождения таковых, данные  изменить
                while (second < lines - 1)   //идём до конца количества рёбер
                {
                    while ((matrix[IndRemember, second] != 1 || matrix[secondIndRemember, second] != 1) && second < lines - 1)    //идём до других единичек вправо по матрице
                        second++;
                    if (matrix[IndRemember, second] == 1 && matrix[secondIndRemember, second] == 1)   //если подобный первому столбец существует - обнулить.
                    {
                        matrix[IndRemember, second] = 0;
                        matrix[secondIndRemember, second] = 0;
                    }
                }
            }
        }

        /// <summary>
        /// Если единиц в столбце меньше 2, добавляем
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="points"></param>
        /// <param name="matrix"></param>
        static void NeedFirstNumbers(int lines, int points, ref int[,] matrix)
        {
            for (int secIndex = 0; secIndex < lines; secIndex++)    //цикл 1
            {
                //переменные
                int numbers = 0; 
                int index = 0;
                while (index < points)    //идём вниз
                {
                    numbers += matrix[index, secIndex];    //добавляем значения элементов
                    index++;
                }
                if (numbers == 1)    //если кол-во единиц = 1, то добавляем единичку
                    matrix[index - 1, secIndex] = 1;
            }   //конец цикла 1
        }

        /// <summary>
        /// Необходимая проверка на только 2 единицы в столбце
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="matrix"></param>
        /// <param name="points"></param>
        static void CreateMatrixWithOne(int lines, ref int[,] matrix, int points)
        {
            for (int secIndex = 0; secIndex < lines; secIndex++)
            {
                //переменные
                int numbers = 0;
                int index = 0;
                while (index < points)  //идём вниз
                {
                    numbers += matrix[index, secIndex];
                    if (numbers > 2)   //единичек больше, чем 1? заменяем на нули
                        matrix[index, secIndex] = 0;
                    index++;
                }

                while (numbers < 2)    //единичек меньше, чем 1? заменяем 0 на 1.
                {
                    index--;
                    if (matrix[index, secIndex] == 1)
                        index--;
                    matrix[index, secIndex] = 1;
                    numbers++;
                }
            }
        }

        /// <summary>
        /// Функция рисовки финальной матрицы
        /// </summary>
        /// <param name="finalCountMatrix"></param>
        /// <param name="points"></param>
        /// <param name="finalMatrix"></param>
        static void PrintMatrix(int finalCountMatrix, int points, int[,] finalMatrix)
        {
            Console.WriteLine();
            Console.Write("        ");
            for (int i = 0; i < finalCountMatrix; i++)   //выписываем номера рёбер
                Console.Write($"e{i + 1}  ");

            Console.WriteLine();
            for (int i = 0; i < points; i++)   //выписывание номера вершин
            {
                Console.Write($"v{i + 1} - ");
                for (int j = 0; j < finalCountMatrix; j++)    //выписываем элементы матрицы
                    Console.Write("{0,4}", finalMatrix[i, j]);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Работа с записыванием необходимых вершин в список
        /// </summary>
        /// <param name="points">Количество точек</param>
        /// <param name="finalCountMatrix">Количество рёбер</param>
        /// <param name="tempMas">Рабочий массив</param>
        /// <param name="indexes">Список</param>
        static void PreFinalWork(int points, int finalCountMatrix, int[,] tempMas, ref List<int> indexes)
        {
            int[] tempSoloMas = new int[points];

            for (int i = 0; i < points; i++)
                for (int j = 0; j < finalCountMatrix; j++)   //записываем элементы в массив одномерный
                    tempSoloMas[i] += tempMas[i, j];

            int max = Int32.MinValue;
            for (int i = 0; i < points; i++)
                if (tempSoloMas[i] > max)    //находим большее
                    max = tempSoloMas[i];

            for (int i = 0; i < points; i++)
                if (tempSoloMas[i] == max)   //если какая-то вершина = максимуму, то добавляем в рабочий список
                    indexes.Add(i);
        }

        /// <summary>
        /// Поиск вершин для выделения пустого подграфа
        /// </summary>
        /// <param name="finalCountMatrix">Количество рёбер</param>
        /// <param name="finalMatrix">Матрица отредактированная</param>
        /// <param name="tempMas">Матрица финальная</param>
        /// <param name="points">Количество вершин</param>
        static void FindPoints(int finalCountMatrix, int[,] finalMatrix, ref int[,] tempMas, int points)
        {
            for (int secondIndex = 0; secondIndex < finalCountMatrix; secondIndex++)   //цикл 1
            {
                //переменные
                int index = 0;
                int firstIndex = index + 1;
                int temp = 0;
                if (finalMatrix[index, secondIndex] == 0)   //если верхний элемент столбца матрицы  = 0
                {
                    tempMas[index, secondIndex] = 1;    //записываем как 1 данный элемент
                    while (firstIndex < points)    //цикл 2
                    {
                        //переменные
                        int second = 0;
                        int counter = 0;
                        if (finalMatrix[firstIndex, secondIndex] == 0)   //если текущий элемент = 0, то добавляем единицу
                            tempMas[firstIndex, secondIndex] = 1;

                        if (finalMatrix[firstIndex, secondIndex] == 1)   //если текущий элемент = 1, то проверяем, должен ли он быть включен в семейство вершин пустого подграфа
                            while (second < secondIndex)   //цикл 3
                            {
                                if (tempMas[firstIndex, second] == 1)
                                    counter++;
                                second++;
                            }    //конец цикла 3

                        if (finalMatrix[firstIndex, secondIndex] == 1 && temp < 1 && counter == secondIndex)    //если текущий элемент = 1, 1 первая в столбце и элемент может быть включен в семейство вершин пустого подграфа (в таком случае может и вторая в столбце)
                        {
                            tempMas[firstIndex, secondIndex] = 1;
                            temp++;
                        }
                        firstIndex++;
                    }   //конец цикла 2
                } 
                else   //если верхний элемент столбца матрицы  = 1
                {
                    tempMas[index, secondIndex] = finalMatrix[index, secondIndex];   //добавляем единицу в матрицу
                    while (firstIndex < points)    //цикл 3
                    {
                        if (finalMatrix[firstIndex, secondIndex] == 0)    //рассматриваем только 0
                            tempMas[firstIndex, secondIndex] = 1;    //заполняем элементы как 1
                        firstIndex++;
                    }    //конец цикла 3
                }
            }    //конец цикла 1
        }

        /// <summary>
        /// Функция вывода результата
        /// </summary>
        /// <param name="indexes">Список с вершинами</param>
        /// <param name="findedK">Количество вершин</param>
        static void Overall(List<int> indexes, int findedK)
        {
            int id = 0;
            Console.WriteLine($"Количество вершин K = {findedK}");   //выписываем количество K
            if (findedK == 0)
                Console.WriteLine("Пустых подграфов из нуля вершин нет");    //если К = 0
            else if (indexes.Count == findedK)
            {
                Console.Write($"\nНайден пустой подграф из {indexes.Count} вершин. \nВершины:");    //если кол-во вершин = К, то выписываем вершины и их количество
                for (int i = 0; i < indexes.Count; i++)
                    Console.Write($"v{indexes[i] + 1} ");
            }
            else if (indexes.Count > findedK)    //если кол-во вершин больше К, то выписываем первые элементы списка вершин
            {
                Console.Write($"\nНайден пустой подграф из {findedK} вершин. \nВершины:");
                while (id < findedK)
                {
                    Console.Write($"v{indexes[id] + 1} ");
                    id++;
                }
            }
            else
                Console.WriteLine($"Нет пустых подграфов с вершинами, равными {findedK} в графе");    //если кол-во вершин меньше К, то нет таких пустых подграфов
            Console.WriteLine();
        }
    }
}
