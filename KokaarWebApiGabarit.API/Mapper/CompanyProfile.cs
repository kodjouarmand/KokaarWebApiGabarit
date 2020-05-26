using AutoMapper;
using KokaarWebApiGabarit.Model.DataTransferObjects;
using KokaarWebApiGabarit.Model.Entities;

namespace KokaarWepApi.API.Mapper
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyDto>()
                .ForMember(c => c.FullAddress,
                            opt => opt.MapFrom(x => string.Join(' ', x.Address, x.Country)));

            CreateMap<CompanyDto, Company>();
        }
    }
}
