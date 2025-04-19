using System;
using System.Collections.Generic;

namespace SoftwareDesign.Implementation
{
    public sealed class SelectionSort<T> : ISort<T> where T : IComparable<T>
    {
        public bool InPlace => true;

        public T[] Sort(T[] array, SortOrder order)
        {
            Comparer<T> comparer = order.GetComparer<T>();
            int range = array.Length - 1;
            for (int i = 0; i < range; i++)
            {
                int pivot = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (comparer.Compare(array[j], array[pivot]) < 0)
                    {
                        pivot = j;
                    }
                }
                if (pivot != i)
                {
                    array.Swap(i, pivot);
                }
            }
            return array;
        }
    }
}
