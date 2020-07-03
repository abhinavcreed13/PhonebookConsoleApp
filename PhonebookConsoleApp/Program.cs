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
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Phonebook App");

            // ADO.NET FRAMEWORK
            // Connect to SQL Server
            // Verbetim string
            //String connString = @"Data Source=ABHINAVCREED-PC\SQLEXPRESS;Initial Catalog=test;Integrated Security=True";
            //String connString = ConfigurationManager.ConnectionStrings["dbConnString"].ConnectionString;

            //SqlConnection conn = new SqlConnection(connString);

            ////// Exception handling - (try-catch-finally)
            ////try
            ////{
            ////    conn.Open();
            ////    Console.WriteLine("Connection opened");
            ////}
            ////catch (Exception ex)
            ////{
            ////    Console.WriteLine(ex.ToString());
            ////}
            ////finally
            ////{
            ////    conn.Close();
            ////}

            //// Run a query - select
            //// ONLINE MODE
            //String sqlQuery = "Select * from user_data";
            //SqlCommand command = new SqlCommand(sqlQuery, conn);
            //conn.Open();
            //SqlDataReader reader = command.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader.GetValue(0) + "," + reader.GetValue(1));
            //}
            //conn.Close();

            ////Offline Mode
            //SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, conn);
            //conn.Open();
            //DataTable data = new DataTable();
            ////adapter.Fill(data);

            //// stored procedures
            //SqlDataAdapter adapter2 = new SqlDataAdapter();

            //SqlCommand command2 = new SqlCommand("getAllUsers", conn);
            //command2.CommandType = CommandType.StoredProcedure;

            ////adapter2.SelectCommand = command2;

            ////adapter2.Fill(data);

            //Console.WriteLine("Enter User ID:");
            //int userId = Convert.ToInt32(Console.ReadLine());
            
            //var command3 = new SqlCommand("getUser", conn);
            //command3.CommandType = CommandType.StoredProcedure;
            //command3.Parameters.AddWithValue("@userId", userId);

            //adapter2.SelectCommand = command3;

            //adapter2.Fill(data);

            //foreach(DataRow dr in data.Rows)
            //{
            //    Console.WriteLine(string.Format("UserId: {0}, UserName:{1}", dr["user_id"], dr["user_name"]));
            //}

            //conn.Close();

            // Fetch query
            DBManager manager = new DBManager("dbConnString");

            //String sqlQuery = "Select * from user_data";
            //DataTable data = manager.ExecuteQuery(sqlQuery);

            //DataTable data = manager.ExecuteStoredProcedure("getAllUsers");

            Console.WriteLine("Enter User ID:");
            int userId = Convert.ToInt32(Console.ReadLine());
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userId", userId));

            DataTable data = manager.ExecuteStoredProcedure("getUser", parameters);

            foreach (DataRow dr in data.Rows)
            {
                Console.WriteLine(string.Format("UserId: {0}, UserName:{1}", dr["user_id"], dr["user_name"]));
            }

            Console.ReadLine();
        }
    }
}
