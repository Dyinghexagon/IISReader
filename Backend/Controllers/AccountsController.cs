using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Models.Client;
using Backend.Mappers;
using Backend.Services.AccountService;
using Backend.Services.StockService;
using Backend.Models.Backend;

namespace Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountService;
        private readonly IActualStocksService _actualStocksService;
        private readonly AccountMapper _accountMapper;
        private readonly StockListMapper _stockListMapper;
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(
            IAccountsService accountService,
            IActualStocksService actualStocksService,
            AccountMapper accountMapper,
            StockListMapper stockListMapper,
            ILogger<AccountsController> logger
        )
        {
            _accountService = accountService;
            _actualStocksService = actualStocksService;
            _accountMapper = accountMapper;
            _stockListMapper = stockListMapper;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("GetAccount/{login}")]
        public async Task<AccountModel?> GetByLoginAsync(String login)
        {
            var account = await _accountService.GetAccountByLoginAsync(login);

            return _accountMapper.Map(account);
        }

        [AllowAnonymous]
        [HttpGet("GetAccounts")]
        public async Task<List<AccountModel?>> GetAccountsAsync()
        {
            var users = await _accountService.GetAllAsync();
            var userModels = new List<AccountModel?>();

            foreach(var user in users)
            {
                userModels.Add(_accountMapper.Map(user));
            }

            return userModels;
        }

        [AllowAnonymous]
        [HttpPost("Update/{accountId:guid}")]
        public async Task<IResult> UpdateAccountAsync(Guid accountId, [FromBody]AccountModel accountModel)
        {
            try
            {
                var account = _accountMapper.Map(accountModel);

                if (account is null)
                {
                    return Results.BadRequest();
                }
                
                await _accountService.UpdateAsync(accountId, account);

                return Results.Ok(account);
            } catch (Exception ex)
            {
                _logger.LogError("Error from update account model", ex.StackTrace);
                return Results.BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("SetNewStockList/{accountId:guid}/{isAddAllStocks:bool}")]
        public async Task<IResult> SetNewStockListAsync(Guid accountId, bool isAddAllStocks, [FromBody]StockListModel stockListModel)
        {
            var stockList = _stockListMapper.MapStockList(stockListModel);

            if (stockList is null)
            {
                return Results.BadRequest();
            }

            try
            {
                var account = await _accountService.GetAsync(accountId);

                if (isAddAllStocks)
                {
                    var allStock = await _actualStocksService.GetAllAsync();
                    stockList.Stocks.AddRange(allStock);
                }

                account.StockList.Add(stockList);
                await _accountService.UpdateAsync(accountId, account);

                return Results.Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error from add new stock list", ex.StackTrace);
                return Results.BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("UpdateStockList/{accountId:guid}")]
        public async Task<IResult> UpdateStockListAsync(Guid accountId, [FromBody]StockListModel stockListModel)
        {
            var stockList = _stockListMapper.MapStockList(stockListModel);

            if (stockList is null)
            {
                return Results.BadRequest();
            }

            try
            {
                var account = await _accountService.GetAsync(accountId);
                var stockListIndex = account.StockList.FindIndex(stockList => stockList.Id == stockListModel.Id);
                if (stockListIndex == -1)
                {
                    return Results.BadRequest();
                }

                account.StockList[stockListIndex].Stocks.Clear();
                account.StockList[stockListIndex].Stocks.AddRange(stockList.Stocks);
                account.StockList[stockListIndex].Title = stockListModel.Title;
                account.StockList[stockListIndex].CalculationType = stockListModel.CalculationType;
                account.StockList[stockListIndex].Ratio = stockListModel.Ratio;
                account.StockList[stockListIndex].IsNotificated = stockListModel.IsNotificated;
                
                await _accountService.UpdateAsync(accountId, account);

                return Results.Ok(account);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error from update stock list", ex.StackTrace);
                return Results.BadRequest(ex.Message);
            }
        }
    }
}
