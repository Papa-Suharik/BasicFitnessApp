using System.Text.RegularExpressions;
using System.Xml;
using BasicFitnessApp.CustomExceptionHandler;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;
using BasicFitnessApp.Repo;

namespace BasicFitnessApp.Services;

public class WorkoutService(IWorkoutRepo repo, IOpenAIService openAI) : IWorkoutService
{
    public async Task<List<Exercise>> CreateWorkout(CreateWorkoutDto dto)
    {
        var userProfile = await repo.GetUserProfileById(dto.UserId) ?? throw new NoUserProfileException($"No profile found for user: {dto.UserId}");

        var targetMuscles = SetTargetMuscles(dto.TypeOfWorkout, userProfile.Workouts.FirstOrDefault());

        var generatedWorkout = await openAI.GenerateWorkoutAsync(userProfile, targetMuscles, dto.TypeOfWorkout) ?? throw new Exception("");

        var workout = new Workout
        {
            ProfileId = userProfile.Id,
            DateTime = DateTime.UtcNow, 
            TypeOfWorkout = dto.TypeOfWorkout,
            TargetMuscles = targetMuscles,
            Exercises = generatedWorkout.Exercises,
        };

        await repo.AddWorkoutAsync(workout);

        await repo.SaveChangesAsync();

        return generatedWorkout.Exercises;
    }

    public GroupsOfMuscle SetTargetMuscles(TypeOfWorkout type, Workout? lastWorkout = null)
    {
        GroupsOfMuscle targetMuscles;

        if (type == TypeOfWorkout.StrengthTraining)
        {
            if (lastWorkout == null)
            {
                targetMuscles = GroupsOfMuscle.BackShouldersBiceps;
            }
            else if (lastWorkout.TargetMuscles == GroupsOfMuscle.BackShouldersBiceps)
            {
                targetMuscles = GroupsOfMuscle.ChestShoulders;
            }
            else if (lastWorkout.TargetMuscles == GroupsOfMuscle.ChestShoulders)
            {
                targetMuscles = GroupsOfMuscle.Legs;
            }
            else
            {
                targetMuscles = GroupsOfMuscle.BackShouldersBiceps;
            }
        }
        else
        {
            targetMuscles = GroupsOfMuscle.FullBody; 
        }

        return targetMuscles;
    }
}