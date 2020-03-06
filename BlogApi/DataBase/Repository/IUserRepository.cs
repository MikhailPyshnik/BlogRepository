using Models.User;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public interface IUserRepository
    {
        Task<User> Find(string email);

        Task<User> Exists(string email, string userName);

        //Task<User> CreateUser(User user); //
        Task CreateUser(User user); //

        // Task<bool> UpdateUser(string email, User user);

        Task<bool> Delete(string email);
    }
}
