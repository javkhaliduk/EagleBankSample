using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.DTO.Response
{
    [ExcludeFromCodeCoverage]
    public class TransactionsResponseDTO
    {
        public required List<TransactionResponseDTO> Transactions { get; set; }
    }
}
