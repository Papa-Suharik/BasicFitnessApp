using BasicFitnessApp.Dto;
using BasicFitnessApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicFitnessApp.Controllers;

[ApiController]
[Route("api/login")]
[Tags("Authentication")]
public class LoginController(ILoginService service) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Login endpoint")]
    [EndpointDescription("Authenticates a user using their credentials and returns an access token along with a refresh token.")]
    [ProducesResponseType(typeof(LoginResultDto), StatusCodes.Status200OK, Description = "User logged in sucsessfully")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Description = "Input data validation failed")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Description = "Invalid email or password")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await service.Login(dto);
        return Ok(result);
    }
    
    
    [HttpPost("refresh")]
    [EndpointSummary("Token rotation endpoint")]
    [EndpointDescription("Validates the provided refresh token and performs token rotation by issuing a new access token and a new refresh token.")]
    [ProducesResponseType(typeof(LoginResultDto), StatusCodes.Status200OK, Description = "Tokens successfully rotated")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Description = "For security reasons, the endpoint returns the same `401 Unauthorized` response when the user is not found, the refresh token has expired, or token reuse is detected.")]
    public async Task<IActionResult> RefreshToken(RefreshDto dto)
    {
        var result = await service.RefreshTokens(dto);
        return Ok(result);
    }
}