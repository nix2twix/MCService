namespace MCService.Models
{
    public class AdressModel
    {
        public int LocationID { get; set; }
        
        public FIASCode fullFIASCode { get; set; }

    }
    public struct FIASCode
    {
        public int HouseFIASCode { get; set; }
        public string StreetFIASCode { get; set; }
        public string CityFIASCode { get; set; }
        public string RegionFIASCode { get; set; }

        public FIASCode(int houseCode, string streetCode, string cityCode, string regionCode)
        {
            HouseFIASCode = houseCode;
            StreetFIASCode = streetCode;
            CityFIASCode = cityCode;
            RegionFIASCode = regionCode;
        }

        public FIASCode(string streetCode, string cityCode, string regionCode)
        {
            HouseFIASCode = -1;
            StreetFIASCode = streetCode;
            CityFIASCode = cityCode;
            RegionFIASCode = regionCode;
        }

        public FIASCode(string cityCode, string regionCode)
        {
            HouseFIASCode = -1;
            StreetFIASCode = string.Empty;
            CityFIASCode = cityCode;
            RegionFIASCode = regionCode;
        }

        public FIASCode(string regionCode)
        {
            HouseFIASCode = -1;
            StreetFIASCode = string.Empty;
            CityFIASCode = string.Empty;
            RegionFIASCode = regionCode;
        }
    }
}
