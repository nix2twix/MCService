namespace MCService.Services
{
    //Сервис для работы со списком услуг
    public class WorkService
    {
        public string AddNewService(string name, int minCount, int maxCount, string measurement, int idMC)
        {

            return "INSERT INTO Services (name, minCount, maxCount, measure, idMC) "
                + $"VALUES (N'{name}', {minCount}, {maxCount}, N'{measurement}', "
                + $"'{idMC}')";
        }

        public string DeleteServiceByID(int id)
        {
            return $"DELETE FROM Services WHERE id = {id}";
        }

        public string ChangeServiceByID(int id, string name, int minCount, int maxCount, string measurement, int idMC)
        {
            return $"UPDATE Services\n"
                + $"SET name = N'{name}',\n"
                + $"minCount = {minCount},\n"
                + $"maxCount = {maxCount},\n"
                + $"measure = N'{measurement}',\n"
                + $"idMC = {idMC}\n"
                + $"WHERE id = {id}";

        }

        public string GetServiceByID(int id)
        {
            return $"SELECT name, minCount, maxCount, measure, idMC "
                + $"FROM Services WHERE id = {id}";
        }

        public string GetServiceByName(string name)
        {
            return $"SELECT id, name, minCount, maxCount, measure, idMC "
                + $"FROM Services WHERE name = N'{name}'";
        }

    }
}