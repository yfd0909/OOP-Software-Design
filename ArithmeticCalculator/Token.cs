namespace ArithmeticCalculator
{
    public abstract record Token(string Expression);

    public record NumberToken(string Expression, int Value) : Token(Expression);

    public record OperatorToken(string Expression, string Operator) : Token(Expression);

    public enum ParenthesisType
    {
        Open,
        Close
    }

    public record ParenthesisToken(
        string Expression,
        ParenthesisType ParenthesisType) : Token(Expression);
}
