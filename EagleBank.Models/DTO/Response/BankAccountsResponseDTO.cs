using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.DTO.Response
{
    [ExcludeFromCodeCoverage]
    public class BankAccountsResponseDTO
    {
        public List<BankAccountResponseDTO>? Accounts { get; set; }
    }
}
