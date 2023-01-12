using Backend.Models;
using MongoDB.Driver;
using Backend.Models.Backend;
using Microsoft.Extensions.Options;

namespace Backend.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _usersCollection;

        public UserService(IOptions<IISReaderDatabaseSettings> IISReaderDatabaseSettings)
        {
            var mongoClient = new MongoClient(IISReaderDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(IISReaderDatabaseSettings.Value.DatabaseName);

            _usersCollection = mongoDatabase.GetCollection<User>(IISReaderDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetUsersAsync() => await _usersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetUserAsync(String id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateUserAsync(User newBook) => await _usersCollection.InsertOneAsync(newBook);

        public async Task UpdateUserAsync(String id, User updatedBook) =>
            await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

        public async Task RemoveUserAsync(String id) => await _usersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
