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

        public async Task<TransactionResponse> CreateTransactionAsync(CreateTransactionRequest request,string accountNumber,string userId)
        {
            var result = await _transactionRepository.CreateTransactionAsync(request.ToCreateTransactionDTO(accountNumber), userId);
            return result.ToTransactionResponse();
        }

        public async Task<TransactionResponse> GetTransactionByTransactionIdAsync(string transactionId,string userId,string accountNumber)
        {
            var result= await _transactionRepository.GetTransactionByTransactionIdAsync(transactionId, userId, accountNumber);
            return result.ToTransactionResponse();
        }

        public async Task<TransactionsResponse> GetTransactionsByAccountNumberAsync(string accountNumber, string userId)
        {
            var result = await _transactionRepository.GetTransactionsAsync(accountNumber, userId);
            return result.ToTransactionsResponse();
        }
    }
}
