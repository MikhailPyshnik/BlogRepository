using Models.Comment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.CommentService
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsAsync();

        Task<Comment> GetCommentAsync(string timeOfCreateId);

        Task<Comment> CreateCommentAsync(UpdateCommentRequest commentRequest);

        Task<Comment> UpdateCommentAsync(string timeOfCreateId, UpdateCommentRequest commentRequest);

        Task DeleteCommentAsync(string timeOfCreateId);

        Task GetCurrentBlogAsync(string blogId);
    }
}
