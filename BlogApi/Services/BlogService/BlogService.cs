using AutoMapper;
using DataBase.Repository;
using Models.Blog;
using System;
using System.Collections.Generic;
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

        public async Task<Blog> CreateBlogAsync(UPDBlogRequest blog)
        {
            var blg = _mapper.Map<UPDBlogRequest, Blog>(blog);
            blg.CreatedOn = DateTime.Now;
            blg.UpdatedOn = DateTime.Now;

            var blogs = await _repository.GetAll();

            await _repository.Create(blg);
            return blg;
        }

        public async Task DeleteBlogAsync(string blogId)
        {
            await _repository.Delete(blogId);
        }

        public async Task<Blog> GetBlogAsync(string blogId)
        {
            var blog = await _repository.Get(blogId);

            return blog;
        }

        public async Task<IEnumerable<Blog>> GetBlogsAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<Blog> UpdateBlogAsync(string blogId, UPDBlogRequest blogRequest)
        {
            var blogResult = _mapper.Map<UPDBlogRequest, Blog>(blogRequest);
            blogResult.UpdatedOn = DateTime.Now;

            var res = await _repository.Update(blogId.ToString(), blogResult);
            return blogResult;
        }
    }
}
