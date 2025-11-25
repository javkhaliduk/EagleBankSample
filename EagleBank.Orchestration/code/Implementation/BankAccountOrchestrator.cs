using EagleBank.Models.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Orchestration.DataConversion;
using EagleBank.Repository.Interfaces;

namespace EagleBank.Orchestration.code.Implementation
{
    public class BankAccountOrchestrator(IBankAccountRepository bankAccountRepository) : IBankAccountOrchestrator
    {
        readonly IBankAccountRepository _bankAccountRepository = bankAccountRepository;

        public async Task<CreateBankAccountResponse> CreateBankAccountAsync(CreateBankAccountRequest request, string userId)
        {
            var result = await _bankAccountRepository.AddBankAccountAsync(request.ToBankAccountRequestDTO(), userId);
            return result.ToCreateBankAccountResponse();
        }

        public Task<bool> DeleteByAccountNumberAsync(string accountNumber, string userId)
        {
            var result = _bankAccountRepository.DeleteBankAccountAsync(accountNumber, userId);
            return result;
        }

        public async Task<BankAccountsResponse> GetAllBankAccountsAsync(string userId)
        {
            var result = await _bankAccountRepository.GetAllBankAccountsAsync(userId);
            return result.ToBankAccountsResponse();
        }

        public async Task<BankAccountResponse> GetByAccountNumberAsync(string accountNumber, string userId)
        {
            var result = await _bankAccountRepository.GetBankAccountAsync(accountNumber, userId);
            return result.ToBankAccountResponse();
        }

        public async Task<PatchBankAccountResponse> UpdateBankAccountAsync(PatchBankAccountRequest request, string accountNumber, string userId)
        {
            var result = await _bankAccountRepository.UpdateBankAccountAsync(request.ToBankAccountRequestDTO(), accountNumber, userId);
            return result.ToPatchBankAccountResponse();
        }
    }
}
