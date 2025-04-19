using System;
using System.Collections.Generic;

namespace SoftwareDesign
{
    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public static class SortOrderExtensions
    {
        public static Comparer<T> GetComparer<T>(this SortOrder order)
            where T : IComparable<T>
        {
            return order switch
            {
                SortOrder.Ascending => Comparer<T>.Create((x, y) => x.CompareTo(y)),
                SortOrder.Descending => Comparer<T>.Create((x, y) => y.CompareTo(x)),
                _ => throw new ArgumentOutOfRangeException(nameof(order)),
            };
        }
    }
}
