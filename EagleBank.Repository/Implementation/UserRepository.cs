using EagleBank.Models.APIException;
using EagleBank.Models.common;
using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Models.Entities;
using EagleBank.Repository.DataConversion;
using EagleBank.Repository.Interfaces;
using EagleBank.Repository.Service;

namespace EagleBank.Repository.Implementation
{
    public class UserRepository(ICacheService cacheService) : IUserRepository
    {
        readonly ICacheService _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        const string CacheKeyPrefix = "User_";

        public async Task<UserDetailsResponseDTO> AddUserDetailsAsync(UserDetailsRequestDTO request)
        {
            var userEntity = request.ToEntity();
            await _cacheService.WriteToCacheAsync($"{CacheKeyPrefix}_{request.Id}", userEntity);
            return userEntity.ToResponseDTO();
        }

        public async Task<bool> DeleteUserDetailsAsync(string userId)
        {
            return await _cacheService.RemoveFromCacheAsync(userId);
        }

        public async Task<UserDetailsResponseDTO> GetUserDetailsAsync(string userId)
        {
            var (found, userEntity) = await _cacheService.GetFromCacheAsync<UserEntity>($"{CacheKeyPrefix}_{userId}");
            if (!found)
            {
                throw new NotFoundErrorException("User was not found");
            }
            
            return userEntity.ToResponseDTO();
        }

        public async Task<UserDetailsResponseDTO> UpdateUserDetailsAsync(UserDetailsRequestDTO request)
        {
            var (found, userEntity) = await _cacheService.GetFromCacheAsync<UserEntity>($"{CacheKeyPrefix}_{request.Id}");
            if (!found)
            {
                throw new NotFoundErrorException("User was not found");
            }
            
            await _cacheService.WriteToCacheAsync($"{CacheKeyPrefix}_{request.Id}", userEntity);
            return userEntity.ToResponseDTO();
        }
    }
}
