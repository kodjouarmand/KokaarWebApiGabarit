using AutoMapper;
using Catsa.Domain.Assemblers.Users;
using Catsa.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catsa.Domain.Assemblers.Mapper
{
    public class ApplicationUserProfile : Profile
    {
        public ApplicationUserProfile()
        {
            CreateMap<UserForRegistrationDto, ApplicationUser>();
        }
    }
}
