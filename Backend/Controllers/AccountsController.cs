﻿using Microsoft.AspNetCore.Mvc;
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

        [AllowAnonymous]
        [HttpPost("Update/{accountId:guid}")]
        public async Task<IResult> UpdateAccount(Guid accountId, [FromBody]AccountModel accountModel)
        {
            try
            {
                var account = _accountMapper.Map(accountModel);

                if (account is null)
                {
                    return Results.BadRequest();
                }
                
                await _accountService.UpdateAsync(accountId, account);

                return Results.Ok();
            } catch (Exception ex)
            {
                _logger.LogError("Error from update account model", ex.StackTrace);
                return Results.BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("SetNewStockList/{accountId:guid}")]
        public async Task<IResult> SetNewStockListAsync(Guid accountId, [FromBody]StockListModel stockListModel)
        {
            var stockList = _stockListMapper.MapStockList(stockListModel);

            if (stockList is null)
            {
                return Results.BadRequest();
            }

            try
            {
                var account = await _accountService.GetAsync(accountId);
                account.StockList?.Add(stockList);
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
        [HttpPut("UpdateStockList/{accountId:guid}")]
        public async Task<IResult> UpdateStockListAsync(Guid accountId, [FromBody] StockListModel stockListModel)
        {
            var stockList = _stockListMapper.MapStockList(stockListModel);

            if (stockList is null)
            {
                return Results.BadRequest();
            }

            try
            {
                var account = await _accountService.GetAsync(accountId);
                var updatedStockList = account.StockList.Find(stockList =>  stockList.Id == stockList.Id);
                if (updatedStockList is null) {
                    return Results.BadRequest();
                }

                updatedStockList.Title = stockList.Title;
                updatedStockList.CalculationType = stockList.CalculationType;
                updatedStockList.Stocks.Clear();
                updatedStockList.IsNotificated = stockList.IsNotificated;
                updatedStockList.Stocks.AddRange(stockList.Stocks);

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
