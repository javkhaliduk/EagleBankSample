using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationRequest
    {
        [RegularExpression("^usr-[A-Za-z0-9]+$",ErrorMessage = "Username must be in the format usr-<alphanumeric>")]
        public required string Username { get; set; }
        [StringLength(50, MinimumLength = 8)]
        public required string Password { get; set; }
    }
}
