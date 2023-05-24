using Backend.Models.Backend.StockModel;
using Backend.Models.Client;

namespace Backend.Services.ArchiveStockService
{
    public interface IArchiveStockService
    {
        public Task<IList<ArchiveStock>> GetAllAsync();

        public Task<ArchiveStock> GetAsync(String id);

        public Task CreateAsync(ArchiveStock item);

        public Task UpdateAsync(String id, ArchiveStock item);

        public Task DeleteAsync(String id);

        public Task<List<StockChartData>> GetSecurityChartData(String secid);

        public Task UpdateDataAsync();
    }
}
