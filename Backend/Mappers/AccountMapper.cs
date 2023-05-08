using Backend.Models.Backend;
using Backend.Models.Client;

namespace Backend.Mappers
{
    public class AccountMapper : IModelMapper
    {
        private readonly StockListMapper _stockListMapper = new();
        private readonly NotificationMapper _notificationMapper = new();

        public AccountModel? Map(Account? account)
        {
            return account == null
                ? null
                : new AccountModel()
                {
                    Id = account.Id,
                    Login = account.Login,
                    Password = "",
                    StockList = _stockListMapper.Map(account.StockList),
                    Notifications = _notificationMapper.MapNotificationList(account.Notifications)
                };
        }

        public Account? Map(AccountModel account)
        {
            return account == null
                ? null
                : new Account(
                    account.Id, 
                    account.Login, 
                    account.Password, 
                    _stockListMapper.Map(account.StockList),
                    _notificationMapper.MapNotificationList(account.Notifications));
        }
    }
}
