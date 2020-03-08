using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Models.User
{
    public class User 
    {      
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOn { get; set; }

        public List<byte> PasswordHash { get; set; }
        public List<byte> PasswordSalt { get; set; }
    }
}
