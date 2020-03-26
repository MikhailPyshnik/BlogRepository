namespace BlogApi.Models.Blog
{
    public class CreateBlogRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

        // public BlogCategoryByEnum Category { get; set; } = BlogCategoryByEnum.None;
    }
}
