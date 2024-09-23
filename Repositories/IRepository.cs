namespace QuizApi.Repositories
{
    public interface IRepository<T, QueryDTO, CreateDTO, UpdateDTO> where T : class
    {
        public Task<IEnumerable<QueryDTO>> GetAllAsync();
        public Task<QueryDTO> GetByIdAsync(int id);
        public Task<QueryDTO> CreateAsync(CreateDTO entity);
        public Task UpdateAsync(int id, UpdateDTO entity);
        public Task DeleteAsync(int id);
    }
}