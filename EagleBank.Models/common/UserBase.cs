using System.ComponentModel.DataAnnotations;

namespace EagleBank.Models.common
{
    public class UserBase
    {
        [RegularExpression("^usr-[A-Za-z0-9]+$", ErrorMessage = "Username must be in the format usr-<alphanumeric>")]
        public required string Name { get; set; }
        public required Address Address { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
    }
    
}
