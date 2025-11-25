using EagleBank.code;
using EagleBank.Models.Request;
using Microsoft.AspNetCore.Mvc;
namespace EagleBank.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class AuthenticateController(IJWTGenerator jWTGenerator) : ControllerBase
    {
        private readonly IJWTGenerator _jWTGenerator = jWTGenerator ?? throw new ArgumentNullException(nameof(jWTGenerator));

        [HttpPost]
        public IActionResult Post([FromBody]AuthenticationRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("The request didn't supply all the necessary data");
            }

            return Ok(_jWTGenerator.GenerateToken(request.Username));

        }
    }
}
