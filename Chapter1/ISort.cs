using System;

namespace SoftwareDesign
{
    public interface ISort<T> where T : IComparable<T>
    {
        public bool InPlace { get; }

        public T[] Sort(T[] array, SortOrder order);
    }
}
