using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDAL
    {
        DataTable ExecuteQuery(string sqlQuery);

        DataTable ExecuteStoredProcedure(string procedureName);

        DataTable ExecuteStoredProcedure(string procedureName, List<SqlParameter> parameters);
    }
}
