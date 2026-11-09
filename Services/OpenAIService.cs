using System.Text.Json;
using System.Text.Json.Schema;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;
using Microsoft.AspNetCore.Http.Json;
using OpenAI;
using OpenAI.Chat;

namespace BasicFitnessApp.Services;

public class OpenAIService(ChatClient chatClient) : IOpenAIService
{
    private readonly string _systemPromt = """
        You are professional fitness trainer. Create a plan of exercises for the workout based on the user's data.
        Consider user's age, goal, expirience, target muscles and type of training requested. Create around 3-4 exercises for main
        muscle and 1-2 for smaller muscles based on the requested TargetMuscles for training session. Lets say user requested
        strenght training targeting chest and shoulders- you create list of at least 3 exercises for chest and 1-2 for shoulders. Aim for 
        workout that can be done within 1 - 1.5 hours. Response should be only in JSON format.
        """;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    public async Task<GeneratedWorkout?> GenerateWorkoutAsync(UserProfile userProfile, GroupsOfMuscle targetMuscles, TypeOfWorkout typeOfWorkout, CancellationToken cancellationToken)
    {
        var systemPrompt = _systemPromt;

        var userPrompt = $"""
        Create a workout plan for user based on user's information:
        - Age:  {userProfile.Age}
        - Height:   {userProfile.Height}   
        - Weight:   {userProfile.Weight}
        - Gender:   {userProfile.Gender}
        - Goal: {userProfile.Goal}
        - Expirience:   {userProfile.Experience}
        - TargetMuscles: {targetMuscles} 
        - TypeOfWorkout: {typeOfWorkout}
        """;

        var messages = new List<ChatMessage>
        {
          new SystemChatMessage(systemPrompt),
          new UserChatMessage(userPrompt)
        };

        var options = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat
            (
                jsonSchemaFormatName: "workout_for_response",
                jsonSchema: BinaryData.FromString
                ("""
                    {
                        "type": "object",
                        "properties": {
                            "exercises": {
                                "type": "array",
                                "items": {
                                    "type": "object",
                                    "properties": {
                                        "nameOfExercise": {"type": "string"},
                                        "sets": {
                                            "type": "integer",
                                            "minumum": 2,
                                            "maximum": 5
                                        },
                                        "reps": {
                                            "type": "integer",
                                            "minimum": 5,
                                            "maximum": 15
                                        },
                                        "targetMuscles": {
                                            "type": "string",
                                            "enum": ["Chest", "Back", "Biceps", "Triceps", "Shoulders", "Quads", "Hamstrings", "Glutes", "Calves", "Abdominals", "Obliques"]}
                                    },
                                    "required": ["nameOfExercise", "sets", "reps", "targetMuscles"],
                                    "additionalProperties": false  
                                }
                            }
                        },
                        "required": ["exercises"],
                        "additionalProperties": false
                    }
                    """),
                jsonSchemaIsStrict: true
            ),
            Temperature = 0.7f
        };
        
        ChatCompletion response = await chatClient.CompleteChatAsync(messages, options, cancellationToken);

        var jsonResponse = response.Content[0].Text;

        return JsonSerializer.Deserialize<GeneratedWorkout>(jsonResponse, _jsonOptions);
    }
}

