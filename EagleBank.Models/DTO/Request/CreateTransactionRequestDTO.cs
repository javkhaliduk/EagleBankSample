using System.Diagnostics.CodeAnalysis;
using static EagleBank.Models.common.Enumerations;

namespace EagleBank.Models.DTO.Request
{
    [ExcludeFromCodeCoverage]
    public class CreateTransactionRequestDTO
    {
        public required decimal Amount { get; set; }
        public required Currency Currency { get; set; }
        public required TransactionType Type { get; set; }
        public required string Reference { get; set; }
        public required string AccountNumber { get; set; }
    }
}
