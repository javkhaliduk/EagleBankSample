using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Repository.Entities
{
    [ExcludeFromCodeCoverage]
    public class TransactionsEntity
    {
        public required decimal TotalBalance { get; set; }
        public required List<TransactionEntity> Transactions { get; set; }
    }
}
