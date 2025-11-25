namespace EagleBank.Models.common
{
    public class ErrorResponse
    {
        public string? Message { get; set; }
        public List<ErrorDetails>? Details { get; set; }
    }
}
