using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Errors
{
    public class DatabaseException : Exception
    {
        public DatabaseException()
        {

        }

        public DatabaseException(string message) : base(message)
        {

        }

        public DatabaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}