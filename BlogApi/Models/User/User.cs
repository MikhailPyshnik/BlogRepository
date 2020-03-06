using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Models.User
{
    public class User 
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]

        ////[BsonId(IdGenerator = typeof(CounterIDGenerator))]
        //public string Id { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }

        //public string UserName { get; set; }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public List<byte> PasswordHash { get; set; }
        public List<byte> PasswordSalt { get; set; }

        //public byte[] PasswordHash { get; set; }
        //public byte[] PasswordSalt { get; set; }
    }
}
