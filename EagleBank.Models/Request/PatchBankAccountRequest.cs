using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class PatchBankAccountRequest
    {
        public required string Name { get; set; }
        public required string AccountType { get; set; }
    }
}
