using System.Diagnostics.CodeAnalysis;
using static EagleBank.Models.common.Enumerations;

namespace EagleBank.Models.DTO.Response
{
    [ExcludeFromCodeCoverage]
    public class BankAccountResponseDTO
    {
        public required string AccountNumber { get; set; }
        public required string SortCode { get; set; }
        public required string Name { get; set; }
        public required AccountType AccountType { get; set; }
        public required decimal Balance { get; set; }
        public required Currency Currency { get; set; }
        public required DateTime CreatedTimestamp { get; set; }
        public required DateTime UpdatedTimestamp { get; set; }
    }
}
