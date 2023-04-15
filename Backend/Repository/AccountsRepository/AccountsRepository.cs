using Backend.Models.Backend;
using Backend.Models.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Backend.Repository.AccountRepository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly IMongoCollection<Account> _usersCollection;

        public AccountsRepository(IOptions<DatabaseOptions> IISReaderDatabaseSettings)
        {
            var mongoClient = new MongoClient(IISReaderDatabaseSettings.Value.ConnectionString);

            var database = mongoClient.GetDatabase(IISReaderDatabaseSettings.Value.DatabaseName);

            _usersCollection = database.GetCollection<Account>(IISReaderDatabaseSettings.Value.AccountsCollectionName);
        }

        public async Task<Account> GetAsync(Guid id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<IList<Account>> GetAllAsync() => await _usersCollection.Find(_ => true).ToListAsync();

        public async Task CreateAsync(Account account) => await _usersCollection.InsertOneAsync(account);

        public async Task DeleteAsync(Guid id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);

        public async Task UpdateAsync(Guid id, Account item) => await _usersCollection.ReplaceOneAsync(x => x.Id == id, item);
    }
}
