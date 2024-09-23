namespace QuizApi.Repositories
{
    public interface IRepository<T, TToQuery, TToCreate, TToUpate> where T : class
    {
        public Task<IEnumerable<TToQuery>> GetAllAsync();
        public Task<TToQuery> GetByIdAsync(int id);
        public Task<TToQuery> CreateAsync(TToCreate entity);
        public Task UpdateAsync(int id, TToUpate entity);
        public Task DeleteAsync(int id);
    }
}