using DataBase.Context;
using DataBase.Repository;
using Microsoft.Extensions.Options;
using Models.Exeptions;
using Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApi.Api.Simple_API_for_Authentication
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userResitory;

        public UserService(IUserRepository userResitory)
        {
            _userResitory = userResitory;
        }

        // private DataContext _context;

        //private readonly ApplicationContext _context;

        //public UserService(IOptions<DataBase.Context.ApplicationSetting> settings)
        //{
        //    _context = new ApplicationContext(settings);
        //}

        //public UserService(DataContext context)
        //{
        //    _context = context;
        //}

        //public UserSimpl Authenticate(string username, string password)
        //{
        //    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        //        return null;

        //    var user = _context.Users.SingleOrDefault(x => x.Username == username);

        //    // check if username exists
        //    if (user == null)
        //        return null;

        //    // check if password is correct
        //    if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
        //        return null;

        //    // authentication successful
        //    return user;
        //}

        ////public IEnumerable<User> GetAll()
        ////{
        ////    return _userResitory.;
        ////}

        ////public User GetById(string email)
        ////{
        ////    return _context.Users.Find(id);
        ////}

        //public UserSimpl Create(UserSimpl user, string password)
        //{
        //    // validation
        //    if (string.IsNullOrWhiteSpace(password))
        //        throw new AppException("Password is required");

        //    if (_context.Users.Any(x => x.Username == user.Username))
        //        throw new AppException("Username \"" + user.Username + "\" is already taken");

        //    byte[] passwordHash, passwordSalt;
        //    CreatePasswordHash(password, out passwordHash, out passwordSalt);

        //    user.PasswordHash = passwordHash;
        //    user.PasswordSalt = passwordSalt;

        //    _context.Users.Add(user);
        //    _context.SaveChanges();

        //    return user;
        //}

        ////public void Update(User userParam, string password = null)
        ////{
        ////    var user = _context.Users.Find(userParam.Id);

        ////    if (user == null)
        ////        throw new AppException("User not found");

        ////    // update username if it has changed
        ////    if (!string.IsNullOrWhiteSpace(userParam.Username) && userParam.Username != user.Username)
        ////    {
        ////        // throw error if the new username is already taken
        ////        if (_context.Users.Any(x => x.Username == userParam.Username))
        ////            throw new AppException("Username " + userParam.Username + " is already taken");

        ////        user.Username = userParam.Username;
        ////    }

        ////    // update user properties if provided
        ////    if (!string.IsNullOrWhiteSpace(userParam.FirstName))
        ////        user.FirstName = userParam.FirstName;

        ////    if (!string.IsNullOrWhiteSpace(userParam.LastName))
        ////        user.LastName = userParam.LastName;

        ////    // update password if provided
        ////    if (!string.IsNullOrWhiteSpace(password))
        ////    {
        ////        byte[] passwordHash, passwordSalt;
        ////        CreatePasswordHash(password, out passwordHash, out passwordSalt);

        ////        user.PasswordHash = passwordHash;
        ////        user.PasswordSalt = passwordSalt;
        ////    }

        ////    _context.Users.Update(user);
        ////    _context.SaveChanges();
        ////}

        ////public void Delete(string email)
        ////{
        ////    var user = _context.Users.Find(id);
        ////    if (user != null)
        ////    {
        ////        _context.Users.Remove(user);
        ////        _context.SaveChanges();
        ////    }
        ////}

        //public async Task<UserSimpl> Authenticate(string username, string password)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<UserSimpl> Create(UserSimpl user1, string password)
        //{
        //    //if (await _userResitory.Exists(user1.Email, user1.Name) != null)
        //    //{
        //    //    throw new RequestException($"The user with email -{registerUser.Email} and user name -{registerUser.Name}  is created.");
        //    //}

        //    //var user = _mapper.Map<UserRegistrationModel, User>(registerUser);
        //    //await _userResitory.CreateUser(user);
        //    //return _mapper.Map<User, UserResponse>(user);
        //}

        //public async Task Delete(string email)
        //{
        //    if (await _userResitory.Find(email) == null)
        //    {
        //        throw new RequestException($"The user with email - {email}  not found.");
        //    }
        //    await _userResitory.Delete(email);
        //}


        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _userResitory.Find(username).Result;

           // var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash.ToArray(), user.PasswordSalt.ToArray()))
                return null;

            // authentication successful
            return user;
        }

        public async Task<User> Create(User user, string password)
        {
            if (await _userResitory.Exists(user.Email, user.Username) != null)
            {
                throw new RequestException($"The user with email -{user.Email} and user name -{user.Username}  is created.");
            }

            // var user = _mapper.Map<UserRegistrationModel, User>(registerUser);

            // validation
            if (string.IsNullOrWhiteSpace(password))
                    throw new AppException("Password is required");

                //if (_context.Users.Any(x => x.Username == user.Username))
                //    throw new AppException("Username \"" + user.Username + "\" is already taken");

                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = new List<byte>(passwordHash);  //passwordHash;
                user.PasswordSalt = new List<byte>(passwordSalt); //passwordSalt; new List<byte>(passwordSalt);

            //_context.Users.Add(user);
            //_context.SaveChanges();
            await _userResitory.CreateUser(user);

            return user;


            //await _userResitory.CreateUser(user);
            //return user;//_mapper.Map<User, UserResponse>(user);
        }

        public async Task Delete(string email)
        {
            if (await _userResitory.Find(email) == null)
            {
                throw new RequestException($"The user with email - {email}  not found.");
            }
            await _userResitory.Delete(email);
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

        public async Task<User> GetById(string name)
        {
           return  await  _userResitory.Find(name);
        }
    }
}
