using BasicFitnessApp.Dto;
using BasicFitnessApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicFitnessApp.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController(ILoginService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await service.Login(dto);
        return Ok(result);
    }
    
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(RefreshDto dto)
    {
        var result = await service.RefreshTokens(dto);
        return Ok(result);
    }
}