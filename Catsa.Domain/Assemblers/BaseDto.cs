using System;

namespace Catsa.Domain.Assemblers
{
    [Serializable()]
    public abstract class BaseDto
    {
        public int Id { get; set; }       
    }
}
