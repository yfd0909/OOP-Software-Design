using System;

namespace Restaurant
{
    public static class Optional
    {
        public static Optional<T> Of<T>(T value)
        {
            return new Optional<T>(value);
        }

        public static Optional<T> AsOptional<T>(this T? value) where T : class
        {
            return value == null
                ? Optional<T>.Empty
                : new Optional<T>(value);
        }
    }

    public class Optional<T>(T? value)
    {
        public static Optional<T> Empty { get; } = new(default);

        public bool HasValue => this != Empty;

        public T Value => this == Empty
            ? throw new EmptyValueException()
            : value!;

        public T OrElse(T replacement)
        {
            if (this == Empty)
            {
                return replacement;
            }
            return value!;
        }

        public T OrElseGet(Func<T> replacementGenerator)
        {
            return value ?? replacementGenerator();
        }

        public Optional<T> Filter(Predicate<T> predicate)
        {
            if (this == Empty)
            {
                return Empty;
            }
            return predicate(value!) ? this : Empty;
        }

        public Optional<TResult> Map<TResult>(Func<T, TResult> selector)
        {
            if (this == Empty)
            {
                return Optional<TResult>.Empty;
            }
            TResult result = selector(value!);
            return new Optional<TResult>(result);
        }

        public Optional<TResult> FlatMap<TResult>(Func<T, Optional<TResult>> selector)
        {
            if (this == Empty)
            {
                return Optional<TResult>.Empty;
            }
            return selector(value!);
        }

        public Optional<TResult> Merge<U, TResult>(Optional<U> other, Func<T, U, TResult> merger)
        {
            if (this == Empty || other == Optional<U>.Empty)
            {
                return Optional<TResult>.Empty;
            }
            TResult result = merger(value!, other.Value);
            return new Optional<TResult>(result);
        }

        public Optional<TResult> FlatMerge<U, TResult>(Optional<U> other, Func<T, U, Optional<TResult>> merger)
        {
            if (this == Empty || other == Optional<U>.Empty)
            {
                return Optional<TResult>.Empty;
            }
            return merger(value!, other.Value);
        }

        public override string ToString()
        {
            if (this == Empty)
            {
                return "Optional.Empty";
            }
            return $"Optional(Value = {value})";
        }
    }

    public class EmptyValueException(string? message = null) : Exception(message);
}
