﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface ISenderContainer
    {
        void SendAccountCreationMessage(string mail);
    }
}