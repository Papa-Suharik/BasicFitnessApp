using System.Text.Json;
using System.Text.Json.Schema;
using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;
using OpenAI;
using OpenAI.Chat;

namespace BasicFitnessApp.Services;

public class OpenAIService(ChatClient chatClient) : IOpenAIService
{
    public async Task<GeneratedWorkoutDto?> GenerateWorkoutAsync(UserProfile userProfile, GroupsOfMuscle targetMuscles, TypeOfWorkout typeOfWorkout)
    {
        var systemPrompt = """
        You are professional fitness trainer. Create a plan of exercises for the workout based on the user's data.
        Consider user's age, goal, expirience, target muscles and type of training requested. Create at least 3 exercises for main
        muscles(like chest, legs, back) and 1-2 for smaller muscles like(triceps, biceps, shoulders). Lets say user requested
        strenght training targeting chest and shoulders- you create list of at least 3 exercises for chest and 1-2 for shoulders.
        Response should be only in JSON format.
        """;

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
                jsonSchemaFormatName: "generated_workout_dto",
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
                                        "sets": {"type": "integer"},
                                        "reps": {"type": "integer"},
                                        "targetMuscles": {"type": "string"}
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
        
        ChatCompletion response = await chatClient.CompleteChatAsync(messages, options);

        var jsonResponse = response.Content[0].Text;

        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<GeneratedWorkoutDto>(jsonResponse, jsonOptions);
    }
}

