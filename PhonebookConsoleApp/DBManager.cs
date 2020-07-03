using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhonebookConsoleApp
{
    class DBManager : IDB
    {
        SqlConnection connection;

        // constructor
        public DBManager(string dbConnKey)
        {
            String connString = ConfigurationManager.ConnectionStrings[dbConnKey].ConnectionString;
            connection = new SqlConnection(connString);
        }

        public DataTable ExecuteQuery(string sqlQuery)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, connection);
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(dt);
                return dt;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return dt;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable ExecuteStoredProcedure(string procedureName)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return dt;
            }
            finally
            {
                connection.Close();
            }
        }

        public DataTable ExecuteStoredProcedure(string procedureName, List<SqlParameter> parameters)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand(procedureName, connection);
            command.CommandType = CommandType.StoredProcedure;

            foreach(SqlParameter param in parameters)
            {
                command.Parameters.Add(param);
            }
            adapter.SelectCommand = command;

            DataTable dt = new DataTable();
            try
            {
                connection.Open();
                adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return dt;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
