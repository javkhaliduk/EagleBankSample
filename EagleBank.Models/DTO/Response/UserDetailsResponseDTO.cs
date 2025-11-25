using EagleBank.Models.common;
using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.DTO.Response
{
    [ExcludeFromCodeCoverage]
    public class UserDetailsResponseDTO:UserBase
    {
        public required string Id { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
    }
}
