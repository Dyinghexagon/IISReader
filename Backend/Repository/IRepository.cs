namespace Backend.Repository
{
    public interface IRepository<T> where T : class
    {
        public Task<IList<T>> GetAllAsync();

        public Task<T> GetAsync(Guid id);

        public Task CreateAsync(T item);

        public Task UpdateAsync(Guid id, T item);

        public Task DeleteAsync(Guid id);
    }
}
