using BasicFitnessApp.Models;

namespace BasicFitnessApp.Dto;

public class UserRegistrationDto
{
    public string Email {get; set;} = null!;
    public string Password {get; set;} = null!;
}
public class UserDto
{
    public Guid Id {get; set;}
    public string Email {get; set;} = null!;
}

public class UserWithProfileDto
{
    public Guid Id {get; set;}
    public string Name {get; set;} = null!;
    public string Email {get; set;} = null!;
    public int Age {get; set;}
    public decimal Height {get; set;}
    public decimal Weight {get; set;}
    public Gender Gender {get; set;}
    public Goal Goal {get; set;}
    public Experience Experience{get; set;}
}

public class UserProfileDto
{
    public string Name {get; set;} = null!;
    public int Age {get; set;}
    public decimal Height {get; set;}
    public decimal Weight {get; set;}
    public Gender Gender {get; set;}
    public Goal Goal {get; set;}
    public Experience Experience{get; set;}
}
public class UserProfileUpdateDto
{
    public string? Name {get; set;}
    public int? Age {get; set;}
    public decimal? Height {get; set;}
    public decimal? Weight {get; set;}
    public Gender? Gender {get; set;}
    public Goal? Goal {get; set;}
    public Experience? Experience{get; set;}
}