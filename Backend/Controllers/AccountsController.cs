using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Models.Client;
using Backend.Mappers;
using Backend.Services.AccountService;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountService;
        private readonly AccountMapper _mapper;

        public AccountsController(
            IAccountsService userService, 
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
            var account = await _accountService.GetAccountByLoginAsync(login);

            return _mapper.Map(account);
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

        public async Task SetNewStockList(StockListModel stockList)
        {
            
        }

    }
}
