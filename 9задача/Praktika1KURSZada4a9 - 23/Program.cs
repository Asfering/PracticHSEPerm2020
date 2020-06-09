using System;

/*
 * Постановка задачи: Выполнить задание, реализовав динамические структуры данных «вручную», без использования коллекций языка C#.
Напишите рекурсивный метод создания двунаправленного списка, в информационные поля элементов которого последовательно заносятся номера с 1 до N (N вводится с клавиатуры). 
Элементы включаются в список в порядке возрастания: в информационном поле первого элемента списка должно быть записано минимальное значение, 
а последнего элемента – максимальное. Разработайте рекурсивные методы поиска и удаления элементов списка.
 */

namespace Praktika1KURSZada4a9___23
{
    class Program
    {
        /// <summary>
        /// Основная функция
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.Write("Введите количество элементов N - ");   //Обращение к пользователю
            int n = 0;
            input:
            try{n = Convert.ToInt32(Console.ReadLine());}  //Ввод N
            catch
            {
                Console.WriteLine("Ошибка: Данные некорректные");   //Сообщение об ошибке
                goto input;    //Отправка на ввод
            }

            if (n < 1)    //Если размер < 1
            {
                Console.WriteLine("Ошибка: Количество элементов не может быть меньше 1");
                goto input;   //Отправка на ввод
            }
            int[] inputInt = new int[n];   //Массив с int
            string[] inputString = new string[n];   //Массив с string
            Console.WriteLine("Введите элементы в строку через пробел");   //Обращение к пользователю
            inputString:
            string inputNum = Console.ReadLine();   //Ввод строки
            inputString = inputNum.Split(' ');   //Массив элементов строки
            if (inputString.Length != n)
            {
                Console.WriteLine("Ошибка: Неверное количество символов в строке");     //Сообщение об ошибке
                goto inputString;   //Отправка на ввод строки
            }

            try
            {
                for (int i = 0; i < inputInt.Length; i++)
                    inputInt[i] = Convert.ToInt32(inputString[i]);    //Конвертация строки в INT
            }
            catch
            {
                Console.WriteLine("Ошибка: Неверно введена строка!");     //Сообщение об ошибке
                goto inputString;   //Отправка на ввод строки
            }

            DoubleList<int> dList = new DoubleList<int>(inputInt);    //Новый список

            int searchElement = 0;
            int deleteElement = 0;
            Search:
            Console.WriteLine("Введите элемент для поиска!");       //Обращение к пользователю
            try {searchElement = Convert.ToInt32(Console.ReadLine());}
            catch{
                Console.WriteLine("Ошибка: Данные некорректные");     //Сообщение об ошибке
                goto Search;   //Отправка на ввод
            }
            
            if (dList.Find(searchElement)) Console.WriteLine("Элемент найден!");   //Если элемент найден
            else Console.WriteLine("Элемента нет в списке");    //Если элемент не найден

            Delete:
            Console.WriteLine("Введите элемент для удаления");   //Обращение к пользователю
            try {deleteElement = Convert.ToInt32(Console.ReadLine());}
             catch {
                 Console.WriteLine("Ошибка: Данные некорректные");     //Сообщение об ошибке
                goto Delete;   //Отправка на ввод
            }

            if (dList.Delete(deleteElement)) Console.WriteLine("Элемент удалён!");   //Если элемент найден
            else Console.WriteLine("Элемента нет в списке");    //Если элемент не найден

            if (dList.CountAll == 0)
                Console.WriteLine("Список пуст");   //Если список пустой
            else    //Иначе вывод списка
            {
                Console.Write("Список - ");   
                foreach (int item in dList)
                    Console.Write($"{item} ");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
