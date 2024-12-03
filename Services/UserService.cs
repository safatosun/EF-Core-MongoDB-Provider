using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDbProvider.Models;
using MongoDbProvider.Repositories;

namespace MongoDbProvider.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(User user)
        {
            var userToDelete = _dbContext.Users.Where(u=>u.Id == user.Id).FirstOrDefault();
            if (userToDelete is null)
                throw new Exception("The user to delete cannot be found.");
            _dbContext.Users.Remove(userToDelete);
            _dbContext.SaveChanges();
        }

        public void EditUser(User user)
        {
            var userToUpdate = _dbContext.Users.FirstOrDefault(u=> u.Id == user.Id );
            if (userToUpdate is null)
                throw new Exception("The user to delete cannot be found.");
            userToUpdate.Name = user.Name;
            userToUpdate.Surname = user.Surname;
            userToUpdate.Age = user.Age;
            _dbContext.Users.Update(userToUpdate);
            _dbContext.SaveChanges();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.OrderByDescending(c => c.Id).Take(20).AsNoTracking();
        }

        public User? GetUserById(ObjectId id)
        {
            return _dbContext.Users.FirstOrDefault(u=>u.Id == id);
        }
    }
}
