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
    BackTricepsBiceps,
    ChestShoulders,
    Legs,
    FullBody
}

// [Flags]
// public enum GroupsOfMuscle
// {
//     None = 0,
//     Chest = 1 << 0,
//     Back = 1 << 1,
//     Biceps = 1 << 2,
//     Triceps = 1 << 3,
//     Shoulders = 1 << 4,
//     Quads = 1 << 5,
//     Hamstrings = 1 << 6,
//     Glutes = 1 << 7,
//     Calves = 1 << 8,
//     Abdominals = 1 << 9,
//     Obliques = 1 << 10,

//     UpperBody = Chest | Shoulders | Triceps
// }