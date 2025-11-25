using EagleBank.Models.DTO.Request;
using EagleBank.Models.DTO.Response;
using EagleBank.Models.Entities;

namespace EagleBank.Repository.DataConversion
{
    public static class UserEntityConverter
    {
        public static UserEntity ToEntity(this UserDetailsRequestDTO request)
        {
            return new UserEntity
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address.ToEntity(),
                CreatedTimeStamp = DateTime.UtcNow,
                UpdatedTimeStamp = DateTime.UtcNow
            };
        }

        public static UserDetailsResponseDTO ToResponseDTO(this UserEntity entity)
        {
            return new UserDetailsResponseDTO
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                PhoneNumber = entity.PhoneNumber,
                Address = entity.Address.ToDTO(),
                CreatedTimeStamp = entity.CreatedTimeStamp,
                UpdatedTimeStamp = entity.UpdatedTimeStamp
            };
        }
    }
}
