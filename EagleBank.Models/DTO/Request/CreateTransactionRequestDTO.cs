using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.DTO.Request
{
    [ExcludeFromCodeCoverage]
    public class CreateTransactionRequestDTO
    {
        public required decimal Amount { get; set; }
        public required string Currency { get; set; }
        public required string Type { get; set; }
        public required string Reference { get; set; }
        public required string AccountNumber { get; set; }
    }
}
