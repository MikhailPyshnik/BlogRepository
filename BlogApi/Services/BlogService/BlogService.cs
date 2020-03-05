﻿using AutoMapper;
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

        public async Task<Blog> CreateBlogAsync(UPDBlogRequest blogRequest)
        {
            if (blogRequest.Text.Length >= 2)
            {
                throw new RequestException("Blog has length  more 2000 symbols.");
            }

            var blg = _mapper.Map<UPDBlogRequest, Blog>(blogRequest);
            blg.CreatedOn = DateTime.Now;
            blg.UpdatedOn = DateTime.Now;


            var blogs = await _repository.GetAll();

            if (blogs.Select(a => a.Title).Contains(blogRequest.Title))
            {
                throw new RequestException("Blog exists this title!");
            }

            await _repository.Create(blg);
            return blg;
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
            return await _repository.GetAll();
        }

        public async Task<Blog> UpdateBlogAsync(string blogId, UPDBlogRequest blogRequest)
        {
            if (blogRequest.Text.Length >= 2000)
            {
                throw new RequestException("Blog has length  more 2000 symbols.");
            }

            var blogResult = _mapper.Map<UPDBlogRequest, Blog>(blogRequest);
            blogResult.UpdatedOn = DateTime.Now;

            var res = await _repository.Update(blogId.ToString(), blogResult);
            if (!res)
            {
                throw new ResponseException("Blog has not update", 500);
            }
            return blogResult;
        }
    }
}
