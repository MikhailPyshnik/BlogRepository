using Models.User;
using System.Threading.Tasks;

namespace Services.UserService
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAccount(RegisterUserRequest registerUser);

        Task<UserResponse> AuthenticateUser(LoginUserRequest loginUser);
    }
}
