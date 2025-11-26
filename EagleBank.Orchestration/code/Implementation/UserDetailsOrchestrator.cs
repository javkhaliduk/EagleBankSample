using EagleBank.Models.APIException;
using EagleBank.Models.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Orchestration.DataConversion;
using EagleBank.Repository.Interfaces;

namespace EagleBank.Orchestration.code.Implementation
{
    public class UserDetailsOrchestrator(IUserRepository userRepository, IBankAccountRepository bankAccountRepository) : IUserDetailsOrchestrator
    {
        readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        readonly IBankAccountRepository _bankAccountRepository= bankAccountRepository ?? throw new ArgumentNullException(nameof(bankAccountRepository));
        public async Task<UserDetailsResponse> AddUserDetailsAsync(UserDetailsRequest request)
        {
            var userId = $"usr-{new Random().Next(1, 10000).ToString()}";
            var dtoRequest = request.Convert(userId);
            dtoRequest.CreatedTimeStamp = DateTime.Now;
            dtoRequest.UpdatedTimeStamp = DateTime.Now;
            var response = await _userRepository.AddUserDetailsAsync(dtoRequest);
            return response.Convert();
        }

        public async Task<bool> DeleteUserDetailsAsync(string userId)
        {
            var bankAccounts =  await _bankAccountRepository.GetAllBankAccountsAsync(userId);
            if (bankAccounts != null)
            {
                throw new ConflictErrorException("A user cannot be deleted when they are associated with a bank account");
            }
            var response = await _userRepository.DeleteUserDetailsAsync(userId);
            return response;
        }

        public async Task<UserDetailsResponse> GetUserDetailsAsync(string userId)
        {
            var response = await _userRepository.GetUserDetailsAsync(userId);
            return response.Convert();
        }

        public async Task<UserDetailsResponse> UpdateUserDetailsAsync(UserDetailsRequest request, string userId)
        {
            var userDetails = await _userRepository.GetUserDetailsAsync(userId);
            if (userDetails == null)
            {
                throw new NotFoundErrorException("User was not found");
            }
            var response = await _userRepository.UpdateUserDetailsAsync(request.Convert(userId));
            return response.Convert();
        }

    }
}
