using Backend.Models.Backend;
using Backend.Models.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Repository.StockRepository
{
    public class AchiveStocksRepository : IArchiveStocksRepository
    {
        private readonly IMongoCollection<ArchiveData> _archiveStocksCollection;

        public AchiveStocksRepository(IOptions<DatabaseOptions> IISReaderDatabaseSettings)
        {
            var mongoClient = new MongoClient(IISReaderDatabaseSettings.Value.ConnectionString);

            var database = mongoClient.GetDatabase(IISReaderDatabaseSettings.Value.DatabaseName);

            _archiveStocksCollection = database.GetCollection<ArchiveData>(IISReaderDatabaseSettings.Value.ArchiveStocksCollectionName);
        }

        public async Task CreateAsync(ArchiveData item) => await _archiveStocksCollection.InsertOneAsync(item);

        public async Task DeleteAsync(String id) => await _archiveStocksCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<IList<ArchiveData>> GetAllAsync() => await _archiveStocksCollection.Find(_ => true).ToListAsync();

        public async Task<ArchiveData> GetAsync(String id) => await _archiveStocksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(String id, ArchiveData item) => await _archiveStocksCollection.ReplaceOneAsync(x => x.Id == id, item);

    }
}
