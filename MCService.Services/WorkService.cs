namespace MCService.Services
{
    //Сервис для работы со списком услуг
    public class WorkService
    {
        public string AddNewService(int id, string name, int minCount, int maxCount, string measurement, int idMC)
        {

            return "INSERT INTO Services (name, minCount, maxCount, measure, idMC) "
                + $"VALUES (N'{name}', N'{minCount}', N'{maxCount}', N'{measurement}', "
                + $"'{idMC}')";
        }

        public string DeleteServiceByID(int id)
        {
            return $"DELETE FROM Services WHERE id={id}";
        }

        public string ChangeServiceByID(int id, string name, int minCount, int maxCount, string measurement, int idMC)
        {
            return "UPDATE Services "
                + $"SET (N'{name}', N'{minCount}', N'{maxCount}', N'{measurement}', " 
                + $"'{idMC}')\nWHERE id={id}";

        }

        public string GetServiceByID(int id)
        {
            return $"SELECT name, minCount, maxCount, measure, idMC FROM Services WHERE id = {id}";
        }

    }
}