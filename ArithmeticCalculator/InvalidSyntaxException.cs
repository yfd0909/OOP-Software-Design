using System;

namespace ArithmeticCalculator
{
    public class InvalidSyntaxException(string? message = null) : Exception(message);
}
