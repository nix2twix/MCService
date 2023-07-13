using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCService.Services
{
    //Сервис для работы со списком цен
    internal class PriceService 
    {
        private SqlConnection sqlConnection = null;
        private void AddNewPrice(int price, bool isPriceOnRequest, int locationID, int serviceID)
        {

            sqlConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\GIT\\MCApp\\MCServiceCatalog.mdf; Integrated Security = True");
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("INSERT INTO Prices (price, ifPriceOnRequest, idLocation, idService)"
                + $"VALUES ('{price}', '{isPriceOnRequest}', '{locationID}', '{serviceID}')", sqlConnection);

            Console.WriteLine(command.ExecuteNonQuery().ToString() + " lines has been executed successfully!");
        }

        private void DeletePriceByID(int id)
        {
            sqlConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\GIT\\MCApp\\MCServiceCatalog.mdf; Integrated Security = True");
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("DELETE FROM Prices "
                + $"WHERE id={id}", sqlConnection);

            Console.WriteLine(command.ExecuteNonQuery().ToString() + " lines has been executed successfully!");
        }

        private void ChangePriceByID(int id, int price, bool isPriceOnRequest, int locationID, int serviceID)
        {
            sqlConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\GIT\\MCApp\\MCServiceCatalog.mdf; Integrated Security = True");
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("UPDATE Prices\n"
                + $"SET price = {price},\nifPriceOnRequest = {isPriceOnRequest},\nidLocation = {locationID},\n"
                + $"idService = {serviceID}\nWHERE id={id}", sqlConnection);

            Console.WriteLine(command.ExecuteNonQuery().ToString() + " lines has been executed successfully!");

        }

        private void GetPriceByCode(int codeFias, int idService) 
        {
            sqlConnection = new SqlConnection("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = D:\\GIT\\MCApp\\MCServiceCatalog.mdf; Integrated Security = True");
            sqlConnection.Open();

            SqlCommand command = new SqlCommand("SELECT price FROM Prices\n" 
                + $"WHERE idLocation = {codeFias} AND idService = {idService}", sqlConnection);

            Console.WriteLine(command.ExecuteNonQuery().ToString() + " lines has been executed successfully!");

        }
    }
}
