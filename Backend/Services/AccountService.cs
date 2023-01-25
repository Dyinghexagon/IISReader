using MongoDB.Driver;
using Backend.Models.Backend;
using Microsoft.Extensions.Options;
using Backend.Models.Options;
using Backend.Models.Client;

namespace Backend.Services
{
    public class AccountService
    {
        private readonly IMongoCollection<Account> _usersCollection;

        public AccountService(IOptions<IISReaderDatabaseOptions> IISReaderDatabaseSettings)
        {
            var mongoClient = new MongoClient(IISReaderDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(IISReaderDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<Account>(IISReaderDatabaseSettings.Value.AccountsCollectionName);
        }

        public async Task<List<Account>> GetAccountsAsync() => await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<Account?> GetAccountAsync(Guid id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Account?> Authenticate(String login, String password)
        {
            if (String.IsNullOrEmpty(login) || String.IsNullOrEmpty(password))
            {
                return null;
            }

            var account = (await _usersCollection.FindAsync(x => x.Login == login)).FirstOrDefault();

            if (account == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt))
            {
                return null;
            }

            return account;
        }

        public async Task CreateAsync(AccountModel newAccount, String password) {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            await _usersCollection.InsertOneAsync(new Account()
            {
                Id = newAccount.Id,
                Email = newAccount.Email,
                Login = newAccount.Login,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            });
        }

        public async Task UpdateAsync(Guid id, Account updatedBook) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveAsync(Guid id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);

        private static bool VerifyPasswordHash(String password, Byte[] storedHash, Byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        private static void CreatePasswordHash(String password, out Byte[] passwordHash, out Byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
