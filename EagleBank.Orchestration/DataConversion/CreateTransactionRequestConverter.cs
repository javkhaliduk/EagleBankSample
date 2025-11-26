using EagleBank.Models.DTO.Request;
using EagleBank.Models.Request;

namespace EagleBank.Orchestration.DataConversion
{
    public static class CreateTransactionRequestConverter
    {
        public static CreateTransactionRequestDTO ToCreateTransactionDTO(this CreateTransactionRequest request,string accountNumber)
        {
            
            return new CreateTransactionRequestDTO
            {
                AccountNumber = accountNumber,
                Amount = request.Amount,
                Currency = request.Currency.ToCurrencyEnum(),
                Reference = request.Reference,
                Type = request.Type.ToTransactionTypeEnum(),
            };
        }
    }
}
