using System;

namespace Zoo
{
    public class Animal
    {
        private readonly string _id;
        private readonly string _name;
        private readonly Species _species;

        public Animal(string id, string name, Species species)
        {
            _id = id;
            _name = name;
            _species = species;
        }

        public string Id => _id;

        public string Name => _name;

        public Species Species => _species;

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
