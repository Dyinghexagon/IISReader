using Backend.Models.Backend;
using Backend.Models.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Repository.StockRepository
{
    public class StocksRepository : IStocksRepository
    {
        private readonly IMongoCollection<Stock> _stocksCollection;

        public StocksRepository(IOptions<DatabaseOptions> IISReaderDatabaseSettings)
        {
            var mongoClient = new MongoClient(IISReaderDatabaseSettings.Value.ConnectionString);

            var database = mongoClient.GetDatabase(IISReaderDatabaseSettings.Value.DatabaseName);

            _stocksCollection = database.GetCollection<Stock>(IISReaderDatabaseSettings.Value.StocksCollectionName);
        }

        public async Task CreateAsync(Stock item) => await _stocksCollection.InsertOneAsync(item);

        public async Task DeleteAsync(Guid id) => await _stocksCollection.DeleteOneAsync(x => x.Id == id);

        public async Task<IList<Stock>> GetAllAsync() => await _stocksCollection.Find(_ => true).ToListAsync();

        public async Task<Stock> GetAsync(Guid id) => await _stocksCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(Guid id, Stock item) => await _stocksCollection.ReplaceOneAsync(x => x.Id == id, item);
    }
}
