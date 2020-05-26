using AutoMapper;
using KokaarWebApiGabarit.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KokaarWebApiGabarit.Model.DataTransferObjects.Mapper
{
    public class ApplicationUserMapProfile : Profile
    {
        public ApplicationUserMapProfile()
        {
            CreateMap<UserForRegistrationDto, UserAccount>();
        }
    }
}
