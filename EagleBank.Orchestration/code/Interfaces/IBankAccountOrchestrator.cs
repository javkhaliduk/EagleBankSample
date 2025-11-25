using EagleBank.Models.Request;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.code.Interfaces
{
    public interface IBankAccountOrchestrator
    {
        public Task<CreateBankAccountResponse> CreateBankAccountAsync(CreateBankAccountRequest request,string userId);
        public Task<PatchBankAccountResponse> UpdateBankAccountAsync(PatchBankAccountRequest request,string accountNumber, string userId);
        public Task<BankAccountResponse> GetByAccountNumberAsync(string accountNumber, string userId);
        public Task<bool> DeleteByAccountNumberAsync(string accountNumber, string userId);
        public Task<BankAccountsResponse> GetAllBankAccountsAsync(string userId);
    }
}
