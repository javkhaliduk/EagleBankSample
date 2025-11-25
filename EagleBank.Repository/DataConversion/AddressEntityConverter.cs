using EagleBank.Models.common;
using EagleBank.Models.Entities;

namespace EagleBank.Repository.DataConversion
{
    public static class AddressEntityConverter
    {
        public static AddressEntity ToEntity(this Address dto)
        {
            return new AddressEntity
            {
                Line1 = dto.Line1,
                Line2 = dto.Line2,
                Line3 = dto.Line3,
                Town = dto.Town,
                County = dto.County,
                Postcode = dto.Postcode
            };
        }
        public static Address ToDTO(this AddressEntity entity)
        {
            return new Address
            {
                Line1 = entity.Line1,
                Line2 = entity.Line2,
                Line3 = entity.Line3,
                Town = entity.Town,
                County = entity.County,
                Postcode = entity.Postcode
            };
        }
    }
}
