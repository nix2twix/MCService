using MCService.Services;
using MCService.Web.Controllers;
using MCService.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MCService.Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        SqlDriver sqlDriver = new SqlDriver();
        AdressService adressService = new AdressService();

        [HttpGet("{id:int}", Name = "GetAdresByID")]
        public ActionResult GetLocationByID(int id)
        {
            sqlDriver.Open();
            var adressInfo = sqlDriver.ExecReader(adressService.GetAdressByID(id));
            adressInfo.Read();

            return Ok(new AdressModel()
            {
                LocationID = (int)adressInfo["idLocation"],
                fullFIASCode = new FIASCode
                {
                    RegionFIASCode = adressInfo["regionFIASCode"].ToString(),
                    CityFIASCode = adressInfo["cityFIASCode"].ToString(),
                    StreetFIASCode = adressInfo["streetFIASCode"].ToString(),
                    HouseFIASCode = (int)adressInfo["houseFIASCode"]
                }
            });
        }

        [HttpPost("{regionFIASCode}/{cityFIASCode}/{streetFIASCode}/{houseFIASCode}", Name = "AddNewFullAdress")]
        public ActionResult AddNewFullAdress(int idMC, int idLocation, int houseFIASCode, string streetFIASCode, string cityFIASCode, string regionFIASCode)
        {
            sqlDriver.Open();

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

            var newAdressModel = new AdressModel()
            {
                LocationID = idLocation,
                fullFIASCode = new FIASCode
                {
                    RegionFIASCode = regionFIASCode,
                    CityFIASCode = cityFIASCode,
                    StreetFIASCode = streetFIASCode,
                    HouseFIASCode = houseFIASCode

                }
            };

            foreach (var id in locationsIDList)
            {
                adressList.Clear();

                using (var reader = sqlDriver.ExecReader($"SELECT * FROM Adresses WHERE idLocation = {id}"))
                {
                    while (reader.Read())
                    {
                        adressList.Add(new AdressModel()
                        {
                            LocationID = (int)reader["idLocation"],
                            fullFIASCode = new FIASCode
                            {
                                RegionFIASCode = reader["regionFIASCode"].ToString(),
                                CityFIASCode = reader["cityFIASCode"].ToString(),
                                StreetFIASCode = reader["streetFIASCode"].ToString(),
                                HouseFIASCode = (int)reader["houseFIASCode"]
                            }
                        });
                    }
                }

                foreach (var adress in adressList)
                {
                    if (newAdressModel.fullFIASCode.RegionFIASCode
                            == adress.fullFIASCode.RegionFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");

                    if (newAdressModel.fullFIASCode.CityFIASCode != string.Empty
                        && newAdressModel.fullFIASCode.CityFIASCode
                    == adress.fullFIASCode.CityFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");

                    if (newAdressModel.fullFIASCode.StreetFIASCode != string.Empty
                        && newAdressModel.fullFIASCode.StreetFIASCode
                    == adress.fullFIASCode.StreetFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");

                    if (newAdressModel.fullFIASCode.HouseFIASCode != -1
                        && newAdressModel.fullFIASCode.HouseFIASCode
                    == adress.fullFIASCode.HouseFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");
                }
            }

            return Ok(sqlDriver.ExecNonQuery(adressService.AddNewAdress(idLocation, 
                houseFIASCode, streetFIASCode, cityFIASCode, regionFIASCode)) 
                + " adresses was added successfully!");
        }

        [HttpPost("{regionFIASCode}/{cityFIASCode}/{streetFIASCode}")]
        public ActionResult AddNewAdress(int idMC, int idLocation, string streetFIASCode, string cityFIASCode, string regionFIASCode)
        {
            sqlDriver.Open();

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

            var newAdressModel = new AdressModel()
            {
                LocationID = idLocation,
                fullFIASCode = new FIASCode
                {
                    RegionFIASCode = regionFIASCode,
                    CityFIASCode = cityFIASCode,
                    StreetFIASCode = streetFIASCode,

                }
            };

            foreach (var id in locationsIDList)
            {
                adressList.Clear();

                using (var reader = sqlDriver.ExecReader($"SELECT * FROM Adresses WHERE idLocation = {id}"))
                {
                    while (reader.Read())
                    {
                        adressList.Add(new AdressModel()
                        {
                            LocationID = (int)reader["idLocation"],
                            fullFIASCode = new FIASCode
                            {
                                RegionFIASCode = reader["regionFIASCode"].ToString(),
                                CityFIASCode = reader["cityFIASCode"].ToString(),
                                StreetFIASCode = reader["streetFIASCode"].ToString(),
                            }
                        });
                    }
                }

                foreach (var adress in adressList)
                {
                    if (newAdressModel.fullFIASCode.RegionFIASCode
                            == adress.fullFIASCode.RegionFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");

                    if (newAdressModel.fullFIASCode.CityFIASCode != string.Empty
                        && newAdressModel.fullFIASCode.CityFIASCode
                    == adress.fullFIASCode.CityFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");

                    if (newAdressModel.fullFIASCode.StreetFIASCode != string.Empty
                        && newAdressModel.fullFIASCode.StreetFIASCode
                    == adress.fullFIASCode.StreetFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");
                }
            }

            return Ok(sqlDriver.ExecNonQuery(adressService.AddNewAdress(idLocation,
                streetFIASCode, cityFIASCode, regionFIASCode)) 
                + " adresses was added successfully!");
        }

        [HttpPost("{regionFIASCode}/{cityFIASCode}")]
        public ActionResult AddNewAdress(int idMC, int idLocation, string cityFIASCode, string regionFIASCode)
        {
            sqlDriver.Open();

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

            var newAdressModel = new AdressModel()
            {
                LocationID = idLocation,
                fullFIASCode = new FIASCode
                {
                    RegionFIASCode = regionFIASCode,
                    CityFIASCode = cityFIASCode,
                }
            };

            foreach (var id in locationsIDList)
            {
                adressList.Clear();

                using (var reader = sqlDriver.ExecReader($"SELECT * FROM Adresses WHERE idLocation = {id}"))
                {
                    while (reader.Read())
                    {
                        adressList.Add(new AdressModel()
                        {
                            LocationID = (int)reader["idLocation"],
                            fullFIASCode = new FIASCode
                            {
                                RegionFIASCode = reader["regionFIASCode"].ToString(),
                                CityFIASCode = reader["cityFIASCode"].ToString(),
                            }
                        });
                    }
                }

                foreach (var adress in adressList)
                {
                    if (newAdressModel.fullFIASCode.RegionFIASCode
                            == adress.fullFIASCode.RegionFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");

                    if (newAdressModel.fullFIASCode.CityFIASCode != string.Empty
                        && newAdressModel.fullFIASCode.CityFIASCode
                    == adress.fullFIASCode.CityFIASCode)
                        return BadRequest("This adress already exists in the list of locations of the current MC!");
                }
            }

            return Ok(sqlDriver.ExecNonQuery(adressService.AddNewAdress(idLocation,
                cityFIASCode, regionFIASCode)) 
                + " adresses was added successfully!");
        }

        [HttpPost("{regionFIASCode}")]
        public ActionResult AddNewAdress(int idMC, int idLocation, string regionFIASCode)
        {
            sqlDriver.Open();

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

            var newAdressModel = new AdressModel()
            {
                LocationID = idLocation,
                fullFIASCode = new FIASCode
                {
                    RegionFIASCode = regionFIASCode,
                }
            };

            foreach (var id in locationsIDList)
            {
                adressList.Clear();

                using (var reader = sqlDriver.ExecReader($"SELECT * FROM Adresses WHERE idLocation = {id}"))
                {
                    while (reader.Read())
                    {
                        adressList.Add(new AdressModel()
                        {
                            LocationID = (int)reader["idLocation"],
                            fullFIASCode = new FIASCode
                            {
                                RegionFIASCode = reader["regionFIASCode"].ToString(),
                            }
                        });
                    }
                }

                foreach (var adress in adressList)
                {
                    if (newAdressModel.fullFIASCode.RegionFIASCode
                            == adress.fullFIASCode.RegionFIASCode)
                        return BadRequest("This address already exists in the list of locations of the current MC!");
                }
            }

            return Ok(sqlDriver.ExecNonQuery(adressService.AddNewAdress(idLocation,
                regionFIASCode)) + " adresses was added successfully!");
        }

        [HttpPut("{id:int}", Name = "UpdateAdressByID")]
        public ActionResult UpdateLocationByID(int id, int houseFIASCode, int idLocation, string streetFIASCode, string cityFIASCode, string regionFIASCode)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(adressService.ChangeAdressByID
                (id, houseFIASCode, idLocation, streetFIASCode, cityFIASCode, regionFIASCode))
                + " adresses was updated successfully!");
        }

        [HttpDelete("{id:int}", Name = "DeleteAdressByID")]
        public ActionResult DeleteLocationByID(int id)
        {
            sqlDriver.Open();

            return Ok(sqlDriver.ExecNonQuery(adressService.DeleteAdressByID(id))
                + " adresses was deleted successfully!");
        }
    }
}
