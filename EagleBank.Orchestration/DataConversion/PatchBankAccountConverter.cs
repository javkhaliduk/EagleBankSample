using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Models.Request;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.DataConversion
{
    public static class PatchBankAccountConverter
    {
        public static BankAccountRequestDTO ToBankAccountRequestDTO(this PatchBankAccountRequest request)
        {
            return new BankAccountRequestDTO
            {
                AccountType = request.AccountType,
                Name = request.Name
            };
        }
        public static PatchBankAccountResponse ToPatchBankAccountResponse(this BankAccountResponseDTO dto)
        {
            return new PatchBankAccountResponse
            {
                AccountNumber = dto.AccountNumber,
                SortCode = dto.SortCode,
                Name = dto.Name,
                AccountType = dto.AccountType,
                Balance = dto.Balance,
                Currency = dto.Currency,
                CreatedTimestamp = dto.CreatedTimestamp,
                UpdatedTimestamp = dto.UpdatedTimestamp


            };
        }
    }
}
