using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class BankAccountsResponse
    {
        public required BankAccountResponse[] Accounts { get; set; }
    }
}
