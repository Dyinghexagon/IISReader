using Backend.Models.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Backend.Models.Options;
using Backend.Services.AccountService;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountsService _accountService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAccountsService accountService,
            ILogger<AuthController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountModel account)
        {
            try
            {
                await _accountService.CreateAndPrepareAccountAsync(account);
                var token = GetJwtToken(account.Login);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in registration account method", ex.StackTrace);
                return BadRequest(ex.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IResult> Login([FromBody] AccountModel accountModel)
        {
            var account = await _accountService.Login(accountModel.Login, accountModel.Password);

            if (account == null)
            {
                return Results.Unauthorized();
            }

            var roken = GetJwtToken(account.Login);

            var response = new
            {
                acces_token = roken,
                login = account.Login
            };

            return Results.Json(response);
        }

        private static String GetJwtToken(String login)
        {
            var claims = new List<Claim>() { new Claim(ClaimTypes.Name, login) };
            var jwt = new JwtSecurityToken(
                issuer: RegistrationOptions.Issuer,
                audience: RegistrationOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
                signingCredentials: new(RegistrationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
