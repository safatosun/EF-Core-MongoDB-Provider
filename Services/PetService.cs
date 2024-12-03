using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDbProvider.Models;
using MongoDbProvider.Repositories;

namespace MongoDbProvider.Services
{
    public class PetService : IPetService
    {
        private readonly ApplicationDbContext _dbContext;

        public PetService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddPet(Pet pet)
        {
            var userToPet = _dbContext.Users.FirstOrDefault(u => u.Id == pet.UserId);
            if (userToPet is null)
                throw new Exception("The person to adopt the pet could not be found.");
            _dbContext.Pets.Add(pet);
            _dbContext.SaveChanges();
        }

        public void DeletePet(Pet pet)
        {
            var petToDelete = _dbContext.Pets.FirstOrDefault(p => p.Id == pet.Id);
            if (petToDelete is null)
                throw new Exception("The pet to delete cannot be found.");
            _dbContext.Pets.Remove(petToDelete);
            _dbContext.ChangeTracker.DetectChanges();
            Console.WriteLine(_dbContext.ChangeTracker.DebugView.LongView);
            _dbContext.SaveChanges();
        }

        public void EditPet(Pet pet)
        {
            var petToUpdate = _dbContext.Pets.FirstOrDefault(p => p.Id == pet.Id);
            if (petToUpdate is null)
                throw new Exception("The pet to update cannot be found.");
            petToUpdate.Name = pet.Name;
            petToUpdate.Species = pet.Species;
            petToUpdate.Age = pet.Age;
            petToUpdate.UserId = pet.UserId;
            _dbContext.Pets.Update(petToUpdate);
            _dbContext.SaveChanges();   
        }

        public IEnumerable<Pet> GetAllPets()
        {
            return _dbContext.Pets.AsNoTracking();  
        }

        public Pet? GetPetById(ObjectId id)
        {
            return _dbContext.Pets.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }
    }
}
