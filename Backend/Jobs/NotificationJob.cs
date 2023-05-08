using Backend.Models.Backend;
using Backend.Services.AccountService;
using Backend.Services.StockService;
using Quartz;
using System.Linq;

namespace Backend.Jobs
{
    public class NotificationJob : IJob
    {
        private readonly IAccountsService _accountService;
        private readonly IStocksService _stocksService;
        private readonly ILogger<NotificationJob> _logger;
        private readonly Int64 _commonVolume;
        public NotificationJob(
            IAccountsService accountService,
            IStocksService stocksService,
            ILogger<NotificationJob> logger
        )
        {
            _accountService = accountService;
            _stocksService = stocksService;
            _logger = logger;
            _commonVolume = 1000;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var notificatedStcoks = new List<Stock>();
            var stocks = await _stocksService.GetAllAsync();
            foreach (var stock in stocks.Where(stock => stock.CurrentVolume > _commonVolume))
            {
                notificatedStcoks.Add(stock);
            }
            
            var accounts = await _accountService.GetAllAsync();
            foreach(var account in accounts.Where(account => account.StockList.Any()))
            {
                foreach (var stockList in account.StockList.Where(stockList => stockList.IsNotificated))
                {
                    foreach (var stock in stockList.Stocks.Where(stock => notificatedStcoks.Contains(stock))) 
                    {
                        _logger.LogInformation($"Account: {account.Login}\n stock: {stock.SecId}\n");
                    }
                }
            }
        }
    }
}
