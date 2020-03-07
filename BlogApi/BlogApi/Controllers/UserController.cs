using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BlogApi.Autentification;
using BlogApi.DataBase.ApplicationSetting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.User;
using Services.UserService;

namespace BlogApi.Controllers
{
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
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
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token

            var rootData = new LoginResponse(tokenString, user.UserName, user.Email);
            return Ok(rootData);

            //return Ok(new User
            //{
            //    Id = user.Id,
            //    Username = user.Username,
            //    FirstName = user.FirstName,
            //    LastName = user.LastName
            //    //Token = tokenString
            //});
        }

        [AllowAnonymous]
        [HttpPost("register")]
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var user = _userService.GetById(id);
        //    var model = _mapper.Map<UserModel>(user);
        //    return Ok(model);
        //  GetByEmail(string email);
        //}


        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _userService.Delete(id);
            return Ok();
        }
    }
}