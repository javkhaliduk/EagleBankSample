using System.ComponentModel.DataAnnotations;

namespace EagleBank.Models.common
{
    public class UserBase
    {
        public required string Name { get; set; }
        public required Address Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
    
}
