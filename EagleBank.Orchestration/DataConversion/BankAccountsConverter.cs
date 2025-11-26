using EagleBank.Models.DTO.Response;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.DataConversion
{
    public static class BankAccountsConverter
    {
        public static BankAccountsResponse ToBankAccountsResponse(this BankAccountsResponseDTO from)
        {
            return new BankAccountsResponse
            {
                Accounts = from.Accounts?.Select(accountDto => accountDto.ToBankAccountResponse()).ToArray() ?? Array.Empty<BankAccountResponse>()
            };
        }
        public static BankAccountResponse ToBankAccountResponse(this BankAccountResponseDTO from)
        {
            return new BankAccountResponse
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
