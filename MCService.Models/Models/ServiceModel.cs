namespace MCService.Models
{
    public class ServiceModel : BaseModel
    {
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
        public string Measurement { get; set; }
        public int CompanyID { get; set; }

    }
}
