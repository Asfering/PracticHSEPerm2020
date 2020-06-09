using System;
using System.Diagnostics;

/*
 * Постановка задачи: Выполнить сравнение двух предложенных методов сортировки одномерных массивов, содержащих n элементов, по количеству пересылок и сравнений.
 * Для этого необходимо выполнить программную реализацию двух методов сортировки, включив в нее подсчет количества пересылок (т.е. перемещений элементов с одного места на другое) и сравнений.
 * Провести анализ методов сортировки для трех массивов: упорядоченного по возрастанию, упорядоченного по убыванию и неупорядоченного. Все три массива следует отсортировать обоими методами сортировки.
 * Найти в литературе теоретические оценки сложности каждого из методов и сравнить их с оценками, полученными на практике.
 * Сделать выводы о том, насколько отличаются теоретические и практические оценки количества операций, объяснить почему это происходит.
 * Сравнить оценки сложности двух алгоритмов. Вариант задания определяется парой (X, Y), где X, Y – порядковые номера методов сортировки из приведенного списка: 
5. Сортировка с помощью двоичного дерева. 
6. Сортировка подсчётом. 

 */

namespace Praktika1KURSZada4a12___5_6
{
    class Program
    {
        #region EasySort

        /// <summary>
        /// Сортировка подсчётом
        /// </summary>
        /// <param name="array"></param>
        /// <param name="compare"></param>
        /// <param name="changes"></param>
        /// <returns></returns>
        static void EasySort(int[] array, ref int compare, ref int changes)
        {
            int[] count = new int[array.Length];   //Определяем массив элементов
            for (var i = 0; i < array.Length; i++)   
            {
                count[array[i]]++;   //Добавляем в массив те элементы, которые чаще встречаются. То есть, будь у нас строка вида 3 4 5 5 3 2 5, наш массив будет таков: count[3] = 2, count[5] = 3.
                compare++;   //добавляем сравнение, т.к. i<array.Length
            }

            compare++;   //Добавляем сравнение в случае i = array.length

            int index = 0;
            for (int i = 0; i < count.Length; i++)   //цикл до конца массива
            {
                for (int j = 0; j < count[i]; j++)    //видим какой-либо элемент, записываем его в полученный индекс
                {
                    if (array[index] != i)   //если '5' уже находится на этой позиции, то нет смысла его туда ставить. Иначе заходим
                    {
                        array[index] = i;    //ставим
                        index++;   //некст элемент
                        compare++;   //добавляем сравнение, т.к. array[index] != i
                        changes++;   //добавляем пересылку, т.к. элемент добавился
                    }
                    else   //не заходим
                    {
                        compare++;   //добавляем сравнение, т.к. array[index] == i
                        index++;   //изменяем индекс
                    }
                }
                compare++;   //добавляем сравнение, т.к. j = count[i]
            }
            compare++;  //добавялем сравнение, т.к. j = count.Length
        }
        #endregion

        static void Main(string[] args)
        {
            Console.Write("Размер массива: 100\n");
            int sizeMas = 100;

            Random rnd = new Random();

            //Объявление массивов
            int[] increaseArray = new int[sizeMas];
            int[] decreaseArray = new int[sizeMas];
            int[] standartArray = new int[sizeMas];
            //Присваивание значений элементам
            for (int i = 0; i < sizeMas; i++)
            {
                standartArray[i] = rnd.Next(0, sizeMas);
                increaseArray[i] = i;
                decreaseArray[i] = 99-i;
            }
            //Необходимые переменные для подсчёта
            int counterCompareStandart = 0, counterChangesStandart = 0;
            int counterCompareIncrease = 0, counterChangesIncrease = 0;
            int counterCompareDecrease= 0, counterChangesDecrease = 0;
            int treeCounterCompareStandart = 0, treeCounterChangesStandart = 0;
            int treeCounterCompareIncrease = 0, treeCounterChangesIncrease = 0;
            int treeCounterCompareDecrease = 0, treeCounterChangesDecrease = 0;
            
            //Вывод показателей неупорядоченного
            Console.WriteLine($"Массив из {sizeMas} элементов, неупорядоченный");
            Stopwatch MyTimeHasCome = new Stopwatch();
            MyTimeHasCome.Start();
            TreeWork(standartArray, ref treeCounterCompareStandart, ref treeCounterChangesStandart);
            MyTimeHasCome.Stop();
            Console.WriteLine("==========================================================================================================");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Бинарное дерево");
            Console.ResetColor();
            Console.WriteLine("Кол-во сравнений: " + treeCounterCompareStandart);
            Console.WriteLine("Кол-во пересылок: " + treeCounterChangesStandart);
            Console.WriteLine("Время: " +MyTimeHasCome.Elapsed.Ticks + " ticks");
            MyTimeHasCome.Reset();
            MyTimeHasCome.Start();
            EasySort(standartArray, ref counterCompareStandart, ref counterChangesStandart);
            MyTimeHasCome.Stop();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Подсчёт");
            Console.ResetColor();
            Console.WriteLine("Кол-во сравнений: " + counterCompareStandart);
            Console.WriteLine("Кол-во пересылок: " + counterChangesStandart);
            Console.WriteLine("Время: " + MyTimeHasCome.Elapsed.Ticks + " ticks");
            Console.WriteLine("==========================================================================================================\n");
            MyTimeHasCome.Reset();

            //Вывод показателей по возрастанию
            Console.WriteLine($"Массив из {sizeMas} элементов, по возрастанию");
            MyTimeHasCome.Start();
            TreeWork(increaseArray, ref treeCounterCompareIncrease, ref treeCounterChangesIncrease);
            MyTimeHasCome.Stop();
            Console.WriteLine("==========================================================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Бинарное дерево");
            Console.ResetColor();
            Console.WriteLine("Кол-во сравнений: " + treeCounterCompareIncrease);
            Console.WriteLine("Кол-во пересылок: " + treeCounterChangesIncrease);
            Console.WriteLine("Время: " + MyTimeHasCome.Elapsed.Ticks + " ticks");
            MyTimeHasCome.Reset();
            MyTimeHasCome.Start();
            EasySort(increaseArray, ref counterCompareIncrease, ref counterChangesIncrease);
            MyTimeHasCome.Stop();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Подсчёт");
            Console.ResetColor();
            Console.WriteLine("Кол-во сравнений: " + counterCompareIncrease);
            Console.WriteLine("Кол-во пересылок: " + counterChangesIncrease);
            Console.WriteLine("Время: " + MyTimeHasCome.Elapsed.Ticks + " ticks");
            Console.WriteLine("==========================================================================================================\n");
            MyTimeHasCome.Reset();

            //Вывод показателей по убыванию
            Console.WriteLine($"Массив из {sizeMas} элементов, по убыванию");
            MyTimeHasCome.Start();
            TreeWork(decreaseArray, ref treeCounterCompareDecrease, ref treeCounterChangesDecrease);
            MyTimeHasCome.Stop();
            Console.WriteLine("==========================================================================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Бинарное дерево");
            Console.ResetColor();
            Console.WriteLine("Кол-во сравнений: " + treeCounterCompareDecrease);
            Console.WriteLine("Кол-во пересылок: " + treeCounterChangesDecrease);
            Console.WriteLine("Время: " + MyTimeHasCome.Elapsed.Ticks + " ticks");
            MyTimeHasCome.Reset();
            MyTimeHasCome.Start();
            EasySort(decreaseArray, ref counterCompareDecrease, ref counterChangesDecrease);
            MyTimeHasCome.Stop();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Подсчёт");
            Console.ResetColor();
            Console.WriteLine("Кол-во сравнений: " + counterCompareDecrease);
            Console.WriteLine("Кол-во пересылок: " + counterChangesDecrease);
            Console.WriteLine("Время: " + MyTimeHasCome.Elapsed.Ticks + " ticks");
            Console.WriteLine("==========================================================================================================\n");
            MyTimeHasCome.Reset();
            Console.ReadKey();
        }


        /// <summary>
        /// Отправка в класc TreeSort
        /// </summary>
        /// <param name="array"></param>
        /// <param name="compare"></param>
        /// <param name="changes"></param>
        /// <returns></returns>
        public static int[] TreeWork(int[] array, ref int compare, ref int changes)
        {
            int index = 0;
            TreeSort tree = new TreeSort(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                compare++;
                tree.AddRecurs(new TreeSort(array[i]), ref compare, ref changes);
            }
            return tree.ToMas(ref compare,ref changes, index, ref array);
        }
    }

}
