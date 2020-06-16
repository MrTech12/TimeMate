using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Exceptions
{
    public class AccountException : Exception
    {
        public AccountException()
        {

        }

        public AccountException(string message): base(message)
        {

        }
    }
}
