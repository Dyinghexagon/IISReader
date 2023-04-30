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
        private readonly AccountMapper _accountMapper;
        private readonly StockListMapper _stockListMapper;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            IAccountsService userService, 
            AccountMapper accountMapper,
            StockListMapper stockListMapper,
            ILogger<AccountsController> logger
        )
        {
            _accountService = userService;
            _accountMapper = accountMapper;
            _stockListMapper = stockListMapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("GetAccount/{login}")]
        public async Task<AccountModel?> GetByLogin(String login)
        {
            var account = await _accountService.GetAccountByLoginAsync(login);

            return _accountMapper.Map(account);
        }

        [AllowAnonymous]
        [HttpGet("GetAccounts")]
        public async Task<List<AccountModel?>> GetAccounts()
        {
            var users = await _accountService.GetAllAsync();
            var userModels = new List<AccountModel?>();

            foreach(var user in users)
            {
                userModels.Add(_accountMapper.Map(user));
            }

            return userModels;
        }

        [HttpPost("SetNewStockList/{id:guid}")]
        public async Task<IActionResult> SetNewStockList(Guid id, [FromBody] StockListModel stockListModel)
        {
            var stockList = _stockListMapper.MapStockList(stockListModel);
            
            if (stockList is null)
            {
                return BadRequest();
            }

            try
            {
                var account = await _accountService.GetAsync(id);
                account.StockList?.Add(stockList);
                await _accountService.UpdateAsync(id, account);

                return Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error from add new stock list", ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

    }
}
