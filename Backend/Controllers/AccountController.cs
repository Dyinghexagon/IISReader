using Backend.Models.Client;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Mappers;

namespace Backend.Controllers
{
    [ApiController]
    [Route("accountApi")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountService _userService;
        private readonly AccountMapper _mapper;

        public AccountController(
            ILogger<AccountController> logger, 
            AccountService userService, 
            AccountMapper mapper
        )
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("GetAccount/{id}")]
        public async Task<AccountModel?> GetUser(Guid userId)
        {
           var user = await _userService.GetAccountAsync(userId);

            return _mapper.Map(user);
        }

        [HttpGet("GetAccounts")]
        public async Task<List<AccountModel?>> GetUsers()
        {
            var users = await _userService.GetAccountsAsync();
            var res = new List<AccountModel?>();
            foreach(var user in users)
            {
                res.Add(_mapper.Map(user));
            }

            return res;
        }

        [HttpPost]
        public async Task<AccountModel> CreateUser([FromBody]AccountModel user)
        {
            await _userService.CreateAsync(_mapper.Map(user));

            return user;
        }

    }
}
