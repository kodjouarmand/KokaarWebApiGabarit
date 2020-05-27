using System;
using System.Collections.Generic;
using System.Text;

namespace Catsa.BusinessLogic.Exceptions
{
    public class CommandValidationException : Exception
    {
        public CommandValidationException(string errorMessage): base(errorMessage)
        {
        }
    }
}
