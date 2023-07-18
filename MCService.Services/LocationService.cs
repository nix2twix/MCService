using MCService.Models;
using MCService.Database;

namespace MCService.Services
{
    public class LocationService
    {
        private readonly SqlDriver sqlDriver;
        public LocationService(SqlDriver sqlDriver = null)
        {
            this.sqlDriver = sqlDriver;
            this.sqlDriver.Open();
        }
        ~LocationService() 
        { 
            sqlDriver.Close(); 
        }

        public LocationModel AddNewLocation(LocationModel model)
        {
            sqlDriver.ExecNonQuery("INSERT INTO Locations (name, idMC)"+ 
                $"VALUES (N'{model.Name}', {model.CompanyID})");
            return model;
        }

        public LocationModel GetLocationByID(int id)
        {
            var locationInfo = sqlDriver.ExecReader($"SELECT name, idMC FROM Locations WHERE id = {id}");
            locationInfo.Read();

            return(new LocationModel
            {
                Name = locationInfo[0].ToString(),
                CompanyID = (int)locationInfo[1]
            });
        }

        public string GetMCNameByLocationID(int id)
        {
            return "SELECT name FROM ManagementCompanies " +
                $"WHERE id = (SELECT idMC FROM Locations WHERE id = {id})";
        }
        public string DeleteLocationByID(int id)
        {
            return sqlDriver.ExecNonQuery($"DELETE FROM Locations WHERE id = {id}")
                + " adresses was deleted successfully!";
        }

        public LocationModel ChangeLocationByID(int id, LocationModel newModel)
        {
            sqlDriver.ExecNonQuery("UPDATE Locations\n"
                 + $"SET name = N'{newModel.Name}',\n"
                 + $"idMC = {newModel.CompanyID}\n"
                 + $"WHERE id={id}");
            return newModel;
        }
    }
}
