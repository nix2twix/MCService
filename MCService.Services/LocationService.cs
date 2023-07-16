using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCService.Services
{
    public class LocationService
    {
        public string AddNewLocation(string name, int idMC)
        {

            return "INSERT INTO Locations (name, idMC) "
                + $"VALUES (N'{name}', {idMC})";
        }

        public string GetLocationByID(int id)
        {
            return $"SELECT name, idMC FROM Locations WHERE id = {id}";
        }

        public string GetMCNameByLocationID(int id)
        {
            return "SELECT name FROM ManagementCompanies " +
                $"WHERE id = (SELECT idMC FROM Locations WHERE id = {id})";
        }
        public string DeleteLocationByID(int id)
        {
            return $"DELETE FROM Locations WHERE id={id}";
        }

        public string ChangeLocationByID(int id, string name, int idMC)
        {
            return "UPDATE Locations\n"
                 + $"SET name = {name},\n"
                 + $"idMC = {idMC}\n"
                 + $"WHERE id={id}";

        }
    }
}
