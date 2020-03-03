using Models.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public interface IUserRepository
    {
        Task<User> Find(string email);

        Task<User> Exists(string email, string userName);

        Task CreateUser(User user);
    }
}
