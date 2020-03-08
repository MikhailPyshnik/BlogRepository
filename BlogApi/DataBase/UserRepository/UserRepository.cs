using BlogApi.Models.Exceptions;
using DataBase.Context;
using Microsoft.Extensions.Options;
using Models.User;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(IOptions<DataBase.Context.ApplicationSetting> settings)
        {
            _context = new ApplicationContext(settings);
        }

        public async Task CreateUser(User user)
        {
            try
            {
                await _context.Users.InsertOneAsync(user);
            }
            catch
            {
                throw new MongoDBException($"Dont create user by email - {user.Email}.");
            }
        }

        public async Task UpdateUser(User user) //Task<User>
        {
            var udateUser = await FindUser(user.Email);

            UpdateResult actionResult;
            try
            {
                var filter = Builders<User>.Filter.Eq(s => s.Email, user.Email);
                var update = Builders<User>.Update
                                .Set(s => s.PasswordHash, user.PasswordHash)
                                .Set(s => s.PasswordSalt, user.PasswordSalt);
                                //
                                //.Set(s => s.Commets, obj.Commets)
                                //.CurrentDate(s => s.UpdatedOn);

                actionResult = await _context.Users.UpdateOneAsync(filter, update);
            }
            catch
            {
                throw new MongoDBException($"Dont update User by email - {user.Email}.");
            }

          //  return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                return await _context.Users.Find(user => true).ToListAsync();
            }
            catch
            {
                throw new MongoDBException($"Dont find all users.");
            }
        }

        public async Task<User> FindUser(string email)
        {
            try
            {
                var filter = Builders<User>.Filter.Eq("Email", email);
                return await _context.Users
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch
            {
                throw new MongoDBException($"Dont find user by email - {email}.");
            }
        }

        public async Task<User> ExistsUser(string email, string userName)
        {
            try
            {
                return await _context.Users.Find(exist => exist.Email == email || exist.UserName == userName).FirstOrDefaultAsync();
            }
            catch
            {
                throw new MongoDBException($"Exist user by email - {email} and useName - {userName}.");
            }
        }

        public async Task<bool> DeleteUser(string email)
        {
            try
            {
                DeleteResult actionResult;

                actionResult = await _context.Users.DeleteOneAsync(Builders<User>.Filter.Eq("Email", email));

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch
            {
                throw new MongoDBException($"Dont delete user by email - {email}.");
            }
        }
    }
}