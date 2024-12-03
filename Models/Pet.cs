using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace MongoDbProvider.Models
{
    [Collection("pets")]
    public class Pet
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public int? Age { get; set; }
        public ObjectId UserId { get; set; }
    }
}
