using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using SoftwareDesign.Implementation;

namespace SoftwareDesign
{
    public static class Program
    {
        private static void Main()
        {
            Application application = new(Console.In);
            application.Start();
        }
    }

    public static class SortAlgorithmFactory
    {
        public static IReadOnlyCollection<ISort<T>> Create<T>() where T : IComparable<T>
        {
            return [
                new BubbleSort<T>(),
                new SelectionSort<T>(),
                new InsertionSort<T>()
            ];
        }
    }

    public class Application
    {
        private readonly TextReader _reader;
        private readonly IReadOnlyList<ISort<int>> _sorts;

        public Application(TextReader reader)
        {
            _reader = reader;
            _sorts = [.. SortAlgorithmFactory.Create<int>()];
        }

        public void Start()
        {
            Console.WriteLine("Sort Algorithm Application");
            while (true)
            {
                PrintMenu();
                string query = AwaitInput();
                if (query == "q" || query == "Q")
                {
                    break;
                }
                if (!int.TryParse(query, out int index)
                    || index < 1 || _sorts.Count < index)
                {
                    Console.WriteLine("Unknown input. Try again.");
                    continue;
                }
                ISort<int> sort = _sorts[index];
                query = AwaitInput("Input the number of iteration.");
                int iteration;
                while (!int.TryParse(query, out iteration) || iteration < 1)
                {
                    Console.WriteLine("Unknown input. Try again.");
                    query = AwaitInput("Input the number of iteration.");
                }
                foreach (int it in Enumerable.Range(1, iteration))
                {
                    int[] array = IntRandomArrayGenerator.Generate(100);
                    int[] result = sort.Sort(array, SortOrder.Ascending);
                    if (!result.IsSorted(SortOrder.Ascending.GetComparer<int>()))
                    {
                        throw new ApplicationException($"Iteration {it} failed.");
                    }
                    Console.WriteLine($"Iteration {it} succeeded.");
                }
            }
        }

        private void PrintMenu()
        {
            foreach ((int index, ISort<int> sort) in _sorts.Index())
            {
                Console.WriteLine($"({index + 1}): {GetSortAlgorithmName(sort)}");
            }
            Console.WriteLine("(Q): Quit");
        }

        private string AwaitInput(string? query = null)
        {
            if (query is not null)
            {
                Console.WriteLine(query);
            }
            Console.Write(">>> ");
            string input = _reader.ReadLine() ?? throw new IOException("Cannot read a line.");
            return input.Trim();
        }

        private static string GetSortAlgorithmName(ISort<int> sort)
        {
            Type type = sort.GetType();
            StringBuilder builder = new();
            foreach (char ch in type.Name)
            {
                if (!char.IsAsciiLetter(ch))
                {
                    break;
                }
                if (char.IsUpper(ch))
                {
                    if (builder.Length != 0)
                    {
                        builder.Append(' ');
                    }
                }
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
