using Backend.Models;
using MongoDB.Driver;
using Backend.Models.Backend;
using Microsoft.Extensions.Options;

namespace Backend.Services
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _usersCollection;

        public AccountService(IOptions<IISReaderDatabaseSettings> IISReaderDatabaseSettings)
        {
            var mongoClient = new MongoClient(IISReaderDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(IISReaderDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<Account>(IISReaderDatabaseSettings.Value.AccountsCollectionName);
        }

        public async Task<List<Account>> GetAccountsAsync() => await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<Account?> GetAccountAsync(Guid id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Account? newAccount) {
            if (newAccount != null) { 
                await _usersCollection.InsertOneAsync(newAccount);
            }
        }

        public async Task UpdateAsync(Guid id, Account updatedBook) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(Guid id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
