using AutoMapper;
using BlogApi.Models.Blog;
using DataBase.Repository;
using Models.Blog;
using Models.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.BlogService
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<Blog> _repository;
        private readonly IMapper _mapper;

        public BlogService(IRepository<Blog> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Blog> CreateBlogAsync(CreateBlogRequest blogRequest)
        {
            if (blogRequest.Text.Length >= 2000)
            {
                throw new RequestException("Blog has length  more 2000 symbols.");
            }

            var blog = _mapper.Map<CreateBlogRequest, Blog>(blogRequest);
            var dateTimeNow = DateTime.Now;
            blog.CreatedOn = dateTimeNow;
            blog.UpdatedOn = dateTimeNow;

            var blogs = await _repository.GetAll();

            if (blogs.Select(a => a.Title).Contains(blogRequest.Title))
            {
                throw new RequestException("Blog exists this title!");
            }

            await _repository.Create(blog);
            return blog;
        }

        public async Task DeleteBlogAsync(string blogId)
        {
            if (!await _repository.Delete(blogId))
            {
                throw new NotFoundException($"Not found blog by id ={blogId}");
            }
        }

        public async Task<Blog> GetBlogAsync(string blogId)
        {
            var blog = await _repository.Get(blogId);

            if (blog == null)
            {
                throw new NotFoundException($"Not found blog by id ={blogId}");
            }

            return blog;
        }

        public async Task<IEnumerable<Blog>> GetBlogsAsync()
        {
            var blogs = await _repository.GetAll();
            //var responseBlog = _mapper.Map<IEnumerable<Blog>, IEnumerable<BlogResponce>>(blogs);
            return blogs;
        }

        public async Task<Blog> UpdateBlogAsync(string blogId, UpdateBlogRequest blogRequest)
        {
            var blog = await  this.GetBlogAsync(blogId);

            if (blog == null)
            {
                throw new NotFoundException($"Not found blog by id ={blogId}");
            }

            if (blogRequest.Text.Length >= 2000)
            {
                throw new RequestException("Blog has length  more 2000 symbols.");
            }

            var blogResult = _mapper.Map<UpdateBlogRequest, Blog>(blogRequest);
            blogResult.UpdatedOn = DateTime.Now;

            var blogUpdate = await _repository.Update(blogId.ToString(), blogResult);
            if (!blogUpdate)
            {
                throw new ResponseException("Blog has not update", 500);
            }
            return blogResult;
        }

        public async Task<IEnumerable<Blog>> SearchByPartialTitleOccurrenceUserNameOrCategory(string search)
        {
            var allBlogs = await _repository.GetAll();

            if (String.IsNullOrEmpty(search))
            {
               throw new NotFoundException("Search string is null or empty");
            }

            var searchBlogs = allBlogs.Where(s => (s.Title.Contains(search))
                                          || (s.UserName.Contains(search))
                                          || (s.Category.Contains(search))).ToList();

            return searchBlogs;
        }
    }
}
