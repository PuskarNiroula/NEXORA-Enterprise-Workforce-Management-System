using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = NEXORA.Application.Interfaces.ServiceInterfaces.IAuthenticationService;

namespace Nexora.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthenticationController(IAuthenticationService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await service.Authenticate(
            request.Email,
            request.Password
        );

        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

        var identity = new ClaimsIdentity(
            jwt.Claims,
            IdentityConstants.ApplicationScheme
        );

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            IdentityConstants.ApplicationScheme,
            principal
        );

        return Ok(new { token });
    }
}