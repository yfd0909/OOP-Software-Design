using System;

namespace Zoo
{
    public class Animal(string id, string name, Species species)
    {
        public string Id { get; } = id;

        public string Name { get; } = name;

        public Species Species { get; } = species;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is not Animal other)
            {
                return false;
            }
            return Id == other.Id
                && Name == other.Name
                && Species == other.Species;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Species);
        }
    }
}
