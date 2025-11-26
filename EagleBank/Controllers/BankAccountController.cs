using EagleBank.filters;
using EagleBank.Models.APIException;
using EagleBank.Models.Request;
using EagleBank.Orchestration.code.Interfaces;
using EagleBank.Orchestration.DataConversion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace EagleBank.Controllers
{
    [ApiController]
    [Route("/v1/accounts")]
    [CustomValidationFilter]
    public partial class BankAccountController(IBankAccountOrchestrator bankAccountOrchestrator,IUserDetailsOrchestrator userDetailsOrchestrator) : EagleBankBaseController
    {
        readonly IBankAccountOrchestrator _bankAccountOrchestrator = bankAccountOrchestrator ?? throw new ArgumentNullException(nameof(bankAccountOrchestrator));
        readonly IUserDetailsOrchestrator _userDetailsOrchestrator = userDetailsOrchestrator ?? throw new ArgumentNullException(nameof(userDetailsOrchestrator));

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(CreateBankAccountRequest request)
        {
            var userId = GetUserId();

            var userDetails = await _userDetailsOrchestrator.GetUserDetailsAsync(userId);
            if(userDetails==null )
            {
               throw new ForbiddenErrorException("The user is not allowed to access the transaction");
            }


            var result = await _bankAccountOrchestrator.CreateBankAccountAsync(request, userId!);
            return Created("",result);
        }
        [Authorize]
        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> Get([FromRoute]string accountNumber)
        {

            if (string.IsNullOrEmpty(accountNumber))
            {
                return BadRequest("The request didn't supply all the necessary data");
            }
            if (!AccountNumberRegex().IsMatch(accountNumber))
            {
                return BadRequest("Invalid account number format");
            }
            var userId = GetUserId();

            var result = await _bankAccountOrchestrator.GetByAccountNumberAsync(accountNumber, userId);

            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var userId = GetUserId();

            var result = await _bankAccountOrchestrator.GetAllBankAccountsAsync(userId);

            return Ok(result);
        }
        [Authorize]
        [HttpPatch("{accountNumber}")]
        public async Task<IActionResult> Patch(PatchBankAccountRequest request, [FromRoute]string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                return BadRequest("The request didn't supply all the necessary data");
            }
            if (!AccountNumberRegex().IsMatch(accountNumber))
            {
                return BadRequest("Invalid account number format");
            }
            var userId = GetUserId();

            var result = await _bankAccountOrchestrator.UpdateBankAccountAsync(request,accountNumber, userId);

            return Ok(result);
        }
        [Authorize]
        [HttpDelete("{accountNumber}")]
        public async Task<IActionResult> Delete([FromRoute] string accountNumber)
        {
            if (string.IsNullOrEmpty(accountNumber))
            {
                return BadRequest("The request didn't supply all the necessary data");
            }
            if (!AccountNumberRegex().IsMatch(accountNumber))
            {
                return BadRequest("Invalid account number format");
            }

            var userId = GetUserId();

            var result = await _bankAccountOrchestrator.DeleteByAccountNumberAsync(accountNumber, userId);
            return result ? NoContent(): BadRequest();
        }

        [GeneratedRegex("^01\\d{6}$")]
        private static partial Regex AccountNumberRegex();
    }
}
