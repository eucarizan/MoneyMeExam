using System.Collections.ObjectModel;

namespace QuoteCalculator
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public ICollection<Loan> Loans { get; set; }

        public User()
        {
            Loans = new Collection<Loan>();
        }
    }
}
