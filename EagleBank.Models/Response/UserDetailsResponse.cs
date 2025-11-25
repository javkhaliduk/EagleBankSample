using EagleBank.Models.common;
using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Response
{
    [ExcludeFromCodeCoverage]
    public class UserDetailsResponse:UserBase
    {
        public required string Id { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
        public DateTime UpdatedTimeStamp { get; set; }
    }
}
