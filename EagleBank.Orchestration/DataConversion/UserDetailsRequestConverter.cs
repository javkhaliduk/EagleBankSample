using EagleBank.Models.DTO.Request;
using EagleBank.Models.Request;

namespace EagleBank.Orchestration.DataConversion
{
    public static class UserDetailsRequestConverter
    {
        public static UserDetailsRequestDTO Convert(this UserDetailsRequest from,string userId)
        {
            return new UserDetailsRequestDTO
            {
                Name = from.Name,
                Email = from.Email,
                Address = from.Address,
                PhoneNumber = from.PhoneNumber,
                Id = userId,
            };
        }
    }
}
