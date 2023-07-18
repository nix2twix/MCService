using MCService.Models;
using MCService.Database;

namespace MCService.Services
{
    public class PriceService 
    {
        private readonly SqlDriver sqlDriver;
        public PriceService(SqlDriver sqlDriver = null)
        {
            this.sqlDriver = sqlDriver;
            this.sqlDriver.Open();
        }

        ~PriceService() 
        { 
            sqlDriver.Close();
        }
        public string AddNewPrice(PriceModel newModel)
        {
            int isRequest = Convert.ToInt32(newModel.isPriceOnRequest);

            return sqlDriver.ExecNonQuery("INSERT INTO Prices (price, ifPriceOnRequest, idLocation, idService, name) "
                + $"VALUES ({newModel.Price}, {isRequest}, {newModel.LocationID}, "
                + $"{newModel.ServiceID}, N'{newModel.Name}')")
                + " adresses was added successfully!";
        }

        public string DeletePriceByID(int id)
        {

            return sqlDriver.ExecNonQuery($"DELETE FROM Prices WHERE id = {id}")
                + " adresses was deleted successfully!";
        }

        public PriceModel ChangePriceByID(int id, PriceModel newModel)
        {
            sqlDriver.ExecNonQuery("UPDATE Prices\n"
                 + $"SET price = {newModel.Price},\nifPriceOnRequest = {newModel.isPriceOnRequest},"
                 + $"\nidLocation = {newModel.LocationID},\n"
                 + $"idService = {newModel.ServiceID},\n"
                 + $"name = N'{newModel.Name}'\n"
                 + $"WHERE id={id}");

            return newModel;
        }

        public PriceModel GetPriceByID(int id)
        {
            var priceInfo = sqlDriver.ExecReader("SELECT price, ifPriceOnRequest, idLocation, idService "
                + $"FROM Prices WHERE id = {id}");
            priceInfo.Read();

            int serviceId = (int)priceInfo[3];

            var model = new PriceModel
            {
                Name = string.Empty,
                Price = (int)priceInfo[0],
                isPriceOnRequest = (bool)priceInfo[1],
                LocationID = (int)priceInfo[2],
                ServiceID = serviceId
            };

            sqlDriver.Close();
            sqlDriver.Open();

            var serviceInfo = sqlDriver.ExecReader($"SELECT name, minCount, maxCount, measure, idMC "
                + $"FROM Services WHERE id = {serviceId}");
            serviceInfo.Read();

            model.Name = serviceInfo["name"].ToString();

            return model;
        }

        public PriceModel GetPriceByCode(int codeFias, int idService) 
        {

            var priceInfo = sqlDriver.ExecReader("SELECT price FROM Prices\n"
                + $"WHERE idLocation = {codeFias} "
                + $"AND idService = {idService}");
            priceInfo.Read();

            var serviceInfo = sqlDriver.ExecReader($"SELECT *"
                 + $"FROM Services WHERE id = {idService}");
            serviceInfo.Read();

            return new PriceModel
            {
                Name = serviceInfo["name"].ToString(),
                Price = (int)priceInfo[0],
                isPriceOnRequest = (bool)priceInfo[1],
                LocationID = (int)priceInfo[2],
                ServiceID = idService
            };
        }
    }
}
