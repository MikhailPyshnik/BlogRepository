using AutoMapper;
using Models.User;
//using BlogApi.Autentification;

namespace Services.UserService
{
    public class UserServiceMapping : Profile
    {
        public UserServiceMapping()
        {
            CreateMap<UserRegistrationModel, User>();
            CreateMap<User, UserRegistrationModel>();
            CreateMap<User, UserResponse>();
        }
    }
}