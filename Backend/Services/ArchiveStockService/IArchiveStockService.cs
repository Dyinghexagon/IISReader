using Backend.Models.Backend;

namespace Backend.Services.ArchiveStockService
{
    public interface IArchiveStockService : IStockService<ArchiveData>
    {
        public Task CreateAsync(ArchiveData item);

        public Task UpdateAsync(String id, ArchiveData item);

        public Task DeleteAsync(String id);

        public Task UpdateDataAsync();
    }
}
