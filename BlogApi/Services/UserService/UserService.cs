using AutoMapper;
using BlogApi.Models.Exceptions;
using BlogApi.Models.User;
using DataBase.Repository;
using Models.Exeptions;
using Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userResitory;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userResitory, IMapper mapper)
        {
            _userResitory = userResitory;
            _mapper = mapper;
        }

        public async Task<User> Authenticate(LoginUser loginUser)
        {
            if (string.IsNullOrEmpty(loginUser.Email) || string.IsNullOrEmpty(loginUser.Password))
            {
                throw new ServiceException("Login user is invalid (email or password).");
            }

            var user = await _userResitory.FindUser(loginUser.Email);

            // check if username exists
            if (user == null)
            {
                throw new ServiceException($"User with email : {loginUser.Email} - is not register.");
            }

            // check if password is correct
            if (!VerifyPasswordHash(loginUser.Password, user.PasswordHash.ToArray(), user.PasswordSalt.ToArray()))
            {
                throw new ServiceException($"User with email : {loginUser.Email} - is incorret password.");
            }

            // authentication successful
            return user;
        }

        public async Task<UserRegistrationResponse> Create(UserRegistrationModel registerUser)
        {
            if (await _userResitory.ExistsUser(registerUser.Email, registerUser.UserName) != null)
            {
                throw new RequestException($"The user with email -{registerUser.Email} and user name - {registerUser.UserName} is created.");
            }

            if (string.IsNullOrWhiteSpace(registerUser.Password))
            {
                throw new RequestException($"The user with email -{registerUser.Email} password is empty.");
            }

            //if (_context.Users.Any(x => x.Username == user.Username))
            //    throw new AppException("Username \"" + user.Username + "\" is already taken");

            var user = _mapper.Map<UserRegistrationModel, User>(registerUser);
            var dateTimeNow = DateTime.Now;
            user.CreatedOn = dateTimeNow;

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(registerUser.Password, out passwordHash, out passwordSalt);

            user.PasswordHash = new List<byte>(passwordHash);
            user.PasswordSalt = new List<byte>(passwordSalt); 

            await _userResitory.CreateUser(user);

            var responseUser = _mapper.Map<User, UserRegistrationResponse>(user);

            return responseUser;
            //return user;//_mapper.Map<User, UserResponse>(user);
        }

        public async Task Delete(string email)
        {
            if (await _userResitory.FindUser(email) == null)
            {
                throw new RequestException($"The user with email - {email}  not found.");
            }
            await _userResitory.DeleteUser(email);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return  await _userResitory.GetAllUsers();

            //var responseUser = _mapper.Map<User, UserResponceAllUsers>(tes);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userResitory.FindUser(email);
        }

        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
