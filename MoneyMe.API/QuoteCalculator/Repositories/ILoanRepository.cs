namespace QuoteCalculator.Repositories
{
    public interface ILoanRepository
    {
        Task<List<Loan>> GetAllAsync();
        Task<Loan> AddLoan(Loan loan);
        Task<Loan> GetLoanAsync(int repaymentAmt);
    }
}
