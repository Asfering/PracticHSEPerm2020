using System;
using System.IO;

/*
 * Постановка задачи: Вася и Петя играют в следующую игру. Вася рисует в трёхмерном пространстве N точек и говорит число M. Петя должен ответить, можно ли соединить эти N точек с помощью M непересекающихся линий, так, чтобы выполнялись следующие условия:
   1.	Каждая линия соединяет ровно две точки.
   2.	Никакая линия не соединяет точку саму с собой.
   3.	Для любой пары точек существует не более одной соединяющей их линии.
   4.	Существуют такие две точки А и В, что из А нельзя добраться вдоль нарисованных линий до В (по каждой линии можно идти в любую сторону).
   Если Петя отвечает правильно, то он выигрывает, иначе выигрывает Вася.
   Ваша задача – написать программу, которая поможет Пете всегда выигрывать.
 */

namespace Test1Practic
{
    class Program
    {
        static void Main(string[] args)
        {
            //объявление переменных
            StreamReader sr = new StreamReader("input.txt");
            StreamWriter sw = new StreamWriter("output.txt");
            long size = Convert.ToInt64(sr.ReadLine());
            string line = "";
            while (!sr.EndOfStream)   //запись элементов в строку
                line += sr.ReadLine() + " ";
            sr.Close();   //закрываем запись из файла
            long[] newArray = new long[size * 2];    //массив для элементов
            int index = 0;
            foreach (var item in line.Split(new char[] { ' ', ',', ';', '!', '?', '.', ':' }, StringSplitOptions.RemoveEmptyEntries))   //достаем элементы из строки
            {
                newArray[index] = Convert.ToInt64(item);    //конверт в long
                index++;
            }

            //объявление N точек и M линий
            long[] N = new long[size];  
            long[] M = new long[size];

            for (int i = 0, j = 0; i < newArray.Length-1; i += 2, j++)    //заполняем N и M
            {
                N[j] = newArray[i];
                M[j] = newArray[i + 1];
            }

            for (int i = 0; i < N.Length; i++)    //функция проверки каждой пары
            {
                if((N[i] - 1) * (N[i] - 2) >= 2*M[i])    //если (N-1)*(N-2) >= 2M
                    sw.WriteLine("Yes");   //вывод "да"
                else 
                    sw.WriteLine("No");   //вывод "нет"
            }
            sw.Close();    //закрываем запись в файл
        }
    }
}
