using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IDatabaseContext
    {
        SqlConnection GetConnection();
    }
}
