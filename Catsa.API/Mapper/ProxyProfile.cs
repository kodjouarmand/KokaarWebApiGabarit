using AutoMapper;
using Catsa.Domain.Assemblers.Proxies;
using Catsa.Domain.Entities;

namespace Catsa.API.Mapper
{
    public class ProxyProfile : Profile
    {
        public ProxyProfile()
        {
            CreateMap<Proxy, Proxy>();
            CreateMap<Proxy, ProxyQueryDto>();
            CreateMap<Proxy, ProxyCommandDto>();
            CreateMap<ProxyCommandDto, Proxy>();            

        }
    }
}
