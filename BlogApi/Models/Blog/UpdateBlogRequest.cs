using BlogApi.Models.Blog;

namespace Models.Blog
{
    public class UpdateBlogRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        //public BlogCategoryByEnum Category { get; set; } = BlogCategoryByEnum.None;
    }
}
