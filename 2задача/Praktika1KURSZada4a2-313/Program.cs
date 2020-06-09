using System;
using System.Runtime.Serialization;
using System.IO;
using System.Linq;

/*
 * Постановка задачи: На автобусную остановку каждую минуту подходит автобус одного из маршрутов. Диспетчерская служба собрала данные за N минут – номера маршрутов каждого автобуса. 
Требуется определить максимально возможное время ожидания для пассажира, желающего уехать определенным маршрутом. 
Т.е. в данной последовательности номеров маршрутов нужно найти два самых удаленных числа, равных между собой, между которыми нет равных им. 
 */
namespace Praktika1KURSZada4a2_313
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");   //Объявление записи с файла
            StreamWriter sw = new StreamWriter("output.txt");    //Объявление записи в файл
            string lineReader = "";
            try
            {
                int sizeInput = Convert.ToInt32(sr.ReadLine());    //1 элемент в файле - размер массива

                while (!sr.EndOfStream)    //Добавляем элементы из файла в строку
                    lineReader = sr.ReadLine();
                sr.Close();    //Закрытие записи

                string[] array = new string[sizeInput];   //Создаем массив string
                array = lineReader.Split(' ');   //Добавляем элементы из строки в массив string
                int[] ArrInt = new int[sizeInput];            //Создаем массив int
                for (int i = 0; i < sizeInput; i++)             //Конвертируем string в int
                    ArrInt[i] = Convert.ToInt32(array[i]);
                sw.WriteLine(Convert.ToString(Massive(ArrInt))); //Запись вывода в файл
                sw.Close();   //Закрытие записи
                Console.WriteLine("Данные записаны в Output.exe");   //Сообщение пользователю
                Console.ReadLine();
            }
            catch
            {
                sw.WriteLine("Неверный формат входных данных");   //Сообщение пользователю
                sw.Close();   //Закрытие записи
            }

            Console.ReadLine();
        }

        public static int Massive(int[] array)    //Функция для проверки схожести и возвращения большего времени
        {
            int counterForArray = 0;
            int counterFinally = 0;
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = i + 1; j < array.Length; j++)
                    if (array[i] == array[j])
                    {
                        counterForArray = j - i;
                        break;
                    }
                
                if (counterForArray > counterFinally)
                    counterFinally = counterForArray;
            }

            return counterFinally;
        }
    }
}
