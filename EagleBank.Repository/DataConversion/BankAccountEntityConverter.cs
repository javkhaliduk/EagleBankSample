using EagleBank.Models.common;
using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Repository.Entities;

namespace EagleBank.Repository.DataConversion
{
    public static class BankAccountEntityConverter
    {
        public static BankAccountEntity ToCreateBankAccountEntity(this BankAccountRequestDTO request)
        {
            return new BankAccountEntity
            {
                AccountNumber = $"01{new Random().Next(1, 10000).ToString("D6")}",
                SortCode = "10-10-10",
                Name = request.Name,
                AccountType = request.AccountType,
                Balance = 0,
                Currency = Enumerations.Currency.GBP,
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow
            };
        }
        public static BankAccountEntity ToUpdateBankAccountEntity(this BankAccountRequestDTO request, BankAccountEntity existingEntity)
        {
            existingEntity.Name = request.Name;
            existingEntity.AccountType = request.AccountType;
            existingEntity.UpdatedTimestamp = DateTime.UtcNow;
            return existingEntity;
        }
        public static BankAccountResponseDTO ToResponseDTO(this BankAccountEntity entity)
        {
            return new BankAccountResponseDTO
            {
                AccountNumber = entity.AccountNumber,
                SortCode = entity.SortCode,
                Name = entity.Name,
                AccountType = entity.AccountType,
                Balance = entity.Balance,
                Currency = entity.Currency,
                CreatedTimestamp = entity.CreatedTimestamp,
                UpdatedTimestamp = entity.UpdatedTimestamp
            };
        }
        public static BankAccountsResponseDTO ToResponseDTO(this BankAccountsEntity entities)
        {
            return new BankAccountsResponseDTO
            {
                Accounts = entities?.Accounts?.Select(e => e.ToResponseDTO()).ToList()
            };
        }

    }
}
