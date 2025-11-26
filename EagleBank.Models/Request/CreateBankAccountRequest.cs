using EagleBank.Models.common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class CreateBankAccountRequest
    {
        [Required]
        [StringLength(25, MinimumLength = 8)]
        public required string Name { get; set; }
        [Required]
        [AllowedValues([Constants.ACCOUNT_TYPE_CURRENT, Constants.ACCOUNT_TYPE_BUSINESS],ErrorMessage =$"Account type must be either '{Constants.ACCOUNT_TYPE_CURRENT}' or '{Constants.ACCOUNT_TYPE_BUSINESS}'")]
        public required string AccountType { get; set; }
    }
}
