using AspNetCore.Identity.Mongo.Model;
using System;
//using MongoDB.Bson;
//using MongoDB.Bson.Serialization.Attributes;

namespace BlogApi.Autentification
{
    public class ApplicationUser : MongoUser
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]

        //  public string Id { get; set; }

        //public string Email { get; set; }

        //public string Password { get; set; }

        public string Name { get; set; }

        //public string Name { get; set; }

        public string LastName { get; set; }

        public DateTime? Birthdate { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }
    }
}
