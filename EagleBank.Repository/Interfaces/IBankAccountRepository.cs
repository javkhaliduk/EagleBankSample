using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;

namespace EagleBank.Repository.Interfaces
{
    public interface IBankAccountRepository
    {
        public Task<BankAccountResponseDTO> AddBankAccountAsync(BankAccountRequestDTO request, string userId);
        public Task<BankAccountResponseDTO> GetBankAccountAsync(string accountNumber, string userId);
        public Task<bool> DeleteBankAccountAsync(string accountNumber, string userId);
        public Task<BankAccountsResponseDTO> GetAllBankAccountsAsync(string userId);
        public Task<BankAccountResponseDTO> UpdateBankAccountAsync(BankAccountRequestDTO request,string accountNumber, string userId);   
    }
}
