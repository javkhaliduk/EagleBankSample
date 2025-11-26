using EagleBank.Models.APIException;
using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Repository.DataConversion;
using EagleBank.Repository.Entities;
using EagleBank.Repository.Interfaces;
using EagleBank.Repository.Service;
using static EagleBank.Models.common.Enumerations;

namespace EagleBank.Repository.Implementation
{
    public class TransactionRepository(ICacheService cacheService) : ITransactionRepository
    {
        readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        const string CacheKeyPrefix = "Transaction_";

        public async Task<TransactionResponseDTO> CreateTransactionAsync(CreateTransactionRequestDTO request, string userId)
        {
            var transactionEntity = request.ToCreateTransactionEntity(userId);

            var  (_,existingTransactions) = await _cacheService.GetFromCacheAsync<TransactionsEntity>($"{CacheKeyPrefix}_{userId}_{request.AccountNumber}"); ;

            var transactions = existingTransactions ?? new TransactionsEntity { TotalBalance=0, Transactions = [] };

            if(transactionEntity.Type== TransactionType.Withdrawal)
            {
                if(transactions.TotalBalance < transactionEntity.Amount)
                    throw new UnprocessableErrorException("Insufficient funds to process transaction");
            }
            
            if(transactionEntity.Type== TransactionType.Deposit)
            {
                transactions.TotalBalance += transactionEntity.Amount;
            }
            else if(transactionEntity.Type== TransactionType.Withdrawal)
            {
                transactions.TotalBalance -= transactionEntity.Amount;
            }

            transactions.Transactions.Add(transactionEntity);

            await _cacheService.WriteToCacheAsync($"{CacheKeyPrefix}_{userId}_{request.AccountNumber}", transactions);
            
            return transactionEntity.ToTransactionDTO();
        }
        public async Task<TransactionResponseDTO> GetTransactionByTransactionIdAsync(string transactionId, string userId, string accountNumber)
        {
            var (found, transactions) = await _cacheService.GetFromCacheAsync<TransactionsEntity>($"{CacheKeyPrefix}_{userId}_{accountNumber}");
            
            if (transactions ==null)

                throw new NotFoundErrorException("No transactions found for the account");
            
            var transaction = transactions.Transactions.FirstOrDefault(t => t.Id == transactionId)?.ToTransactionDTO();
            
            return transaction ?? throw new NotFoundErrorException("Transaction not found");
        }

        public async Task<TransactionsResponseDTO> GetTransactionsAsync(string accountNumber, string userId)
        {
            var (found, transactions) = await _cacheService.GetFromCacheAsync<TransactionsEntity>($"{CacheKeyPrefix}_{userId}_{accountNumber}");
            
            if (transactions == null)
                throw new NotFoundErrorException("No transactions found for the user");
            
            return transactions.ToTransactionsDTO();
        }
    }
}
