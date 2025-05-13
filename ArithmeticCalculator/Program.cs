using System;
using System.Collections.Generic;
using System.IO;

namespace ArithmeticCalculator
{
    public static class Program
    {
        private static readonly string[] TestCases = [
            "10 + 3 * 5 / (16 - 4)",
            "(17 + 10) * 3 / 9",
            "-2 + 5",
            "3 + (-1)"
        ];

        private static void Main(string[] args)
        {
            List<ITokenGeneratorStrategy> tokenGeneratorStrategies = [
                new OperatorTokenGeneratorStrategy(),
                new ParenthesisTokenGeneratorStrategy(),
                new NumberTokenGeneratorStrategy()
            ];
            foreach (string testCase in TestCases)
            {
                TokenGenerator tokenGenerator = new(testCase, tokenGeneratorStrategies);
                SyntaxTreeBuilder builder = new(tokenGenerator.Generate());
                SyntaxNode topNode = builder.Build();
                Console.WriteLine(topNode);
                Console.WriteLine($"Evaluate \"{testCase}\": {topNode.Evaluate()}");
            }
        }
    }
}
