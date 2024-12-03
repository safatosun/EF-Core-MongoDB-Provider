using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace MongoDbProvider.Models
{
    [Collection("users")]
    public class User
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
    }
}
