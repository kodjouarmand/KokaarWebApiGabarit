using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catsa.Model.Entities
{
    public class Proxy : BaseEntity<int>
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }

}
