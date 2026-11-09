using BasicFitnessApp.Models;

namespace BasicFitnessApp.Dto;
public record CreateWorkout
(
    Guid UserId,
    TypeOfWorkout TypeOfWorkout
);
public record GeneratedWorkout
(
    List<ExerciseForResponse> Exercises
);
public record WorkoutForResponse(
    int Id,
    string TypeOfWorkout,
    string TargetMuscles,
    List<ExerciseForResponse> Exercises
);
public record ExerciseForResponse(
    string NameOfExercise,
    int Sets,
    int Reps,
    string TargetMuscles  
);