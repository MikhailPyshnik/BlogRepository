using AutoMapper;
using BlogApi.Models.Comment;
using Models.Comment;

namespace Services.CommentService
{
    public class CommentServiceMapping : Profile
    {
        public CommentServiceMapping()
        {
            CreateMap<CreateComment, UpdateCommentRequest>();
            CreateMap<UpdateCommentRequest, CommentModel>();
            CreateMap<UpdateCommentRequest, CommentModel>();
            CreateMap<CommentModel, CommentResponce>();
        }
    }
}
