﻿using AutoMapper;
using BlogApi.Models.Blog;
using Models.Blog;
using System.Collections.Generic;

namespace Services.BlogService
{
    public class BlogServiceMapping : Profile
    {
        public BlogServiceMapping()
        {
            CreateMap<UpdateBlogRequest, Blog>()
                .ForMember(x => x.Category, x => x.MapFrom(y => y.Category.ToString()));
            CreateMap<CreateBlogRequest, Blog>()
                .ForMember(x => x.Category, x => x.MapFrom(y => y.Category.ToString()));
            CreateMap<IEnumerable<Blog>, List<BlogResponce>>();
        }
    }
}
