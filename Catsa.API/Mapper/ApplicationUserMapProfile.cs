using AutoMapper;
using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catsa.Domain.Assemblers.Mapper
{
    public class ApplicationUserMapProfile : Profile
    {
        public ApplicationUserMapProfile()
        {
            CreateMap<UserForRegistrationDto, UserAccount>();
        }
    }
}
