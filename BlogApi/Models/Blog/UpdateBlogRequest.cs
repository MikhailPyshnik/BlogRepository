using System.Collections.Generic;

namespace Models.Blog
{
    public class UpdateBlogRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
    }
}
