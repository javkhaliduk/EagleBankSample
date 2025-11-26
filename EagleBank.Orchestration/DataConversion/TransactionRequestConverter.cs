using EagleBank.Models.DTO.Response;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.DataConversion
{
    public static class TransactionRequestConverter
    {
        public static TransactionResponse ToTransactionResponse(this TransactionResponseDTO transactionResponseDTO)
        {
            return new TransactionResponse
            {
                Amount = transactionResponseDTO.Amount,
                Currency = transactionResponseDTO.Currency.ToStringValue(),
                CreatedTimestamp = transactionResponseDTO.CreatedTimestamp,
                Id = transactionResponseDTO.Id,
                Reference = transactionResponseDTO.Reference,
                Type = transactionResponseDTO.Type.ToStringValue(),
                UserId = transactionResponseDTO.UserId

            };
        }

        public static TransactionsResponse ToTransactionsResponse(this TransactionsResponseDTO transactionResponseDTO)
        {
            return new TransactionsResponse
            {
               Transactions= transactionResponseDTO.Transactions.ConvertAll(t => t.ToTransactionResponse())

            };
        }
    }
}
