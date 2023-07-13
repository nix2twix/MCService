using Microsoft.Data.SqlClient;

namespace MCService.Services
{
    //Сервис для работы со списком услуг
    public class WorkService
    {
        private SqlConnection sqlConnection = null;
        private void AddNewService(int id, string name, int minCount, int maxCount, string measurement, int idMC)
        {
  
            sqlConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\GIT\\MCApp\\MCServiceCatalog.mdf; Integrated Security = True");
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Services (name, minCount, maxCount, measure, idMC)"
                + $"VALUES (N'{name}', N'{minCount}', N'{maxCount}', N'{measurement}', '{idMC}')", sqlConnection);

           Console.WriteLine(command.ExecuteNonQuery().ToString() + " lines has been executed successfully!");
        }

        private void DeleteServiceByID(int id)
        {
            sqlConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\GIT\\MCApp\\MCServiceCatalog.mdf; Integrated Security = True");
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Services "
                + $"WHERE id={id}", sqlConnection);

            Console.WriteLine(command.ExecuteNonQuery().ToString() + " lines has been executed successfully!");
        }

        private void ChangeServiceByID(int id, string name, int minCount, int maxCount, string measurement, int idMC)
        {
            sqlConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\GIT\\MCApp\\MCServiceCatalog.mdf; Integrated Security = True");
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("UPDATE Services "
                + $"SET (N'{name}', N'{minCount}', N'{maxCount}', N'{measurement}', '{idMC}')", sqlConnection);

            Console.WriteLine(command.ExecuteNonQuery().ToString() + " lines has been executed successfully!");

        }

    }
}