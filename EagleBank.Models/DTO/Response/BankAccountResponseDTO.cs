using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.DTO.Response
{
    [ExcludeFromCodeCoverage]
    public class BankAccountResponseDTO
    {
        public required string AccountNumber { get; set; }
        public required string SortCode { get; set; }
        public required string Name { get; set; }
        public required string AccountType { get; set; }
        public required decimal Balance { get; set; }
        public required string Currency { get; set; }
        public required DateTime CreatedTimestamp { get; set; }
        public required DateTime UpdatedTimestamp { get; set; }
    }
}
