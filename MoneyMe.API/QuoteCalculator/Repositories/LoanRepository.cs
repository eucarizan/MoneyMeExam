using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Data;

namespace QuoteCalculator.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DataContext dataContext;

        public LoanRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<Loan> AddLoan(Loan loan)
        {
            await dataContext.Loans.AddAsync(loan);
            await dataContext.SaveChangesAsync();

            return loan;
        }

        public async Task<List<Loan>> GetAllAsync()
        {
            return await dataContext.Loans.ToListAsync();
        }

        public async Task<Loan> GetLoanAsync(int id)
        {
            return await dataContext.Loans
                .Where(loan => loan.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
