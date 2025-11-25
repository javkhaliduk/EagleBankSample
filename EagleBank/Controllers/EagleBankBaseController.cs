using EagleBank.Models.APIException;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EagleBank.Controllers
{
    public abstract class EagleBankBaseController : ControllerBase
    {
        protected virtual string GetUserId()
        {
            return User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
                   ?? throw new UnAuthorisedErrorException("Access token is missing or invalid");
        }

        protected IActionResult? ValidateUser(string requestedUserId)
        {
            var actualUserId = GetUserId();

            if (string.IsNullOrEmpty(requestedUserId))
                return BadRequest("The request didn't supply all the necessary data");

            if (actualUserId != requestedUserId)
                return StatusCode(403, "This user is not allowed to access the transaction");

            return null; 
        }
    }
}
