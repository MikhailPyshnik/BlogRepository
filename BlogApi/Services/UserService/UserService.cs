using AutoMapper;
using BlogApi.Services.UserService;
using DataBase.Repository;
using Microsoft.IdentityModel.Tokens;
using Models.Exeptions;
using Models.User;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userResitory;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings; //= new AppSettings();

        public UserService(IUserRepository userResitory, IMapper mapper)
        {
            _userResitory = userResitory;
            _mapper = mapper;
        }

        public async Task<UserResponse> AuthenticateUser(LoginUserRequest loginUser)
        {
            var user = await _userResitory.Find(loginUser.Email);
            if (user == null)
            {
                throw new RequestException($"The user with email - {loginUser.Email} not found.");
            }

            //if (user.Password != loginUser.Password)
            //{
            //    throw new RequestException($"{user.UserName} - password is incorrect.");
            //}

            //https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api#users-controller-cs
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenToString = tokenHandler.WriteToken(token);

            var response = _mapper.Map<User, UserResponse>(user);
            response.Token = tokenToString;
            return response;
        }

        public async Task<UserResponse> CreateUserAccount(UserRegistrationModel registerUser)
        {
            if (await _userResitory.Exists(registerUser.Email, registerUser.Name) !=null)
            {
                throw new RequestException($"The user with email -{registerUser.Email} and user name -{registerUser.Name}  is created.");
            }

            var user = _mapper.Map<UserRegistrationModel, User>(registerUser);
            await _userResitory.CreateUser(user);
            return _mapper.Map<User, UserResponse>(user);
        }

        public async Task DeleteUserAccountAsync(string email)
        {
            if (await _userResitory.Find(email) == null)
            {
                throw new RequestException($"The user with email - {email}  not found.");
            }
            await _userResitory.Delete(email);
        }
    }
}
