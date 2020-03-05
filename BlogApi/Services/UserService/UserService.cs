using AutoMapper;
using DataBase.Repository;
using Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userResitory;
        private readonly IMapper _mapper;

        public Task<UserResponse> AuthenticateUser(LoginUserRequest loginUser)
        {
            throw new NotImplementedException();
        }

        public Task<UserResponse> CreateUserAccount(RegisterUserRequest registerUser)
        {
            throw new NotImplementedException();
        }
    }
}
