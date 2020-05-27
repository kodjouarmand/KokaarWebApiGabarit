using System;
using System.ComponentModel.DataAnnotations;

namespace Catsa.Domain.Entities
{
    public abstract class BaseEntity<TEntityKeyType>
    {
        [Key]
        public TEntityKeyType Id { get; set; }
        public DateTime? CreationDate { get; set; }
        public string CreationUser { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public string LastModificationUser { get; set; }
        //[Timestamp]
        //public byte[] RowVersion { get; set; }
    }
}
