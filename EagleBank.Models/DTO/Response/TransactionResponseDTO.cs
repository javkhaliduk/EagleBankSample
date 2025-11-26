using System.Diagnostics.CodeAnalysis;
using static EagleBank.Models.common.Enumerations;

namespace EagleBank.Models.DTO.Response
{
    [ExcludeFromCodeCoverage]
    public class TransactionResponseDTO
    {
        public required string Id { get; set; }
        public required decimal Amount { get; set; }
        public required string AccountNumber { get; set; }
        public required Currency Currency { get; set; }
        public required TransactionType Type { get; set; }
        public required string Reference { get; set; }
        public required string UserId { get; set; }
        public required DateTime CreatedTimestamp { get; set; }
    }
}
