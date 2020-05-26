using System;
using System.Collections.Generic;
using System.Text;

namespace Catsa.Business.Validations
{
    public class ValidationException : Exception
    {
        public ValidationException(string errorMessage): base(errorMessage)
        {
        }
    }
}
