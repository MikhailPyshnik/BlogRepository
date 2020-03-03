using DataBase.Context;
using Microsoft.Extensions.Options;
using Models.User;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext _context;

        public UserRepository(IOptions<DataBase.Context.ApplicationSetting> settings)
        {
            _context = new ApplicationContext(settings);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Find(user => true).ToListAsync();
        }

        public async Task<User> Get(string name)
        {
            var filter = Builders<User>.Filter.Eq("Id", name);
            return await _context.Users
                            .Find(filter)
                            .FirstOrDefaultAsync();
        }

        public async Task Create(User obj)
        {
            await _context.Users.InsertOneAsync(obj);
        }

        public async Task<UpdateResult> Update(string id, string body)
        {
            var filter = Builders<User>.Filter.Eq(s => s.Id.ToString(), id);
            var update = Builders<User>.Update
                            .Set(s => s.Email, body)
                            .Set(s => s.Password, body)
                            .Set(s => s.UserName, body);
            // .CurrentDate(s => s.UpdatedOn);

            return await _context.Users.UpdateOneAsync(filter, update);
        }

        public async Task<DeleteResult> Delete(string name)
        {
            return await _context.Users.DeleteOneAsync(Builders<User>.Filter.Eq("Id", name));
        }

        public Task<bool> Update(string id, User obj)
        {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<User>.Delete(string name)
        {
            throw new NotImplementedException();
        }
    }
}
