namespace ServiceDomain
{
    public class Service : BaseEntity
    {
        public int MinCount { get; set; }
        public int MaxCount { get; set; }
        public string Measurement { get; set; }
        public int MCId { get; set; }
    }
}