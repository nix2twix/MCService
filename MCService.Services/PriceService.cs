namespace MCService.Services
{
    //Сервис для работы со списком цен
    public class PriceService 
    {
        public string AddNewPrice(int price, bool isPriceOnRequest, int locationID, int serviceID)
        {
            return "INSERT INTO Prices (price, ifPriceOnRequest, idLocation, idService) "
                + $"VALUES ('{price}', '{isPriceOnRequest}', '{locationID}', "
                + $"'{serviceID}')";
        }

        public string DeletePriceByID(int id)
        {
            return $"DELETE FROM Prices WHERE id={id}";
        }

        public string ChangePriceByID(int id, int price, bool isPriceOnRequest, int locationID, int serviceID)
        {
            return "UPDATE Prices\n"
                 + $"SET price = {price},\nifPriceOnRequest = {isPriceOnRequest},"
                 + $"\nidLocation = {locationID},\n"
                 + $"idService = {serviceID}\nWHERE id={id}";
        }

        public string GetPriceByID(int id)
        {
            return "SELECT price, ifPriceOnRequest, idLocation, idService "
                + $"FROM Prices WHERE id = {id}";
        }

        public string GetPriceByCode(int codeFias, int idService) 
        {

            return "SELECT price FROM Prices\n"
                + $"WHERE idLocation = {codeFias} AND idService = {idService}";
        }
    }
}
