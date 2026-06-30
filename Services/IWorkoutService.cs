using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;

namespace BasicFitnessApp.Services;

public interface IWorkoutService
{
    Task<List<Exercise>> CreateWorkout(CreateWorkoutDto dto);
    GroupsOfMuscle SetTargetMuscles(TypeOfWorkout type, Workout? lastWorkout = null);
}