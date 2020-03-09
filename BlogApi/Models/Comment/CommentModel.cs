using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Models.Comment
{
    public class CommentModel
    {
        public string Text { get; set; }

        [BsonDateTimeOptions]
        public DateTime UpdatedOn { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; }
    }
}
