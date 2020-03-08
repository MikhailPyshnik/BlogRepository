using AutoMapper;
using BlogApi.Models.User;
using Models.User;
using System.Collections.Generic;

namespace Services.UserService
{
    public class UserServiceMapping : Profile
    {
        public UserServiceMapping()
        {
            CreateMap<UserRegistrationModel, User>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UserResponceAllUsers>();
            CreateMap<User, UserRegistrationResponse>();
            CreateMap<IEnumerable<User>, IEnumerable<UserRegistrationResponse>>();
        }
    }
}