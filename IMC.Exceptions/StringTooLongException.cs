using System;
using System.Collections.Generic;
using System.Text;

namespace IMC.Exceptions
{
    public class StringTooLongException : Exception
    {
        public StringTooLongException(string message) : base(message)
        {
        }
    }
}
