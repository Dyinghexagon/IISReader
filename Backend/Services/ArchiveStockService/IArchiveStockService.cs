using Backend.Models.Backend;

namespace Backend.Services.ArchiveStockService
{
    public interface IArchiveStockService : IStockService<ArchiveStock>
    {
        public Task CreateAsync(ArchiveStock item);

        public Task UpdateAsync(String id, ArchiveStock item);

        public Task DeleteAsync(String id);

        public Task UpdateDataAsync();

        public Task PrepareForDataUpdateAsync();
    }
}
