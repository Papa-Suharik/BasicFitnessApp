using BasicFitnessApp.Dto;
using Microsoft.AspNetCore.Mvc;

namespace BasicFitnessApp.Controllers;

[ApiController]
[Route("api/workout")]
public class WorkoutController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkoutDto dto)
    {
        return Ok();
    }
}