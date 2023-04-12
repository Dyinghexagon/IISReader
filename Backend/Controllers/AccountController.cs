using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Models.Client;
using Backend.Services;
using Backend.Mappers;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly AccountMapper _mapper;

        public AccountController(
            AccountService userService, 
            AccountMapper mapper
        )
        {
            _accountService = userService;
            _mapper = mapper;
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
            var users = await _accountService.GetAllAsync();
            var userModels = new List<AccountModel?>();
            foreach(var user in users)
            {
                userModels.Add(_mapper.Map(user));
            }

            return userModels;
        }

    }
}
