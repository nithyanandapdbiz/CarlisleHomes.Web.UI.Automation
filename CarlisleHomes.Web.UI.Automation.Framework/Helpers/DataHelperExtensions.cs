using System.Data;
using System.Data.SqlClient;

namespace CarlisleHomes.Web.UI.Automation.Framework.Helpers
{
    public static class DataHelperExtensions
    {
        //Open the connection
        public static SqlConnection DBConnect(this SqlConnection sqlConnection, string connectionString)
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
                return sqlConnection;
            }
            catch (Exception e)
            {
                LogHelper.SerilogWrite("ERROR :: " + e.Message);
            }
            return null;
        }

        //Execution
        public static DataTable ExecuteQuery(this SqlConnection sqlConnection, string queryString)
        {
            DataSet dataset;
            try
            {
                //Checking for the state of connection
                if (sqlConnection == null || ((sqlConnection != null && (sqlConnection.State == ConnectionState.Closed || sqlConnection.State == ConnectionState.Broken))))
                    sqlConnection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(queryString, sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.Text;

                dataset = new DataSet();
                dataAdapter.Fill(dataset, "table");
                sqlConnection.Close();
                return dataset.Tables["table"];
            }
            catch (Exception e)
            {
                dataset = null;
                sqlConnection.Close();
                LogHelper.SerilogWrite("ERROR :: " + e.Message);
                return null;
            }
            finally
            {
                sqlConnection.Close();
                dataset = null;
            }
        }

        //Closing the connection
        public static void DBClose(this SqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.Close();
            }
            catch (Exception e)
            {
                LogHelper.SerilogWrite("ERROR :: " + e.Message);
            }
        }
    }
}