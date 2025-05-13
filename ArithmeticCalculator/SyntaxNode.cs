using System.Collections.Generic;

namespace ArithmeticCalculator
{
    public abstract record SyntaxNode(Token Token, List<SyntaxNode> ChildNodes)
    {
        public abstract int Evaluate();
    }

    public record OperatorNode(Token Token, List<SyntaxNode> ChildNodes) : SyntaxNode(Token, ChildNodes)
    {
        public override int Evaluate()
        {
            if (Token is not OperatorToken operatorToken)
            {
                throw new InvalidSyntaxException("unknown node evaluated: " + typeof(Token));
            }
            string operatorValue = operatorToken.Operator;
            switch (operatorValue)
            {
            case "+":
            {
                return ChildNodes switch
                {
                    [SyntaxNode unaryOperand] => unaryOperand.Evaluate(),
                    [SyntaxNode leftOperand, SyntaxNode rightOperand] =>
                        leftOperand.Evaluate() + rightOperand.Evaluate(),
                    _ => throw new InvalidSyntaxException()
                };
            }
            case "-":
            {
                return ChildNodes switch
                {
                    [SyntaxNode unaryOperand] => -unaryOperand.Evaluate(),
                    [SyntaxNode leftOperand, SyntaxNode rightOperand] =>
                        leftOperand.Evaluate() - rightOperand.Evaluate(),
                    _ => throw new InvalidSyntaxException()
                };
            }
            case "*":
            {
                return ChildNodes switch
                {
                    [SyntaxNode leftOperand, SyntaxNode rightOperand] =>
                        leftOperand.Evaluate() * rightOperand.Evaluate(),
                    _ => throw new InvalidSyntaxException()
                };
            }
            case "/":
            {
                return ChildNodes switch
                {
                    [SyntaxNode unaryOperand] => unaryOperand.Evaluate(),
                    [SyntaxNode leftOperand, SyntaxNode rightOperand] =>
                        leftOperand.Evaluate() / rightOperand.Evaluate(),
                    _ => throw new InvalidSyntaxException()
                };
            }
            default:
                throw new InvalidSyntaxException();
            }
        }
    }

    public record OperandNode(Token Token) : SyntaxNode(Token, [])
    {
        public override int Evaluate()
        {
            if (Token is not NumberToken numberToken)
            {
                throw new InvalidSyntaxException("unknown node evaluated: " + typeof(Token));
            }
            return numberToken.Value;
        }
    }
}
