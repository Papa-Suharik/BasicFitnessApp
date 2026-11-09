using System.Linq.Expressions;
using System.Text.RegularExpressions;
using BasicFitnessApp.CustomExceptionHandler;
using BasicFitnessApp.Dto;

namespace BasicFitnessApp.Models;

public class User
{
    public Guid Id {get; private set;}
    public string Email {get; private set;} = null!;
    public string Password {get; private set;} = null!;
    public UserProfile? Profile {get; private set;}
    private readonly List<RefreshToken> _tokens = [];
    public IReadOnlyCollection<RefreshToken> Tokens => _tokens.AsReadOnly();
    private User(){}
    public User(string email, string password)
    {
        SetEmail(email);
        SetPassword(password);
    }
    public void SetEmail(string email)
    {
        if(string.IsNullOrWhiteSpace(email))
        {
            throw new InvariantException("Email can't be null or empty");
        }

        Email = email;
    }
    public void SetPassword(string password)
    {
        if(string.IsNullOrWhiteSpace(password))
        {
            throw new InvariantException("Password can't be null or empty");
        }

        Password = password;
    }
    public void SetProfile(string name, int age, decimal height, decimal weight, Gender gender, Goal goal, Experience experience)
    {
        Profile = new UserProfile(name, age, height, weight, gender, goal, experience);
    }
}