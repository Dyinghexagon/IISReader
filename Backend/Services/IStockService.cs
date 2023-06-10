using Backend.Models;

namespace Backend.Services
{
    public interface IStockService<T> where T : StockBase
    {
        public Task<IList<T>> GetAllAsync();

        public Task<T?> GetAsync(String id);
    }
}
