using Backend.Models.Backend;
using Backend.Models.Backend.StockModel;

namespace Backend.Repository.StockRepository
{
    public interface IArchiveStocksRepository
    {
        public Task<IList<ArchiveData>> GetAllAsync();

        public Task<ArchiveData> GetAsync(String id);

        public Task CreateAsync(ArchiveData item);

        public Task UpdateAsync(String id, ArchiveData item);

        public Task DeleteAsync(String id);
    }
}
