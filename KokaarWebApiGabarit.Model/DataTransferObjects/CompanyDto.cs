using System;
using System.Collections.Generic;
using System.Text;

namespace KokaarWebApiGabarit.Model.DataTransferObjects
{
    public class CompanyDto : BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string FullAddress { get; set; }
    }
}
