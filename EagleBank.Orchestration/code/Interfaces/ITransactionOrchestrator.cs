using EagleBank.Models.Request;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.code.Interfaces
{
    public interface ITransactionOrchestrator
    {
        public Task<TransactionResponse> CreateTransactionAsync(CreateTransactionRequest request,string accountNumber,string userId);
        public Task<TransactionResponse> GetTransactionByTransactionIdAsync(string transactionId, string userId,string accountNumber);
        public Task<TransactionsResponse> GetTransactionsByAccountNumberAsync(string accountNumber, string userId);
    }
}
