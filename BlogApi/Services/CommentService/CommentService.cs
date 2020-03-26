using AutoMapper;
using DataBase.Repository;
using Models.Blog;
using Models.Comment;
using Models.Exeptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Blog> _blogresitory;
        private readonly IMapper _mapper;
        private Blog blogCurrent;

        public CommentService(IRepository<Blog> blogresitory, IMapper mapper)
        {
            _blogresitory = blogresitory;
            _mapper = mapper;
        }

        public async Task<CommentModel> CreateCommentAsync(UpdateCommentRequest commentRequest)
        {
            if (commentRequest.Text.Length >= 200)
            {
                throw new RequestException("Comment has length  more 200 symbols.");
            }

            var comment = _mapper.Map<UpdateCommentRequest, CommentModel>(commentRequest);
            var dateTimeNow = DateTime.Now;
            comment.CreatedOn = dateTimeNow;
            comment.UpdatedOn = dateTimeNow;

            comment.Id = RenerateStringID();
            blogCurrent.Commets.Add(comment);

            await _blogresitory.Update(blogCurrent.Id, blogCurrent);

            return comment;
        }

        public async Task DeleteCommentAsync(string commentId)
        {
            var result = blogCurrent.Commets;

            var currentCommnetId = result.Where(c => c.Id == commentId).FirstOrDefault();

            if (currentCommnetId == null)
            {
                throw new NotFoundException($"Not found comment by id - {commentId}");
            }

            blogCurrent.Commets.Remove(currentCommnetId);

            await _blogresitory.Update(blogCurrent.Id, blogCurrent);

        }

        public async Task<CommentModel> GetCommentAsync(string commentId)
        {
            var result = blogCurrent.Commets;
            var currentCommnetId = result.Where(c => c.Id == commentId).FirstOrDefault();

            if (currentCommnetId == null)
            {
                throw new NotFoundException($"Not found comment by id - {commentId}");
            }
            return currentCommnetId;
        }

        public async Task<IEnumerable<CommentModel>> GetCommentsAsync() 
        {
            var result = blogCurrent.Commets;

            if (result == null)
            {
                throw new NotFoundException($"Not found comment for blog.");
            }

            return result;
        }

        public async Task GetCurrentBlogAsync(string blogId)
        {
            blogCurrent = await _blogresitory.Get(blogId);
            if (blogCurrent == null)
            {
                throw new NotFoundException($"Not found blog.");
            }
        }

        public async Task<CommentModel> UpdateCommentAsync(string commentId, UpdateCommentRequest commentRequest)
        {
            var result = blogCurrent.Commets;
            if (commentRequest.Text.Length >= 200)
            {
                throw new RequestException("Comment has length  more 200 symbols.");
            }

            var currentCommnet = result.Where(c => c.Id == commentId).FirstOrDefault();

            int index = blogCurrent.Commets.IndexOf(currentCommnet);
            if (index < 0)
            {
                throw new NotFoundException($"Not found comment.");
            }

            var comment = _mapper.Map<UpdateCommentRequest, CommentModel>(commentRequest);
            comment.Id = commentId;
            comment.CreatedOn = currentCommnet.CreatedOn;
            comment.UpdatedOn = DateTime.Now;

            result.RemoveAt(index);
            result.Insert(index, comment);

            await _blogresitory.Update(blogCurrent.Id, blogCurrent);

            return comment;
        }

        private string RenerateStringID()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars,10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
