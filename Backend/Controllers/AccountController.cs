using Backend.Models.Client;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Backend.Mappers;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Backend.Models.Options;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly AccountService _accountService;
        private readonly AccountMapper _mapper;
        private readonly String _secret;

        public AccountController(
            ILogger<AccountController> logger, 
            AccountService userService, 
            AccountMapper mapper,
            IOptions<RegistrationOptions> options
        )
        {
            _logger = logger;
            _accountService = userService;
            _mapper = mapper;
            _secret = options.Value.Secret;
        }

        [AllowAnonymous]
        [HttpGet("GetAccount/{login}")]
        public async Task<AccountModel?> GetByLogin(String login)
        {
           var user = await _accountService.GetAccountAsync(login);

            return _mapper.Map(user);
        }

        [AllowAnonymous]
        [HttpGet("GetAccounts")]
        public async Task<List<AccountModel?>> GetUsers()
        {
            var users = await _accountService.GetAccountsAsync();
            var res = new List<AccountModel?>();
            foreach(var user in users)
            {
                res.Add(_mapper.Map(user));
            }

            return res;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<AccountModel> CreateUser([FromBody]AccountModel account)
        {
            await _accountService.CreateAsync(account, account.Password);

            return account;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<String> Authenticate([FromBody]AccountModel accountModel)
        {
            var account = await _accountService.Authenticate(accountModel.Email, accountModel.Password);

            if (account == null)
            {
                return "";
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Id.ToString())
                }),
                Expires= DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

    }
}
