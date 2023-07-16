using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace MCService.Services
{
    public class AdressService
    {
        public string AddNewAdress(int idLocation, int houseFIASCode, string streetFIASCode, string cityFIASCode, string regionFIASCode)
        {

            return "INSERT INTO Adresses "
                    + "(idLocation, streetFIASCode, cityFIASCode, regionFIASCode, houseFIASCode) "
                    + $"VALUES ({idLocation}, N'{streetFIASCode}', N'{cityFIASCode}', "
                    + $"N'{regionFIASCode}', {houseFIASCode})";
        }

        public string AddNewAdress(int idLocation, string streetFIASCode, string cityFIASCode, string regionFIASCode)
        {

            return "INSERT INTO Adresses "
                    + "(idLocation, streetFIASCode, cityFIASCode, regionFIASCode, houseFIASCode) "
                    + $"VALUES ({idLocation}, N'{streetFIASCode}', N'{cityFIASCode}', "
                    + $"N'{regionFIASCode}', NULL)";
        }

        public string AddNewAdress(int idLocation, string cityFIASCode, string regionFIASCode)
        {

            return "INSERT INTO Adresses "
                    + "(idLocation, streetFIASCode, cityFIASCode, regionFIASCode, houseFIASCode) "
                    + $"VALUES ({idLocation}, NULL, N'{cityFIASCode}', "
                    + $"N'{regionFIASCode}', NULL)";
        }

        public string AddNewAdress(int idLocation, string regionFIASCode)
        {

            return "INSERT INTO Adresses "
                    + "(idLocation, streetFIASCode, cityFIASCode, regionFIASCode, houseFIASCode) "
                    + $"VALUES ({idLocation}, NULL, NULL, "
                    + $"N'{regionFIASCode}', NULL)";
        }

        public string DeleteAdressByID(int id)
        {
            return $"DELETE FROM Adresses WHERE id = {id}";
        }
        public string GetAdressByID(int id)
        {
            return $"SELECT * FROM Adresses WHERE id = {id}";
        }

        public string ChangeAdressByID(int id, int houseFIASCode, int idLocation, string streetFIASCode, string cityFIASCode, string regionFIASCode)
        {
            return "UPDATE Adresses\n"
                 + $"SET idLocation = {idLocation},\n"
                 + $"streetFIASCode = N'{streetFIASCode}',\n"
                 + $"cityFIASCode = N'{cityFIASCode}',\n"
                 + $"regionFIASCode = N'{regionFIASCode}'\n"
                 + $"houseFIASCode = {houseFIASCode}\n"
                 + $"WHERE id = {id}";

        }
    }
}
