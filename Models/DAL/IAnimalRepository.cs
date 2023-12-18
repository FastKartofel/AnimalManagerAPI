using PatientCareAPI.Models;

namespace PatientCareAPI.DAL
{
    public interface IAnimalRepository
    {
        IEnumerable<Animal> GetAnimals(string orderBy);

        Animal AddAnimal(Animal animal);

        void DeleteAnimal(int idAnimal);

        void ChangeAnimal(int idAnimal, Animal animal);
    }
}
