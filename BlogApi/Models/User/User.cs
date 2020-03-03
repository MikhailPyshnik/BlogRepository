using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.User
{
    public class User
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
