using Backend.Models.Client;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
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
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly String _secret;

        public AuthController(AccountService accountService, IOptions<RegistrationOptions> options)
        {
            _accountService = accountService;
            _secret = options.Value.Secret;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountModel account)
        {
            try
            {
                await _accountService.CreateAndPrepareAccountAsync(account);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Login([FromBody] AccountModel accountModel)
        {
            var account = await _accountService.Login(accountModel.Login, accountModel.Password);

            if (account == null)
            {
                return NotFound();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, account.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(tokenString);
        }
    }
}
