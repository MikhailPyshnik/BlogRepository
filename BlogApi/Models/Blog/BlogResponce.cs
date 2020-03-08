using System;

namespace BlogApi.Models.Blog
{
    public class BlogResponce
    {
        public string Title { get; set; }

        public DateTime UpdatedOn { get; set; }

        public BlogCategoryByEnum Category { get; set; }

        public string UserName { get; set; }
    }
}
