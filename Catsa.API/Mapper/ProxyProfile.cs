using AutoMapper;
using Catsa.Model.DataTransferObjects;
using Catsa.Model.Entities;

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
