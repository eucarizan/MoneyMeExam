namespace QuoteCalculator.DTO
{
    public class AddUserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
    }
}
