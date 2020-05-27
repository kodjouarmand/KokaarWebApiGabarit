using System;
using System.Collections.Generic;
using System.Text;

namespace Catsa.Domain.Assemblers
{
    [Serializable()]
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string LastModificationUser { get; set; }
        //public byte[] RowVersion { get; set; }

        public bool IsNew() => Id == 0;        
    }
}
