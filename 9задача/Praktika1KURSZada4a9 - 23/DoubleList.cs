using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Praktika1KURSZada4a9___23
{
    public class DoubleList<T> : IEnumerable<T>
    {
        public DoubleItem<int> Head { get; set; }   //начало списка
        public DoubleItem<int> Tail { get; set; }   //конец списка
        private int Count { get; set; }   //счётчик

        /// <summary>
        /// Счётчик элементов на вывод
        /// </summary>
        public int CountAll
        {
            get { return Count;}
        }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public DoubleList()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        /// <summary>
        /// Конструктор с массивом
        /// </summary>
        /// <param name="dataMas"></param>
        public DoubleList(params int[] dataMas)
        {
            if(dataMas.Length > 1)
                Permutation(0, 1, dataMas);   //сортировка
            AddMas(0,dataMas);   //добавление
        }

        /// <summary>
        /// Сортировка по возрастанию
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        private static int[] Permutation(int i, int j, params int[] array)
        {
            int temp = 0;
            if (array[i] > array[j])   //сортировка пузырьком
            {
                temp = array[i];
                array[i] = array[j];
                array[j] = temp;
            }
            if (j+1 == array.Length && i+2 != array.Length)
                Permutation(i + 1, i+1, array);   //рекурсия
            else if (i + 2 == array.Length && j + 1 == array.Length)
                return array;    //конец
            else
                Permutation(i, j + 1, array);    //рекурсия
            return array;
        }

        /// <summary>
        /// Добавление в массив
        /// </summary>
        /// <param name="i"></param>
        /// <param name="array"></param>
        private void AddMas(int i, params int[] array)
        {
            var item = new DoubleItem<int>(array[i]);
            if (Head == null)
                Head = item;    //добавление в голову
            else   //добавление после головы
            {
                Tail.Next = item;     //некст после ласт = item
                item.Previous = Tail;    //привиус перед айтем = tail
            }
            Tail = item;   //конец - item;
            Count++;   //+элемент
            if (i+1 < array.Length)
                AddMas(i+1, array);   //рекурсия
            else
                return;
        }

        /// <summary>
        /// Добавление
        /// </summary>
        /// <param name="data"></param>
        public void Add(int data)
        {
            var item = new DoubleItem<int>(data);
            if (Head == null)
            {
                Head = item;
            }
            else
            {
                Tail.Next = item;
                item.Previous = Tail;
            }
            Tail = item;
            Count++;
        }

        /// <summary>
        /// Булевая удаление на вывод
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete(int data)
        {
            if (DeletePrivate(Head, data)) return true;   //тру,фолс
            return false;
        }

        /// <summary>
        /// Булевая на удаление на приват
        /// </summary>
        /// <param name="current"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePrivate(DoubleItem<int> current, int data)
        {
            if (current.Data.Equals(data))   //если элемент найден
            {
                if (current.Next != null)   //некст элемент не нул
                    current.Next.Previous = current.Previous;   //привиус некст элемента = привиус текущий
                else Tail = current.Previous;   //конец списка - привиус текущий

                if (current.Previous != null)   //привиус элемент не нул
                    current.Previous.Next = current.Next;   //некст привиус элемента = нект текущий
                else
                    Head = current.Next;   //начало списка - некст текущий
                Count--;    //счётчик элементов минус
                return true;
            }
            else if (current.Next != null) if(DeletePrivate(current.Next, data)) return true;    //если не найден, то рекурсия
            return false;
        }

        /// <summary>
        /// Булевая на поиск на вывод
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Find(int data)
        {
            if (FindElement(Head, data)) return true;
            return false;
        }

        /// <summary>
        /// Булевая на поиск на приват
        /// </summary>
        /// <param name="current"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool FindElement(DoubleItem<int> current, int data)
        {
            if (current.Data.Equals(data))   //если элемент найден
                return true;
            else if (current.Next != null)   //если не найден, то рекурсия
                if (FindElement(current.Next, data)) return true;
            else if (current.Next == null) return false;   //если некст нулл = фолс
            return false;
        }

        /// <summary>
        /// Нумератор
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        /// <summary>
        /// Нумерейбл
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>)GetEnumerator();
        }

        /// <summary>
        /// Задний нумератор
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> BackEnumerator()
        {
            DoubleItem<int> current = Tail;
            while (current != null)
            {
                yield return current.Data;
                current = current.Previous;
            }
        }
    }
}