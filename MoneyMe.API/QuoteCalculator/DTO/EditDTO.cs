namespace QuoteCalculator.DTO
{
    public class EditDTO
    {
        public int UserId { get; set; }
        public int LoanId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public int Amount { get; set; }
        public int Term { get; set; }
        public double RepaymentAmount { get; set; }
    }
}
