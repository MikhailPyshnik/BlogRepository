using AutoMapper;
using Models.Comment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.CommentService
{
    public class CommentServiceMapping : Profile
    {
        public CommentServiceMapping()
        {
            CreateMap<UPDCommentRequest, Comment>();
        }
    }
}
