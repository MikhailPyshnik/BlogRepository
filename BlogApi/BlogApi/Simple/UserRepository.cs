using BlogApi.Models.Exceptions;
using DataBase.Context;
using Microsoft.Extensions.Options;
using Models.User;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BlogApi.Api.Simple_API_for_Authentication
{
    public class UserRepository :IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(IOptions<DataBase.Context.ApplicationSetting> settings)
        {
            _context = new ApplicationContext(settings);
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                await _context.Users.InsertOneAsync(user);
                return user;
            }
            catch
            {
                throw new MongoDBException($"Dont create user by email - {user.Email}.");
            }
        }

        public async Task<bool> Delete(string email)
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

        public async Task<User> Exists(string email, string userName)
        {
            try
            {
                return await _context.Users.Find(exist => exist.Email == email || exist.Username == userName).FirstOrDefaultAsync();
            }
            catch
            {
                throw new MongoDBException($"Exist user by email - {email} and useName - {userName}.");
            }
        }

        public async Task<User> Find(string email)
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

        //public async Task<UpdateResult> Update(string id, string body)
        //{
        //    var filter = Builders<UserSimpl>.Filter.Eq(s => s.Id.ToString(), id);
        //    var update = Builders<UserSimpl>.Update
        //                    .Set(s => s.Email, body)
        //                    .Set(s => s.Password, body)
        //                    .Set(s => s.UserName, body);

            //    return await _context.Users.UpdateOneAsync(filter, update);
            //}

            //public async Task<UserSimpl> Find(string email)
            //{
            //    try
            //    {
            //        var filter = Builders<UserSimpl>.Filter.Eq("Email", email);
            //        return await _context.Users
            //                        .Find(filter)
            //                        .FirstOrDefaultAsync();
            //    }
            //    catch
            //    {
            //        throw new MongoDBException($"Dont find user by email - {email}.");
            //    }
            //}

            //public async Task<UserSimpl> Exists(string email, string userName)
            //{
            //    try
            //    {
            //        return await _context.Users.Find(exist => exist.Email == email || exist.UserName == userName).FirstOrDefaultAsync();
            //    }
            //    catch
            //    {
            //        throw new MongoDBException($"Exist user by email - {email} and useName - {userName}.");
            //    }
            //}

            //public async Task CreateUser(UserSimpl user)
            //{
            //    try
            //    {
            //        await _context.Users.InsertOneAsync(user);
            //    }
            //    catch
            //    {
            //        throw new MongoDBException($"Dont create user by email - {user.Email}.");
            //    }
            //}

            //public async Task<bool> Delete(string email)
            //{
            //    try
            //    {
            //        DeleteResult actionResult;

            //        actionResult = await _context.Users.DeleteOneAsync(Builders<UserSimpl>.Filter.Eq("Email", email));

            //        return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            //    }
            //    catch
            //    {
            //        throw new MongoDBException($"Dont delete user by email - {email}.");
            //    }
            //}
        }
}
