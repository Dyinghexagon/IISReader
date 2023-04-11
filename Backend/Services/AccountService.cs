using MongoDB.Driver;
using Backend.Models.Backend;
using Microsoft.Extensions.Options;
using Backend.Models.Options;
using Backend.Models.Client;
using Backend.Helpers;
using Backend.Repository;

namespace Backend.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IList<Account>> GetAccountsAsync() => await _accountRepository.GetAllAsync();

        public async Task<Account?> GetAccountAsync(String login)
        {
            var acconts = await _accountRepository.GetAllAsync();
            var account = acconts.FirstOrDefault(a => a.Login == login);
            return account;
        }

        public async Task<Account?> Login(String Email, String password)
        {
            if (String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(password))
            {
                return null;
            }

            var account = await GetAccountAsync(Email);

            if (account == null)
            {
                return null;
            }

            if (!CryptoUtils.VerifyPasswordHash(password, Convert.FromBase64String(account.PasswordHash), 
                Convert.FromBase64String(account.PasswordSalt)))
            {
                return null;
            }

            return account;
        }

        public async Task CreateAsync(AccountModel accountModel) {
            var accounts = await GetAccountsAsync();
            if (accounts.Select(x => x.Login == accountModel.Login).SingleOrDefault()){
                throw new Exception("Repit login!");
            }
            var account = new Account(accountModel.Id, accountModel.Login, accountModel.Password);
            await _accountRepository.CreateAsync(account);
        }

        public async Task UpdateAsync(Guid id, Account account) => await _accountRepository.UpdateAsync(id, account);

        public async Task RemoveAsync(Guid id) => await _accountRepository.DeleteAsync(id);

    }
}
