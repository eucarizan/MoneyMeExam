namespace QuoteCalculator.DTO
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; }
        public int Amount { get; set; }
        public int Term { get; set; }
        public int RepaymentAmount { get; set; }
    }
}
