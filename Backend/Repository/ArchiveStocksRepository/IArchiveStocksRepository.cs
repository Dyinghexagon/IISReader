using Backend.Models.Backend.StockModel;

namespace Backend.Repository.StockRepository
{
    public interface IArchiveStocksRepository
    {
        public Task<IList<ArchiveStock>> GetAllAsync();

        public Task<ArchiveStock> GetAsync(String id);

        public Task CreateAsync(ArchiveStock item);

        public Task UpdateAsync(String id, ArchiveStock item);

        public Task DeleteAsync(String id);
    }
}
