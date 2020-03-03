using Models.Blog;
using Models.Comment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.CommentService
{
    public interface ICommentService
    {
        //Task<IEnumerable<Comment>> GetCommentsAsync(string blogId);

        Task<IEnumerable<Comment>> GetCommentsAsync();

        Task<Comment> GetCommentAsync(string commentId);

        Task<Comment> CreateCommentAsync(UPDCommentRequest commentRequest);

        Task<Comment> UpdateCommentAsync(string commentId, UPDCommentRequest commentRequest);

        Task DeleteCommentAsync(string commentId);

        Task GetCurrentBlogAsync(string blogId);
    }
}
