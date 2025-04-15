using System.Collections.Generic;

namespace Zoo
{
    public class AnimalCollection()
    {
        private readonly List<Animal> _animals = [];

        public bool AddAnimal(Animal animal)
        {
            if (_animals.Contains(animal))
            {
                return false;
            }
            _animals.Add(animal);
            return true;
        }

        public int AddAnimals(IEnumerable<Animal> animals)
        {
            int count = 0;
            foreach (Animal animal in animals)
            {
                if (AddAnimal(animal))
                {
                    count += 1;
                }
            }
            return count;
        }

        public bool RemoveAnimal(Animal animal)
        {
            return _animals.Remove(animal);
        }

        public IEnumerable<Animal> FindAllAnimals()
        {
            return [.. _animals];
        }

        public IEnumerable<Animal> FindAnimalsBySpecies(Species species)
        {
            List<Animal> results = [];
            foreach (Animal animal in _animals)
            {
                if (animal.Species == species)
                {
                    results.Add(animal);
                }
            }
            return results;
        }

        public IEnumerable<Animal> FindAnimalsByName(string name)
        {
            List<Animal> results = [];
            foreach (Animal animal in _animals)
            {
                if (animal.Name == name)
                {
                    results.Add(animal);
                }
            }
            return results;
        }
    }
}
