using System;
using System.Collections.Generic;
using System.Text;

namespace Catsa.Model.DataTransferObjects
{
    public class ProxyDto : BaseDto
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
