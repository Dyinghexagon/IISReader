using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class AccountMapper : IModelMapper
    {
        public AccountMapper() { }

        public AccountModel? Map(Account? account)
        {
            return account == null 
                ? null 
                : new AccountModel() 
                {
                    Id = account.Id,
                    Login = account.Login,
                    Email = account.Email,
                    Password = ""
                };
        }

        public Account? Map(AccountModel account)
        {
            return account == null 
                ? null 
                : new Account() 
                { 
                    Id = account.Id,
                    Login = account.Login,
                    Email = account.Email,
                    PasswordHash = new Byte[0],
                    PasswordSalt = new Byte[0]
                };
        }
    }
}
