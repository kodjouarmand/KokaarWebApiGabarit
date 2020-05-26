using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KokaarWebApiGabarit.Model.Entities
{
    public class Company : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
    }

}
