using BasicFitnessApp.Dto;
using BasicFitnessApp.Models;

namespace BasicFitnessApp.Mappers;

public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email
        };
    }

    public static UserWithProfileDto ToUserWithProfileDto(this User user)
    {
        return new UserWithProfileDto
        {
          Id = user.Id,
          Name = user.Profile!.Name!,
          Email = user.Email,
          Age = user.Profile.Age,
          Height = user.Profile.Height,
          Weight = user.Profile.Weight,
          Gender = user.Profile.Gender,
          Goal = user.Profile.Goal,
          Experience = user.Profile.Experience
        };
    }
}