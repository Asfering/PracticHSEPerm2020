using System;
using System.Collections.Generic;
using System.Text;

namespace Praktika1KURSZada4a9___23
{
    public class DoubleItem<T>
    {
        public int Data { get; set; }
        public DoubleItem<int> Previous { get; set; }
        public DoubleItem<int> Next { get; set; }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public DoubleItem()
        {

        }

        /// <summary>
        /// Конструктор с датой
        /// </summary>
        /// <param name="data"></param>
        public DoubleItem(int data)
        {
            Data = data;
        }

        /// <summary>
        /// Перегрузка ToString
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
