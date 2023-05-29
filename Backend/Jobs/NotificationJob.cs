using Backend.Hubs.NotificationHub;
using Backend.Models.Backend;
using Backend.Services.AccountService;
using Backend.Services.ArchiveStockService;
using Backend.Services.StockService;
using Microsoft.AspNetCore.SignalR;
using Quartz;

namespace Backend.Jobs
{
    public class NotificationJob : IJob
    {
        private readonly IAccountsService _accountService;
        private readonly IActualStocksService _actualStocksService;
        private readonly IArchiveStockService _archiveStockService;
        private readonly ILogger<NotificationJob> _logger;
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationJob(
            IAccountsService accountService,
            IActualStocksService actualStocksService,
            IArchiveStockService archiveStockService,
            IHubContext<NotificationHub> hub,
            ILogger<NotificationJob> logger
        )
        {
            _accountService = accountService;
            _actualStocksService = actualStocksService;
            _archiveStockService = archiveStockService;
            _hub = hub;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var accounts = await _accountService.GetAllAsync();
            foreach(var account in accounts.Where(account => account.StockList.Any()))
            {
                foreach (var stockList in account.StockList.Where(stockList => stockList.IsNotificated))
                {
                    foreach (var stock in stockList.Stocks) 
                    {
                        var actualStockData = await _actualStocksService.GetAsync(stock.Id);
                        if (actualStockData is null)
                        {
                            continue;
                        }
                        var archiveData = await _archiveStockService.GetAsync(stock.Id);
                        var volume = archiveData.GetVolume(stockList.CalculationType);
                        if (actualStockData.CurrentVolume > volume) {
                            if (account.Notifications.Count < 200)
                            {
                                account.Notifications.Add(new Notification()
                                {
                                    Id = Guid.NewGuid(),
                                    Date = DateTime.Now,
                                    SecId = stock.Id,
                                    Title = "Заголовок",
                                    Description = "Описание",
                                    isReaded = false,
                                    Volume = stock.CurrentVolume
                                });
                            }
                            else
                            {
                                account.Notifications.Clear();
                            }
                        }

                    }
                }

                await _accountService.UpdateAsync(account.Id, account);
                _logger.LogInformation($"Notification for account: {account.Id} send!");
                await _hub.Clients.All.SendAsync("TransferStockData");
            }
        }
    }
}
