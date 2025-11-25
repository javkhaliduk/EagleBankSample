using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class TransactionResponse
    {
        public required string Id { get; set; }
        public required decimal Amount { get; set; }
        public required string Currency { get; set; }
        public required string Type { get; set; }
        public required string Reference { get; set; }
        public required string UserId { get; set; }
        public required DateTime CreatedTimestamp { get; set; }
    }
}
