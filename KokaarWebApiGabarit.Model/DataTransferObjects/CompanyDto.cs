using System;
using System.Collections.Generic;
using System.Text;

namespace KokaarWebApiGabarit.Model.DataTransferObjects
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullAddress { get; set; }
    }
}
