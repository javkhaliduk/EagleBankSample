using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Entities
{
    [ExcludeFromCodeCoverage]
    public class UserEntity
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required AddressEntity Address { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
    }
}