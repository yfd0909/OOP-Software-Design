using System;

namespace Zoo
{
    public class Species
    {
        public string Value { get; }

        public Species(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(
                    "Cannot accept an empty or white space string for a species name.", nameof(value));
            }
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is not Species other)
            {
                return false;
            }
            return Value.Equals(other.Value);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString()
        {
            return Value;
        }

        public static implicit operator string(Species species) => species.Value;

        public static implicit operator Species(string species) => new(species);

        public static bool operator ==(Species x, Species y)
        {
            return x.Equals(y);
        }

        public static bool operator !=(Species x, Species y)
        {
            return !x.Equals(y);
        }
    }
}
