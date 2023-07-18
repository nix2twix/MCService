namespace MCService.Models
{
    public class PriceModel : BaseModel
    {
        public int Price { get; set; }

        public bool isPriceOnRequest { get; set; }

        public int LocationID { get; set; }

        public int ServiceID { get; set; }
    }
}
