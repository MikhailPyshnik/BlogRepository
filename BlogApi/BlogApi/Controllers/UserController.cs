using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogApi.DataBase.ApplicationSetting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.User;
using Services.UserService;

namespace BlogApi.Controllers
{
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly AppSettings _appSettings;

        public UserController(IUserService userService, IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }


        /// <summary>
        /// Login user.
        /// </summary>
        /// <param name="loginUser">New user.</param>
        /// <returns>String.</returns>
        /// <response code="200">Return comment.</response>
        /// <response code ="400"> Not valid.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate([FromBody]LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.Authenticate(loginUser);

            if (user == null)
            {
                BadRequest(new { message = "Username or password is incorrect" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1440),//DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var rootData = new LoginResponse(tokenString, user.UserName, user.Email);
            return Ok(rootData);
        }


        /// <summary>
        /// Registration User.
        /// </summary>
        /// <param name="model">A new user.</param>
        /// <returns>A <see cref="CreatedAtActionResult"/>.</returns>
        /// <response code="201">Returns a new user.</response>
        /// <response code ="400"> Not valid.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody]UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.Create(model);

            var uri = $"/User/{user.UserName}";
            return Created(uri, user);
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>All available users.</returns>
        /// <response code="200">Return users.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        /// <summary>
        /// Gets a new password on email.
        /// </summary>
        /// <param name="email">Email to send a new password.</param>
        /// <returns>String.</returns>
        /// <response code="200">Return comment.</response>
        /// <response code="404">Not Found.</response>
        /// <response code="500">Server error.</response>
        [AllowAnonymous]
        [HttpPost("sendnewpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> GetNewPassword([FromBody] string email)
        {
            var password  = await _userService.SendNewPasswordForForgettenPassword(email);
            return Ok(password);
        }
    }
}