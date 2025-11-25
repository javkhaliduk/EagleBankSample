using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class PatchBankAccountResponse
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
