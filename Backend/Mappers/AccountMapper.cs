using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class AccountMapper : IModelMapper
    {
        public AccountMapper() { }

        public AccountModel? Map(Account? account)
        {
            return account == null ? null : new AccountModel(account.Id, account.Email, account.Login, account.Password);
        }

        public Account? Map(AccountModel account)
        {
            return account == null ? null : new Account(account.Id, account.Email, account.Login, account.Password);
        }
    }
}
