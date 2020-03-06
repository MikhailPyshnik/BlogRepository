using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Models.User
{
    public class User //: MongoUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        //[BsonId(IdGenerator = typeof(CounterIDGenerator))]
        public string Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName { get; set; }
    }
}
