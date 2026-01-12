using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services.Auth;
using MyLedgerApp.Application.Validation;

namespace MyLedgerApp.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    [Authorize]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login an existing User, by it's Credentials. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns>A token with expire time.</returns>
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponseDTO> Login([FromBody] LoginRequest request)
        {
            LoginValidator.Run(request);

            var response = _authService.Authenticate(request);

            return Ok(response);
        }

        /// <summary>
        /// Refresh the session, if the current Token expires in 5 minutes or less.
        /// </summary>
        /// <returns>A token with expire time.</returns>
        [HttpPost("refresh")]
        public ActionResult<LoginResponseDTO> Refresh()
        {
            var authHeader = Request.Headers.Authorization.FirstOrDefault();

            var refreshToken = authHeader?.Replace("Bearer ", "") ?? "";

            var response = _authService.RefreshToken(refreshToken);

            return Ok(response);
        }
    }
}
