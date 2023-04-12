using Backend.Models.Backend;
using Backend.Models.Client;
using Backend.Helpers;
using Backend.Repository;

namespace Backend.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IList<Account>> GetAllAsync() => await _accountRepository.GetAllAsync();
        public async Task<Account> GetAsync(Guid id) => await _accountRepository.GetAsync(id);

        public async Task<Account?> GetAccountAsync(String login)
        {
            var acconts = await _accountRepository.GetAllAsync();
            var account = acconts.FirstOrDefault(a => a.Login == login);
            return account;
        }

        public async Task<Account?> Login(String login, String password)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                return null;
            }

            var account = await GetAccountAsync(login);

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

        public async Task CreateAndPrepareAccountAsync(AccountModel accountModel) {
            var accounts = await GetAllAsync();
            if (accounts.Select(x => x.Login == accountModel.Login).SingleOrDefault()){
                throw new Exception("Repit login!");
            }
            var account = new Account(accountModel.Id, accountModel.Login, accountModel.Password);
            await CreateAsync(account);
        }

        public async Task UpdateAsync(Guid id, Account account) => await _accountRepository.UpdateAsync(id, account);

        public async Task DeleteAsync(Guid id) => await _accountRepository.DeleteAsync(id);

        public async Task CreateAsync(Account account)
        {
            await _accountRepository.CreateAsync(account);
        }
    }
}
