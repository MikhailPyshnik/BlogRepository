using BlogApi.Models.Blog;
using Models.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.BlogService
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogResponce>> GetBlogsAsync();

        Task<Blog> GetBlogAsync(string blogId);

        Task<Blog> CreateBlogAsync(CreateBlogRequest blogRequest);

        Task<Blog> UpdateBlogAsync(string blogId, UpdateBlogRequest blogRequest);

        Task<IEnumerable<Blog>> SearchByPartialTitleOccurrenceUserNameOrCategory(SearchBlogRequest search);

        Task DeleteBlogAsync(string blogId);
    }
}
