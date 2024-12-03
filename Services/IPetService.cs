using MongoDB.Bson;
using MongoDbProvider.Models;

namespace MongoDbProvider.Services
{
    public interface IPetService
    {
        IEnumerable<Pet> GetAllPets();
        Pet? GetPetById(ObjectId id);
        void AddPet(Pet pet);
        void EditPet(Pet pet);
        void DeletePet(Pet pet);
    }
}
