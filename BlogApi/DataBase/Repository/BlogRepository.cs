using BlogApi.Models.Exceptions;
using DataBase.Context;
using Microsoft.Extensions.Options;
using Models.Blog;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class BlogRepository : IRepository<Blog>
    {
        private readonly ApplicationContext _context;

        public BlogRepository(IOptions<DataBase.Context.ApplicationSetting> settings)
        {
            _context = new ApplicationContext(settings);
        }

        public async Task Create(Blog obj)
        {
            try
            {
                await _context.Blogs.InsertOneAsync(obj);
            }
            catch
            {
                throw new MongoDBException($"Dont create blog with title - {obj.Title}.");
            }
        }

        public async Task<bool> Delete(string name)
        {
            DeleteResult actionResult;

            try
            {
                actionResult = await _context.Blogs.DeleteOneAsync(Builders<Blog>.Filter.Eq("Id", name));
            }
            catch
            {
                throw new MongoDBException($"Dont delete blog by id - {name}.");
            }
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<Blog> Get(string name)
        {
            try
            {
                var filter = Builders<Blog>.Filter.Eq("Id", name);
                return await _context.Blogs
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch
            {
                throw new MongoDBException($"Dont find blog by Id - {name}.");
            }
        }

        public async Task<IEnumerable<Blog>> GetAll()
        {
            return await _context.Blogs.Find(blog => true).ToListAsync();
        }

        public async Task<bool> Update(string id,  Blog obj)
        {
            UpdateResult actionResult;
            try
            {
                var filter = Builders<Blog>.Filter.Eq(s => s.Id, id);
                var update = Builders<Blog>.Update
                                .Set(s => s.Text, (string)obj.Text)
                                .Set(s => s.Title, obj.Title)
                                .Set(s => s.Commets, obj.Commets)
                                .CurrentDate(s => s.UpdatedOn);

                actionResult = await _context.Blogs.UpdateOneAsync(filter, update);
            }
            catch
            {
                throw new MongoDBException($"Dont update blog by id - {id}.");
            }

            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }
    }
}