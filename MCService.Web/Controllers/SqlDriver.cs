using Microsoft.Data.SqlClient;

namespace MCService.Web.Controllers
{
    public class SqlDriver
    {
        private SqlConnection sqlConnection = null;

        public void Open()
        {
            sqlConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\GIT\\MCApp\\MCServiceCatalog.mdf; Integrated Security = True");
            sqlConnection.Open();
        }
        public string ExecNonQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, sqlConnection);
            return command.ExecuteNonQuery().ToString();
        }

        public string ExecScalar(string query)
        {
            SqlCommand command = new SqlCommand(query, sqlConnection);
            return command.ExecuteScalar().ToString();
        }

        public SqlDataReader ExecReader(string query)
        {
            SqlCommand command = new SqlCommand(query, sqlConnection);
            return command.ExecuteReader();
        }
    }
}
