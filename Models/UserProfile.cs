using BasicFitnessApp.CustomExceptionHandler;

namespace BasicFitnessApp.Models;

public class UserProfile
{
    public int Id {get; private set;}
    public Guid UserId {get; private set;}
    public string Name {get; private set;} = null!;
    public int Age {get; private set;} 
    public decimal Height{get; private set;}
    public decimal Weight {get; private set;}
    public Gender Gender {get; private set;}
    public Goal Goal {get; private set;}
    public Experience Experience {get; set;}
    private readonly List<Workout> _workouts = [];
    public IReadOnlyCollection<Workout> Workouts => _workouts.AsReadOnly();
    public User? User {get; private set; }
    private UserProfile(){}
    public UserProfile(string name, int age, decimal height, decimal weight, Gender gender, Goal goal, Experience experience)
    {
        Validate(name, age, height, weight, gender, goal, experience);
    
        Name = name;
        Age = age;
        Height = height;
        Weight = weight;
        Gender = gender;
        Goal = goal;
        Experience = experience;
    }

    private static void Validate(string name, int age, decimal height, decimal weight, Gender gender, Goal goal, Experience experience)
    {
        ValidateName(name);
        ValidateAge(age);
        ValidateHeight(height);
        ValidateWeight(weight);
        ValidateGender(gender);
        ValidateGoal(goal);
        ValidateExpirience(experience);
    }
    public void UpdateProfile(string? name = null, int? age = null, decimal? height = null, decimal? weight = null, Gender? gender = null, Goal? goal = null, Experience? experience = null)
    {
        if(name != null)
        {
            ValidateName(name);
            Name = name;
        }

        if(age != null)
        {
            ValidateAge(age.Value);
            Age = age.Value;
        }

        if(height != null)
        {
            ValidateHeight(height.Value);
            Height = height.Value;
        }

        if(weight != null)
        {
            ValidateWeight(weight.Value);
            Weight = weight.Value;
        }

        if(gender != null)
        {
            ValidateGender(gender.Value);
            Gender = gender.Value;
        }

        if(goal != null)
        {
            ValidateGoal(goal.Value);
            Goal = goal.Value;
        }
        if(experience != null)
        {
           ValidateExpirience(experience.Value);
           Experience = experience.Value;
        }
    }
    private static void ValidateName(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
        {
            throw new InvariantException("Name can't be null or empty");
        }
    }
    private static void ValidateAge(int age)
    {
        if(age < 18 || age > 120)
        {
            throw new InvariantException("Age can't be less than 18 and more than 120");
        }
    }
    private static void ValidateHeight(decimal height)
    {
        if(height < 50 || height > 300)
        {
            throw new InvariantException("Height can't be less than 50 and more than 300");
        }
    }
    private static void ValidateWeight(decimal weight)
    {
        if(weight < 40 || weight > 500)
        {
            throw new InvariantException("Weight can't be less than 40 and more than 500");
        }
    }
    private static void ValidateGender(Gender gender)
    {
        if(!Enum.IsDefined(gender))
        {
            throw new InvariantException("Invalid gender input!");
        }
    }
    private static void ValidateGoal(Goal goal)
    {
        if(!Enum.IsDefined(goal))
        {
            throw new InvariantException("Invalid goal input!");
        }
    }

    private static void ValidateExpirience(Experience experience)
    {
        if(!Enum.IsDefined(experience))
        {
            throw new InvariantException("Invalid experience input!");
        }
    }
} 

public enum Gender
{
    NotSpecified,
    Male, 
    Female
}

public enum Experience
{
    NotSpecified,
    Begginer, 
    SomeExpirience,
    Expirienced
}

public enum Goal
{
    NotSpecified, 
    MuscleGrow,
    FatLoss,
    MainGain
}