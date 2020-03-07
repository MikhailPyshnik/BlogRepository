using AutoMapper;
using BlogApi.Models.User;
using Models.User;

namespace Services.UserService
{
    public class UserServiceMapping : Profile
    {
        public UserServiceMapping()
        {
            CreateMap<UserRegistrationModel, User>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UserResponceAllUsers>();
            CreateMap<User, UserResponceAllUsers>();
        }
    }
}