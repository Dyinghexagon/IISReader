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
        private readonly AccountMapper _mapper;
        private readonly Logger<AccountsController> _logger;
        public AccountsController(
            IAccountsService userService, 
            AccountMapper mapper,
            Logger<AccountsController> logger
        )
        {
            _accountService = userService;
            _mapper = mapper;
            _logger = logger;
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
        public async Task<List<AccountModel?>> GetAccounts()
        {
            var users = await _accountService.GetAllAsync();
            var userModels = new List<AccountModel?>();
            foreach(var user in users)
            {
                userModels.Add(_mapper.Map(user));
            }

            return userModels;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAccount(AccountModel accountModel)
        {
            try
            {
                var account = _mapper.Map(accountModel);
                await _accountService.UpdateAsync(accountModel.Id, account);

                return Ok(account);
            } catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest(ex.Message);
            }
        }

    }
}