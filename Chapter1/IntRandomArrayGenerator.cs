using System;

namespace SoftwareDesign
{
    public static class IntRandomArrayGenerator
    {
        public static int[] Generate(int length, int min = 0, int max = int.MaxValue)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(length, nameof(length));
            Random random = new Random();
            int[] array = new int[length];
            for (int i = 0; i < length; i++)
            {
                array[i] = random.Next(min, max);
            }
            return array;
        }
    }
}
