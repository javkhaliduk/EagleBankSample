using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class CreateTransactionRequest
    {
        public required decimal Amount { get; set; }
        public required string Currency { get; set; }
        public required string Type { get; set; }
        public required string Reference { get; set; }
    }
}
