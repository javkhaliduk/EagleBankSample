using EagleBank.Models.Request;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.code.Interfaces
{
    public interface ITransactionOrchestrator
    {
        public Task<TransactionResponse> CreateTransaction(CreateTransactionRequest request,string accountNumber,string userId);
        public Task<TransactionResponse> GetTransactionByTransactionId(string transactionId, string userId,string accountNumber);
        public Task<TransactionsResponse> GetTransactionsByAccountNumber(string accountNumber, string userId);
    }
}
