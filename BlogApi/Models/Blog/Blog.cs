using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Models.Blog
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Title { get; set; }

        public string Text { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOn { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; }

       // public BlogCategoryByEnum Category { get; set; }

        public string Category { get; set; }

        public List<Models.Comment.CommentModel> Commets { get; set; }
    }
}
