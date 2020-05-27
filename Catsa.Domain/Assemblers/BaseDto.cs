using System;

namespace Catsa.Domain.Assemblers
{
    public abstract class BaseDto<TEntityKey>
    {
        public TEntityKey Id { get; set; }       
    }
}
