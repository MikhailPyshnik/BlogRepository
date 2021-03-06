﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.ApplicationSetting
{
    interface IBlogDBSettings
    {
        string BlogCollectionName { get; set; }
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
