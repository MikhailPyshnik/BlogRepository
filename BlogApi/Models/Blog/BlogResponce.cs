using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogApi.Models.Blog
{
    public class BlogResponce
    {
        public string Title { get; set; } = string.Empty;

        public DateTime UpdatedOn { get; set; }

        public string UserName { get; set; }
    }
}
