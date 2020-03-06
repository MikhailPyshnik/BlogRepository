using Models.User;
using System.Threading.Tasks;

namespace Services.UserService
{
    public interface IUserService
    {
        Task<UserResponse> CreateUserAccount(UserRegistrationModel registerUser);

        Task<UserResponse> AuthenticateUser(LoginUserRequest loginUser);


        //Task<UserResponse> UpdateUserAccountAsync(UserRegistrationModel userRequest);

        Task DeleteUserAccountAsync(string email);

    }
}
