using AutoMapper;
using Models.User;

namespace Services.UserService
{
    public class UserServiceMapping : Profile
    {
        public UserServiceMapping()
        {
            CreateMap<RegisterUserRequest, User>();
            CreateMap<User, UserResponse>();
        }
    }
}