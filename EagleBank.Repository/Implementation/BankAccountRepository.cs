using EagleBank.Models.APIException;
using EagleBank.Models.common;
using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Repository.DataConversion;
using EagleBank.Repository.Entities;
using EagleBank.Repository.Interfaces;
using EagleBank.Repository.Service;

namespace EagleBank.Repository.Implementation
{
    public class BankAccountRepository(ICacheService cacheService) : IBankAccountRepository
    {
        readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        const string CacheKeyPrefix = "BankAccount_";

        public async Task<BankAccountResponseDTO> AddBankAccountAsync(BankAccountRequestDTO request, string userId)
        {
            var entity = request.ToCreateBankAccountEntity();
            var accounts = new BankAccountsEntity();
            accounts.Accounts = new List<BankAccountEntity>();
            accounts.Accounts.Add(entity);
            await _cacheService.WriteToCacheAsync($"{CacheKeyPrefix}_{userId}", accounts);
            return entity.ToResponseDTO();
        }

        public async Task<bool> DeleteBankAccountAsync(string accountNumber, string userId)
        {
            var (found, accounts) = await _cacheService.GetFromCacheAsync<BankAccountsEntity>($"{CacheKeyPrefix}_{userId}");
            if (!found)
            {
                throw new NotFoundErrorException("User was not found");
            }
            var account = accounts?.Accounts?.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if (account == null)
            {
                throw new NotFoundErrorException("Account was not found");
            }
            

            accounts?.Accounts?.Remove(account);
            await _cacheService.WriteToCacheAsync(userId, accounts);
            return true;
        }

        public async Task<BankAccountsResponseDTO> GetAllBankAccountsAsync(string userId)
        {
            var (found, accounts) = await _cacheService.GetFromCacheAsync<BankAccountsEntity>($"{CacheKeyPrefix}_{userId}");
            if (!found)
            {
                throw new NotFoundErrorException("User was not found");
            }
            return accounts.ToResponseDTO();
        }

        public async Task<BankAccountResponseDTO> GetBankAccountAsync(string accountNumber, string userId)
        {
            var (found, accounts) = await _cacheService.GetFromCacheAsync<BankAccountsEntity>($"{CacheKeyPrefix}_{userId}");
            if (!found)
            {
                throw new NotFoundErrorException("User was not found");
            }
            var account = accounts?.ToResponseDTO()?.Accounts?.FirstOrDefault(x => x.AccountNumber == accountNumber);

            if (account == null)
            {
                throw new NotFoundErrorException("Account was not found");
            }
            return account;
        }

        public async Task<BankAccountResponseDTO> UpdateBankAccountAsync(BankAccountRequestDTO request,string accountNumber, string userId)
        {
            var (found, accounts) = await _cacheService.GetFromCacheAsync<BankAccountsEntity>($"{CacheKeyPrefix}_{userId}");
            if (!found)
            {
                throw new NotFoundErrorException("User was not found");
            }

            var account = accounts?.ToResponseDTO()?.Accounts?.FirstOrDefault(x => x.AccountNumber == accountNumber);

            if (account == null)
            {
                throw new NotFoundErrorException("Account was not found");
            }

            account.AccountType = request.AccountType;
            account.Name = request.Name;



            accounts?.Accounts?.RemoveAll(x => x.AccountNumber == accountNumber);
            accounts?.Accounts?.Add(new BankAccountEntity
            {
                AccountNumber = accountNumber,
                AccountType = account.AccountType,
                Name = account.Name,
                Balance = account.Balance,
                CreatedTimestamp = account.CreatedTimestamp,
                Currency = account.Currency,
                SortCode = account.SortCode,
                UpdatedTimestamp = DateTime.UtcNow

            });
            await _cacheService.WriteToCacheAsync($"{CacheKeyPrefix}_{userId}", accounts);
            return account;

        }
    }
}
