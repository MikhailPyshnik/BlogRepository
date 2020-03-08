using Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> FindUser(string email);

        Task<User> ExistsUser(string email, string userName);

        Task CreateUser(User user);

        Task UpdateUser(User user);

        Task<bool> DeleteUser(string email);
    }
}
