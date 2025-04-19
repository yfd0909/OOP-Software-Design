using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SoftwareDesign
{
    public static class ArrayExtensions
    {
        public static string ToCollectionString<T>(this T[] array)
        {
            string innerString = string.Join(", ", array);
            return $"[{innerString}]";
        }

        public static void Swap<T>(this T[] array, int i, int j)
        {
            array.EnsureIndexRange(i);
            array.EnsureIndexRange(j);
            (array[i], array[j]) = (array[j], array[i]);
        }

        public static bool IsLeftSorted<T>(this T[] array, int i, IComparer<T> comparer)
        {
            array.EnsureIndexRange(i);
            if (i == 0)
            {
                return true;
            }
            return comparer.Compare(array[i - 1], array[i]) <= 0;
        }

        public static bool IsRightSorted<T>(this T[] array, int i, IComparer<T> comparer)
        {
            array.EnsureIndexRange(i);
            if (i + 1 == array.Length)
            {
                return true;
            }
            return comparer.Compare(array[i], array[i + 1]) <= 0;
        }

        public static bool IsSorted<T>(this T[] array, IComparer<T> comparer)
        {
            int bound = array.Length - 1;
            for (int i = 0; i < bound; i++)
            {
                if (!array.IsRightSorted(i, comparer))
                {
                    return false;
                }
            }
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void EnsureIndexRange<T>(this T[] array, int i)
        {
            if (i < 0 || array.Length <= i)
            {
                throw new IndexOutOfRangeException($"Index is out of range: {i}");
            }
        }
    }
}
