using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class CreateBankAccountRequest
    {
        public required string Name { get; set; }
        public required string AccountType { get; set; }
    }
}
