using System;

namespace Catsa.Domain.Assemblers
{
    [Serializable()]
    public abstract class BaseDto<TEntityKey>
    {
        public TEntityKey Id { get; set; }       
    }
}
