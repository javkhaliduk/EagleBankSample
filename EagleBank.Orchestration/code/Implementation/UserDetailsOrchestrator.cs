using EagleBank.Models.APIException;
using EagleBank.Models.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Orchestration.DataConversion;
using EagleBank.Repository.Interfaces;

namespace EagleBank.Orchestration.code.Implementation
{
    public class UserDetailsOrchestrator(IUserRepository userRepository) : IUserDetailsOrchestrator
    {
        readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

        public async Task<UserDetailsResponse> AddUserDetailsAsync(UserDetailsRequest request)
        {
            var dtoRequest = request.Convert(Guid.NewGuid().ToString());
            dtoRequest.CreatedTimeStamp = DateTime.Now;
            dtoRequest.UpdatedTimeStamp = DateTime.Now;
            var response = await _userRepository.AddUserDetailsAsync(dtoRequest);
            return response.Convert();
        }

        public Task<bool> DeleteUserDetailsAsync(string userId)
        {
            var response = _userRepository.DeleteUserDetailsAsync(userId);
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
