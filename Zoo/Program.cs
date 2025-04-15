using System;

namespace Zoo
{
    public static class Program
    {
        private static void Main()
        {
            Application application = new(Console.In);
            application.Start();
        }
    }
}
