using Models.Comment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.CommentService
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentModel>> GetCommentsAsync();

        Task<CommentModel> GetCommentAsync(string timeOfCreateId);

        Task<CommentModel> CreateCommentAsync(UpdateCommentRequest commentRequest);

        Task<CommentModel> UpdateCommentAsync(string timeOfCreateId, UpdateCommentRequest commentRequest);

        Task DeleteCommentAsync(string timeOfCreateId);

        Task GetCurrentBlogAsync(string blogId);
    }
}
