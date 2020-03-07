using Models.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.BlogService
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetBlogsAsync();

        Task<Blog> GetBlogAsync(string blogId);

        Task<Blog> CreateBlogAsync(UpdateBlogRequest blogRequest);

        Task<Blog> UpdateBlogAsync(string blogId, UpdateBlogRequest blogRequest);

        Task DeleteBlogAsync(string blogId);
    }
}
