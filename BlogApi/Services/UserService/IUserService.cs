using BlogApi.Models.User;
using Models.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.UserService
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetByEmail(string email);

        Task<User> Authenticate(LoginUser loginUser);

        Task<UserRegistrationResponse> Create(UserRegistrationModel registerUser);

        Task Delete(string email);
    }
}
