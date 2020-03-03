using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models.User;
using Services.UserService;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegisterUserRequest registerUser)
        {
            await _userService.CreateUserAccount(registerUser);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginUserRequest loginUser)
        {
            var user = await _userService.AuthenticateUser(loginUser);

            return Ok(user);
        }
    }
}