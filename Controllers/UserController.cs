using System.ComponentModel;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicFitnessApp.Controllers;


[ApiController]
[Route("api/users")]
[Tags("User configuration endpoints")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create new User")]
    [EndpointDescription("Creates new User entity")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created, Description = "User was registered successfully")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Description = "Input data validation failed(i.e emailed is taken or wrong data format)")]
    public async Task<IActionResult> CreateUser([FromBody]UserRegistrationDto dto, CancellationToken cancellationToken)
    {
        var user = await service.AddUserAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetUser), new {id = user.Id}, user);
    }

    [Authorize]
    [HttpGet("{id}")]
    [EndpointSummary("Get User Information")]
    [EndpointDescription("Retrives User information from database(profile is not included).")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK, Description = "User was fetched successfully")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Description = "Boban")]
    public async Task<IActionResult> GetUser([FromRoute]Guid id, CancellationToken cancellationToken)
    {
        var user = await service.GetUserByIdAsync(id, cancellationToken);
        
        return Ok(user);
    }

    [Authorize]
    [HttpGet("profile/{id}")]
    public async Task<IActionResult> GetUserWithProfile([FromRoute]Guid id, CancellationToken cancellationToken)
    {
        var user = await service.GetUserByIdWithProfileAsync(id, cancellationToken);

        return Ok(user);
    }

    [Authorize]
    [HttpPut("profile/{id}")]
    public async Task<IActionResult> CreateUserProfile([FromBody]UserProfileDto dto, [FromRoute]Guid id, CancellationToken cancellationToken)
    {
        var user = await service.CreateUserProfile(id, dto, cancellationToken);

        return CreatedAtAction(nameof(GetUser), new {id = user.Id}, user);
    }
    
    [Authorize]
    [HttpPatch("profile/{id}")]
    public async Task<IActionResult> UpdateUserProfile([FromBody]UserProfileUpdateDto dto, [FromRoute]Guid id, CancellationToken cancellationToken)
    {
        var user = await service.UpdateUserProfile(id, dto, cancellationToken);

        return CreatedAtAction(nameof(GetUser), new {id = user.Id}, user);
    }
}