using System.Text;

namespace ArithmeticCalculator
{
    public interface ITokenGeneratorStrategy
    {
        public bool CanGenerate(string expression, int index);

        public Token Generate(string expression, ref int index);
    }

    public class ParenthesisTokenGeneratorStrategy : ITokenGeneratorStrategy
    {
        public bool CanGenerate(string expression, int index)
        {
            char ch = expression[index];
            return ch == '(' || ch == ')';
        }

        public Token Generate(string expression, ref int index)
        {
            char ch = expression[index];
            ParenthesisType parenthesisType = ch == '('
                        ? ParenthesisType.Open
                        : ParenthesisType.Close;
            index++;
            return new ParenthesisToken(ch.ToString(), parenthesisType);
        }
    }

    public class OperatorTokenGeneratorStrategy : ITokenGeneratorStrategy
    {
        public bool CanGenerate(string expression, int index)
        {
            char ch = expression[index];
            return ch == '+' || ch == '-' || ch == '*' || ch == '/';
        }

        public Token Generate(string expression, ref int index)
        {
            char ch = expression[index];
            string stringValue = ch.ToString();
            index++;
            return new OperatorToken(stringValue, stringValue);
        }
    }

    public class NumberTokenGeneratorStrategy : ITokenGeneratorStrategy
    {
        public bool CanGenerate(string expression, int index)
        {
            char ch = expression[index];
            return char.IsDigit(ch);
        }

        public Token Generate(string expression, ref int index)
        {
            StringBuilder builder = new();
            char ch;
            while (index < expression.Length && char.IsDigit(ch = expression[index]))
            {
                builder.Append(ch);
                index++;
            }
            string stringValue = builder.ToString();
            int intValue = int.Parse(stringValue);
            return new NumberToken(stringValue, intValue);
        }
    }
}
