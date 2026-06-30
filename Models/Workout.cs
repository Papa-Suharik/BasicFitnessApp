namespace BasicFitnessApp.Models;
public class Workout
{
    public int Id {get; set;}
    public int ProfileId {get; set;}
    public DateTime DateTime {get; set;}
    public TypeOfWorkout TypeOfWorkout {get; set;}
    public GroupsOfMuscle TargetMuscles {get; set;}
    public List<Exercise> Exercises {get; set;} = [];
    public UserProfile? UserProfile {get; set;}
}

public class Exercise
{
    public int Id {get; set;}
    public int WorkoutId {get; set;}
    public string NameOfExercise {get; set;} = null!;
    public int Sets {get; set;}
    public int Reps {get; set;}
    public string TargetMuscles {get; set;} = null!;
    public Workout? Workout {get; set;}
}
public enum TypeOfWorkout
{
    Cardio,
    StrengthTraining,
    Functional
}

public enum GroupsOfMuscle
{
    BackShouldersBiceps,
    ChestShoulders,
    Legs,
    FullBody
}