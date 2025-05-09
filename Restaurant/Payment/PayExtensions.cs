namespace Restaurant.Payment
{
    public static class PayExtensions
    {
        public static uint GetStringHashCode(this string s)
        {
            int length = s.Length;
            uint hashCode = 0;
            for (int i = length - 1; i >= 0; i--)
            {
                hashCode *= 31;
                char ch = s[i];
                hashCode += ch;
            }
            return hashCode;
        }
    }
}
