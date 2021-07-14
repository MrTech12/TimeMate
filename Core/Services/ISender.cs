using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public interface ISender
    {
        void SendAccountCreationMessage(string recipient);

        void CreateAccountCreationMessage(string recipient);
    }
}
