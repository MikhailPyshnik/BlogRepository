using AutoMapper;
using Models.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.BlogService
{
    public class BlogServiceMapping : Profile
    {
        public BlogServiceMapping()
        {
            CreateMap<UPDBlogRequest, Blog>();
        }
    }
}
