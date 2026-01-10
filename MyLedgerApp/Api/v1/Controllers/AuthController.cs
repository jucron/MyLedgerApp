using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyLedgerApp.Api.v1.Models;
using MyLedgerApp.Application.Services.Auth;
using MyLedgerApp.Application.Validation;
using MyLedgerApp.Common.Utils;

namespace MyLedgerApp.Api.v1.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("api/v1/login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {

            LoginValidator.Run(request);

            var response = _authService.Authenticate(request);

            return Ok(response);
        }
    }
}
