using EagleBank.filters;
using EagleBank.Models.APIException;
using EagleBank.Models.Request;
using EagleBank.Models.Response;
using EagleBank.Orchestration.code.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EagleBank.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    [CustomValidationFilter]
    public class UsersController(IUserDetailsOrchestrator userDetailsOrchestrator) : EagleBankBaseController
    {
        
        readonly IUserDetailsOrchestrator _userDetailsOrchestrator = userDetailsOrchestrator ?? throw new ArgumentNullException(nameof(userDetailsOrchestrator));

        [Authorize]
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get([FromRoute] string userId)
        {

            var validationResult = ValidateUser(userId);

            if (validationResult != null) return validationResult;

            var result = await _userDetailsOrchestrator.GetUserDetailsAsync(userId);

            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDetailsRequest request)
        {
            var result = await _userDetailsOrchestrator.AddUserDetailsAsync(request);
            return Created("User has been created successfully", result); ;
        }
        [Authorize]
        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete([FromRoute] string userId)
        {

            var validationResult = ValidateUser(userId);

            if (validationResult != null) return validationResult;

            var result = await _userDetailsOrchestrator.DeleteUserDetailsAsync(userId);

            return StatusCode(204, result);
        }
        [Authorize]
        [HttpPatch("{userId}")]
        public async Task<IActionResult> Patch([FromRoute] string userId, [FromBody] UserDetailsRequest request)
        {

            var validationResult = ValidateUser(userId);

            if (validationResult != null) return validationResult;

            var result = await _userDetailsOrchestrator.UpdateUserDetailsAsync(request, userId);
            return Ok(result);
        }
        

    }
}
