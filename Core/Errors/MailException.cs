using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Errors
{
    public class MailException : Exception
    {
        public MailException()
        {

        }

        public MailException(string message) : base(message)
        {

        }

        public MailException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}