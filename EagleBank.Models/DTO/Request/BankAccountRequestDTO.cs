using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.DTO.Request
{
    [ExcludeFromCodeCoverage]
    public class BankAccountRequestDTO
    {
        public required string Name { get; set; }
        public required string AccountType { get; set; }
    }
}
