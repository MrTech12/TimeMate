using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Repositories
{
    public interface IDatabaseRepository
    {
        string GetConnectionString();
    }
}
