using System;
using System.Collections.Generic;

namespace SoftwareDesign.Implementation
{
    public class InsertionSort<T> : ISort<T> where T : IComparable<T>
    {
        public bool InPlace => true;

        public T[] Sort(T[] array, SortOrder order)
        {
            int length = array.Length;
            Comparer<T> comparer = order.GetComparer<T>();
            for (int i = 1; i < length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (!array.IsLeftSorted(j, comparer))
                    {
                        array.Swap(j - 1, j);
                    }
                }
            }
            return array;
        }
    }
}
