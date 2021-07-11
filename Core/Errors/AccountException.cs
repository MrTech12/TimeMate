using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Errors
{
    public class AccountException : Exception
    {
        public AccountException()
        {

        }

        public AccountException(string message) : base(message)
        {

        }

        public AccountException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}