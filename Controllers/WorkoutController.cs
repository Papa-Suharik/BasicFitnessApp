using BasicFitnessApp.Dto;
using BasicFitnessApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicFitnessApp.Controllers;

[ApiController]
[Route("api/workout")]
public class WorkoutController(IWorkoutService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateWorkoutDto dto)
    {
        var workout = await service.CreateWorkout(dto);
        return Ok(workout);
    }
}