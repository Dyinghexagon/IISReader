using MongoDB.Driver;
using Backend.Models.Backend;
using Microsoft.Extensions.Options;
using Backend.Models.Options;
using Backend.Models.Client;
using Backend.Helpers;

namespace Backend.Services
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _usersCollection;

        public AccountService(IOptions<DatabaseOptions> IISReaderDatabaseSettings)
        {
            var mongoClient = new MongoClient(IISReaderDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(IISReaderDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<Account>(IISReaderDatabaseSettings.Value.AccountsCollectionName);
        }

        public async Task<List<Account>> GetAccountsAsync() => await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<Account?> GetAccountAsync(String login) => await _usersCollection.Find(x => x.Login == login).FirstOrDefaultAsync();

        public async Task<Account?> Login(String Email, String password)
        {
            if (String.IsNullOrEmpty(Email) || String.IsNullOrEmpty(password))
            {
                return null;
            }

            var account = await GetAccountAsync(Email);

            if (account == null)
            {
                return null;
            }

            if (!CryptoUtils.VerifyPasswordHash(password, Convert.FromBase64String(account.PasswordHash), 
                Convert.FromBase64String(account.PasswordSalt)))
            {
                return null;
            }

            return account;
        }

        public async Task CreateAsync(AccountModel accountModel) {
            var accounts = await GetAccountsAsync();
            if (accounts.Select(x => x.Login == accountModel.Login).SingleOrDefault()){
                throw new Exception("Repit login!");
            }
            var account = new Account(accountModel.Id, accountModel.Login, accountModel.Password);
            await _usersCollection.InsertOneAsync(account);
        }

        public async Task UpdateAsync(Guid id, Account updatedBook) => await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(Guid id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);

    }
}
