using Backend.Models.Client;
using Backend.Models.Backend;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers
{
    [ApiController]
    [Route("userApi")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("GetUser")]
        public UserModel GetUser()
        {
            return new UserModel(Guid.NewGuid().ToString(), "saransk7@ya.ru", "dfnt", "1q3dbmw3Q!");
        }

        [HttpGet("GetUsers")]
        public async Task<List<UserModel>> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            var res = new List<UserModel>();
            foreach(var user in users)
            {
                res.Add(UserModel.From(user));
            }

            return res;
        }

        [HttpPost("CreateUser")]
        public async Task<UserModel> CreateUser(UserModel user)
        {
            await _userService.CreateUserAsync(Backend.Models.Backend.User.From(user));

            return user;
        }

    }
}
