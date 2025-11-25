using EagleBank.filters;
using EagleBank.Models.APIException;
using EagleBank.Models.Request;
using EagleBank.Orchestration.code.Implementation;
using EagleBank.Orchestration.code.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EagleBank.Controllers
{
    [ApiController]
    [Route("/v1/accounts/{accountNumber}/transactions")]
    [Authorize]
    [CustomValidationFilter]
    public class AccountTransactionsController(ITransactionOrchestrator transactionOrchestrator,IBankAccountOrchestrator bankAccountOrchestrator) : EagleBankBaseController
    {
        readonly ITransactionOrchestrator _transactionOrchestrator = transactionOrchestrator ?? throw new ArgumentNullException(nameof(transactionOrchestrator));
        readonly IBankAccountOrchestrator _bankAccountOrchestrator = bankAccountOrchestrator ?? throw new ArgumentNullException(nameof(bankAccountOrchestrator));

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] string accountNumber)
        {

            var userId = GetUserId(); 

            await ValidateBankAccount(accountNumber, userId);

            var transactions = await _transactionOrchestrator.GetTransactionsByAccountNumber(accountNumber, userId!);

            return Ok(transactions);
        }
        [HttpGet("{transactionId}")]
        public async Task<IActionResult> Get([FromRoute] string accountNumber, [FromRoute] string transactionId)
        {
            var userId = GetUserId();

            await ValidateBankAccount(accountNumber, userId);

            var transaction = await _transactionOrchestrator.GetTransactionByTransactionId(transactionId, userId, accountNumber);

            return Ok(transaction);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromRoute] string accountNumber, CreateTransactionRequest request)
        {
            var userId = GetUserId();

            await ValidateBankAccount(accountNumber, userId);

             _ = await _transactionOrchestrator.CreateTransaction(request,accountNumber, userId!);

            return Created();
        }

        private async Task ValidateBankAccount(string accountNumber, string userId)
        {
            var bankAccount = await _bankAccountOrchestrator.GetByAccountNumberAsync(accountNumber, userId);

            if (bankAccount == null)
            {
                throw new NotFoundErrorException("Bank account was not found");
            }
        }
    }}
