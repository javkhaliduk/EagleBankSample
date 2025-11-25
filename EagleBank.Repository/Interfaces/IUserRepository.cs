using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;

namespace EagleBank.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Task<UserDetailsResponseDTO> AddUserDetailsAsync(UserDetailsRequestDTO request);
        public Task<UserDetailsResponseDTO> GetUserDetailsAsync(string userId);
        public Task<UserDetailsResponseDTO> UpdateUserDetailsAsync(UserDetailsRequestDTO request);
        public Task<bool> DeleteUserDetailsAsync(string userId);
    }
}
