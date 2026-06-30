using BasicFitnessApp.Models;

namespace BasicFitnessApp.Dto;
public class CreateWorkoutDto
{
    public Guid UserId {get; set;}
    public TypeOfWorkout TypeOfWorkout {get; set;}
}

public class GeneratedWorkoutDto
{
    public List<Exercise> Exercises {get; set;} = [];
}
public record WorkoutForResponse(
    
);
public record ExerciseForResponse(
    string NameOfExercise,
    int Sets,
    int Reps,
    string TargetMuscles  
);