using EagleBank.Models.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Orchestration.DataConversion;
using EagleBank.Repository.Interfaces;

namespace EagleBank.Orchestration.code.Implementation
{
    public class TransactionOrchestrator(ITransactionRepository transactionRepository) : ITransactionOrchestrator
    {
        readonly ITransactionRepository _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));

        public async Task<TransactionResponse> CreateTransaction(CreateTransactionRequest request,string accountNumber,string userId)
        {
            var result = await _transactionRepository.CreateTransaction(request.ToCreateTransactionDTO(accountNumber), userId);
            return result.ToTransactionResponse();
        }

        public async Task<TransactionResponse> GetTransactionByTransactionId(string transactionId,string userId,string accountNumber)
        {
            var result= await _transactionRepository.GetTransactionByTransactionId(transactionId, userId, accountNumber);
            return result.ToTransactionResponse();
        }

        public async Task<TransactionsResponse> GetTransactionsByAccountNumber(string accountNumber, string userId)
        {
            var result = await _transactionRepository.GetTransactions(accountNumber, userId);
            return result.ToTransactionsResponse();
        }
    }
}
