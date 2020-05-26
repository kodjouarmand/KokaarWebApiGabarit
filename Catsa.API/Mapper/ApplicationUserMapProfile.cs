using AutoMapper;
using Catsa.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catsa.Model.DataTransferObjects.Mapper
{
    public class ApplicationUserMapProfile : Profile
    {
        public ApplicationUserMapProfile()
        {
            CreateMap<UserForRegistrationDto, UserAccount>();
        }
    }
}
