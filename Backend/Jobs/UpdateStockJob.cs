using Backend.Services.StockService;
using Quartz;

namespace Backend.Jobs
{
    public class UpdateStockJob : IJob
    {
        private readonly IStocksService _stocksService;
        private readonly ILogger<UpdateStockJob> _logger;

        public UpdateStockJob(
            IStocksService stocksService,
            ILogger<UpdateStockJob> logger
        )
        {
            _stocksService = stocksService;
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            var stocks = await _stocksService.GetAllAsync();
        }
    }
}
