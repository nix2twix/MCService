using MCService.Models;
using MCService.Database;

namespace MCService.Services
{
    //Сервис для работы со списком услуг
    public class WorkService
    {
        private readonly SqlDriver sqlDriver;
        public WorkService(SqlDriver sqlDriver = null)
        {
            this.sqlDriver = sqlDriver;
            this.sqlDriver.Open();
        }

        ~WorkService() 
        { 
            sqlDriver.Close();
        }
        public string AddNewService(ServiceModel model)
        {
            return sqlDriver.ExecNonQuery("INSERT INTO Services (name, minCount, maxCount, measure, idMC) "
                + $"VALUES (N'{model.Name}', {model.MinCount}, {model.MaxCount}, N'{model.Measurement}', "
                + $"'{model.CompanyID}')")
                + " services was added successfully!";
        }

        public string DeleteServiceByID(int id)
        {
            return sqlDriver.ExecNonQuery($"DELETE FROM Services WHERE id = {id}")
                + " services was deleted successfully!";
        }

        public ServiceModel ChangeServiceByID(int id, ServiceModel newModel)
        {
            sqlDriver.ExecNonQuery($"UPDATE Services\n"
                + $"SET name = N'{newModel.Name}',\n"
                + $"minCount = {newModel.MinCount},\n"
                + $"maxCount = {newModel.MaxCount},\n"
                + $"measure = N'{newModel.Measurement}',\n"
                + $"idMC = {newModel.CompanyID}\n"
                + $"WHERE id = {id}");

            return newModel;

        }

        public ServiceModel GetServiceByID(int id)
        {
            var serviceInfo = sqlDriver.ExecReader($"SELECT name, minCount, maxCount, measure, idMC "
                + $"FROM Services WHERE id = {id}");
            serviceInfo.Read();

            return new ServiceModel
            {
                Name = serviceInfo["name"].ToString(),
                CompanyID = (int)serviceInfo["idMC"],
                MaxCount = (int)serviceInfo["maxCount"],
                MinCount = (int)serviceInfo["minCount"],
                Measurement = serviceInfo["measure"].ToString()
            };
        }

    }
}