using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Models.Comment
{
    public class Comment
    {
        public string Text { get; set; } = string.Empty;

        [BsonDateTimeOptions]
        public DateTime UpdatedOn { get; set; }

        [BsonDateTimeOptions]
        public DateTime CreatedOn { get; set; }

        public string UserName { get; set; } = string.Empty;
    }
}
