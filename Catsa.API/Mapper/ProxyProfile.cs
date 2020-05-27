using AutoMapper;
using Catsa.Domain.Assemblers;
using Catsa.Domain.Entities;

namespace Catsa.API.Mapper
{
    public class ProxyProfile : Profile
    {
        public ProxyProfile()
        {
            CreateMap<Proxy, ProxyDto>();
            CreateMap<ProxyDto, Proxy>();
        }
    }
}
