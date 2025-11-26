using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;

namespace EagleBank.Repository.Interfaces
{
    public interface ITransactionRepository
    {
        public Task<TransactionResponseDTO> CreateTransactionAsync(CreateTransactionRequestDTO request, string userId);
        public Task<TransactionsResponseDTO> GetTransactionsAsync(string accountNumber, string userId);
        public Task<TransactionResponseDTO> GetTransactionByTransactionIdAsync(string transactionId, string userId, string accountNumber); 
    }
}
