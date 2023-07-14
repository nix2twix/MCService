namespace MCService.Services
{
    public class AdressService
    {
        public string AddNewAdress(int idLocation, string streetFIASCode, string cityFIASCode, string regionFIASCode)
        {

            return "INSERT INTO Adresses (idLocation, streetFIASCode, cityFIASCode, regionFIASCode) "
                + $"VALUES (N'{idLocation}', N'{streetFIASCode}', N'{cityFIASCode}', N'{regionFIASCode}'";
        }

        public string DeleteAdressByID(int id)
        {
            return $"DELETE FROM Adresses WHERE id={id}";
        }

        public string ChangeServiceByID(int houseFIASCode, int idLocation, string streetFIASCode, string cityFIASCode, string regionFIASCode)
        {
            return "UPDATE Adresses "
                + $"SET (N'{idLocation}', N'{streetFIASCode}', N'{cityFIASCode}', N'{regionFIASCode}'"
                + $"\nWHERE houseFIASCode={houseFIASCode}";

        }
    }
}
