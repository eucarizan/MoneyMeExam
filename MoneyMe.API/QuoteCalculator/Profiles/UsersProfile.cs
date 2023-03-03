using AutoMapper;
using QuoteCalculator.DTO;

namespace QuoteCalculator.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ReverseMap();
            CreateMap<Loan, LoanDTO>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id))
                .ReverseMap();
        }
    }
}
