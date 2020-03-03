using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Models.Comment;

namespace Models.Blog
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Title { get; set; }
        public string Text { get; set; } = string.Empty;

        [BsonDateTimeOptions]
        public DateTime UpdatedOn { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public List<Models.Comment.Comment> Commets { get; set; } = new List<Models.Comment.Comment>();
    }
}
