using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        //private readonly IMapper _mapper;
        //private readonly UserManager<User> _userManager;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //public UserController(IUserService userService, IMapper mapper, UserManager<User> userManager) : this(userService)
        //{
        //    _mapper = mapper;
        //    _userManager = userManager;
        //}

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]UserRegistrationModel registerUser)
        {


            //var block = await _blockService.CreateBlogAsync(blockRequest);

            //var uri = $"/blocks/{block.Id}";
            //return Created(uri, block);


            var responceUser =  await _userService.CreateUserAccount(registerUser);
            var uri = $"/blocks/{responceUser.UserName}";
            return Created(uri, responceUser);
            //return Ok();


            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //var user = _mapper.Map<User>(registerUser);

            //var result = await _userManager.CreateAsync(user, registerUser.Password);
            //if (!result.Succeeded)
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.TryAddModelError(error.Code, error.Description);
            //    }

            //    return Ok(string.Join(",", result.Errors?.Select(error => error.Description)));
            //}

            //await _userManager.AddToRoleAsync(user, "Visitor");

            //return RedirectToAction(nameof(HomeController.Index), "Home");
            //await _userService.CreateUserAccount(registerUser);
            //return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login([FromBody] LoginUserRequest loginUser)
        {
            var user = await _userService.AuthenticateUser(loginUser);

            return Ok(user);
        }
    }
}