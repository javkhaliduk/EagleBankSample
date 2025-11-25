namespace EagleBank.Models.common
{
    public class Address
    {
        public required string Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public required string Town { get; set; }
        public required string County { get; set; }
        public required string Postcode { get; set; }
    }
}
