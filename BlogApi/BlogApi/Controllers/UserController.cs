using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlogApi.Api.Simple_API_for_Authentication;
using BlogApi.Autentification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.User;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private Api.Simple_API_for_Authentication.IUserService _userService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public UserController(
            Api.Simple_API_for_Authentication.IUserService userService,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _userService = userService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Email, model.Password).Result;

            if (user == null)
                return null; // BadRequest(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username) //user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token

            var rootData = new LoginResponse(tokenString, user.Username, user.Email);
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
        public  IActionResult Register([FromBody]RegisterModel model)
        {
            // map model to entity
            var user = _mapper.Map<User>(model);

            try
            {
                // create user
                _userService.Create(user, model.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var users = _userService.GetAll();
        //    var model = _mapper.Map<IList<UserModel>>(users);
        //    return Ok(model);
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    var user = _userService.GetById(id);
        //    var model = _mapper.Map<UserModel>(user);
        //    return Ok(model);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody]UpdateModel model)
        //{
        //    // map model to entity and set id
        //    var user = _mapper.Map<User>(model);
        //    user.Id = id;

        //    try
        //    {
        //        // update user 
        //        _userService.Update(user, model.Password);
        //        return Ok();
        //    }
        //    catch (AppException ex)
        //    {
        //        // return error message if there was an exception
        //        return BadRequest(new { message = ex.Message });
        //    }
        //}

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _userService.Delete(id);
            return Ok();
        }


        //private readonly IUserService _userService;
        ////private readonly IMapper _mapper;
        ////private readonly UserManager<User> _userManager;

        //public UserController(IUserService userService)
        //{
        //    _userService = userService;
        //}

        ////public UserController(IUserService userService, IMapper mapper, UserManager<User> userManager) : this(userService)
        ////{
        ////    _mapper = mapper;
        ////    _userManager = userManager;
        ////}

        //[AllowAnonymous]
        //[HttpPost("register")]
        //public async Task<ActionResult> Register([FromBody]UserRegistrationModel registerUser)
        //{


        //    //var block = await _blockService.CreateBlogAsync(blockRequest);

        //    //var uri = $"/blocks/{block.Id}";
        //    //return Created(uri, block);


        //    var responceUser =  await _userService.CreateUserAccount(registerUser);
        //    var uri = $"/blocks/{responceUser.UserName}";
        //    return Created(uri, responceUser);
        //    //return Ok();


        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return BadRequest(ModelState);
        //    //}

        //    //var user = _mapper.Map<User>(registerUser);

        //    //var result = await _userManager.CreateAsync(user, registerUser.Password);
        //    //if (!result.Succeeded)
        //    //{
        //    //    foreach (var error in result.Errors)
        //    //    {
        //    //        ModelState.TryAddModelError(error.Code, error.Description);
        //    //    }

        //    //    return Ok(string.Join(",", result.Errors?.Select(error => error.Description)));
        //    //}

        //    //await _userManager.AddToRoleAsync(user, "Visitor");

        //    //return RedirectToAction(nameof(HomeController.Index), "Home");
        //    //await _userService.CreateUserAccount(registerUser);
        //    //return Ok();
        //}

        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<ActionResult<UserResponse>> Login([FromBody] LoginUserRequest loginUser)
        //{
        //    var user = await _userService.AuthenticateUser(loginUser);

        //    return Ok(user);
        //}
    }
}