using System.Text.RegularExpressions;
using System.Xml;
using BasicFitnessApp.CustomExceptionHandler;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;
using BasicFitnessApp.Repo;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BasicFitnessApp.Services;

public class WorkoutService(IWorkoutRepo repo, IOpenAIService openAI) : IWorkoutService
{
    public async Task<WorkoutForResponse> CreateWorkout(CreateWorkout dto, CancellationToken cancellationToken)
    {
        var userProfile = await repo.GetUserProfileById(dto.UserId, cancellationToken) ?? throw new NoUserProfileException($"No profile found for user: {dto.UserId}");

        var targetMuscles = SetTargetMuscles(dto.TypeOfWorkout, userProfile.Workouts.FirstOrDefault());

        var generatedWorkout = await openAI.GenerateWorkoutAsync(userProfile, targetMuscles, dto.TypeOfWorkout, cancellationToken) ?? throw new OpenAiInternalError("Error while generating the workout, please try again later");

        var workout = new Workout
        {
            ProfileId = userProfile.Id,
            DateTime = DateTime.UtcNow, 
            TypeOfWorkout = dto.TypeOfWorkout,
            TargetMuscles = targetMuscles,
            Exercises = generatedWorkout.Exercises.Select(x => new Exercise
            {
                NameOfExercise = x.NameOfExercise,
                Sets = x.Sets,
                Reps = x.Reps,
                TargetMuscles = x.TargetMuscles
            }).ToList(),
        };

        await repo.AddWorkoutAsync(workout, cancellationToken);

        await repo.SaveChangesAsync();

        var workoutForResponse = new WorkoutForResponse(
            workout.Id,
            workout.TypeOfWorkout.ToString(),
            workout.TargetMuscles.ToString(),
            generatedWorkout.Exercises
        );

        return workoutForResponse;
    }

    public GroupsOfMuscle SetTargetMuscles(TypeOfWorkout type, Workout? lastWorkout = null)
    {
        GroupsOfMuscle targetMuscles;

        if (type == TypeOfWorkout.StrengthTraining)
        {
            if (lastWorkout == null)
            {
                targetMuscles = GroupsOfMuscle.BackTricepsBiceps;
            }
            else if (lastWorkout.TargetMuscles == GroupsOfMuscle.BackTricepsBiceps)
            {
                targetMuscles = GroupsOfMuscle.ChestShoulders;
            }
            else if (lastWorkout.TargetMuscles == GroupsOfMuscle.ChestShoulders)
            {
                targetMuscles = GroupsOfMuscle.Legs;
            }
            else
            {
                targetMuscles = GroupsOfMuscle.BackTricepsBiceps;
            }
        }
        else
        {
            targetMuscles = GroupsOfMuscle.FullBody; 
        }

        return targetMuscles;
    }
}