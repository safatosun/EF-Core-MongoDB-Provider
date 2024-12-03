using MongoDB.Bson;
using MongoDbProvider.Models;

namespace MongoDbProvider.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(ObjectId id);
        void AddUser(User user);
        void EditUser(User user);
        void DeleteUser(User user);
    }
}
