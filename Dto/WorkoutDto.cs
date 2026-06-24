using BasicFitnessApp.Models;

namespace BasicFitnessApp.Dto;
public class CreateWorkoutDto
{
    public Guid UserId {get; set;}
    public TypeOfWorkout TypeOfWorkout {get; set;}
}
public class WorkoutDetailsDto
{
    public Guid UserId {get; set;}
    public int Age {get;set;}
    public decimal Height {get; set;}
    public decimal Weight {get; set;}
    public Gender Gender {get; set;}
    public Goal Goal {get; set;}
    public Experience Experience {get; set;}
}