using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer
{
    public interface ISender
    {
        void SendAccountCreationMail(string recipient);

        void CreateAccountCreationMail(string recipient);
    }
}
