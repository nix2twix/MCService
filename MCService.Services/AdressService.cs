using MCService.Models;
using MCService.Database;

namespace MCService.Services
{
    public class AdressService
    {
        private readonly SqlDriver sqlDriver;
        public AdressService(SqlDriver sqlDriver = null)
        {
            this.sqlDriver = sqlDriver;
            this.sqlDriver.Open();
        }
        ~AdressService() 
        {
            sqlDriver.Close();
        }

        public string AddNewAdress(AdressModel model)
        {
            return sqlDriver.ExecNonQuery("INSERT INTO Adresses "
            + "(idLocation, streetFIASCode, cityFIASCode, regionFIASCode, houseFIASCode) "
            + $"VALUES ({model.LocationID}, N'{model.fullFIASCode.StreetFIASCode}', N'{model.fullFIASCode.CityFIASCode}', "
            + $"N'{model.fullFIASCode.RegionFIASCode}', {model.fullFIASCode.HouseFIASCode})")
                + " adresses was added successfully!";
        }

        public bool TryAddNewAdress(int idMC, AdressModel model)
        {
            var locationsIDList = new List<int>();

            using (var reader = sqlDriver.ExecReader
                ($"SELECT id FROM Locations WHERE idMC = {idMC}"))
            {
                while (reader.Read())
                {
                    locationsIDList.Add((int)reader[0]);
                }
            }

            var adressList = new List<AdressModel>();

            foreach (var id in locationsIDList)
            {
                adressList.Clear();

                using (var reader = sqlDriver.ExecReader($"SELECT * FROM Adresses " 
                    + $"WHERE idLocation = {id}"))
                {
                    while (reader.Read())
                    {
                        int house = -1;

                        if (reader["houseFIASCode"] != DBNull.Value)
                            house = (int)reader["houseFIASCode"];

                        adressList.Add(new AdressModel()
                        {
                            LocationID = (int)reader["idLocation"],
                            fullFIASCode = new FIASCode
                            {
                                RegionFIASCode = reader["regionFIASCode"].ToString(),
                                CityFIASCode = reader["cityFIASCode"].ToString(),
                                StreetFIASCode = reader["streetFIASCode"].ToString(),
                                HouseFIASCode = house
                            }
                        });
                    }
                }

                foreach (var adress in adressList)
                {
                    if (model.fullFIASCode.RegionFIASCode
                            == adress.fullFIASCode.RegionFIASCode)
                        return false;

                    if (model.fullFIASCode.CityFIASCode != string.Empty
                        && model.fullFIASCode.CityFIASCode
                    == adress.fullFIASCode.CityFIASCode)
                        return false;

                    if (model.fullFIASCode.StreetFIASCode != string.Empty
                        && model.fullFIASCode.StreetFIASCode
                    == adress.fullFIASCode.StreetFIASCode)
                        return false;

                    if (model.fullFIASCode.HouseFIASCode != -1
                        && model.fullFIASCode.HouseFIASCode
                    == adress.fullFIASCode.HouseFIASCode)
                        return false;
                }
            }
            return true;
        }

        public string DeleteAdressByID(int id)
        {
            return sqlDriver.ExecNonQuery($"DELETE FROM Adresses WHERE id = {id}")
                + " adresses was deleted successfully!";
        }
        public AdressModel GetAdressByID(int id)
        {
            var adressInfo = sqlDriver.ExecReader($"SELECT * FROM Adresses WHERE id = {id}");
            adressInfo.Read();

            return new AdressModel()
            {
                LocationID = (int)adressInfo["idLocation"],
                fullFIASCode = new FIASCode
                {
                    RegionFIASCode = adressInfo["regionFIASCode"].ToString(),
                    CityFIASCode = adressInfo["cityFIASCode"].ToString(),
                    StreetFIASCode = adressInfo["streetFIASCode"].ToString(),
                    HouseFIASCode = (int)adressInfo["houseFIASCode"]
                }
            };
        }

        public AdressModel ChangeAdressByID(int id, AdressModel newModel)
        {
            sqlDriver.ExecNonQuery("UPDATE Adresses\n"
                 + $"SET idLocation = {newModel.LocationID},\n"
                 + $"streetFIASCode = N'{newModel.fullFIASCode.StreetFIASCode}',\n"
                 + $"cityFIASCode = N'{newModel.fullFIASCode.CityFIASCode}',\n"
                 + $"regionFIASCode = N'{newModel.fullFIASCode.RegionFIASCode}',\n"
                 + $"houseFIASCode = {newModel.fullFIASCode.HouseFIASCode}\n"
                 + $"WHERE id = {id}");

            return newModel;
        }
    }
}
