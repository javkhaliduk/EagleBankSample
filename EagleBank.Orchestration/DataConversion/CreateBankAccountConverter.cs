using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Models.Request;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.DataConversion
{
    public static class CreateBankAccountConverter
    {
        public static BankAccountRequestDTO ToBankAccountRequestDTO(this CreateBankAccountRequest request)
        {
            return new BankAccountRequestDTO
            {
                AccountType = request.AccountType.ToAccountTypeEnum(),
                Name = request.Name
            };
        }
        public static CreateBankAccountResponse ToCreateBankAccountResponse(this BankAccountResponseDTO from)
        {
            return new CreateBankAccountResponse
            {
                AccountNumber = from.AccountNumber,
                AccountType = from.AccountType.ToStringValue(),
                Balance = from.Balance,
                Currency = from.Currency.ToStringValue(),
                CreatedTimestamp = from.CreatedTimestamp,
                Name = from.Name,
                SortCode = from.SortCode,
                UpdatedTimestamp = from.UpdatedTimestamp
            };
        }
    }
}
