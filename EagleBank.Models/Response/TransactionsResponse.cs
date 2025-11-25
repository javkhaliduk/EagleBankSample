using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class TransactionsResponse
    {
        public required List<TransactionResponse> Transactions { get; set; }
    }
}
