using System.Diagnostics.CodeAnalysis;
using static EagleBank.Models.common.Enumerations;

namespace EagleBank.Models.DTO.Request
{
    [ExcludeFromCodeCoverage]
    public class BankAccountRequestDTO
    {
        public required string Name { get; set; }
        public required AccountType AccountType { get; set; }
    }
}
