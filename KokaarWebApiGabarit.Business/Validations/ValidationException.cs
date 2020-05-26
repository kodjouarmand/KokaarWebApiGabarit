using System;
using System.Collections.Generic;
using System.Text;

namespace KokaarWepApi.Business.Validations
{
    public class ValidationException : Exception
    {
        public ValidationException(string errorMessage): base(errorMessage)
        {
        }
    }
}
