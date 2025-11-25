using EagleBank.Models.DTO.Response;
using EagleBank.Models.Response;

namespace EagleBank.Orchestration.DataConversion
{
    public static class UserDetailsResponseConverter
    {
        public static UserDetailsResponse Convert(this UserDetailsResponseDTO from)
        {
            return new UserDetailsResponse
            {
                Id = from.Id,
                Name = from.Name,
                Email = from.Email,
                Address = from.Address,
                PhoneNumber = from.PhoneNumber,
                CreatedTimeStamp = from.CreatedTimeStamp,
                UpdatedTimeStamp = from.UpdatedTimeStamp
            };
        }
    }
}
