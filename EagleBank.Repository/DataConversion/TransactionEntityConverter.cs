using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Repository.Entities;

namespace EagleBank.Repository.DataConversion
{
    public static class TransactionEntityConverter
    {
        public static TransactionEntity ToCreateTransactionEntity(this CreateTransactionRequestDTO request, string userId)
        {
            return new TransactionEntity
            {
                AccountNumber = request.AccountNumber,
                Amount = request.Amount,
                Currency = request.Currency,
                Type = request.Type,
                Reference = request.Reference,
                Id = $"tan-{new Random().Next(1, 10000).ToString()}",
                CreatedTimestamp = DateTime.UtcNow,
                UserId = userId
            };
        }
        public static TransactionResponseDTO ToTransactionDTO(this TransactionEntity request)
        {
            return new TransactionResponseDTO
            {
                Id = request.Id,
                Amount = request.Amount,
                Currency = request.Currency,
                Type = request.Type,
                Reference = request.Reference,
                CreatedTimestamp = request.CreatedTimestamp,
                AccountNumber = request.AccountNumber,
                UserId = request.UserId
            };
        }
        public static TransactionsResponseDTO ToTransactionsDTO(this TransactionsEntity request)
        {
            return new TransactionsResponseDTO
            {
                Transactions = request.Transactions.Select(t => t.ToTransactionDTO()).ToList()
            };
        }
        public static TransactionEntity ToTransactionEntity(this TransactionResponseDTO dto)
        {
            return new TransactionEntity
            {
                Id = dto.Id,
                Amount = dto.Amount,
                AccountNumber = dto.AccountNumber,
                Currency = dto.Currency,
                Type = dto.Type,
                Reference = dto.Reference,
                UserId = dto.UserId,
                CreatedTimestamp = dto.CreatedTimestamp
            };
        }
        public static TransactionsEntity ToTransactionsEntity(this TransactionsResponseDTO dto)
        {
            return new TransactionsEntity
            {
                TotalBalance = dto.Transactions.Sum(t => t.Amount),
                Transactions = dto.Transactions.Select(t => t.ToTransactionEntity()).ToList()
            };
        }
    }
}
