using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.ApplicationSetting
{
    class BlogDBSettings : IBlogDBSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string ArticleCollectionName { get; set; }
        public string UsersCollectionName { get; set; }
    }
}
