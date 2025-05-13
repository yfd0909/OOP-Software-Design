using System;
using System.Collections.Generic;
using System.Linq;

namespace ArithmeticCalculator
{
    public class TokenGenerator(string expression, IEnumerable<ITokenGeneratorStrategy> tokenGeneratorStrategies)
    {
        public List<Token> Generate()
        {
            List<Token> tokens = [];
            for (int i = 0; i < expression.Length;)
            {
                if (char.IsWhiteSpace(expression[i]))
                {
                    i++;
                    continue;
                }
                ITokenGeneratorStrategy tokenGeneratorStrategy = tokenGeneratorStrategies.FirstOrDefault(strategy => strategy.CanGenerate(expression, i))
                    ?? throw new ArgumentException($"Cannot parse the expression: {expression}");
                Token token = tokenGeneratorStrategy.Generate(expression, ref i);
                tokens.Add(token);
            }
            return tokens;
        }
    }
}
