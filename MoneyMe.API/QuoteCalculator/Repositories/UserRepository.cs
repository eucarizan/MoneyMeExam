using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Data;
using QuoteCalculator.DTO;

namespace QuoteCalculator.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<User> AddUser(User user)
        {
            await dataContext.Users.AddAsync(user);
            await dataContext.SaveChangesAsync();

            return user;
        }

        //public async Task<List<User>> GetAllAsync()
        //{
        //    return await dataContext.Users.ToListAsync();
        //}

        public async Task<User> GetAsync(int id)
        {
            return await dataContext.Users
                //.Include(loan => )
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await dataContext.Users
                .Where(e => e.Email == email)
                .FirstOrDefaultAsync();
        }

        public void UpdateUser(User user, EditDTO quote)
        {
            user.FirstName = quote.FirstName;
            user.LastName = quote.LastName;
            user.DateOfBirth = quote.DateOfBirth;
            user.Mobile = quote.Mobile;
            user.Email = quote.Email;

            dataContext.Entry(user).State = EntityState.Modified;

            //return updatedUser;
        }

        public async Task<bool> SaveAllAsync()
        {
            return await dataContext.SaveChangesAsync() > 0;
        }
    }
}
