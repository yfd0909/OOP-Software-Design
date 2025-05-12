using System;
using System.Collections.Generic;
using System.Text;

namespace ArithmeticCalculator
{
    public class TokenGenerator(string expression)
    {
        public List<Token> Generate()
        {
            List<Token> tokens = [];
            for (int i = 0; i < expression.Length;)
            {
                char ch = expression[i];
                if (char.IsWhiteSpace(ch))
                {
                    i++;
                    continue;
                }
                if (ch == '(' || ch == ')')
                {
                    ParenthesisType parenthesisType = ch == '('
                        ? ParenthesisType.Open
                        : ParenthesisType.Close;
                    tokens.Add(new ParenthesisToken(ch.ToString(), parenthesisType));
                    i++;
                    continue;
                }
                if (ch == '+' || ch == '-' || ch == '*' || ch == '/')
                {
                    string stringValue = ch.ToString();
                    tokens.Add(new OperatorToken(stringValue, stringValue));
                    i++;
                    continue;
                }
                if (char.IsDigit(ch))
                {
                    StringBuilder builder = new();
                    while (i < expression.Length && char.IsDigit(ch = expression[i]))
                    {
                        builder.Append(ch);
                        i++;
                    }
                    string stringValue = builder.ToString();
                    int intValue = int.Parse(stringValue);
                    tokens.Add(new NumberToken(stringValue, intValue));
                    continue;
                }
                throw new ArgumentException($"Cannot parse the expression: {expression}");
            }
            return tokens;
        }
    }
}
