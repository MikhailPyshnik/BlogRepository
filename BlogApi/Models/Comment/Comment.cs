using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Comment
{
    public class Comment
    {
        public string Text { get; set; } = string.Empty;

        public DateTime UpdatedOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public int UserId { get; set; }
    }
}
