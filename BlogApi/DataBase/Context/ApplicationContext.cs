using Microsoft.Extensions.Options;
using Models.Blog;
using Models.Comment;
using Models.User;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Context
{
    public class ApplicationContext
    {
        private readonly IMongoDatabase _database = null;

        public ApplicationContext(IOptions<ApplicationSetting> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("Users");
            }
        }

        public IMongoCollection<Blog> Blogs
        {
            get
            {
                return _database.GetCollection<Blog>("Blogs");
            }
        }
    }
}
