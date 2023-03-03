using QuoteCalculator.DTO;

namespace QuoteCalculator.Repositories
{
    public interface IUserRepository
    {
        //Task<List<User>> GetAllAsync();
        Task<User> GetAsync(int id);
        Task<User> AddUser(User user);
        Task<User> GetUserByEmailAsync(string email);
        void UpdateUser(User user, EditDTO quote);
        Task<bool> SaveAllAsync();
    }
}
