using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuoteCalculator.Data;
using QuoteCalculator.DTO;
using QuoteCalculator.Repositories;
using QuoteCalculator.Responses;
using Microsoft.VisualBasic;

namespace QuoteCalculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ILoanRepository loanRepository;

        public QuotesController(IUserRepository userRepository, ILoanRepository loanRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.loanRepository = loanRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetQuote(int id)
        {
            var loan = await loanRepository.GetLoanAsync(id);
            var user = await userRepository.GetAsync(loan.UserId);

            //if (user == null)
            //{
            //    return NotFound();
            //}
            var quoteDTO = new EditDTO
            {
                UserId = user.Id,
                LoanId = loan.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Mobile = user.Mobile,
                Email = user.Email,
                Amount = loan.Amount,
                Term = loan.Term,
                RepaymentAmount = loan.RepaymentAmount
            };

            return Ok(quoteDTO);
        }

        [HttpPost]
        public async Task<CreateQuoteResponseDTO> CreateUser([FromBody] QuoteDTO quote)
        {
            var user = await userRepository.GetUserByEmailAsync(quote.Email);

            if (user == null)
            {
                user = await userRepository.AddUser(new User
                {
                    FirstName = quote.FirstName,
                    LastName = quote.LastName,
                    DateOfBirth = quote.DateOfBirth,
                    Mobile = quote.Mobile,
                    Email = quote.Email
                });
            }

            var loan = await loanRepository.AddLoan(new Loan
            {
                UserId = user.Id,
                User = user,
                Amount = quote.Amount,
                Term = quote.Term,
                // Repayment amount without establishment fee
                RepaymentAmount = getPMT(quote.Amount, quote.Term)
            });

            user.Loans.Add(loan);

            CreateQuoteResponseDTO response = new()
            {
                IsSuccess = true,
                RedirectUrl = "http://localhost:4200/result/" + loan.Id.ToString()
            };


            return response;

            // find if loan is existing
            //var loan = await loanRepository.GetLoanAsync(quote.RepaymentAmount);

            //if (loan == null)
            //{
            //    loan = new Loan
            //    {
            //        UserId = user.Id,
            //        User = user,
            //        Amount = quote.Amount,
            //        Term = quote.Term,
            //        RepaymentAmount = quote.RepaymentAmount,
            //        RedirectUrl = quote.RedirectUrl
            //    };

            //    user.Loans.Add(loan);
            //}

            //return Ok(user);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuote([FromBody] EditDTO quote)
        {
            var user = await userRepository.GetAsync(quote.UserId);

            var loan = await loanRepository.GetLoanAsync(quote.LoanId);
            
            userRepository.UpdateUser(user, quote);

            if (await userRepository.SaveAllAsync())
            {
                double newRepayment = 0;

                if ((loan.Amount != quote.Amount) || (loan.Term != quote.Term))
                {

                    var newLoan = await loanRepository.AddLoan(new Loan
                    {
                        UserId = user.Id,
                        User = user,
                        Amount = quote.Amount,
                        Term = quote.Term,
                        // Repayment amount without establishment fee
                        RepaymentAmount = getPMT(quote.Amount, quote.Term)
                    });

                    user.Loans.Add(newLoan);
                    newRepayment = newLoan.RepaymentAmount;

                }

                var quoteDTO = new EditDTO
                {
                    UserId = user.Id,
                    LoanId = loan.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Mobile = user.Mobile,
                    Email = user.Email,
                    Amount = quote.Amount,
                    Term = quote.Term,
                    RepaymentAmount = newRepayment == 0 ? quote.RepaymentAmount : newRepayment
                };

                return Ok(quoteDTO);
            }

            return BadRequest("Failed to update user");

        }

        [HttpGet("edit")]
        public async Task<IActionResult> GetEdit(int id)
        {
            var loan = await loanRepository.GetLoanAsync(id);
            var user = await userRepository.GetAsync(loan.UserId);

            var editDTO = new EditDTO
            {
                UserId = user.Id,
                LoanId = loan.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                Mobile = user.Mobile,
                Email = user.Email,
                Amount = loan.Amount,
                Term = loan.Term,
                RepaymentAmount = loan.RepaymentAmount
            };

            return Ok(editDTO);
        }

        private double getPMT(int amt, int term)
        {
            double interest = .065;
            
            
            return Financial.Pmt(interest/12, term, amt) * -1;
        }
    }
}
