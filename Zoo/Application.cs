using System;
using System.IO;
using System.Linq;

namespace Zoo
{
    public class Application(TextReader textReader)
    {
        private readonly AnimalCollection _animalCollection = new();

        public void Start()
        {
            PrintMenu();
            for (ApplicationInputResult inputResult = ReadValue();
                !inputResult.IsRequestTermination();
                PrintMenu(), inputResult = ReadValue())
            {
                string value = inputResult.Value.ToLower();
                if (value == "p")
                {
                    PrintAnimals();
                }
                else if (value == "a")
                {
                    AddAnimals();
                }
                else
                {
                    NotifyUnknownInput();
                }
            }
        }

        private ApplicationInputResult ReadValue()
        {
            string value = textReader.ReadLine()
                ?? throw new ApplicationException("입력을 받을 수 없습니다.");
            return new ApplicationInputResult(value);
        }

        private void PrintMenu()
        {
            Console.WriteLine("P: 동물 정보 출력하기");
            Console.WriteLine("A: 동물 정보 등록하기");
            Console.WriteLine("Q: 종료");
        }

        private void PrintAnimals()
        {
            int animalCount = _animalCollection.FindAllAnimals().Count();
            if (animalCount == 0)
            {
                Console.WriteLine("아직 등록된 동물이 없습니다.");
                return;
            }
            foreach (Animal animal in _animalCollection.FindAllAnimals())
            {
                Console.WriteLine($"| 이름: {animal.Name}\t\t| 종: {animal.Species}\t\t |");
            }
        }

        private void AddAnimals()
        {
            Console.Write("동물의 이름을 입력해 주세요: ");
            string name = ReadValue();
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("정확한 동물의 이름을 입력해 주세요.");
                Console.Write("동물의 이름을 입력해 주세요: ");
                name = ReadValue();
            }
            Console.Write("동물의 종을 입력해 주세요: ");
            string species = ReadValue();
            while (string.IsNullOrWhiteSpace(species))
            {
                Console.WriteLine("정확한 동물의 종을 입력해 주세요.");
                Console.Write("동물의 종을 입력해 주세요: ");
                species = ReadValue();
            }
            string id = Guid.NewGuid().ToString();
            Animal animal = new(id, name, species);
            if (_animalCollection.AddAnimal(animal))
            {
                Console.WriteLine("동물 정보가 등록되었습니다.");
            }
            else
            {
                Console.WriteLine("중복된 동물 정보가 이미 등록돼 있습니다.");
            }
        }

        private void NotifyUnknownInput()
        {
            Console.WriteLine("해당하는 명령어를 찾을 수 없습니다.");
        }

        private class ApplicationInputResult(string value)
        {
            public string Value { get; } = value;

            public bool IsRequestTermination()
            {
                return Value.Equals("q", StringComparison.CurrentCultureIgnoreCase);
            }

            public static implicit operator string(ApplicationInputResult inputResult)
            {
                return inputResult.Value;
            }
        }
    }
}
