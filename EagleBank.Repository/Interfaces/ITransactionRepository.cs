using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;

namespace EagleBank.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<TransactionResponseDTO> CreateTransaction(CreateTransactionRequestDTO request, string userId);
        public Task<TransactionsResponseDTO> GetTransactions(string accountNumber, string userId);
        public Task<TransactionResponseDTO> GetTransactionByTransactionId(string transactionId, string userId, string accountNumber); 
    }
}
