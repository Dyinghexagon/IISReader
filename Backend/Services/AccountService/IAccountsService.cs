using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Services.AccountService
{
    public interface IAccountsService : IService<Account>
    {
        public Task<Account?> GetAccountByLoginAsync(String login);

        public Task CreateAndPrepareAccountAsync(AccountModel account);

        public Task<Account?> Login(String login, String password);
    }
}
