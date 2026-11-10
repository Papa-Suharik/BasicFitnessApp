using System.ComponentModel;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicFitnessApp.Controllers;


[ApiController]
[Route("api/users")]
[Tags("User endpoints")]
public class UserController(IUserService service) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create new user")]
    [EndpointDescription("Creates new user entity")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created, Description = "User was registered successfully")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Description = "Input data validation failed(i.e email is already registered or wrong data format)")]

    public async Task<IActionResult> CreateUser([FromBody] UserRegistrationDto dto, CancellationToken cancellationToken)
    {
        var user = await service.AddUserAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [Authorize]
    [HttpGet("{id}")]
    [EndpointSummary("Get user Information")]
    [EndpointDescription("Retrieves User information from database(profile is not included)")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK, Description = "User was fetched successfully")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "No user found under provided data")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var user = await service.GetUserByIdAsync(id, cancellationToken);

        return Ok(user);
    }

    [Authorize]
    [HttpGet("profile/{id}")]
    [EndpointSummary("Get user information with profile")]
    [EndpointDescription("Retrieves User information from database including profile details")]
    [ProducesResponseType(typeof(UserWithProfileDto), StatusCodes.Status200OK, Description = "User was fetched successfully")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "No user found under provided data or no profile configured for this user")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetUserWithProfile([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var user = await service.GetUserByIdWithProfileAsync(id, cancellationToken);

        return Ok(user);
    }

    [Authorize]
    [HttpPut("profile/{id}")]
    [EndpointSummary("Create a user profile")]
    [EndpointDescription("""
    Creates user profile.

    **Request body:**
    Request body should contain all of the properties specified in the schema

    **Validation rules:**
    - **Name:** can't be empty
    - **Age:** must be between 5 and 99.
    - **Height:** must be between 50 and 300.
    - **Weight:** must be between 30 and 300.

    **Available options:**
    - **Gender:** `NotSpecified`, `Male`, `Female`.
    - **Experience:** `NotSpecified`, `Beginner`, `SomeExperience`, `Experienced`.
    - **Goal:** `NotSpecified`, `MuscleGrow`, `FatLoss`, `MainGain`.
    """)]
    [ProducesResponseType(typeof(UserWithProfileDto), StatusCodes.Status200OK, Description = "User profile was created sucsessfully!")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "No user found under provided data")]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Description = "Input data validation failed(i.e email is already registered or wrong data format) or profile for this user is already configured")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileDto dto, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var user = await service.CreateUserProfile(id, dto, cancellationToken);

        return Ok(user);
    }

    [Authorize]
    [HttpPatch("profile/{id}")]
    [EndpointSummary("Update user profile")]
    [EndpointDescription("""
    Updates an existing user profile.

    **Request body:**
    Only properties that need to be updated should be provided.
    Properties that are omitted or set to `null` will remain unchanged.

    **Validation rules:**
    - **Age:** must be between 5 and 99.
    - **Height:** must be between 50 and 300.
    - **Weight:** must be between 30 and 300.

    **Available options:**
    - **Gender:** `NotSpecified`, `Male`, `Female`.
    - **Experience:** `NotSpecified`, `Beginner`, `SomeExperience`, `Experienced`.
    - **Goal:** `NotSpecified`, `MuscleGrow`, `FatLoss`, `MainGain`.
    """)]
    [ProducesResponseType(typeof(UserWithProfileDto), StatusCodes.Status200OK, Description = "User profile was updated sucsessfully!")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "No user found under provided data or user profile is not configured")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileUpdateDto dto, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var user = await service.UpdateUserProfile(id, dto, cancellationToken);

        return Ok(user);
    }
}