namespace QuoteCalculator
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Amount { get; set; }
        public int Term { get; set; }
        public double RepaymentAmount { get; set; }

    }
}
