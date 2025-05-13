using System;
using System.Collections.Generic;
using System.Linq;

namespace ArithmeticCalculator
{
    public class SyntaxTreeBuilder(List<Token> tokens)
    {
        public SyntaxNode Build()
        {
            List<Token> reorderedTokens = ReorderTokens();
            Stack<SyntaxNode> stack = new();
            foreach (Token token in reorderedTokens)
            {
                if (token is NumberToken)
                {
                    stack.Push(new OperandNode(token));
                    continue;
                }
                if (token is OperatorToken)
                {
                    if (stack.Count >= 2)
                    {
                        SyntaxNode rightOperand = stack.Pop();
                        SyntaxNode leftOperand = stack.Pop();
                        SyntaxNode node = new OperatorNode(token, [leftOperand, rightOperand]);
                        stack.Push(node);
                        continue;
                    }
                    if (stack.Count == 1)
                    {
                        SyntaxNode operand = stack.Pop();
                        SyntaxNode node = new OperatorNode(token, [operand]);
                        stack.Push(node);
                        continue;
                    }
                }
                throw new InvalidSyntaxException();
            }
            if (stack.Count != 1)
            {
                throw new InvalidSyntaxException();
            }
            return stack.Pop();
        }

        private List<Token> ReorderTokens()
        {
            List<Token> reorderedTokens = [];
            Stack<Token> stack = new();
            foreach (Token token in tokens)
            {
                // case 1: operands
                if (token is NumberToken)
                {
                    reorderedTokens.Add(token);
                    continue;
                }
                // case 2: parentheses
                if (token is ParenthesisToken parenthesisToken)
                {
                    if (parenthesisToken.ParenthesisType == ParenthesisType.Open)
                    {
                        stack.Push(parenthesisToken);
                    }
                    else
                    {
                        bool valid = false;
                        while (stack.TryPeek(out Token? topToken))
                        {
                            if (topToken is ParenthesisToken topParenthesisToken
                                && topParenthesisToken.ParenthesisType == ParenthesisType.Open)
                            {
                                stack.Pop();
                                valid = true;
                                break;
                            }
                            reorderedTokens.Add(stack.Pop());
                        }
                        if (!valid)
                        {
                            throw new InvalidSyntaxException();
                        }
                    }
                    continue;
                }
                // case 3: operator
                if (token is OperatorToken operatorToken)
                {
                    while (stack.TryPeek(out Token? topToken)
                        && topToken is OperatorToken topOperatorToken)
                    {
                        if (topOperatorToken.GetPriority() < operatorToken.GetPriority())
                        {
                            break;
                        }
                        reorderedTokens.Add(topOperatorToken);
                        stack.Pop();
                        break;
                    }
                    stack.Push(operatorToken);
                    continue;
                }
                throw new InvalidSyntaxException();
            }
            while (stack.TryPop(out Token? topToken))
            {
                reorderedTokens.Add(topToken);
            }
            return reorderedTokens;
        }
    }

    public static class TokenExtensions
    {
        public static int GetPriority(this Token token)
        {
            if (token is not OperatorToken operatorToken)
            {
                return 0;
            }
            return operatorToken switch
            {
                { Operator: "+" } => 1,
                { Operator: "-" } => 1,
                { Operator: "*" } => 2,
                { Operator: "/" } => 2,
                _ => 0
            };
        }
    }
}
