using EagleBank.Models.Request;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.code.Interfaces
{
    public interface IUserDetailsOrchestrator
    {
        public Task<UserDetailsResponse> AddUserDetailsAsync(UserDetailsRequest request);
        public Task<UserDetailsResponse> GetUserDetailsAsync(string userId);
        public Task<UserDetailsResponse> UpdateUserDetailsAsync(UserDetailsRequest request,string userId);
        public Task<bool> DeleteUserDetailsAsync(string userId);
    }
}
