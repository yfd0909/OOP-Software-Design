using System;
using System.Collections.Generic;

namespace SoftwareDesign
{
    public sealed class BubbleSort<T> : ISort<T> where T : IComparable<T>
    {
        public bool InPlace => true;

        public T[] Sort(T[] array, SortOrder order)
        {
            int comparisonRange = array.Length - 1;
            int unsortedPosition = array.Length;
            Comparer<T> comparer = order.GetComparer<T>();
            while (unsortedPosition != 0)
            {
                unsortedPosition = 0;
                for (int i = 0; i < comparisonRange; i++)
                {
                    if (!array.IsRightSorted(i, comparer))
                    {
                        array.Swap(i, i + 1);
                        unsortedPosition++;
                    }
                }
            }
            return array;
        }
    }
}
