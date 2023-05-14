using Backend.Models.Backend;

namespace Backend.Services.StockService
{
    public interface IStocksService : IService<Stock>
    {
        public Task<List<StockChartData>> GetSecurityChartData(String secid);

        public Task<Stock?> GetStockBySecId(String secid);

        public Task UpdateAllAsync(List<Stock> stocks);
    }
}
