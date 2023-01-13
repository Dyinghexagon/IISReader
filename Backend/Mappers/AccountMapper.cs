using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class AccountMapper : IModelMapper
    {
        public AccountMapper() { }

        public AccountModel? Map(Account? user)
        {
            return user == null ? null : new AccountModel(user.Id, user.Email, user.Login, user.Password);
        }

        public Account Map(AccountModel user)
        {
            return new Account(user.Id, user.Email, user.Login, user.Password);
        }
    }
}
