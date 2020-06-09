using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Постановка задачи: Задано содержимое информационных разрядов кодового слова кода Хэмминга. Добавить контрольные разряды и заполнить их.
 */
namespace Praktika1KURSZada4a7___22
{
    class Program
    {
        static void Main()
        {
            string nullString = "";
            List<char> listElements = new List<char>();    //Коллекция первых элементов
            List<char> nextElements = new List<char>();   //Коллекция новых элементов с X
            List<char> endElements = new List<char>();   //Коллекция финальная
            char[] elements = new char[nullString.Length];   //массив для символов

            Listed(ref nullString, ref elements, ref listElements);   //Вызов функции создания первой строки

            Console.WriteLine("Введённая строка - " + nullString); Console.WriteLine();   //Введённая строка

            for (int i = 0; i < listElements.Count; i++)   //Добавление для конечной коллекции
                endElements.Add(listElements[i]); 

            for (int i = 0; i < listElements.Count; i++)   //Добавление для XXX коллекции
                nextElements.Add(listElements[i]);

            //Заменяем первые два символа для удобства

            nextElements.Insert(0,'X');   
            nextElements.Insert(1, 'X');

            listElements.Insert(0, '0');
            listElements.Insert(1, '0');

            int numElement = 2;   //Константа для нумера

            for (int i = 4, j = 3; i < listElements.Count; i = (int) Math.Pow(2, j), j++)   //заменяем номера кодовые на X и 0 
            {
                listElements.Insert(i - 1, '0');
                nextElements.Insert(i - 1,'X');
                numElement++;
            }
            Console.WriteLine("Кол-во контрольных разрядов: " + numElement);   //Вывод контрольных значений
            PrintList(listElements); Console.WriteLine();   //Выписывание рабочего листа
            PrintList(nextElements); Console.WriteLine();   //Выписывание листа с XXX

            int size = 0;   //Константа размера 

            for (int i = 1, j = 0; i < listElements.Count; i = (int)Math.Pow(2, j), j++)   //Цикл для нахождения размера
                size = j+1;
            
            List<char>[] Str = new List<char>[size];   //Массив из коллекций символов
            
            for (int i = 1, j = 0; i < listElements.Count; i = (int)Math.Pow(2, j), j++)
            {
                List<char> emptyList = new List<char>();   //Новый элемент массива
                emptyList.Add(Convert.ToChar(j));   //Название массива
                Str[j] = emptyList;   //Добавление в массив
                Str[j].Clear();   //Очиска элементов в коллекциях  в массиве
            }

            Console.WriteLine();

            WorkWithStr(ref Str, ref listElements, ref numElement);   //Вызов функции работы со строками

            string[] tempNull = new string[0];

            WorkEnd(ref listElements, ref size, ref Str,ref numElement,out tempNull);

            for (int i = 1, j = 1, k = 0; i < endElements.Count; i = (int) Math.Pow(2, j), j++, k++)
                endElements.Insert(i-1,Convert.ToChar(tempNull[k]));   //Финальная строка
            
            Console.Write("Финальное состояние - ");   //Выписываем финальную строку
            foreach (var item in endElements)
                Console.Write(item);
            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// Выписывание массива
        /// </summary>
        /// <param name="list"></param>
        public static void PrintList(List<char> list)   //Функция для выписывание массива
        {
            for (int i = 0; i < list.Count; i++)
                Console.Write(list[i]);
        }

        /// <summary>
        /// Поиск контрольных элементов, финал
        /// </summary>
        /// <param name="listElements"></param>
        /// <param name="size"></param>
        /// <param name="Str"></param>
        /// <param name="numElement"></param>
        /// <param name="tempNull"></param>
        public static void WorkEnd(ref List<char> listElements, ref int size, ref List<char>[] Str, ref int numElement, out string[] tempNull)
        {
            int[] aElem = listElements.Select(ch => int.Parse(ch.ToString())).ToArray();   //Linq для записывания в массив строки с контрольными разрядами

            List<int[]>[] listInteger = new List<int[]>[size];   //Массив коллекций массивов типа int

            for (int i = 1, j = 0; i < listElements.Count; i = (int)Math.Pow(2, j), j++)   //Добавляем в массив коллекции с массивами типа int
            {
                int[] emptyInt = new int[listElements.Count];
                emptyInt[j] = j;
                List<int[]> emptyList = new List<int[]>();
                emptyList.Add(emptyInt);
                listInteger[j] = emptyList;
            }

            for (int i = 0; i < Str.Length - 1; i++)   //Добавление в массив элементов из Str - массива коллекций char
            for (int j = 0,  k = 0; j < listElements.Count; j++, k++)
                if (numElement >= i)
                {
                    int w = 0;
                    listInteger[i][w][k] = Convert.ToInt32(Str[i][j].ToString());
                }

            tempNull = new string[numElement];   // Массив для контрольных значений

            for (int j = 0; j < numElement; j++)
            {
                int temp = 0;
                for (int i = 0; i < listElements.Count; i++)
                {
                    int w = 0;
                    temp += listInteger[j][w][i] * aElem[i];   //работа с финальным кодом. Слева рабочие элементы, необходимые для нахождения кода Хэмминга. Например, 10101010 / 011001100 / 00011110000. Справа изначальная строка с контрольными разрядами равными нулю
                }
                if (temp > 1)   //если темп больше 1, то выписыванием 1
                    tempNull[j] = Convert.ToString(temp % 2);
                else   //иначе 0
                    tempNull[j] = Convert.ToString(temp);
                Console.WriteLine($"{j + 1} контрольный элемент - " + tempNull[j]);   //выписываем контрольные элементы
            }
        }

        /// <summary>
        /// Работа со строками
        /// </summary>
        /// <param name="Str"></param>
        /// <param name="listElements"></param>
        /// <param name="numElement"></param>
        public static void WorkWithStr(ref List<char>[] Str, ref List<char> listElements, ref int numElement)
        {
           //работа с первой строкой
            for (int i = 0; i < listElements.Count / 2; i++)
                Str[0].Add('0');
            for (int i = 0; i < listElements.Count; i += 2)
                Str[0].Insert(i, '1');
            foreach (var item in Str[0])   //выписывание первой строки
                Console.Write(item);
            Console.WriteLine();
            for (int j = 2; j < Str.Length; j++)   //работа с остальными строками
            {
                if (numElement >= j)
                {
                    for (int i = 0; i < listElements.Count; i++)    //выписывание нулей в элемент массива
                        Str[j - 1].Add('0');
                    for (int i = (int)Math.Pow(2, j - 1) - 1; i < listElements.Count; i += (int)Math.Pow(2, j))   //Каждые K символов вставлять единицу (01100110011 // 000111100001111)
                        for (int k = 0; k < (int)Math.Pow(2, j - 1); k++)
                            Str[j - 1].Insert(i + k, '1');

                    if (Str[j - 1].Count > Str[j - 2].Count)   //Обрезаем лишнее
                    {
                        int deletedIndex = Str[j - 2].Count;
                        while (Str[j - 1].Count != Str[j - 2].Count)
                            Str[j - 1].RemoveAt(deletedIndex);
                    }
                    else if (Str[j - 1].Count < Str[j - 2].Count)   //Не хватает - добавляем нуль.
                        Str[j - 1].Add('0');
                    foreach (var item in Str[j - 1])
                        Console.Write(item);
                }
                else break;
                Console.WriteLine();
            }

        }

        /// <summary>
        /// Запись начальной строки
        /// </summary>
        /// <param name="nullString"></param>
        /// <param name="elements"></param>
        /// <param name="listElements"></param>
        /// <returns></returns>
        public static List<char> Listed(ref string nullString, ref char[] elements, ref List<char> listElements)
        {
            bool ok = false;
            while (!ok)   //Ввод строки
            {
                listElements.Clear();
                Console.WriteLine("Введите числовую строку нулей и единиц");    //Обращение к пользователю
                nullString = Console.ReadLine();   //Ввод строки
                if (nullString != null)    //Если не нул
                {
                    elements = nullString.ToCharArray();   //Добавление в массив символов строки
                    for (int i = 0; i < elements.Length; i++)
                        if (elements[i] > 47 && elements[i] < 50)   //Если нуль или 1
                        {
                            listElements.Add(elements[i]);    //Добавление в первую коллекцию
                            ok = true;
                        }
                        else ok = false;
                }
            }

            return listElements;
        }
    }
}