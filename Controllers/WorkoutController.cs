using BasicFitnessApp.Dto;
using BasicFitnessApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasicFitnessApp.Controllers;

[ApiController]
[Route("api/workout")]
[Tags("Workout endpoints")]
public class WorkoutController(IWorkoutService service) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Generate new workout")]
    [EndpointDescription("""
    Generates new workout plan for the user.

    **Request body:**
    Specify the desired workout parameters.
    All required fields must be provided.

    **Available options:**
    - **Type of workout:** `Cardio`, `StrengthTraining`, `Functional`. 

    **How it works:**
    Generates workout plan based on the specified workout type. Checks the user history of workouts and profile details and sends request to AI model to generate plan.
    """)]
    [ProducesResponseType(typeof(WorkoutForResponse), StatusCodes.Status200OK,  Description = "Workout plan was generated sucsessfully")]
    [ProducesResponseType(StatusCodes.Status404NotFound, Description = "No user profile found to generate workout plan")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Description = "Connection to OpenAI was sucsessfull but response can't be processed")]
    public async Task<IActionResult> Create(CreateWorkout dto, CancellationToken cancellationToken)
    {
        var workout = await service.CreateWorkout(dto, cancellationToken);
        return Ok(workout);
    }
}