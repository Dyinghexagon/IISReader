using Backend.Models.Backend.StockModel;

namespace Backend.Services.StockService
{
    public interface IActualStocksService
    {
        public Task<IList<ActualStock>> GetAllAsync();

        public Task<ActualStock?> GetAsync(string id);
    }
}
