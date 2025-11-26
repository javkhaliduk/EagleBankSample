using EagleBank.Models.common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EagleBank.Models.Request
{
    [ExcludeFromCodeCoverage]
    public class CreateTransactionRequest
    {
        [Required]
        public required decimal Amount { get; set; }
        [Required]
        [AllowedValues([Constants.CURRENCY_GBP], ErrorMessage = $"Currency must be {Constants.CURRENCY_GBP}")]
        public required string Currency { get; set; }
        [Required]
        [AllowedValues([Constants.TRANSACTION_TYPE_DEPOSIT, Constants.TRANSACTION_TYPE_WITHDRAWAL], ErrorMessage = $"Deposit type must be '{Constants.TRANSACTION_TYPE_DEPOSIT}' or '{Constants.TRANSACTION_TYPE_WITHDRAWAL}'")]
        public required string Type { get; set; }
        public required string Reference { get; set; }
    }
}
